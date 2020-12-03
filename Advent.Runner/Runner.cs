using Advent.Runner.Web;
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
                    var fetcher = ActivatorUtilities.CreateInstance<ExerciseService>(scope.ServiceProvider);

                    for (int y = 2015; y <= 2020; y++)
                    {
                        for (int d = 1; d <= 25; d++)
                        {
                            var fetched = await fetcher.Fetch(y, d);
                            if (fetched)
                            {
                                await fetcher.RetrieveInput(y, d);
                            }
                        }
                    }

                    await fetcher.SubmitAnswer(2016, 1, 1, "1");
                }
            }
            catch (Exception e)
            {
                runnerLogger.LogError(e, "Runner error");
            }
        }
    }
}
