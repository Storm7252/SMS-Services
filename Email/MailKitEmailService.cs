using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;

namespace SMS.Email
{
    public class MailKitEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public MailKitEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendRegistrationEmail(string toEmail, string userUsername)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var emailUsername = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var senderName = _configuration["EmailSettings:SenderName"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(userUsername, toEmail));  // Corrected line
            message.Subject = "From SoftStacks Technology Pvt. Ltd.";

            // You can use HTML to format the email body as needed
            message.Body = new TextPart("plain")
            {
                Text = $"Greetings {userUsername},\n\nWelcome to SoftStacks Technology Pvt. Ltd. You are now the Family Member of Our Company.Thank you for registering."
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, false);
                client.Authenticate(emailUsername, password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
