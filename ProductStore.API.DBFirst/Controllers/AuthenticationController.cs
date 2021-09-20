using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductStore.API.DBFirst.Authentication;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.DataModels.Models.Authentication;
using ProductStore.API.DBFirst.Services.Authentications;
using ProductStore.API.DBFirst.Services.Authentications.Email;
using ProductStore.API.DBFirst.Utils.Errors;
using ProductStore.API.DBFirst.ViewModels;
using ProductStore.API.DBFirst.ViewModels.Authentication;
using ProductStore.API.DBFirst.ViewModels.Authentication.Email;
using ProductStore.API.DBFirst.ViewModels.Authentication.ResetPassword;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProductStore.API.DBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthentication _authentication;
        private readonly IEmailSender _emailSender;
        private readonly StoreContext _context;
        //private readonly SignInManager<IdentityUser> _signInManager; SignInManager<IdentityUser> signInManager

        public AuthenticationController(UserManager<StoreUser> userManager, IConfiguration configuration, IAuthentication authentication, IEmailSender emailSender, StoreContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authentication = authentication;
            _emailSender = emailSender;
            _context = context;
            //_signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login user)
        {
            // find username & password correct or not
            var userName = await _userManager.FindByNameAsync(user.Username);
            var password = await _userManager.CheckPasswordAsync(userName, user.Password);
            if (userName == null || !password)
            {
                return Unauthorized();
            }
            //get list userRole
            var userRoles = await _userManager.GetRolesAsync(userName);
            //get userClaim
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            // configuration to create token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            // return status & token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Register user)
        {
            // check exist username
            var isUserNameExist = await _userManager.FindByNameAsync(user.Username);
            if (isUserNameExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "UserName already exists!" });
            }
            // check exist email
            var isEmailExist = await _userManager.FindByEmailAsync(user.Email);
            if (isEmailExist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Email already exists!" });
            }
            // create StoreUser (user entity)
            StoreUser storeUser = new StoreUser
            {
                UserName = user.Username,
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            // create hashedPassword & save StoreUser to db
            var result = await _userManager.CreateAsync(storeUser, user.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", ListMessage = result.Errors, Message = error.Description });
            }

            // response
            return Ok(new Response<Object> { Status = "200", Message = "User created successfully!" });
        }

        #region login with refresh token
        [HttpPost("loginRefreshtoken")]
        public async Task<IActionResult> GetTokenAsync(LoginVM model)
        {
            var result = await _authentication.LoginAsync(model);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            if (result.RefreshToken != null)
            {
                Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
            }
            return Ok(result);
        }
        #endregion

        #region register
        [HttpPost("registerConfirmedEmail")]
        public async Task<ActionResult> RegisterAsync(RegisterVM registerModel)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                string token = "";
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Form not valid! Please try again!" });

                var newUser = new StoreUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                var user = await _userManager.FindByEmailAsync(registerModel.Email);

                if (user != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Email exist" });
                }

                var result = await _userManager.CreateAsync(newUser, registerModel.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Confirm email not success", ListMessage = result.Errors });
                    }
                }
                //generate token
                token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token=codeEncoded, email = newUser.Email }, Request.Scheme);
                var message = new MessageVM(new string[] { newUser.Email }, "Confirmation Email", confirmationLink, null);
                // SEND CONFIRMED EMAIL
                await _emailSender.SendConfirmedEmailAsync(message);

                await _userManager.AddToRoleAsync(newUser, "Visitor");
                transaction.Commit();
                return StatusCode(StatusCodes.Status200OK, new RegisterResponseVM { Message = "Register Success", Status = "200", Token = codeEncoded, ListMessage = null });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new AppErrors(ex.Message, ex);
            }
        }

        [HttpGet("confirmed-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Object> { Status = "404", Message = "Email not found" });

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Object> { Status = "404", Message = "Confirm email not success", ListMessage = result.Errors });
            }
            return Ok(result);
        }
        #endregion register

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authentication.RefreshTokenAsync(refreshToken);
            //send refreshToken to cookie
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [Authorize]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var listToken = _authentication.GetById(id);
            return Ok(listToken.Result);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = await _authentication.RevokeToken(token);
            if (!response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token revoked" });
        }

        #region Forgot password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Form not valid! Please try again!" });
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Your email not exist" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //string testEmail = "hoale240803@gmail.com";
            //var callback = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
            //create return url
            //var queryString = System.Web.HttpUtility.ParseQueryString("http://localhost:8081/#/resetpassword");
            //queryString.Add("token", token);
            //queryString.Add("email", user.Email);

            //string longurl = "http://localhost:8081/#/";
            //Uri url = new Uri("http://localhost/rest/something/browse").
            //  AddQuery("page", "0").
            //  AddQuery("pageSize", "200");
            //var uriBuilder = new UriBuilder(longurl);
            //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query.Add(resetpassword);
            //query["token"] = token;
            //query["email"] = user.Email;
            //uriBuilder.Query = query.ToString();
            //longurl = uriBuilder.ToString();

            const string url = "http://localhost:8081/resetpassword";
            var param = new Dictionary<string, string>() { { "token", token }, { "email", user.Email } };

            var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));
            var message = new MessageVM(new string[] { user.Email }, "Reset password token", newUrl.ToString(), null);

            await _emailSender.SendLinkForgotPasswordAsync(message);
            //return Ok(new { token= token});
            return StatusCode(StatusCodes.Status200OK, new ForgotPasswordResponse { Message = "Success", Status = "200", Token = token, Email = forgotPasswordModel.Email });
            //string Url = $"http://localhost:8081/#/resetpassword?token={0}&email={1}";
            //string redirectUrl = string.Format(Url, token, user.Email);
            //return Redirect(redirectUrl);
        }
        #endregion

        #region Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Form not valid! Please try again!" });

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response<Object> { Status = "404", Message = $"Email {resetPasswordModel.Email} not found " });

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                // NOTE: REFRACTOR ONLY RETURN ONE ERROR
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Object> { Status = "Error", Message = "Reset password not success", ListMessage = resetPassResult.Errors });
            }

            return StatusCode(StatusCodes.Status200OK, new Response<object> { Message = "Success", Status = "200", Content= resetPassResult });
        }
        #endregion
        [HttpGet("login-two-step")]
        public async Task<IActionResult> LoginTwoStep(string email, bool rememberMe, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "Email not found!" });
            }
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<Object> { Status = "400", Message = "System error" });
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            var message = new MessageVM(new string[] { email }, "Authentication token", token, null);
            await _emailSender.SendEmailAsync(message);
            return Ok(new Response<Object> { Status = "200", Message = "verifying code" });
        }
    }
}