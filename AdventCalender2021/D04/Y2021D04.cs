using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2021.D04
{
    [Exercise("Day 4: Giant Squid")]
    class Y2021D04 : FileSelectionParsingConsole<BingoGame>, IExercise
    {
        public void Execute()
        {
            Start("D04/Data");
        }

        protected override BingoGame DeserializeData(IList<string> data)
        {
            var drawnNumbers = data.First().Split(",").Select(x => int.Parse(x));

            var allBoardData = data.Skip(2);

            var boards = new List<BingoBoard>();

            bool process = true;
            while (process)
            {
                var boardData = allBoardData.Take(5);

                var items = boardData.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                            .Select(x => x.Select(i => int.Parse(i)))
                            .SelectMany(i => i);

                boards.Add(new BingoBoard(items, 5, 5));

                allBoardData = allBoardData.Skip(6);
                process = allBoardData.Any();
            }

            return new BingoGame
            {
                DrawnNumbers = drawnNumbers.ToArray(),
                Boards = boards.ToArray(),
            };
        }

        protected override void Execute(BingoGame data)
        {
            Timer.Monitor("Part 1", () =>
            {
                bool winnerFound = false;

                int drawnCount = 0;
                BingoBoard winner = null;
                int drawnNumber = -1;
                while (!winnerFound)
                {
                    drawnNumber = data.DrawnNumbers.Skip(drawnCount++).First();

                    foreach (var board in data.Boards)
                    {
                        if (board.Mark(drawnNumber))
                        {
                            winnerFound = true;
                            winner = board;
                            break;
                        }
                    }
                }

                AnswerPartOne(winner.GetUnmarked().Sum() * drawnNumber);
            });

            Timer.Monitor("Part 2", () =>
            {
                BingoBoard winner = null;
                int drawnCount = 0;
                int drawnNumber = -1;

                while (data.Boards.Any(b => !b.Complete))
                {
                    drawnNumber = data.DrawnNumbers.Skip(drawnCount++).First();

                    foreach (var board in data.Boards.Where(b => !b.Complete))
                    {
                        if (board.Mark(drawnNumber))
                        {
                            winner = board;
                        }
                    }
                }

                AnswerPartTwo(winner.GetUnmarked().Sum() * drawnNumber);
            });
        }
    }
}
