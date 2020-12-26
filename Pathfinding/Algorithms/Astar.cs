using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pathfinding.Algorithms
{
    public class Astar : IPathfinder
    {
        private readonly HeuristicCalculator costHeuristic;
        private readonly PriorityQueueSortedList<Node> openSet = new PriorityQueueSortedList<Node>();
        private readonly IWeightedGraph weightedGraph;

        public Astar(IWeightedGraph graph, HeuristicCalculator heuristic)
        {
            weightedGraph = graph;
            costHeuristic = heuristic;
        }

        public delegate int HeuristicCalculator(Node currentLocation, Node goalLocation);

        public List<Vertex2D> GetPath(Node start, Vertex2D goal)
        {
            PrepareStartNodeAndAddToQueue(start);
            while (!openSet.IsEmpty)
            {
                var currentNode = openSet.Dequeue();
                if (IsCurrentNodeAtGoalPosition(goal, currentNode))
                    return ReconstructPathWithNodeParents(currentNode);
                EvaluateNeighbours(currentNode);
            }

            throw new NoPathFoundException(); // throw exception when no path was found
        }

        private bool IsCurrentNodeAtGoalPosition(Vertex2D goalv, Node current)
        {
            return current.Position.Equals(goalv);
        }

        private bool IsCurrentNodeParentNotNull(Node current)
        {
            return current.Parent != null;
        }

        private bool IsNeighbourWithLowestCost(Node neighbour, int stepCost)
        {
            return stepCost < neighbour.GScore;
        }

        private void EvaluateNeighbours(Node current)
        {
            foreach (var neighbour in weightedGraph.GetNeighbours(current.Position))
                FindNeighbourWithLowestCost(current, neighbour);
        }

        private void FindNeighbourWithLowestCost(Node current, Node neighbour)
        {
            var stepCost = current.GScore + neighbour.Cost;
            if (IsNeighbourWithLowestCost(neighbour, stepCost))
            {
                SetNewNeighbourValues(current, neighbour, stepCost);
                AddNeighbourToOpenSet(neighbour);
            }
        }

        private void PrepareStartNodeAndAddToQueue(Node start)
        {
            start.GScore = 0;
            start.FScore = 0;
            openSet.Enqueue(start, 0);
        }

        private List<Vertex2D> ReconstructPathWithNodeParents(Node current)
        {
            var totalPath = new List<Vertex2D> { current.Position };
            while (IsCurrentNodeParentNotNull(current))
            {
                totalPath.Add(current.Parent.Position);
                current = current.Parent;
            }
            return totalPath;
        }

        private void SetNewNeighbourValues(Node current, Node neighbour, int tentativeGScore)
        {
            neighbour.Parent = current;
            neighbour.GScore = tentativeGScore;
            neighbour.FScore = neighbour.GScore + costHeuristic(current, neighbour);
        }

        private void AddNeighbourToOpenSet(Node neighbour)
        {
            if (!openSet.Contains(neighbour))
                openSet.Enqueue(neighbour, neighbour.FScore);
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
