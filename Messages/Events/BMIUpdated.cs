using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Messages.Events
{
    public class BMIUpdated
    {
        public int MeasureId { get; set; }
        public int CardId { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }        
    }
}
