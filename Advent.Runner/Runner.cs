using Advent.Runner.Pipelines;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Advent.Runner
{
    internal class Runner
    {
        public Runner(IServiceCollection services, IConfiguration configuration, IPipeline pipeline)
        {
            Services = services;
            Configuration = configuration;
            Pipeline = pipeline;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
        public IPipeline Pipeline { get; }

        public async Task RunAsync()
        {
            var serviceProvider = Services.BuildServiceProvider();
            var runnerLogger = serviceProvider.GetService<ILogger<Runner>>();
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var filesPipeline = ActivatorUtilities.CreateInstance<GenerateFilesPipeline>(scope.ServiceProvider);

                    var now = DateTime.Now;
                    var targetDate = new DateTime(now.Year, now.Month, now.Day + 1, 0, 0, 0);
                    var waitTimespan = targetDate.AddSeconds(-30) - now;

                    Console.WriteLine($"Waiting {waitTimespan} until execution for {targetDate:yyyy-MM-dd} puzzle...");
                    System.Threading.Thread.Sleep((int)waitTimespan.TotalMilliseconds);

                    var fetched = await filesPipeline.Generate(targetDate.Year, targetDate.Day);
                }
            }
            catch (Exception e)
            {
                runnerLogger.LogError(e, "Runner error");
            }
        }
    }
}
