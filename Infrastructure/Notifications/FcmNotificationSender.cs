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
using System.Net;
using System.Net.Http.Headers;
using System.Linq;

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

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer AAAAtEfU_eM:APA91bGqs-GI48VQjdMYYqicacss7XOdK9-pXnMWLnebE7P2m0owmMmZwVkvLtEyao0V4lXYN7tczsfYuke33RrefmIdONnwhN8dVrdMDIyjVXbBW73Rk2f9x5HsZ3_MKZ8d7P7CBOPm");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = notification.Content,
                        title = notification.Title
                    },
                    data = new
                    {
                        entityId = notification.EntityId,
                        notificationId = notification.Id.ToString(),

                    }
                };

                var json = JsonConvert.SerializeObject(body);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", data);
                //var ss = fcmResponse;
                if (res.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    throw new Exception(await res.Content.ReadAsStringAsync());
                }
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