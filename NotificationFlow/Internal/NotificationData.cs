using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFlow.Internal
{
    public class NotificationData
    {
        private object _sender;
        private object _notification;

        public NotificationData(object sender, object notification)
        {
            _sender = sender;
            _notification = notification;
        }

        public object Sender
        {
            get
            {
                return _sender;
            }
        }

        public object Notification
        {
            get
            {
                return _notification;
            }
        }
    }
}
