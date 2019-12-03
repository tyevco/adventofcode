using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2018.D25
{
    public class Constellation
    {
        public ISet<Star> Stars { get; set; } = new HashSet<Star>();
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            bool same = false;
            if (obj.GetType().Equals(GetType()))
            {
                var other = obj as Constellation;
                if (Stars.Count == other.Stars.Count)
                {
                    var starList = Stars.ToList();
                    var otherList = other.Stars.ToList();
                    same = true;
                    for (int i = 0; i < starList.Count; i++)
                    {
                        if (!starList[i].Equals(otherList[i]))
                        {
                            same = false;
                            break;
                        }
                    }
                }
            }

            return same;
        }
    }
}
