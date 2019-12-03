using System.Collections.Generic;
using System.Text;

namespace AdventCalendar2018.D12
{
    public class Generation
    {
        private static int count;

        public int Id { get; set; }
        public LinkedList<Pot> Pots { get; set; }
        public int FirstSpot
        {
            get
            {
                return Pots.First.Value.Id;
            }
        }
        public int LastSpot
        {
            get
            {
                return Pots.Last.Value.Id;
            }
        }

        public Generation()
        {
            Id = count++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pot in Pots)
            {
                sb.Append(pot);
            }

            return sb.ToString();
        }
    }
}