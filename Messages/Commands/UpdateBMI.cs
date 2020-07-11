using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Messages.Commands
{
    public class UpdateBMI
    {
        public int MeasureId { get; set; }

        public int CardId { get; set; }

        public float Weight { get; set; }
    }
}
