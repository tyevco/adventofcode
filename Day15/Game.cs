using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    public class Game
    {
        public int Round { get; set; } = 0;
        public Map Map { get; }

        public IList<Entity> Entities { get; }

        public bool Finished { get; private set; }

        public Game(Map map)
        {
            Map = map;
            Entities = map.Entities;
        }


        public void Tick()
        {
            var entities = Entities.OrderBy(e => e.X + (e.Y * Map.Width)).ToList();

            foreach (var entity in entities)
            {
                FindPath(entity, entities);
            }

            CheckIfOver();
        }

        private void CheckIfOver()
        {
            if (Entities.Count(e => e.Type == EntityType.Elf && e.Health > 0) == 0
                || Entities.Count(e => e.Type == EntityType.Goblin && e.Health > 0) == 0)
            {
                Finished = true;
            }
        }

        private void FindPath(Entity entity, IList<Entity> entities)
        {
            int x = entity.X;
            int y = entity.Y;

            var targetGrid = Path.FindTarget(Map, entities, entity);

            Console.WriteLine(targetGrid);

        }
    }
}
