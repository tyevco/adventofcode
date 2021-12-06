using Advent.Runner.File;
using Advent.Runner.Web;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Advent.Runner.Pipelines
{
    class GenerateFilesPipeline
    {
        public GenerateFilesPipeline(
                ILogger<GenerateFilesPipeline> logger,
                ExerciseService exerciseService,
                ScriptCreator scriptCreator,
                AnswerCreator answerCreator,
                MarkdownCreator markdownCreator)
       {
            Logger = logger;
            ExerciseService = exerciseService;
            ScriptCreator = scriptCreator;
            AnswerCreator = answerCreator;
            MarkdownCreator = markdownCreator;
        }

        public ILogger<GenerateFilesPipeline> Logger { get; }
        public ExerciseService ExerciseService { get; }
        public ScriptCreator ScriptCreator { get; }
        public AnswerCreator AnswerCreator { get; }
        public MarkdownCreator MarkdownCreator { get; }

        public async Task<bool> Generate(int year, int day)
        {
            bool received = false;

            //while (!received)
            //{
            var model = await ExerciseService.RetrieveExerciseMetadata(year, day);

            if (model != null)
            {
                await ScriptCreator.CreateScript(model);
                await MarkdownCreator.CreateMarkdown(model);
                await AnswerCreator.CreateAnswerFiles(model);
                await ExerciseService.RetrieveInput(year, day);

                received = true;
            }
            else
            {
                System.Threading.Thread.Sleep(5000);
            }
            //}

            return received;
        }
    }
}
