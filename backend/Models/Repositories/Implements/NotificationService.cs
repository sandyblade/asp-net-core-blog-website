/**
 * This file is part of the Sandy Andryanto Blog Application.
 *
 * @author     Sandy Andryanto <sandy.andryanto.blade@gmail.com>
 * @copyright  2024
 *
 * For the full copyright and license information,
 * please view the LICENSE.md file that was distributed
 * with this source code.
 */

using backend.Models.Repositories.Interfaces;
using backend.Models.Entities;
using backend.Models.DTO;
using System.Linq.Dynamic.Core;

namespace backend.Models.Repositories.Implements
{
    public class NotificationService : INotificationRepository
    {
        private readonly AppDbContext _db;
        private readonly IActivityRepository _activityRepository;

        public NotificationService(AppDbContext db, IActivityRepository activityRepository)
        {
            _db = db;
            _activityRepository = activityRepository;
        }

        public Notification GetByUser(User user, long Id)
        {
            Notification notif = _db.Notification.Where(x => x.User == user && x.Id == Id).FirstOrDefault();
            return notif == null ? null : notif;
        }

        public void Delete(User user, long Id)
        {
            Notification notif = _db.Notification.Where(x => x.User == user && x.Id == Id).FirstOrDefault();

            if(notif != null)
            {
                _db.Remove(notif);
                _db.SaveChanges();
            }

            if (user != null)
            {
                _activityRepository.SaveActivity(user, "Delete Notification", "The user delete notification with subject " + notif.Subject);
            }

        }

        public List<Notification> GetByUser(User user, FilterDTO filter)
        {
            var attr = typeof(Notification).GetProperty(filter.OrderBy);
            var data = _db.Notification.AsQueryable();
            data = data.Where(x => x.User == user);

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                data = data.Where(x => x.Subject.Contains(filter.Search) || x.Message.Contains(filter.Search));
            }

            return data.OrderBy(filter.OrderBy + " " + filter.OrderDir).Skip(filter.Offset).Take(filter.Limit).ToList();
        }

    }
}
