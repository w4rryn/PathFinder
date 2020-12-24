using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public interface IWeightedGraph
    {
        int GetCost(Vertex2D from, Node to);
        List<Node> GetNeighbours(Vertex2D node);
        void Add(Vertex2D node, Node neighbour);
    }
}
