using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public interface IWeightedGraph<T>
    {
        int GetCost(T from, Node<T> to);
        List<Node<T>> GetNeighbours(T node);
        void AddNeighbour(T node, Node<T> neighbour);
        void AddNode(T node);
    }
}
