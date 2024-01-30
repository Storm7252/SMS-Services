using SMS.Migrations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace SMS.SmsServices
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _phoneNumber;
        private readonly string _messagingServiceSid;

        public TwilioSmsService(string accountSid, string authToken, string phoneNumber, string messagingServiceSid)
        {
            this._accountSid = accountSid;
            this._authToken = authToken;
            this._phoneNumber = phoneNumber;
            this._messagingServiceSid = messagingServiceSid;
            TwilioClient.Init(_accountSid, authToken);
        }
        public void SendSms(string to, string message)
        {
            var twilioMessage = MessageResource.Create(
                    body: message,
                    /*messagingServiceSid: _messagingServiceSid,*/
                    from: new Twilio.Types.PhoneNumber(_phoneNumber),
                    to:new Twilio.Types.PhoneNumber(to)
                ) ;
        }
    }
}
