using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticDatabaseTransactions.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElasticDatabaseTransactions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDatabasesIfNotExists(host);

            host.Run();
        }

        private static void CreateDatabasesIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                Database1Context context1 = null;
                Database2Context context2 = null;

                try
                {
                    context1 = services.GetRequiredService<Database1Context>();
                    Database1Initializer.Initialize(context1);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }

                try
                {
                    context2 = services.GetRequiredService<Database2Context>();
                    Database2Initializer.Initialize(context1, context2);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
