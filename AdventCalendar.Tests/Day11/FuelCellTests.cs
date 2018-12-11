using AdventCalendar.Day11;
using Xunit;

namespace AdventCalendar.Tests.Day11
{
    public class FuelCellTests
    {
        [Fact]
        public void TestCellAtX122Y79WithSerial57()
        {
            var cell = new FuelCell(122, 79, 57);
            Assert.Equal(-5, cell.PowerLevel);
        }

        [Fact]
        public void TestCellAtX217Y196WithSerial39()
        {
            var cell = new FuelCell(122, 79, 57);
            Assert.Equal(0, cell.PowerLevel);
        }

        [Fact]
        public void TestCellAtX1011Y153WithSerial71()
        {
            var cell = new FuelCell(122, 79, 57);
            Assert.Equal(4, cell.PowerLevel);
        }

    }
}
