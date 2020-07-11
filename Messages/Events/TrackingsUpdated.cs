using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public class TrackingsUpdated
    {
        public int MeasureId { get; set; }

        public bool succeeded { get; set; }
    }
}
