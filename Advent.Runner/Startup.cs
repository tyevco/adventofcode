using Advent.Runner.File;
using Advent.Runner.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Advent.Runner
{
    public class Startup
    {
        public void BuildConfiguration(IConfigurationBuilder configuration)
        {
            configuration.AddJsonFile("appsettings.json", false);
            configuration.AddJsonFile("local.settings.json", true);
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c => c.AddConsole());

            services.Configure<SiteOptions>(o => configuration.GetSection("site").Bind(o));
            services.Configure<ApplicationOptions>(o => configuration.GetSection("app").Bind(o));
            services.AddScoped<ExerciseService>();
            services.AddScoped<ScriptCreator>();

            services.AddHttpClient();
        }

        public void ConfigurePipeline(IPipelineBuilder pipeline, IConfiguration configuration)
        {

        }
    }
}
