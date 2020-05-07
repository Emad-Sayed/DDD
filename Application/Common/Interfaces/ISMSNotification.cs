using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ISMSNotification
    {
        void Send(SMSMessageModel smsMessageModel);

    }
}
