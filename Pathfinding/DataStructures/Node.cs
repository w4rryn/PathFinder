namespace Pathfinding.DataStructures
{
    public class Node
    {
        public Node(Vertex2D vertex, int cost)
        {
            Vertex = vertex;
            Cost = cost;
        }

        public Vertex2D Vertex { get; }
        public int Cost { get; }
    }
}