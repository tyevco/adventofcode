using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D06
{
    public class Diagram
    {
        public IDictionary<string, MassBody> Chart { get; } = new Dictionary<string, MassBody>();

        public MassBody this[string c]
        {
            get
            {
                if (Chart.ContainsKey(c))
                {
                    return Chart[c];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Chart[c] = value;
            }
        }

        public int OrbitalTransfersBetween(string obj, string target)
        {
            var objBody = Chart[obj];
            var targetBody = Chart[target];

            var objToCom = objBody.ToString().Split(" > ");
            var targetToCom = targetBody.ToString().Split(" > ");

            var firstMatching = objToCom.FirstOrDefault(x => targetToCom.Any(t => t == x));
            var firstMatchingBody = Chart[firstMatching];

            return objBody.IndirectOrbits + targetBody.IndirectOrbits - (2 * (firstMatchingBody.IndirectOrbits + 1));
        }
    }
}
