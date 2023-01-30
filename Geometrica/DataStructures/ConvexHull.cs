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
        if(pts != null && 6 < pts.Count())
        {
            var half = pts.Count / 2;
            return JoinHulls(CreateConvexHull(pts.GetRange(0, half)), CreateConvexHull(pts.GetRange(half, pts.Count() - half)));
        }
        else
        {
            return CreateSimpleHull(pts);
        }
    }

    public static List<Point2> CreateSimpleHull(List<Point2> pts)
    {
        if(pts == null)
        {
            throw new ArgumentNullException($"The argument {nameof(pts)} must not be null. Please provide a list points.");
        }
        if(pts.Count() < 3|| 6 < pts.Count)
        {
            throw new ArgumentException($"This method must only be used for 3 to 5 points. You provided a list of {pts.Count()} points.");
        }
        
        if(pts.Count() == 3)
        {
            if (Point2.Orientation(pts[0], pts[1], pts[2]) > 0)
            {
                return new List<Point2>() { pts[0], pts[1], pts[2] };
            }
            else
            {
                return new List<Point2>() { pts[0], pts[2], pts[1] };
            }
        }
        if(pts.Count == 4)
        {
            return ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        }    

        return null;
    }

    public static List<Point2> ConvexHull4(Point2 a, Point2 b, Point2 c, Point2 d)
    {
        //https://stackoverflow.com/questions/2122305/convex-hull-of-4-points
        var abc = Point2.OrientedCCW(a, b, c);
        var abd = Point2.OrientedCCW(a, b, d);
        var bcd = Point2.OrientedCCW(b, c, d);
        var cad = Point2.OrientedCCW(c, a, d);

        abd = abd == abc;
        bcd = bcd == abc;
        cad = cad == abc;

        if (abd && bcd && cad) return abc ? new List<Point2>() { a, b, c } : new List<Point2> { a, c, b };
        if (abd && bcd && !cad) return abc ? new List<Point2>() { a, b, c, d } : new List<Point2>() { a, d, c, b };
        if (abd && !bcd && cad) return abc ? new List<Point2>() { a, b, d, c } : new List<Point2>() { a, c, d, b};
        if (abd && !bcd && !cad) return abc ? new List<Point2>() { a, b, d } : new List<Point2>() { a, d, b };
        if (!abd && bcd && cad) return abc ? new List<Point2>() { a, d, b, c } : new List<Point2>() { a, c, b, d};
        if (!abd && bcd && !cad) return abc ? new List<Point2>() { b, c, d } : new List<Point2>() { b, d, c };
        if (!abd && !bcd && cad) return abc ? new List<Point2>() { c, a, d } : new List<Point2>() { c, d, a };

        throw new Exception("The computation of convex hull of 4 points should not be able to reach this part of code. Check that the provided points are valid.");
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
        switch (points.Count)
        {
            case < 3:
                Hull.Clear();
                return;
            case 3:
                Hull.AddRange(points);
                break;
            default:
            {
                if (!points.Last().Inside(Hull))
                {
                    //reconstruct the hull
                }

                break;
            }
        }
    }

    public override string ToString()
    {
        return string.Join(" ", Hull);
    }
}
