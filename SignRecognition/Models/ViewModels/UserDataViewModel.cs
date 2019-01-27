using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models.ViewModels
{
    public class UserDataViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public bool AdminRights { get; set; }
    }
}
