using System.Collections.Generic;

namespace AdventCalendar2018.D24
{
    public class Squad
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public int Units { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public DamageType AttackType { get; set; }
        public int Initiative { get; set; }
        public int EffectivePower => AttackPower * Units;

        public ISet<DamageType> Weaknesses { get; } = new HashSet<DamageType>();
        public ISet<DamageType> Immunities { get; } = new HashSet<DamageType>();

        public void AddImmunity(DamageType type)
        {
            Immunities.Add(type);
        }

        public void AddWeakness(DamageType type)
        {
            Weaknesses.Add(type);
        }

        public override string ToString()
        {
            return $"Group {Id} contains {Units} units";
        }
    }
}