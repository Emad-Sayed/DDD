using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Services
{
    public interface ISMSNotification
    {
        void Send(SMSMessageModel smsMessageCModel);
    }
}
