namespace Pathfinding.DataStructures
{
    public class Vertex2D
    {
        public int X { get; }
        public int Y { get; }

        public Vertex2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vertex2D Sum(Vertex2D other)
        {
            return new Vertex2D(X + other.X, Y + other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}