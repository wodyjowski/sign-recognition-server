using SignRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Authentication
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(User user);
    }
}
