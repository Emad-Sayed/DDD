using Domain.Common.Interfaces;
using Domain.NotificationManagment.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Persistence.NotificationManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.NotificationManagment
{
    public class NotificationRepository
      : INotificationRepository
    {
        private readonly NotificationContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public NotificationRepository(NotificationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Notification Add(Notification notification)
        {
            return _context.Notifications
                   .Add(notification)
                   .Entity;
        }

        public void Update(Notification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
        }

        public async Task<(int, List<Notification>)> GetAllAsync(string userId, int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Notifications.AsQueryable();

            query = query.Where(x => x.UserId == userId);

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord) ||
                x.Title.Contains(keyWord) ||
                x.Content.Contains(keyWord)
                );
            }


            // apply pagination to notifications
            var notifications = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, notifications);
        }

        public async Task<Notification> FindByIdAsync(string id)
        {
            return await _context.Notifications
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public void UpdateAll(List<Notification> notifications)
        {
            _context.Notifications.UpdateRange(notifications);
        }

        public async Task<List<Notification>> FindByIdsAsync(List<string> ids)
        {
            var notifications = _context.Notifications.Where(x => ids.Contains(x.Id.ToString()));
            return await notifications.ToListAsync();
        }

        public (int, int, int) GetMyNotificationCounterAsync(string userId)
        {
            var query = _context.Notifications.AsQueryable();

            query = query.Where(x => x.UserId == userId);

            var total = query.Count();
            var readed = query.Where(x => x.IsReaded == true).Count();
            var unReaded = query.Where(x => x.IsReaded == false).Count();

            return (total, readed, unReaded);
        }
    }
}
