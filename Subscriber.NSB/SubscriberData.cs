using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber.NSB
{
    public class SubscriberData : ContainSagaData
    {
        public int MeasureId { get; set; }

        public bool IsBMIUpdated { get; set; }

        public bool IsTrackingUpdated { get; set; }
    }
}
