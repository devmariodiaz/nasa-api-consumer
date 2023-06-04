﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaAPIConsumer.Domain
{
    public class Asteroid
    {
        public string Name { get; set; }
        public float Diameter { get; set; }
        public float Velocity { get; set; }
        public DateTime Date { get; set; }
        public string Planet { get; set; }
    }
}
