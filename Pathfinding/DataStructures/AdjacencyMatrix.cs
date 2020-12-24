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
        
        public List<Node> GetNeighbours(Vertex2D vertex)
        {
            return graph[vertex];
        }

        public int GetCost(Vertex2D from, Node to)
        {
            var connectedNodes = GetNeighbours(from);
            return connectedNodes.Single(x => x == to).Cost;
        }

        public void Add(Vertex2D vertex, Node node)
        {
            if (!graph.ContainsKey(vertex))
            {
                graph.Add(vertex, new List<Node>() { node });
                return;
            }
            else
            {
                graph[vertex].Add(node);
            }
        }
    }
}
