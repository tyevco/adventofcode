using Advent.Runner.Extensions;
using Advent.Runner.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReverseMarkdown;
using System.Threading.Tasks;

namespace Advent.Runner.File
{
    public class MarkdownCreator
    {
        public MarkdownCreator(
                    ILogger<MarkdownCreator> logger,
                    IOptions<ApplicationOptions> appOptions)
        {
            Logger = logger;
            AppOptions = appOptions;
        }

        public ILogger<MarkdownCreator> Logger { get; }
        public IOptions<ApplicationOptions> AppOptions { get; }

        public async Task<bool> CreateMarkdown(ExerciseMetadata model, bool overwriteFile = false)
        {
            string file = GetFilename(model);
            if (!overwriteFile)
            {
                if (System.IO.File.Exists(file))
                {
                    return false;
                }
            }

            var config = new Config
            {
                // Include the unknown tag completely in the result (default as well)
                UnknownTags = Config.UnknownTagsOption.Bypass,
                // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                GithubFlavored = true,
                // will ignore all comments
                RemoveComments = true,
                // remove markdown output for links where appropriate
                SmartHrefHandling = true
            };

            var converter = new Converter(config);

            await System.IO.File.WriteAllTextAsync(file, converter.Convert(model.FirstExerciseHtml));

            if (model.PartTwoUnlocked)
            {
                await System.IO.File.AppendAllTextAsync(file, $"\n\n{converter.Convert(model.SecondExerciseHtml)}");
            }

            return true;
        } 

        private string GetFilename(ExerciseMetadata model)
        {
            string day = model.Day.ToString().PadLeft(2, '0');
            string path = this.AppOptions.Value.MarkdownDirectory.Interpolate(("year", model.Year), ("day", day));

            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path));

            return System.IO.Path.Combine(path, $"{model.Day}.md");

        }
    }
}
