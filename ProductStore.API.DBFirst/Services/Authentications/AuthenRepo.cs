using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductStore.API.DBFirst.Authentication;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.DataModels.Models.Authentication;
using ProductStore.API.DBFirst.Services.Authentications.Email;
using ProductStore.API.DBFirst.Utils.GenerateToken;
using ProductStore.API.DBFirst.ViewModels.Authentication;
using ProductStore.API.DBFirst.ViewModels.Authentication.Email;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Authentications
{
    public class AuthenRepo : IAuthentication
    {
        private readonly StoreContext _context;
        private readonly UserManager<StoreUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenRepo(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager, StoreContext context, IConfiguration configuration, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public Task<List<RefreshToken>> GetById(string id)
        {
            return  Task.FromResult(_context.RefreshTokens.Where(x => x.UserId == id).ToList()); 
        }

        public async Task<AuthenticationVM> LoginAsync(LoginVM loginModel)
        {
            try
            {
                // CHECK EXIST EMAIL
                var authenticationModel = new AuthenticationVM();
                var currentUser = await _userManager.FindByEmailAsync(loginModel.Username);
                if (currentUser == null)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"No Accounts Registered with {loginModel.Username}.";
                }
                //CHECK PASSWORD
                if (await _userManager.CheckPasswordAsync(currentUser, loginModel.Password))
                {
                    authenticationModel.IsAuthenticated = true;
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(currentUser);
                    authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    authenticationModel.Email = currentUser.Email;
                    authenticationModel.UserName = currentUser.UserName;
                    //get list role (user, moderator, administration)
                    var rolesList = await _userManager.GetRolesAsync(currentUser).ConfigureAwait(false);
                    authenticationModel.Roles = rolesList.ToList();
                    // checks if there are any active refresh tokens available for the authenticated user
        

                    var activeRefreshToken = (_context.Users
                       .Join(_context.RefreshTokens,
                          user => user.Id,
                          rt => rt.UserId,
                          (user, rt) => new { User = user, ReToken = rt })
                       .Where(x => x.User.Id == currentUser.Id && x.ReToken.IsActive == true && x.ReToken.Expires > DateTime.UtcNow)).FirstOrDefault();

                    if (activeRefreshToken == null)
                    {
                        var refreshToken = GenerateToken.CreateRefreshToken();
                        authenticationModel.RefreshToken = refreshToken.Token;
                        authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                        //set userid
                        refreshToken.UserId = currentUser.Id;
                        refreshToken.IsActive = true;
                        _context.RefreshTokens.Add(refreshToken);
                        _context.SaveChanges();
                    }
                    else
                    {
                        authenticationModel.RefreshToken = activeRefreshToken.ReToken.Token;
                        authenticationModel.RefreshTokenExpiration = activeRefreshToken.ReToken.Expires;
                    }
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Incorrect Credentials for user {loginModel.Username}.";
                authenticationModel.Success = true;
                return authenticationModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthenticationVM { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<AuthenticationVM> RefreshTokenAsync(string jwtToken)
        {
            try
            {
                var authenticationModel = new AuthenticationVM();
                //var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
                // select user exist jwtToken
                var query =
                           from us in _context.Users
                           join rt in _context.RefreshTokens on us.Id equals rt.UserId
                           where rt.Token == jwtToken
                           select new { us.Id, us.UserName, us.Email, refreshToken = rt };

                var usertoken = query.FirstOrDefault();
                ////CHECK isExpired
                //if (DateTime.UtcNow > usertoken.refreshToken.Expires)
                //{
                //    return new AuthenticationVM
                //    {
                //        Errors = new List<string>() { "token has expired, user needs to relogin" },
                //        Success = false,
                //        IsAuthenticated = false

                //    };
                //};
                //get user exist jwtToken
                if (usertoken == null)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Token did not match any users.";
                    return authenticationModel;
                }
                //check token isActive or not
                if (!usertoken.refreshToken.IsActive)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Token Not Active.";
                    return authenticationModel;
                }

                // check if the refresh token has been used
                //if (storedRefreshToken.IsUsed)
                //{
                //    return new AuthResult()
                //    {
                //        Errors = new List<string>() { "token has been used" },
                //        Success = false
                //    };
                //}
                //Generate new Refresh Token and save to Database
                var newRefreshToken = GenerateToken.CreateRefreshToken();
                newRefreshToken.UserId = usertoken.Id;
                _context.RefreshTokens.Add(newRefreshToken);
                _context.SaveChanges();

                //Generates new jwt
                authenticationModel.IsAuthenticated = true;
                var user = _context.Users.Where(x => x.Id == usertoken.Id).FirstOrDefault();

                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = usertoken.Email;
                authenticationModel.UserName = usertoken.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                authenticationModel.RefreshToken = newRefreshToken.Token;
                authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
                return authenticationModel;
            }
            catch (Exception ex)
            {
                return new AuthenticationVM { Errors = new List<string> { ex.Message }, Success = false };
            }
        }

        public async Task<Response> RegisterAsync(RegisterVM registerModel)
        {
            try
            {
                var user = new StoreUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                var userWithSameEmail = await _userManager.FindByEmailAsync(registerModel.Email);
                if (userWithSameEmail == null)
                {
                    var result = await _userManager.CreateAsync(user, registerModel.Password);
                    if (result.Errors.Count() > 0)
                    {
                        return new Response { Status = "Failed", ListMessage = result.Errors };
                    }
                    await _userManager.AddToRoleAsync(user, "USER");
                    // SEND CONFIRMED EMAIL
                    return new Response { Message = $"User Registered with username {user.UserName}", Status = "Success" };
                }
                else
                {
                    
                    return new Response { Message = $"Email {user.Email } is already registered.", Status = "Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Message = ex.Message, Status = "Failed" };
            }
        }

        public async Task<bool> RevokeToken(string token)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var currentUser = _context.RefreshTokens.Where(x => x.Token == token).FirstOrDefault();

                    var users = (_context.Users
                   .Join(_context.RefreshTokens,
                      user => user.Id,
                      rt => rt.UserId,
                      (user, rt) => new { User = user, ReToken = rt })
                   .Where(x => x.User.Id == currentUser.UserId)).ToList();

                    // return false if no user found with token
                    if (users == null) return false;


                    // return false if token is not active
                    foreach (var user in users)
                    {
                        // revoke token and save
                        user.ReToken.Revoked = DateTime.UtcNow;
                        user.ReToken.IsActive = false;
                        if (user.ReToken.Expires < DateTime.UtcNow)
                        {
                            user.ReToken.IsExpired = true;
                            _context.RefreshTokens.UpdateRange(user.ReToken);

                        }
                        else
                        {
                            user.ReToken.IsExpired = false;
                            _context.RefreshTokens.UpdateRange(user.ReToken);
                        }

                    }
                    await _context.SaveChangesAsync();
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        private async Task<JwtSecurityToken> CreateJwtToken(StoreUser user)
        {
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var roleClaims = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));
                }

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
                .Union(userClaims)
                .Union(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(3),
                    signingCredentials: signingCredentials);

                return jwtSecurityToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}