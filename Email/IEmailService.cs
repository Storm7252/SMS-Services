namespace SMS.Email
{
    public interface IEmailService
    {
        void SendRegistrationEmail(string toEmail, string username);
    }
}
