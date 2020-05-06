using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SMSMessage
{
    public interface ISMSMessagesService
    {
        void Send(SMSMessageModel smsMessageModel);

    }
}
