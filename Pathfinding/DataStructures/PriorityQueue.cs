using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.DataStructures
{
    public interface IPriority
    {
        int Priority { get; }
    }

    public class PriorityQueue<T> where T : IPriority
    {
        private readonly List<T> queue = new List<T>();

        public bool IsEmpty => queue.Count == 0;

        public T Pop()
        {
            var highest = queue.First();
            queue.Remove(highest);
            return highest;
        }

        public void Push(T node)
        {
            int index = 0;
            foreach (var element in queue)
            {
                if (node.Priority < element.Priority)
                {
                    queue.Insert(index, node);
                }
            }
        }

        public T FindMax()
        {
            return queue.First();
        }

        public T FindMin()
        {
            return queue.Last();
        }
    }
}
