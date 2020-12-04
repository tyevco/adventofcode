using Advent.Runner.Extensions;
using Advent.Runner.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Advent.Runner.File
{
    public class AnswerCreator
    {
        public AnswerCreator(
                    ILogger<AnswerCreator> logger,
                    IOptions<ApplicationOptions> appOptions)
        {
            Logger = logger;
            AppOptions = appOptions;
        }

        public ILogger<AnswerCreator> Logger { get; }
        public IOptions<ApplicationOptions> AppOptions { get; }

        public async Task<bool> CreateAnswerFiles(ExerciseMetadata model, bool overwriteFile = false)
        {
            string scriptFile = GetFilename(model);
            if (!overwriteFile)
            {
                if (System.IO.File.Exists(scriptFile))
                {
                    return false;
                }
            }

            var answers = new Answers();

            if (model.PartOneAnswered)
            {
                answers.PartOne = model.FirstAnswer;
            }
            if (model.PartOneAnswered)
            {
                answers.PartTwo = model.SecondAnswer;
            }

            await System.IO.File.WriteAllTextAsync(scriptFile, JsonConvert.SerializeObject(answers));

            return true;
        }

        private string GetFilename(ExerciseMetadata model)
        {
            string day = model.Day.ToString().PadLeft(2, '0');
            var path = this.AppOptions.Value.AnswersDirectory.Interpolate(("year", model.Year), ("day", day));
            System.IO.Directory.CreateDirectory(path);

            return System.IO.Path.Combine(path, $"Y{model.Year}D{day}.json");
        }

        private class Answers
        {
            public string PartOne { get; set; }

            public string PartTwo { get; set; }
        }
    }
}
