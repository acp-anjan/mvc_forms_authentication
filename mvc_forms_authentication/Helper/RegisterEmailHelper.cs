using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace mvc_forms_authentication.Helper
{
    public class RegisterEmailHelper
    {
        public static void SendVerificationLink(string userEmail, string verificationLink)
        {
            var fromEmail = new MailAddress(address: ConfiguredData.Email, displayName: "Authentication Application");
            var toEmail = new MailAddress(userEmail);

            var fromEmailPassword = ConfiguredData.Password;
            string subject = "Activate Account";
            string body = "<br/> Please click on the following link in order to activate your account"
                          + "<br/><a href='" + verificationLink + "'>Verify your account.<a/>";
            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: fromEmail.Address, password: fromEmailPassword);

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);

        }
    }
}