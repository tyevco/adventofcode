using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day09
{
    public class MarbleGame
    {
        public LinkedList<Marble> Marbles { get; private set; } = new LinkedList<Marble>();

        private LinkedListNode<Marble> CurrentMarble = null;
        private int MarbleCounter = 0;

        public LinkedList<MarblePlayer> Players { get; private set; } = new LinkedList<MarblePlayer>();

        private MarbleGame()
        {
            // always place first marble before game starts.
            PlaceMarble();
        }

        public static MarbleGameDetails Simulate(int playerCount, int lastMarbleValue)
        {
            var game = new MarbleGame();

            for (int i = 0; i < playerCount; i++)
            {
                game.Players.AddLast(new MarblePlayer()
                {
                    Id = i + 1
                });
            }

            System.Diagnostics.Debug.WriteLine($"[-] [0]");
            Marble lastMarble = null;
            long round = 0;

            if (game.Players.Count > 0)
            {
                LinkedListNode<MarblePlayer> currentPlayer = game.Players.First;

                while (lastMarble == null || (lastMarble.Id < lastMarbleValue))
                {
                    lastMarble = game.PlaceMarble();

                    if (lastMarble.Score > 0)
                        currentPlayer.Value.Marbles.Add(lastMarble);

                    var marbleList = game.Marbles.Select(x => x.Id.Equals(game.CurrentMarble.Value.Id) ? $"[{x.Id}]" : $" {x.Id} ");

                    //System.Diagnostics.Debug.WriteLine($"[{currentPlayer.Value.Id}] {string.Join(' ', marbleList)}");

                    round++;

                    currentPlayer = currentPlayer.Next ?? game.Players.First;
                }

            }

            return new MarbleGameDetails
            {
                Players = game.Players.ToList(),
                HighScore = game.Players.Max(x => x.Score),
                LastMarble = lastMarble,
                Rounds = round
            };
        }

        public Marble PlaceMarble()
        {
            Marble marble = null;
            if (CurrentMarble == null)
            {
                marble = new Marble()
                {
                    Id = MarbleCounter++
                };

                CurrentMarble = Marbles.AddFirst(marble);
            }
            else
            {
                if (MarbleCounter % 23 == 0)
                {
                    CurrentMarble = GetPreviousMarble(6);
                    var marbleRemovedNode = GetPreviousMarble(1);
                    var marbleRemoved = marbleRemovedNode.Value;
                    Marbles.Remove(marbleRemovedNode);

                    marble = new Marble()
                    {
                        Id = MarbleCounter++,
                        Score = marbleRemoved.Id
                    };

                    marble.Score = marbleRemoved.Id + marble.Id;
                }
                else
                {
                    var nextMarblePos = GetNextMarble(1);

                    marble = new Marble()
                    {
                        Id = MarbleCounter++
                    };
                    CurrentMarble = Marbles.AddAfter(nextMarblePos, marble);
                }
            }


            return marble;
        }

        private LinkedListNode<Marble> GetPreviousMarble(int count)
        {
            var node = CurrentMarble;

            for (int i = 0; i < count; i++)
            {
                node = node.Previous ?? Marbles.Last;
            }

            return node;
        }

        private LinkedListNode<Marble> GetNextMarble(int count)
        {
            var node = CurrentMarble;

            for (int i = 0; i < count; i++)
            {
                node = node.Next ?? Marbles.First;
            }

            return node;
        }
    }
}
