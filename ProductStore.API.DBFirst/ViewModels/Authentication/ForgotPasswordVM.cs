using System.ComponentModel.DataAnnotations;

namespace ProductStore.API.DBFirst.ViewModels.Authentication
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}