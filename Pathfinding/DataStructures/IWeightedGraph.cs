using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
    public interface IWeightedGraph<T>
    {
        int GetCost(T from, Node<T> to);
        List<Node<T>> GetNeighbours(T node);
        void Add(T node, Node<T> neighbour);
    }
}
