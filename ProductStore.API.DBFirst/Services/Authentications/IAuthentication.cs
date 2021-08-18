using ProductStore.API.DBFirst.Authentication;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.DataModels.Models.Authentication;
using ProductStore.API.DBFirst.ViewModels.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Authentications
{
    public interface IAuthentication
    {
        Task<Response> RegisterAsync(RegisterVM registerModel);

        Task<AuthenticationVM> LoginAsync(LoginVM loginModel);

        //Task<string> AddRoleAsync(AddRoleModel model);

        Task<AuthenticationVM> RefreshTokenAsync(string jwtToken);

        Task<bool> RevokeToken(string token);

        Task<List<RefreshToken>> GetById(string id);
    }
}