using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCalendar.Day07
{
    public class StepTree
    {
        public IList<StepNode> Children { get; set; }

        public string GetOrder()
        {
            StringBuilder order = new StringBuilder();

            Queue<StepNode> validNodes = GetNodes(this);
            while (validNodes == null || validNodes.Count > 0)
            {
                if (validNodes.Count > 0)
                {
                    var node = validNodes.Dequeue();
                    node.Complete = true;
                    order.Append(node.Id);
                }

                validNodes = GetNodes(this);
            }


            return order.ToString();
        }

        public void Reset()
        {
            foreach (var child in Children)
            {
                ResetNode(child);
            }
        }

        private void ResetNode(StepNode node)
        {
            node.Complete = false;
            node.Constructing = false;

            if (node.Children != null && node.Children.Any())
            foreach (var child in node.Children)
            {
                ResetNode(child);
            }
        }

        private static Queue<StepNode> GetNodes(StepTree tree)
        {
            IList<StepNode> validNodes = new List<StepNode>();
            foreach (var node in tree.Children)
            {
                GetAvailableNodes(node, validNodes);
            }

            return new Queue<StepNode>(validNodes.OrderBy(x => x.Id));
        }

        private static void GetAvailableNodes(StepNode node, IList<StepNode> validNodes)
        {
            if (!node.Complete && !node.Constructing)
            {
                if (!validNodes.Contains(node))
                    validNodes.Add(node);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    if (child.Complete)
                    {
                        GetAvailableNodes(child, validNodes);
                    }
                    else
                    {
                        if (child.Parents.All(x => x.Complete))
                        {
                            if (!validNodes.Contains(child) && !child.Constructing)
                                validNodes.Add(child);
                        }
                    }
                }
            }
        }

        public Build Construct(int numOfWorkers, int baseTimeForEachStep)
        {
            IList<Worker> workers = new List<Worker>();
            for (int i = 0; i < numOfWorkers; i++)
            {
                workers.Add(new Worker());
            }

            StringBuilder order = new StringBuilder();

            Build build = new Build();
            int time = 0;
            var availableNodes = GetNodes(this);

            var visualizer = new TreeVisualizer(workers.Count);

            while (workers.Any(x => x.IsWorking) || availableNodes.Count > 0)
            {
                foreach (var worker in workers)
                {
                    worker.Tick();
                }

                if (workers.Any(x => !x.IsWorking))
                {
                    var freeWorkers = workers.Where(x => !x.IsWorking);

                    foreach (var free in freeWorkers)
                    {
                        if (free.Activity != null)
                        {
                            var activity = free.Activity;
                            order.Append(activity.Id);
                            activity.Complete = true;

                            free.ClearActivity();
                        }
                    }

                    foreach (var free in freeWorkers)
                    {
                        availableNodes = GetNodes(this);

                        if (availableNodes.Count > 0)
                        {
                            free.SetActivity(availableNodes.Dequeue(), baseTimeForEachStep);
                        }
                    }
                }

                visualizer.AddTick(time, workers, order.ToString());

                if (workers.Any(x => x.IsWorking))
                {
                    time++;
                    availableNodes = GetNodes(this);
                }

            }

            var orderOutput = order.ToString();
            visualizer.Print();

            build.Order = orderOutput;
            build.Elapsed = time;

            return build;
        }
    }
}
