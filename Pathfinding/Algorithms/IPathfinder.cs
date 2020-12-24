using Pathfinding.DataStructures;
using System.Collections.Generic;

namespace Pathfinding.Algorithms
{
    public interface IPathfinder
    {
        List<Vertex2D> GetPath(Node start, Vertex2D goal);
    }
}