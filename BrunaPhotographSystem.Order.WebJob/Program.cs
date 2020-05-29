using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host;

namespace BrunaPhotographSystem.Order.WebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var builder = new HostBuilder();

            //builder.ConfigureAppConfiguration(c =>
            //{
            //    c.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            //    c.AddEnvironmentVariables();
            //});

            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage(a =>
                {
                    a.BatchSize = 8;
                    a.NewBatchThreshold = 4;
                    a.MaxDequeueCount = 4;
                    a.MaxPollingInterval = TimeSpan.FromSeconds(15);
                });
            });

            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });

            var host = builder.Build();
            using (host)
            {
                host.Run();
            }
        }
    }
}
