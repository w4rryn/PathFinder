using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public class Node <T>
    {
        public Node(T position, int cost)
        {
            Position = position;
            Cost = cost;
        }

        public T Position { get; }
        public int Cost { get; }
        public int PathCostFromStart { get; internal set; } = int.MaxValue;
        public int PathCost { get; internal set; } = 0;
        public Node<T> Parent { get; internal set; }
    }
}