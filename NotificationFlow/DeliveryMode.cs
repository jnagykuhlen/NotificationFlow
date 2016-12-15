using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationFlow
{
    /// <summary>
    /// Describes how notifications are delivered to the receivers.
    /// </summary>
    public enum DeliveryMode
    {
        /// <summary>
        /// Delivery of this notification is started immediately, possibly delaying the delivery of
        /// prior notifications. This results in a behavior comparable to a function call stack.
        /// </summary>
        Immediate,

        /// <summary>
        /// Delivery of this notification is delayed until the currently pending message has been delivered
        /// to all receivers.
        /// </summary>
        Ordered
    }
}
