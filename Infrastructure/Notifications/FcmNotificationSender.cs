using Domain.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorePush.Google;
using System.Net.Http;
using Domain.NotificationManagment.AggregatesModel;
using Newtonsoft.Json;

namespace Infrastructure.Notifications
{
    public class FcmNotificationSender : INotificationService
    {
        private IConfiguration _configuration { get; set; }
        public FcmNotificationSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(Notification notification, List<string> DevicesIds)
        {
            if (DevicesIds == null) return;

            foreach (var deviceId in DevicesIds)
            {
                if (deviceId == null) return;
                var fcmSettings = new FcmSettings { ServerKey = _configuration["FirebaseNotifications:ServerKey"], SenderId = _configuration["FirebaseNotifications:SenderID"] };

                var fcm = new FcmSender(fcmSettings, new HttpClient());
                var notificationToSend = new GoogleNotification { Title = notification.Title, Content = notification.Content, Data = new GoogleNotification.DataPayload { EntityId = notification.EntityId } };
                    var fcmResponse = await fcm.SendAsync(deviceId, notificationToSend);

            }
        }
    }
    public class GoogleNotification
    {
        public class DataPayload
        {
            // Add your custom properties as needed
            [JsonProperty("entityId")]
            public string EntityId { get; set; }
        }


        [JsonProperty("data")]
        public DataPayload Data { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}