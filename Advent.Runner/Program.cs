using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Advent.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var startup = new Startup();

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            ServiceCollection services = new ServiceCollection();

            startup.BuildConfiguration(configurationBuilder);

            IConfiguration configuration = configurationBuilder.Build();

            startup.ConfigureServices(services, configuration);

            PipelineBuilder pipelineBuilder = new PipelineBuilder();

            startup.ConfigurePipeline(pipelineBuilder, configuration);

            IPipeline pipeline = pipelineBuilder.Build();

            var runner = new Runner(services, configuration, pipeline);

            await runner.RunAsync();
        }
    }
}
