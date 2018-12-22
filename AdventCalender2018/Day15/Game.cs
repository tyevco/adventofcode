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
            var entities = Entities.Where(e => e.Health > 0).OrderBy(e => e.X + (e.Y * Map.Width));
            bool roundOverEarly = false;
            //var roundStart = DateTime.Now;

            foreach (var entity in entities)
            {
                if (entity.Health > 0)
                {
                    //var entityStart = DateTime.Now;

                    if (entities.Any(e => e.Type != entity.Type))
                    {
                        // there are enemies, so keep going
                        var nearby = NearbyEnemies(entity, entities.Where(e => e.Type != entity.Type));

                        if (nearby.Any())
                        {
                            var target = nearby.OrderBy(e => e.Health).ThenBy(e => e.X + e.Y * Map.Width).FirstOrDefault();
                            target.Health -= entity.Attack;

                            //if (target.Health > 0)
                            //    System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} attacks {target.Type} {target.Id} for {entity.Attack} damage, leaving {target.Health} health.");
                            //else
                            //    System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} kills {target.Type} {target.Id}.");
                        }
                        else
                        {
                            var point = FindPath(entity, entities);
                            if (point != null)
                            {
                                if (IsLocationValid(point.X, point.Y))
                                {
                                    entity.X = point.X;
                                    entity.Y = point.Y;

                                    //System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} moves to location ({point.X},{point.Y}).");
                                }
                            }

                            var moveNearby = NearbyEnemies(entity, entities.Where(e => e.Type != entity.Type));

                            if (moveNearby.Any())
                            {
                                var target = moveNearby.OrderBy(e => e.Health).ThenBy(e => e.X + e.Y * Map.Width).FirstOrDefault();
                                target.Health -= entity.Attack;

                                //if (target.Health > 0)
                                //    System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} attacks {target.Type} {target.Id} for {entity.Attack} damage, leaving {target.Health} health.");
                                //else
                                //    System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} kills {target.Type} {target.Id}.");
                            }
                        }
                    }
                    else
                    {
                        roundOverEarly = true;
                    }
                    //var entityEnd = DateTime.Now;

                    //System.Diagnostics.Debug.WriteLine($"{entity.Type} {entity.Id} complete, took {(entityEnd - entityStart).Milliseconds}ms.");
                }
                //System.Diagnostics.Debug.WriteLine(Map);
            }

            //            var roundEnd = DateTime.Now;

            //System.Diagnostics.Debug.WriteLine($"Round complete, took {(roundEnd - roundStart).Milliseconds}ms.");

            if (!roundOverEarly)
            {
                Round++;
            }

            CheckIfOver();
        }

        private IEnumerable<Entity> NearbyEnemies(Entity entity, IEnumerable<Entity> enemies)
        {
            return enemies.Where(p =>
                (p.X == entity.X - 1 && p.Y == entity.Y) ||
                (p.X == entity.X + 1 && p.Y == entity.Y) ||
                (p.X == entity.X && p.Y == entity.Y - 1) ||
                (p.X == entity.X && p.Y == entity.Y + 1)
                                    );
        }

        private bool IsLocationValid(int x, int y)
        {
            bool valid = true;
            var other = Entities.FirstOrDefault(e => e.X == x && e.Y == y && e.Health > 0);
            if (other == null)
            {
                var space = Map[x, y];
                if (space.Type == PlotType.Tree)
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        private void CheckIfOver()
        {
            if (Entities.Count(e => e.Type == EntityType.Elf && e.Health > 0) == 0
                || Entities.Count(e => e.Type == EntityType.Goblin && e.Health > 0) == 0)
            {
                Finished = true;
            }
        }

        private Point FindPath(Entity entity, IEnumerable<Entity> entities)
        {
            return Pathfinding.FindTargetPoint(Map, entities, entity);
        }
    }
}
