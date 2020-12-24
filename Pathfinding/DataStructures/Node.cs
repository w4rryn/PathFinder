using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public class Node
    {
        public Node(Vertex2D vertex, int cost)
        {
            Position = vertex;
            Cost = cost;
        }

        public Vertex2D Position { get; }
        public int Cost { get; }
        public int GScore { get; set; } = int.MaxValue;
        public int fScore { get; set; } = 0;
        public Node Parent { get; set; }

    }
}