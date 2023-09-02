using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using MailKit.Net.Smtp;
using _2.UsersManagement.Application.Interfaces;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services
{
    public class MailService : IMailService
    {
        public async Task SendEmail(SendConfirmationEmailDto request)
        {
            try
            {
                var password = StringCipher.Decrypt(request.Password, GlobalVariables.SoftGuid);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("Armando55441@hotmail.com"));
                email.To.Add(MailboxAddress.Parse(request.Email));
                email.Subject = "Registered user successfully";
                email.Body = new TextPart(TextFormat.Html)
                { Text = $"<h1> User: {request.User} <br>  Password: {password} <h1>" };

                // Set up your SMTP credentials
                var emailClient = new SmtpClient();

                await emailClient.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await emailClient.AuthenticateAsync("Armando55441@hotmail.com", "AdcefyeClcu2o4A");
                await emailClient.SendAsync(email);
            }
            catch (Exception)
            {
                throw new InternalServerError(Codes.FailedEmail);
            }
        }
    }
}
