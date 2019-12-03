using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    public class StepNode
    {
        public char Id;

        public bool Complete { get; set; }

        public bool Constructing { get; set; }

        public int ConstructionTime
        {
            get
            {
                return Id % 32;
            }
        }

        public IList<StepNode> Parents { get; set; } = new List<StepNode>();

        public IList<StepNode> Children { get; set; } = new List<StepNode>();

        public string ToString()
        {
            return $"{Id}>{string.Join(",", Children.Select(x => x.Id))}:{string.Join(",", Parents.Select(x => x.Id))}";
        }
    }
}
