using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.DataStructures
{
    public class AdjacencyList<T> : IWeightedGraph<T>
    {
        private readonly Dictionary<T, List<Node<T>>> graph = new Dictionary<T, List<Node<T>>>();
        
        public List<Node<T>> GetNeighbours(T node)
        {
            return graph[node];
        }

        public int GetCost(T from, Node<T> to)
        {
            var connectedNodes = GetNeighbours(from);
            return connectedNodes.Single(x => x == to).Cost;
        }

        public void Add(T node, Node<T> neighbour)
        {
            if (graph.ContainsKey(node))
                graph[node].Add(neighbour);
            else
                graph.Add(node, new List<Node<T>>() { neighbour });
        }
    }
}
