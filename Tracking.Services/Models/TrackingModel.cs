using System;
using System.Collections.Generic;
using System.Text;

namespace Tracking.Services.Models
{
    public class TrackingModel
    {
        public float Weight { get; set; }

        public DateTime Date { get; set; }

        public int trend { get; set; }

        public int CardId { get; set; }

        public float BMI { get; set; }

        public string Comments { get; set; }
    }
}
