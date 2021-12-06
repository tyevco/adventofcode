using Advent.Runner.Web;
using Advent.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Advent.Runner.File
{
    public class ScriptCreator
    {
        private const string scriptFormat = @"using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar{1}.D{2}
{{
    [Exercise(""{0}"")]
    class Y{1}D{2} : FileSelectionParsingConsole<IList<string>>, IExercise
    {{
        public void Execute()
        {{
            Start(""D{2}/Data"");
        }}

        protected override IList<string> DeserializeData(IList<string> data)
        {{
            // Setup data parser.
            return data;
        }}

        protected override void Execute(IList<string> data)
        {{
            // perform task.
        }}
    }}
}}
";
        public ScriptCreator(
                    ILogger<ScriptCreator> logger,
                    IOptions<ApplicationOptions> appOptions)
        {
            Logger = logger;
            AppOptions = appOptions;
        }

        public ILogger<ScriptCreator> Logger { get; }
        public IOptions<ApplicationOptions> AppOptions { get; }

        public async Task<bool> CreateScript(ExerciseMetadata model, bool overwriteFile = false)
        {
            string scriptFile = GetFilename(model);
            if (!overwriteFile)
            {
                if (System.IO.File.Exists(scriptFile))
                {
                    return false;
                }
            }

            var fileContents = string.Format(scriptFormat, model.Title, model.Year, model.Day.ToString().PadLeft(2, '0'));

            await System.IO.File.WriteAllTextAsync(scriptFile, fileContents);

            return true;
        }

        private string GetFilename(ExerciseMetadata model)
        {
            string day = model.Day.ToString().PadLeft(2, '0');
            var path = this.AppOptions.Value.ScriptDataDirectory.Interpolate(("year", model.Year), ("day", day));

            System.IO.Directory.CreateDirectory(path);

            return System.IO.Path.Combine(path, $"Y{model.Year}D{day}.cs");

        }
    }
}
