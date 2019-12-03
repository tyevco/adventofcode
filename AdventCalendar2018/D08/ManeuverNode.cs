using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2018.D08
{
    public class ManeuverNode
    {
        public IList<ManeuverNode> Children { get; set; } = new List<ManeuverNode>();
        public IList<int> Metadata { get; set; } = new List<int>();

        public int GetMetadataValue()
        {
            return Metadata.Sum();
        }


        public int GetTotalMetadataValue()
        {
            int sum = GetMetadataValue();
            foreach (var child in Children)
            {
                sum += child.GetTotalMetadataValue();
            }

            return sum;
        }

        public int GetIndexValue()
        {
            if (Children.Count == 0)
            {
                return GetMetadataValue();
            }
            else
            {
                int sum = 0;
                foreach (var index in Metadata)
                {
                    if (index - 1 < Children.Count)
                    {
                        sum += Children[index - 1].GetIndexValue();
                    }
                }

                return sum;
            }
        }
    }
}
