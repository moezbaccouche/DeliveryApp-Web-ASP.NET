using DeliveryApp.Services.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;

        public EmailSenderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendBoundOrderDeliveryEmail(string receiverEmail, string receiverFullName, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("DeliveryTN", _config["Email"]));
                mimeMessage.To.Add(new MailboxAddress(receiverFullName, receiverEmail));
                mimeMessage.Subject = "Livraison affectée";
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_config["Email"], _config["Password"]);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendUserConfirmationEmail(string receiverEmail, string receiverFullName, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("DeliveryTN", _config["Email"]));
                mimeMessage.To.Add(new MailboxAddress(receiverFullName, receiverEmail));
                mimeMessage.Subject = "Email de confirmation";
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_config["Email"], _config["Password"]);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendResetPasswordEmail(string receiverEmail, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("DeliveryTN", _config["Email"]));
                mimeMessage.To.Add(new MailboxAddress("", receiverEmail));
                mimeMessage.Subject = "Récuperation mot de passe oublié";
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_config["Email"], _config["Password"]);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendEmail(string receiverEmail, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("DeliveryTN", _config["Email"]));
                mimeMessage.To.Add(new MailboxAddress("", receiverEmail));
                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_config["Email"], _config["Password"]);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
