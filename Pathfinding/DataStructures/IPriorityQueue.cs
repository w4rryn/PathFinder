namespace Pathfinding.DataStructures
{
    internal interface IPriorityQueue<T>
    {
        void Enqueue(T item, int priority);
        T Dequeue();
        bool IsEmpty { get; }
        bool Contains(T item);
    }
}
