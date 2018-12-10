using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day8
{
    public class ManeuverTreeParser
    {
        public static ManeuverNode Parse(string data)
        {
            var dataQueue = new Queue<int>(data.Split(' ').Select(x => int.Parse(x)).ToList());

            ManeuverNode parentNode = new ManeuverNode();

            if (dataQueue.Count > 0)
            {
                createChildren(parentNode, dataQueue);
            }

            return parentNode;
        }

        private static ManeuverNode createChildren(ManeuverNode node, Queue<int> dataQueue)
        {
            int numOfChildren = dataQueue.Dequeue();
            int numOfMeta = dataQueue.Dequeue();

            for (int c = 0; c < numOfChildren; c++)
            {
                ManeuverNode child = new ManeuverNode();
                createChildren(child, dataQueue);
                node.Children.Add(child);
            }

            for (int m = 0; m < numOfMeta; m++)
            {
                int metaValue = dataQueue.Dequeue();
                node.Metadata.Add(metaValue);
            }

            return node;
        }
    }
}
