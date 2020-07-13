using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.Api.DTO
{
    public class TrackingDTO
    {
        [Range(5, 300)]
        public float Weight { get; set; }
        public DateTime Date { get; set; }
        public int trend { get; set; }
        public float BMI { get; set; }
        public string Comments { get; set; }
    }
}
