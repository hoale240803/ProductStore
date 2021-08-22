using Microsoft.AspNetCore.Identity;
using ProductStore.API.DBFirst.Authentication;
using ProductStore.API.DBFirst.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.SeedingDatas
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Visitor.ToString()));
            //Seed Default User
            var defaultUser = new StoreUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}