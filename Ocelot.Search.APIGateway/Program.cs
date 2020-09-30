using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile(Path.Combine("Web.Bff.Twitter.Search", "configuration.json"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((o) =>
                        {
                            o.ConfigureHttpsDefaults(options =>
                            {
                                options.ServerCertificate = new X509Certificate2(File.ReadAllBytes("./certificado-desenv/certificate.pfx"), "senhacertitau");
                            } );
                        }
                    );
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://*:5001;http://*:5000");
                });
    }
}