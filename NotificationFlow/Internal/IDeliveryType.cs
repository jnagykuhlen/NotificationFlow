using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFlow.Internal
{
    public interface IDeliveryType
    {
        void Deliver(object sender, object notification, INotificationContext context);
    }
}
