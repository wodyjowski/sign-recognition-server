using Microsoft.AspNetCore.Http;
using SignRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models.FormModels
{
    public class LocationImageFormModel : IPredictionLocation
    {
        public string Class { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IFormFile Image { get; set; }
    }
}
