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

        public List<Vertex2D> GetPath(Vertex2D start, Vertex2D goal)
        {
            var openSet = new PriorityQueueSortedList<Vertex2D>();
            openSet.Enqueue(start, 0);
            var cameFrom = new Dictionary<Vertex2D, Vertex2D>();
            var gScore = new Dictionary<Vertex2D, int>();
            gScore.Add(start, 0);
            var fScore = new Dictionary<Vertex2D, int>();
            fScore.Add(start, 0);
            while (!openSet.IsEmpty)
            {
                var current = openSet.Dequeue();

                if (current.Equals(goal))
                {
                    return ReconstructPath(cameFrom, current);
                }

                foreach (var neighbour in weightedGraph.GetNeighbours(current))
                {
                    var tentativeGScore = gScore[current] + weightedGraph.GetCost(current, neighbour);
                    if (!gScore.ContainsKey(neighbour.Vertex))
                    {
                        gScore.Add(neighbour.Vertex, int.MaxValue);
                    }
                    if (tentativeGScore < gScore[neighbour.Vertex])
                    {
                        cameFrom[neighbour.Vertex] = current;
                        gScore[neighbour.Vertex] = tentativeGScore;
                        fScore[neighbour.Vertex] = gScore[neighbour.Vertex] + costHeuristic(current, neighbour.Vertex);
                        if (!openSet.Contains(neighbour.Vertex))
                        {
                            openSet.Enqueue(neighbour.Vertex, fScore[neighbour.Vertex]);
                        }
                    }
                }
            }

            throw new NoPathFoundException();
        }

        private List<Vertex2D> ReconstructPath(Dictionary<Vertex2D, Vertex2D> cameFrom, Vertex2D current)
        {
            var totalPath = new List<Vertex2D>();
            totalPath.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
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
