using System.Collections.Generic;

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
    }
}
