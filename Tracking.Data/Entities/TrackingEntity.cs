using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tracking.Data.Entities
{
    public class TrackingEntity
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public float Weight { get; set; }

        public DateTime Date { get; set; }

        public int Trend { get; set; }

        public float BMI { get; set; }

        public string Comments { get; set; }
    }
}
