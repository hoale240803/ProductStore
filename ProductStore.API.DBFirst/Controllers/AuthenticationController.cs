using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductStore.API.DBFirst.Authentication;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.Services.Authentications;
using ProductStore.API.DBFirst.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthentication _authentication;

        public AuthenticationController(UserManager<StoreUser> userManager, IConfiguration configuration, IAuthentication authentication)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authentication = authentication;
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "UserName already exists!" });
            }
            // check exist email
            var isEmailExist = await _userManager.FindByEmailAsync(user.Email);
            if (isEmailExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email already exists!" });
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", ListMessage = result.Errors });
            }

            // response
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("loginRefreshtoken")]
        public async Task<IActionResult> GetTokenAsync(DataModels.Models.Authentication.LoginVM model)
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

        [HttpPost("registerHasRole")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> RegisterAsync(DataModels.Models.Authentication.RegisterVM registerModel)
        {
            var result = await _authentication.RegisterAsync(registerModel);
            return Ok(result);
        }

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
    }
}