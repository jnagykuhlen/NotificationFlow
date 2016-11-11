using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationFlow
{
    /// <summary>
    /// Represents a component which is subscribed to a particular notification type.
    /// </summary>
    /// <typeparam name="T">Type of notifications this component is interested in.</typeparam>
    public interface INotificationListener<in T>
    {
        /// <summary>
        /// Defines how a received notification of specified type is handled by this component.
        /// </summary>
        /// <param name="sender">The sender of the received notification.</param>
        /// <param name="notification">The received notification to handle by this component.</param>
        /// <param name="context">The context of the received notification which can be used to send subsequent notifications.</param>
        void HandleNotification(object sender, T notification, INotificationContext context);
    }

    /// <summary>
    /// Represents a component which is subscribed to notifications of arbitrary type.
    /// </summary>
    public interface INotificationListener : INotificationListener<object> { }
}
