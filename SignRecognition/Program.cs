using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SignRecognition.Models.DBContext;

namespace SignRecognition
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {

            var host = WebHost.CreateDefaultBuilder(args)
                        .UseStartup<Startup>().Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            return host;
        }
    }
}
