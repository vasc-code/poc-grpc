using Infrastructure.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        protected BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool IsValidOperation()
        {
            return _notifications.IsValidOperation();
        }

        protected bool HasNotifications()
        {
            return _notifications.HasNotifications();
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetErrorMessages().Select(notification => notification.Value).ToList();
        }

        protected IEnumerable<string> GetNotifications()
        {
            return _notifications.GetNotifications().Select(notification => notification.Value).ToList();
        }
    }
}
