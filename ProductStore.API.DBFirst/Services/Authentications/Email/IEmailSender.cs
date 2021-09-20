using ProductStore.API.DBFirst.ViewModels.Authentication.Email;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Authentications.Email
{
    public interface IEmailSender
    {
        void SendEmail(MessageVM message);

        Task SendEmailAsync(MessageVM message);

        Task SendLinkForgotPasswordAsync(MessageVM message);
        Task SendConfirmedEmailAsync(MessageVM message);


    }
}