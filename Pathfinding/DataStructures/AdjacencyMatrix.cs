using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.DataStructures
{
    public class AdjacencyMatrix : IWeightedGraph
    {
        private readonly Dictionary<Vertex2D, List<Node>> graph = new Dictionary<Vertex2D, List<Node>>();
        
        public List<Node> GetNeighbours(Vertex2D node)
        {
            return graph[node];
        }

        public int GetCost(Vertex2D from, Node to)
        {
            var connectedNodes = GetNeighbours(from);
            return connectedNodes.Single(x => x == to).Cost;
        }

        public void Add(Vertex2D node, Node neighbour)
        {
            if (!graph.ContainsKey(node))
            {
                graph.Add(node, new List<Node>() { neighbour });
                return;
            }
            else
            {
                graph[node].Add(neighbour);
            }
        }
    }
}
