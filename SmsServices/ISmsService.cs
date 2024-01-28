namespace SMS.SmsServices
{
    public interface ISmsService
    {
        void SendSms(string to, string message);
    }
}
