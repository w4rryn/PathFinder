using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Algorithms
{
    public class Astar : IPathfinder
    {
        private readonly HeuristicCalculator costHeuristic;
        private readonly IWeightedGraph weightedGraph;

        public delegate int HeuristicCalculator(Vertex2D currentLocation, Vertex2D goalLocation);

        public Astar(IWeightedGraph graph, HeuristicCalculator heuristic)
        {
            weightedGraph = graph;
            costHeuristic = heuristic;
        }

        public List<Vertex2D> GetPath(Vertex2D startv, Vertex2D goalv)
        {
            var openSet = new PriorityQueueSortedList<Node>();
            var start = new Node(startv, 0);
            openSet.Enqueue(start, 0);
            start.GScore = 0;
            start.FScore = 0;
            while (!openSet.IsEmpty)
            {
                var current = openSet.Dequeue();

                if (current.Position.Equals(goalv))
                {
                    return ReconstructPath(current);
                }

                foreach (var neighbour in weightedGraph.GetNeighbours(current.Position))
                {
                    var tentativeGScore = current.GScore + neighbour.Cost;
                    if (tentativeGScore < neighbour.GScore)
                    {
                        neighbour.Parent = current;
                        neighbour.GScore = tentativeGScore;
                        neighbour.FScore = neighbour.GScore + costHeuristic(current.Position, neighbour.Position);
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Enqueue(neighbour, neighbour.FScore);
                        }
                    }
                }
            }

            throw new NoPathFoundException();
        }

        private List<Vertex2D> ReconstructPath(Node current)
        {
            var totalPath = new List<Vertex2D>
            {
                current.Position
            };
            while (current.Parent != null)
            {
                totalPath.Add(current.Parent.Position);
                current = current.Parent;
            }
            return totalPath;
        }
    }

    [Serializable]
    public class NoPathFoundException : Exception
    {
        public NoPathFoundException()
        {
        }

        public NoPathFoundException(string message) : base(message)
        {
        }

        public NoPathFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPathFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
