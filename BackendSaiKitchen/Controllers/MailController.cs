using System.Collections.Generic;
using BackendSaiKitchen.CustomModel;
using BackendSaiKitchen.Helper;
using Microsoft.AspNetCore.Mvc;
using SaiKitchenBackend.Controllers;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
        [HttpPost("whatsapp")]
        public async Task<string> SendTextWhatsapp([FromForm] List<string> sendto, [FromForm] string message)
        {
            using var connection = new MySqlConnection("Server=147.182.217.248;Database=db_social;User Id=sameeradmin;Password=eW3Tn$wC42;");
            await connection.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM `sp_whatsapp_sessions` WHERE data  LIKE '%971503062669%' ORDER BY id DESC LIMIT 1;", connection);
            using var reader = await command.ExecuteReaderAsync();
            object value = null;
            while (await reader.ReadAsync())
            {
                Constants.WhatsappInstanceId = reader.GetValue(3).ToString();
                // do something with 'value'

            }

            string val = null;
            foreach (var to in sendto)
            {

             val=    await Helper.Helper.SendWhatsappMessage(to, "text", message);
            }

            return val;
        }


    }
}
