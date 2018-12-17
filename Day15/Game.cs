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
            var entities = Entities.Where(e => e.Health > 0).OrderBy(e => e.X + (e.Y * Map.Width)).ToList();

            foreach (var entity in entities)
            {
                var point = FindPath(entity, entities);
                if (point != null)
                {
                    entity.X = point.X;
                    entity.Y = point.Y;
                }
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

        private Point FindPath(Entity entity, IList<Entity> entities)
        {
            int x = entity.X;
            int y = entity.Y;

            return Path.FindTargetPoint(Map, entities, entity);
        }
    }
}
