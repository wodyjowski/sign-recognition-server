using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models.FormModels
{
    public class UpdateEmailAndPasswordFormModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
