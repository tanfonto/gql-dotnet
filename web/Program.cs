using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gqlpoc.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) => {
                    var envName = ctx.HostingEnvironment.EnvironmentName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"../", $"config/appsettings.{envName}.json");
                    cfg.AddJsonFile(path);
               })
                .UseWebRoot("../public")
                .UseStartup<Startup>()
                .Build();
    }
}
