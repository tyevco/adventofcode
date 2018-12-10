using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Day9
{
    public class MarbleGame
    {
        public LinkedList<Marble> Marbles { get; private set; } = new LinkedList<Marble>();

        public List<MarblePlayer> Players { get; private set; } = new List<MarblePlayer>();

        private MarbleGame() { }

        public static MarbleGameDetails Simulate(int playerCount, int lastMarbleValue)
        {
            var game = new MarbleGame();

            for (int i = 0; i < playerCount; i++)
            {
                game.Players.Add(new MarblePlayer());
            }

            return null;
        }
    }
}
