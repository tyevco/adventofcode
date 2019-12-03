using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    public class StepTreeParser
    {
        Regex stepParser = new Regex("Step (.+?) must be finished before step (.+?) can begin.");
        IDictionary<char, StepNode> steps = new Dictionary<char, StepNode>();


        public static StepTree Parse(IList<string> sampleData)
        {
            return new StepTreeParser().parse(sampleData);
        }

        private StepTree parse(IList<string> data)
        {
            StepTree root = new StepTree();

            foreach (var line in data)
            {
                var match = stepParser.Match(line);

                var before = match.Groups[1].Value[0];
                var after = match.Groups[2].Value[0];

                StepNode beforeStep;
                if (steps.ContainsKey(before))
                {
                    beforeStep = steps[before];
                }
                else
                {
                    beforeStep = new StepNode()
                    {
                        Id = before
                    };
                    steps.Add(before, beforeStep);
                }

                StepNode afterStep;
                if (steps.ContainsKey(after))
                {
                    afterStep = steps[after];
                }
                else
                {
                    afterStep = new StepNode()
                    {
                        Id = after
                    };

                    steps.Add(after, afterStep);
                }

                beforeStep.Children.Add(afterStep);
                afterStep.Parents.Add(beforeStep);
            }

            root.Children = steps.Where(x => x.Value.Parents.Count == 0).Select(x => x.Value).OrderBy(x => x.Id).ToList();

            return root;
        }
    }
}
