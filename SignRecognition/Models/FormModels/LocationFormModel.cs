using SignRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models.FormModels
{
    public class LocationFormModel : IPredictionLocation
    {
        public string Class { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
