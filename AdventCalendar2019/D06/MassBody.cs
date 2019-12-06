using System.Collections.Generic;
using System.Diagnostics;

namespace AdventCalendar2019.D06
{
    [DebuggerDisplay("{Name}")]
    public class MassBody
    {
        public MassBody Orbitting { get; set; } = null;

        public IList<MassBody> Satellites { get; } = new List<MassBody>();

        public string Name { get; set; }

        public int DirectOrbits => Satellites.Count;

        public int IndirectOrbits => 1 + Orbitting?.IndirectOrbits ?? 0;

        public override string ToString()
        {
            return Name + (Orbitting != null ? $" > {Orbitting.ToString()}" : "");
        }
    }
}
