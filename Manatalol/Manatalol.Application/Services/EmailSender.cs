using Manatalol.Application.Configurations;
using Manatalol.Application.DTO.EmailTemplates;
using Manatalol.Application.Helpers;
using Manatalol.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Manatalol.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly RazorViewToStringRenderer _razorRenderer;
        private readonly EmailSettings _emailSettings;


        public EmailSender(RazorViewToStringRenderer razorRenderer, EmailSettings emailSettings)
        {
            _razorRenderer = razorRenderer;
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string link, string sendTo)
        {
            try
            {
                var model = new EmailResetModel
                {
                    UserName = sendTo,
                    ResetLink = link
                };

                var htmlBody = await _razorRenderer.RenderViewToStringAsync("Pages/EmailTemplates/ResetPassword.cshtml", model);

                var mail = new MailMessage();
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(_emailSettings.UserName);

                using var smtp = new SmtpClient(_emailSettings.SmtpServer)
                {
                    Port = _emailSettings.SmtpPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
                    EnableSsl = true,
                };

                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                throw ex;
            }
        }
    }
}