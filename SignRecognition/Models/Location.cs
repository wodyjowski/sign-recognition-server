using SignRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models
{
    public class Location : IPredictionLocation
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifDate { get; set; }
        public string Class { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
