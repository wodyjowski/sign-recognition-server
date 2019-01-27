using SignRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models
{
    public class Prediction : IPredictionLocation
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        public User User { get; set; }
        public string Class { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FileName { get; set; }
    }
}
