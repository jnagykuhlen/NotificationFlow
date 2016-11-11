using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NotificationFlow.Internal;

namespace NotificationFlow
{
    /// <summary>
    /// Represents a central entity in the application's architecture, acting as a mediator between
    /// several components which are unaware of each other. Notifications sent to this notification manager
    /// are delivered to all registered receivers that subscribed to the notification's particular type.
    /// </summary>
    public class NotificationManager : INotificationContext
    {
        private List<object> _receivers;
        private Dictionary<Type, IDeliveryType> _deliveryTypes;
        private LinkedList<NotificationData> _pendingNotifications;
        private LinkedListNode<NotificationData> _currentNode;
        
        /// <summary>
        /// Creates a new notification manager.
        /// </summary>
        public NotificationManager()
        {
            _receivers = new List<object>();
            _deliveryTypes = new Dictionary<Type, IDeliveryType>();
            _pendingNotifications = new LinkedList<NotificationData>();
            _currentNode = null;
        }

        /// <summary>
        /// Registers a receiver to this notification manager. It will receive all notifications sent to this
        /// notification manager, provided that the component subscribed to the particular notification type.
        /// </summary>
        /// <param name="receiver">The component which is registered for receiving notifications.</param>
        public void RegisterReceiver(object receiver)
        {
            if (receiver == null)
                throw new ArgumentNullException("receiver");

            _receivers.Add(receiver);
            _deliveryTypes.Clear();
        }

        /// <summary>
        /// Unregisters a receiver from this notification manager. It will not receive any notification sent to this
        /// notification manager anymore.
        /// </summary>
        /// <param name="receiver"></param>
        public void UnregisterReceiver(object receiver)
        {
            if (receiver == null)
                throw new ArgumentNullException("receiver");

            _receivers.Remove(receiver);
            _deliveryTypes.Clear();
        }
        
        /// <summary>
        /// Sends a notification via this notification manager. The notification is delivered
        /// to all registered components that subscribed to its type.
        /// </summary>
        /// <param name="sender">The sender of the notification.</param>
        /// <param name="notification">The notification to deliver.</param>
        public void SendNotification(object sender, object notification)
        {
            if (notification == null)
                throw new ArgumentNullException("notification");

            NotificationData notificationData = new NotificationData(sender, notification);
            if (_currentNode == null)
            {
                _pendingNotifications.AddFirst(notificationData);
                ProcessPendingNotifications();
            }
            else
            {
                _currentNode = _pendingNotifications.AddAfter(_currentNode, notificationData);
            }
        }

        private void ProcessPendingNotifications()
        {
            while(_pendingNotifications.First != null)
            {
                _currentNode = _pendingNotifications.First;
                NotificationData notificationData = _currentNode.Value;
                DeliverNotification(notificationData.Sender, notificationData.Notification);
                _pendingNotifications.RemoveFirst();
            }

            _currentNode = null;
        }

        private void DeliverNotification(object sender, object notification)
        {
            Type type = notification.GetType();
            IDeliveryType deliveryType;
            if (!_deliveryTypes.TryGetValue(type, out deliveryType))
            {
                deliveryType = (IDeliveryType)Activator.CreateInstance(typeof(DeliveryType<>).MakeGenericType(type), _receivers);
                _deliveryTypes.Add(type, deliveryType);
            }

            deliveryType.Deliver(sender, notification, this);
        }

        /// <summary>
        /// Gets all receivers that are registered to this notification manager.
        /// </summary>
        public IEnumerable<object> Receivers
        {
            get
            {
                return _receivers;
            }
        }
    }
}
