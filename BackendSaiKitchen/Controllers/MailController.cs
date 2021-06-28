using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System.Threading.Tasks;

namespace BackendSaiKitchen.Controllers
{
    public class MailController : BaseController
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] PasswordRequest request)
        {
            await mailService.SendWelcomeEmailAsync(request);
            return Ok();
        }

    }
}
