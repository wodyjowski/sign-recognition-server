using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SignRecognition.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        [JsonIgnore]
        public ICollection<Prediction> Predictions { get; set; }
        public ICollection<AppToken> Tokens { get; set; }

    }
}
