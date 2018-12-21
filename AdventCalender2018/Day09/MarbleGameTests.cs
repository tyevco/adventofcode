using System;
using System.Collections.Generic;
using System.Text;
using Day09;
using Xunit;

namespace Tests.Day09
{
    public class MarbleGameTests
    {
        [Fact]
        public void Simulate468PlayersAndLastMarbleIsWorth71010Points()
        {
            var details = MarbleGame.Simulate(468, 71010);
            Assert.Equal(374287, details.HighScore);
        }

        [Fact]
        public void Simulate468PlayersAndLastMarbleIsWorth7101000Points()
        {
            var details = MarbleGame.Simulate(468, 7101000);
            Assert.Equal(3083412635, details.HighScore);
        }

        [Fact]
        public void Simulate7PlayersAndLastMarbleIsWorth25Points()
        {
            var details = MarbleGame.Simulate(7, 25);
            Assert.Equal(32, details.HighScore);
        }

        [Fact]
        public void Simulate10PlayersAndLastMarbleIsWorth1618Points()
        {
            var details = MarbleGame.Simulate(10, 1618);
            Assert.Equal(8317, details.HighScore);
        }

        [Fact]
        public void Simulate13PlayersAndLastMarbleIsWorth7999Points()
        {
            var details = MarbleGame.Simulate(13, 7999);
            Assert.Equal(146373, details.HighScore);
        }

        [Fact]
        public void Simulate17PlayersAndLastMarbleIsWorth1104Points()
        {
            var details = MarbleGame.Simulate(17, 1104);
            Assert.Equal(2764, details.HighScore);
        }

        [Fact]
        public void Simulate21PlayersAndLastMarbleIsWorth6111Points()
        {
            var details = MarbleGame.Simulate(21, 6111);
            Assert.Equal(54718, details.HighScore);
        }

        [Fact]
        public void Simulate30PlayersAndLastMarbleIsWorth5807Points()
        {
            var details = MarbleGame.Simulate(30, 5807);
            Assert.Equal(37305, details.HighScore); // 37305 - this is coming back wrong
        }
    }
}
