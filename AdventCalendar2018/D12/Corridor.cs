using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class Corridor
    {
        public LinkedList<Pot> Pots { get; set; } = new LinkedList<Pot>();

        private Generation Current { get; set; } = null;

        public void AddPot(Pot pot)
        {
            Pots.AddLast(pot);
        }

        internal Generation SpawnGeneration(IList<SpawnRule> rules)
        {
            LinkedList<Pot> pots;
            if (Current == null)
            {
                pots = Pots;
            }
            else
            {
                pots = Current.Pots;
            }

            AddLeft(pots);
            AddRight(pots);

            LinkedList<Pot> next = new LinkedList<Pot>();

            LinkedListNode<Pot> pot = pots.First;

            while (pot != null)
            {
                var sL = pot?.Previous?.Previous?.Value;
                var fL = pot?.Previous?.Value;
                var t = pot.Value;
                var fR = pot?.Next?.Value;
                var sR = pot?.Next?.Next?.Value;

                PlantStatus result = PlantStatus.Nothing;

                foreach (SpawnRule rule in rules)
                {
                    var status = rule.Check(sL, fL, t, fR, sR);
                    if (status != PlantStatus.Nothing)
                    {
                        //System.Diagnostics.Debug.WriteLine($"{pot?.Previous?.Previous?.Value.ToString() ?? "."}{pot?.Previous?.Value.ToString() ?? "."}{pot.Value.ToString() ?? "."}{pot?.Next?.Value.ToString() ?? "."}{pot?.Next?.Next?.Value.ToString() ?? "."} : {rule} = {status}");
                        result = status;
                    }
                }

                var newPot = new Pot()
                {
                    Id = pot.Value.Id
                };

                if (result == PlantStatus.Nothing)
                {
                    //System.Diagnostics.Debug.WriteLine($"{pot?.Previous?.Previous?.Value.ToString() ?? "."}{pot?.Previous?.Value.ToString() ?? "."}{pot.Value.ToString() ?? "."}{pot?.Next?.Value.ToString() ?? "."}{pot?.Next?.Next?.Value.ToString() ?? "."} : No Match = {PlantStatus.Decay}");
                    newPot.HasPlant = false;
                }
                else
                {
                    newPot.HasPlant = result == PlantStatus.Alive;
                }

                next.AddLast(newPot);

                pot = pot.Next;
            }

            Current = new Generation()
            {
                Pots = next
            };

            return Current;
        }

        private void AddLeft(LinkedList<Pot> pots)
        {
            var firstPlant = pots.FirstOrDefault(x => x.HasPlant);
            var firstPot = pots.First.Value;
            var neededPots = (firstPlant.Id + firstPot.Id) + 2;
            var lastId = firstPot.Id - 1;

            while (neededPots > 0)
            {
                var newPot = new Pot
                {
                    Id = lastId--,
                    HasPlant = false
                };
                pots.AddFirst(newPot);
                neededPots--;
            }
        }

        private void AddRight(LinkedList<Pot> pots)
        {
            var lastPlant = pots.LastOrDefault(x => x.HasPlant);
            var lastPot = pots.Last.Value;
            var neededPots = (lastPlant.Id - lastPot.Id) + 2;

            while (neededPots > 0)
            {
                var newPot = new Pot
                {
                    Id = pots.Last.Value.Id + 1,
                    HasPlant = false
                };
                pots.AddLast(newPot);
                neededPots--;
            }
        }
    }

}
