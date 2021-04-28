using BackendSaiKitchen.CustomModel;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Helper
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(PasswordRequest request);
    }
}
