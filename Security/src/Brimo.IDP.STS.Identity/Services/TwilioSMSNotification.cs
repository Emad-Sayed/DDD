using Brimo.IDP.STS.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Brimo.IDP.STS.Identity.Services
{
    public class TwilioSMSNotification : ISMSNotification
    {
        private readonly TwilioSMSConfigurations _sMSMessagesConfigurations;
        public TwilioSMSNotification(TwilioSMSConfigurations sMSMessagesConfigurations)
        {
            _sMSMessagesConfigurations = sMSMessagesConfigurations;
        }
        public void Send(SMSMessageModel smsMessageModel)
        {
            TwilioClient.Init(_sMSMessagesConfigurations.AccountSID, _sMSMessagesConfigurations.AuthToken);
            var message = MessageResource.Create(
            body: smsMessageModel.Message,
            from: new Twilio.Types.PhoneNumber(_sMSMessagesConfigurations.FromPhoneNumber),
            to: new Twilio.Types.PhoneNumber(smsMessageModel.ToPhoneNumber));


        }
    }
}
