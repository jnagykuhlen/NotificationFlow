using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFlow.Internal
{
    public class DeliveryType<T> : IDeliveryType
    {
        private INotificationListener<T>[] _listeners;

        public DeliveryType(IEnumerable<object> receivers)
        {
            _listeners = receivers.OfType<INotificationListener<T>>().ToArray();
        }

        public void Deliver(object sender, object notification, INotificationContext context)
        {
            foreach (INotificationListener<T> listener in _listeners)
                listener.HandleNotification(sender, (T)notification, context);
        }
    }
}
