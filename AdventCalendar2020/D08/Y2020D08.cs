using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D08
{
    [Exercise("Day 8: Handheld Halting")]
    class Y2020D08 : FileSelectionParsingConsole<GameMan>, IExercise
    {
        public void Execute()
        {
            Start("D08/Data");
        }

        protected override GameMan DeserializeData(IList<string> data)
        {
            return new GameMan(data);
        }

        protected override void Execute(GameMan game)
        {
            AnswerPartOne(game.Process());

            AnswerPartTwo(game.ProcessTwo());
        }

    }
}
