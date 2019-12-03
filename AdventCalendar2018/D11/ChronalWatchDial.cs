using System;

namespace LongRunning.Day11
{
    public class ChronalWatchDial
    {
        static void Main(string[] args)
        {
            var t = new Tests.Day11.FuelCellTests();

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
