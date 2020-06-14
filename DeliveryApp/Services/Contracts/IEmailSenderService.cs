using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IEmailSenderService
    {
        Task SendUserConfirmationEmail(string receiverEmail, string receiverFullName, string message);
        Task SendResetPasswordEmail(string receiverEmail, string message);
        Task SendBoundOrderDeliveryEmail(string receiverEmail, string receiverFullName, string message);
        Task SendEmail(string receiverEmail, string subject, string message);
    }
}
