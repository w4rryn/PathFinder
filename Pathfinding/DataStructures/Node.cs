using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public class Node
    {
        public Node(Vertex2D position, int cost)
        {
            Position = position;
            Cost = cost;
        }

        public Vertex2D Position { get; }
        public int Cost { get; }
        public int GScore { get; internal set; } = int.MaxValue;
        public int FScore { get; internal set; } = 0;
        public Node Parent { get; internal set; }
    }
}