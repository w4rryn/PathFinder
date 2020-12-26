using Pathfinding.DataStructures;
using System.Collections.Generic;

namespace Pathfinding.Algorithms
{
    public interface IPathfinder<T>
    {
        List<T> GetPath(Node<T> start, T goal);
    }
}