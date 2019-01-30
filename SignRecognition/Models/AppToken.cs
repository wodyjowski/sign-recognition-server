using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models
{
    public class AppToken
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
