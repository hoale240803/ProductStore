using System.ComponentModel.DataAnnotations;

namespace ProductStore.API.DBFirst.ViewModels.Authentication.TwoStepAuthen
{
    public class TwoStepAuthenVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }

        public bool RememberMe { get; set; }
    }
}