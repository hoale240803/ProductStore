using System.ComponentModel.DataAnnotations;

namespace ProductStore.API.DBFirst.DataModels.Models.Authentication
{
    public class LoginVM
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}