using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;

namespace Pathfinding.Algorithms
{
    public class Astar : IPathfinder
    {
        private readonly HeuristicCalculator costHeuristic;
        private readonly IWeightedGraph weightedGraph;
        public Astar(IWeightedGraph graph, HeuristicCalculator heuristic)
        {
            weightedGraph = graph;
            costHeuristic = heuristic;
        }

        public delegate int HeuristicCalculator(Vertex2D currentLocation, Vertex2D goalLocation);

        public List<Vertex2D> GetPath(Vertex2D start, Vertex2D goal)
        {
            var openSet = new PriorityQueueSortedList<Vertex2D>();
            openSet.Enqueue(start, 0);
            var cameFrom = new Dictionary<Vertex2D, Vertex2D>();
            var costSoFar = new Dictionary<Vertex2D, int>();

            cameFrom[start] = null;
            costSoFar[start] = 0;

            while (!openSet.IsEmpty)
            {
                var current = openSet.Dequeue();
                if (current == goal)
                {
                    break;
                }
                foreach (var next in weightedGraph.GetNeighbours(current))
                {
                    int newCost = costSoFar[current] + weightedGraph.GetCost(current, next);

                    if (!costSoFar.ContainsKey(next.Vertex) || newCost < costSoFar[next.Vertex])
                    {
                        costSoFar[next.Vertex] = newCost;
                        var priority = newCost + costHeuristic(next.Vertex, goal);
                        openSet.Enqueue(next.Vertex, priority);
                        cameFrom[next.Vertex] = current;
                    }
                }
            }
            return ReconstructPath(cameFrom, goal);
        }

        private List<Vertex2D> ReconstructPath(Dictionary<Vertex2D, Vertex2D> cameFrom, Vertex2D goal)
        {
            var totalPath = new List<Vertex2D>() { goal };
            var parent = cameFrom[goal];
            while (parent != null)
            {
                totalPath.Add(parent);
                parent = cameFrom[parent];
            }
            return totalPath;
        }
    }
}
