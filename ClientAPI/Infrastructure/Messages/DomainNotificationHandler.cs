using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private ICollection<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual IEnumerable<DomainNotification> GetErrorMessages()
        {
            return _notifications.Where(notification => notification.IsError == true);
        }

        public virtual IEnumerable<DomainNotification> GetNotifications()
        {
            return _notifications.Where(notification => notification.IsError == false);
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any(notification => notification.IsError == false);
        }

        public virtual bool IsValidOperation() 
        {
            return !_notifications.Any(notification => notification.IsError == true);
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}