using System;

namespace AdventCalendar2018.D11
{
    public class ChronalWatchDial
    {
        public void Execute()
        {
            var t = new FuelCellTests();

            try
            {
                t.FindLargestSubSectionWithMostPowerWithSerial7989();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                t.FindLargestSubSectionWithMostPowerWithSerial18();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                t.FindLargestSubSectionWithMostPowerWithSerial42();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
