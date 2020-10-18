using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.Interfaces;

namespace Domain.NotificationManagment.AggregatesModel
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<(int, List<Notification>)> GetAllAsync(string userId, int pageNumber, int pageSize, string keyWord);
        (int, int, int) GetMyNotificationCounterAsync(string userId);
        Notification Add(Notification notification);
        void Update(Notification notification);
        void Delete(Notification notification);
        Task<Notification> FindByIdAsync(string id);
    }
}
