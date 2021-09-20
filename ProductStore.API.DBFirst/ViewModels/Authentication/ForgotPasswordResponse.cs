namespace ProductStore.API.DBFirst.ViewModels.Authentication
{
    public class ForgotPasswordResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}