using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public interface IWeightedGraph
    {
        int GetCost(Vertex2D from, Node to);
        List<Node> GetNeighbours(Vertex2D vertex);
        void Add(Vertex2D vertex, Node node);
    }
}
