using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ByteBank.Forum.App_Start.Identity
{
    public class EmailServico : IIdentityMessageService
    {
        private readonly string _emailOrigem = ConfigurationManager.AppSettings["emailServico:email_rementente"];
        private readonly string _senha = ConfigurationManager.AppSettings["emailServico:email_senha"];

        public async Task SendAsync(IdentityMessage message)
        {
            using (var email = new MailMessage())
            {
                email.Subject = message.Subject;
                email.To.Add(message.Destination);
                email.Body = message.Body;
                email.From = new MailAddress(_emailOrigem);

                using (var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(_emailOrigem, _senha);

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.Timeout = 20_000;

                    await smtp.SendMailAsync(email);
                }
            }
        }
    }
}