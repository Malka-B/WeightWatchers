using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public class MeasureAdded
    {
        public int MeasureId { get; set; }
        public int CardId { get; set; }
        public float Weight { get; set; }
        public string Status { get; set; }
    }
}
