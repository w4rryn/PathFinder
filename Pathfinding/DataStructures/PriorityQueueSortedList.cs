using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.DataStructures
{
    public class PriorityQueueSortedList<T> : IPriorityQueue<T>
    {
        private struct Cell
        {
            public readonly T Payload;
            public readonly int Priority;

            public Cell(T payload, int priority)
            {
                Payload = payload;
                Priority = priority;
            }
        }

        private List<Cell> queue = new List<Cell>();
        public bool IsEmpty => queue.Count == 0;

        public T Dequeue()
        {
            var item = queue.First();
            queue.Remove(item);
            return item.Payload;
        }

        public void Enqueue(T item, int priority)
        {
            queue.Add(new Cell(item, priority));
            queue = queue.OrderBy(x => x.Priority).ToList();
        }

        public bool Contains(T item)
        {
            return queue.Any(x => x.Payload.Equals(item));
        }
    }
}
