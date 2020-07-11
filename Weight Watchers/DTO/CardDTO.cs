﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weight_Watchers.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float BMI { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }
    }
}
