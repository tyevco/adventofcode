using AdventCalendar.Day11;
using Xunit;

namespace AdventCalendar.Tests.Day11
{
    public class FuelCellTests
    {


        [Fact]
        public void FindLargestSubSectionWithMostPowerWithSerial7989()
        {
            var grid = new FuelGrid(300, 300, 7989);
            var point = grid.FindLargestSubSectionWithMostPower();

            Assert.Equal(90, point.X);
            Assert.Equal(269, point.Y);
            Assert.Equal(16, point.Size);
            Assert.Equal(30, point.TotalPower);
        }

        [Fact]
        public void Find3x3SubSectionWithMostPowerWithSerial7989()
        {
            var grid = new FuelGrid(300, 300, 7989);
            var point = grid.FindSubSectionWithMostPower(3);

            Assert.Equal(19, point.X);
            Assert.Equal(17, point.Y);
            Assert.Equal(29, point.TotalPower);
        }


        [Fact]
        public void FindLargestSubSectionWithMostPowerWithSerial18()
        {
            var grid = new FuelGrid(300, 300, 18);
            var point = grid.FindLargestSubSectionWithMostPower();

            Assert.Equal(90, point.X);
            Assert.Equal(269, point.Y);
            Assert.Equal(16, point.Size);
            Assert.Equal(113, point.TotalPower);
        }


        [Fact]
        public void FindLargestSubSectionWithMostPowerWithSerial42()
        {
            var grid = new FuelGrid(300, 300, 42);
            var point = grid.FindLargestSubSectionWithMostPower();

            Assert.Equal(232, point.X);
            Assert.Equal(251, point.Y);
            Assert.Equal(12, point.Size);
            Assert.Equal(119, point.TotalPower);
        }

        [Fact]
        public void Find3x3SubSectionWithMostPowerWithSerial42()
        {
            var grid = new FuelGrid(300, 300, 42);
            var point = grid.FindSubSectionWithMostPower(3);

            Assert.Equal(21, point.X);
            Assert.Equal(61, point.Y);
            Assert.Equal(30, point.TotalPower);
        }

        [Fact]
        public void TestCellAtX3Y5WithSerial8()
        {
            var cell = new FuelCell(3, 5, 8);
            Assert.Equal(4, cell.PowerLevel);
        }

        [Fact]
        public void TestCellAtX122Y79WithSerial57()
        {
            var cell = new FuelCell(122, 79, 57);
            Assert.Equal(-5, cell.PowerLevel);
        }

        [Fact]
        public void TestCellAtX217Y196WithSerial39()
        {
            var cell = new FuelCell(217, 196, 39);
            Assert.Equal(0, cell.PowerLevel);
        }

        [Fact]
        public void TestCellAtX101Y153WithSerial71()
        {
            var cell = new FuelCell(101, 153, 71);
            Assert.Equal(4, cell.PowerLevel);
        }

    }
}
