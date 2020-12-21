using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace InternetTechnologies.Server.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var webHostBuilder = new WebHostBuilder();

            //var host = webHostBuilder.UseKestrel()
            //              .UseStartup<Startup>()
            //              .UseContentRoot(Directory.GetCurrentDirectory())
            //              .Build();

            //host.Run(); // Слава Украине!!!

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseKestrel();
                });
    }
}
