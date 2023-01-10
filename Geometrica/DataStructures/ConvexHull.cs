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
        //divide and conquer
        if(6 < pts.Count())
        {
            var half = pts.Count / 2;
            return JoinHulls(CreateConvexHull(pts.GetRange(0, half)), CreateConvexHull(pts.GetRange(half, pts.Count() - half)));
        }
        else
        {
            return CreateSimpleHull(pts);
        }
    }

    public List<int> CreateSimpleHull(List<Point2> pts)
    {
        throw new NotImplementedException();
    }

    public List<int> JoinHulls(List<int> list1, List<int> list2)
    {
        throw new NotImplementedException();
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
