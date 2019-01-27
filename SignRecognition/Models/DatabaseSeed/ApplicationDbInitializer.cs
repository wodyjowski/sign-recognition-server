using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignRecognition.Models.DatabaseSeed
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<User> userManager, string username, string email, string password)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                User user = new User
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
