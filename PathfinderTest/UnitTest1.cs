using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pathfinding.DataStructures;

namespace PathfinderTest
{
    public class Node : IPriority
    {

        int payload;

        public Node(int payload, int priority)
        {
            this.payload = payload;
            Priority = priority;
        }

        public int Priority { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPrioQueue()
        {
            var p = new PriorityQueue<Node>();
            var lowest = new Node(1, 10);
            var highest = new Node(2, 2);
            var middle = new Node(3, 5);
            p.Push(lowest);
            p.Push(highest);
            p.Push(middle);
            Assert.AreEqual(p.Pop().Priority, highest.Priority);
        }

        [TestMethod]
        public void TestPrioQueueMax()
        {
            var p = new PriorityQueue<Node>();
            var lowest = new Node(1, 10);
            var highest = new Node(2, 2);
            var middle = new Node(3, 5);
            p.Push(lowest);
            p.Push(highest);
            p.Push(middle);
            Assert.AreEqual(p.FindMax().Priority, highest.Priority);
        }

        [TestMethod]
        public void TestPrioQueueMin()
        {
            var p = new PriorityQueue<Node>();
            var lowest = new Node(1, 10);
            var highest = new Node(2, 2);
            var middle = new Node(3, 5);
            p.Push(lowest);
            p.Push(highest);
            p.Push(middle);
            Assert.AreEqual(p.FindMin().Priority, lowest.Priority);
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            var p = new PriorityQueue<Node>();
            Assert.IsTrue(p.IsEmpty);
        }

        [TestMethod]
        public void TestIsNotEmpty()
        {
            var p = new PriorityQueue<Node>();
            var lowest = new Node(1, 10);
            var highest = new Node(2, 2);
            var middle = new Node(3, 5);
            p.Push(lowest);
            p.Push(highest);
            p.Push(middle);
            Assert.IsFalse(p.IsEmpty);
        }

        [TestMethod]
        public void TestIsPopremove()
        {
            var p = new PriorityQueue<Node>();
            var lowest = new Node(1, 10);
            var highest = new Node(2, 2);
            var middle = new Node(3, 5);
            p.Push(lowest);
            p.Push(highest);
            p.Push(middle);
            p.Pop();
            Assert.IsTrue(p.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestFindMaxEmpty()
        {
            var p = new PriorityQueue<Node>();
            p.FindMax();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestFindMinEmpty()
        {
            var p = new PriorityQueue<Node>();
            p.FindMin();
        }
    }
}
