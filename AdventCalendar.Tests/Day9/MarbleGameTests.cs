using System;
using System.Collections.Generic;
using System.Text;
using AdventCalendar.Day9;
using Xunit;

namespace AdventCalendar.Tests.Day9
{
    public class MarbleGameTests
    {
        [Fact]
        public void Simulate468PlayersAndLastMarbleIsWorth71010Points()
        {
            var details = MarbleGame.Simulate(468, 71010);
            Assert.Equal(8317, details.HighScore);
        }

        [Fact]
        public void Simulate7PlayersAndLastMarbleIsWorth32Points()
        {
            var details = MarbleGame.Simulate(7, 32);
            Assert.Equal(8317, details.HighScore);
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
            Assert.Equal(37305, details.HighScore);
        }
    }
}
