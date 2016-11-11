using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationFlow
{
    /// <summary>
    /// Gives access to the notification system to deliver notifications.
    /// </summary>
    public interface INotificationContext
    {
        /// <summary>
        /// Sends a notification in this notification context.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="notification">The notification to deliver.</param>
        void SendNotification(object sender, object notification);
    }
}
