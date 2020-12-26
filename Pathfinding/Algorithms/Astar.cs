using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pathfinding.Algorithms
{
    public class Astar<T> : IPathfinder<T>
    {
        private readonly HeuristicCalculator costHeuristic;
        private readonly PriorityQueueSortedList<Node<T>> openSet = new PriorityQueueSortedList<Node<T>>();
        private readonly IWeightedGraph<T> weightedGraph;

        public Astar(IWeightedGraph<T> graph, HeuristicCalculator heuristic)
        {
            weightedGraph = graph;
            costHeuristic = heuristic;
        }

        public delegate int HeuristicCalculator(Node<T> currentLocation, Node<T> goalLocation);

        public List<T> GetPath(Node<T> start, T goal)
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

        private bool IsCurrentNodeAtGoalPosition(T goalv, Node<T> current)
        {
            return current.Position.Equals(goalv);
        }

        private bool IsCurrentNodeParentNotNull(Node<T> current)
        {
            return current.Parent != null;
        }

        private bool IsNeighbourWithLowestCost(Node<T> neighbour, int stepCost)
        {
            return stepCost < neighbour.PathCostFromStart;
        }

        private void EvaluateNeighbours(Node<T> current)
        {
            foreach (var neighbour in weightedGraph.GetNeighbours(current.Position))
                FindNeighbourWithLowestCost(current, neighbour);
        }

        private void FindNeighbourWithLowestCost(Node<T> current, Node<T> neighbour)
        {
            var stepCost = current.PathCostFromStart + neighbour.Cost;
            if (IsNeighbourWithLowestCost(neighbour, stepCost))
            {
                SetNewNeighbourValues(current, neighbour, stepCost);
                AddNeighbourToOpenSet(neighbour);
            }
        }

        private void PrepareStartNodeAndAddToQueue(Node<T> start)
        {
            start.PathCostFromStart = 0;
            start.PathCost = 0;
            openSet.Enqueue(start, 0);
        }

        private List<T> ReconstructPathWithNodeParents(Node<T> current)
        {
            var totalPath = new List<T> { current.Position };
            while (IsCurrentNodeParentNotNull(current))
            {
                totalPath.Add(current.Parent.Position);
                current = current.Parent;
            }
            return totalPath;
        }

        private void SetNewNeighbourValues(Node<T> current, Node<T> neighbour, int tentativeGScore)
        {
            neighbour.Parent = current;
            neighbour.PathCostFromStart = tentativeGScore;
            neighbour.PathCost = neighbour.PathCostFromStart + costHeuristic(current, neighbour);
        }

        private void AddNeighbourToOpenSet(Node<T> neighbour)
        {
            if (!openSet.Contains(neighbour))
                openSet.Enqueue(neighbour, neighbour.PathCost);
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
