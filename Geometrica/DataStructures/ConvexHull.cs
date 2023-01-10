using Geometrica.Primitives;

namespace Geometrica.DataStructures;

public class ConvexHull
{
    private readonly List<Point2> points = new();
    public readonly List<Point2> Hull = new();

    public ConvexHull()
    {
    }

    public ConvexHull(List<Point2> pts)
    {
        points = pts;
        Hull = CreateConvexHull(pts);
    }

    public List<Point2> CreateConvexHull(List<Point2> pts)
    {
        //divide and conquer
        if(pts.Count() > 6)
        {
            var half = pts.Count / 2;
            return JoinHulls(CreateConvexHull(pts.GetRange(0, half)), CreateConvexHull(pts.GetRange(half, pts.Count() - half)));
        }
        else
        {
            return CreateSimpleHull(pts);
        }
    }

    public List<Point2> CreateSimpleHull(List<Point2> pts)
    {
        if(pts == null)
        {
            throw new ArgumentNullException($"The argument {nameof(pts)} must not be null. Please provide a list points.");
        }
        if(3 > pts.Count() || pts.Count > 6)
        {
            throw new ArgumentException($"This method must only be used for 3 to 5 points. You provided a list of {pts.Count()} points.");
        }
        
        if(pts.Count() == 3)
        {
            if (Point2.Orientation(pts[0], pts[1], pts[2]) < 0)
            {
                return new List<Point2>() { pts[0], pts[1], pts[2] };
            }
            else
            {
                return new List<Point2>() { pts[0], pts[2], pts[1] };
            }
        }

        return null;
    }

    public List<Point2> JoinHulls(List<Point2> list1, List<Point2> list2)
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
            Hull.AddRange(points);
        }
        else
        {
            if (!points.Last().Inside(Hull))
            {
                //reconstruct the hull
            }
        }
    }

    public override string ToString()
    {
        return string.Join(" ", Hull);
    }
}
