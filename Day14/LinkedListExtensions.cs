using System.Collections.Generic;

namespace Day14
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> Skip<T>(this LinkedList<T> list, int skip)
        {
            var node = list.First;
            for (int i = 0; i < skip; i++)
            {
                node = node.Next;
            }

            return node;
        }

        public static LinkedListNode<T> Rewind<T>(this LinkedListNode<T> node, int skip)
        {
            for (int i = 0; i < skip; i++)
            {
                node = node.PreviousOrFirst();
            }

            return node;
        }

        public static IList<T> Take<T>(this LinkedListNode<T> node, int takeCount)
        {
            IList<T> values = new List<T>();
            values.Add(node.Value);

            var retNode = node;
            while (values.Count < takeCount)
            {
                retNode = retNode.Next;
                values.Add(retNode.Value);
            }

            return values;
        }


        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> node)
        {
            if (node.Next == null)
            {
                return node.List.First;
            }
            else
            {
                return node.Next;
            }
        }

        public static LinkedListNode<T> PreviousOrFirst<T>(this LinkedListNode<T> node)
        {
            if (node.Previous == null)
            {
                return node.List.Last;
            }
            else
            {
                return node.Previous;
            }
        }
    }
}
