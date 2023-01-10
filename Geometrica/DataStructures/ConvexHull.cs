using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class ConvexHull
{
    private readonly List<Point2> points = new();
    public readonly List<int> Hull = new();

    public ConvexHull(List<Point2> pts)
    {
        points = pts;
        Hull = CreateConvexHull(pts);
    }

    public List<int> CreateConvexHull(List<Point2> pts)
    {
        return null;
    }

    public void Add(Point2 point)
    {
        points.Add(point);
        UpdateConvexHull();
    }

    private void UpdateConvexHull()
    {
        if (points.Count < 3) 
        { 
            Hull.Clear();
            return;
        }
        else if (points.Count == 3)
        {
            Hull.AddRange(new List<int>(){ 0, 1, 2});
        }
        else
        {
            if (!points.Last().Inside(GetHullPoints()))
            {
                //reconstruct the hull
            }
        }
    }

    private List<Point2> GetHullPoints()
    {
        var hullPoints = new List<Point2>();
        foreach(var i in Hull)
        {
            hullPoints.Add(points[i]);
        }

        return hullPoints;
    }

    public override string ToString()
    {
        return string.Join(" ", GetHullPoints());
    }
}
