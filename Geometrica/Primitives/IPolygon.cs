namespace Geometrica.Primitives
{
    public interface IPolygon
    {
        public Point2 this[int key] { get; }
        int Count { get; }
        bool IsConvex { get; }
    }
}
