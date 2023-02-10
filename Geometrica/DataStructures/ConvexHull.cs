using Geometrica.Primitives;
using static System.Double;
using static System.Math;

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
        if (6 < pts?.Count)
        {
            var half = pts.Count / 2;
            return JoinHulls(CreateConvexHull(pts.GetRange(0, half)), CreateConvexHull(pts.GetRange(half, pts.Count - half)));
        }
        else
        {
            return CreateSimpleHull(pts);
        }
    }

    public static List<Point2> CreateSimpleHull(List<Point2> pts)
    {
        if (pts == null)
        {
            throw new ArgumentNullException($"The argument {nameof(pts)} must not be null. Please provide a list points.");
        }

        if (pts.Count < 3 || 6 < pts.Count)
        {
            throw new ArgumentException($"This method must only be used for 3 to 5 points. You provided a list of {pts.Count} points.");
        }

        if (pts.Count == 3)
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
        if (pts.Count == 4)
        {
            return ConvexHull4(pts[0], pts[1], pts[2], pts[3]);
        }

        if (pts.Count == 5)
        {
            return BruteForce(pts.ToArray());
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
        if (abd && !bcd && cad) return abc ? new List<Point2>() { a, b, d, c } : new List<Point2>() { a, c, d, b };
        if (abd && !bcd && !cad) return abc ? new List<Point2>() { a, b, d } : new List<Point2>() { a, d, b };
        if (!abd && bcd && cad) return abc ? new List<Point2>() { a, d, b, c } : new List<Point2>() { a, c, b, d };
        if (!abd && bcd && !cad) return abc ? new List<Point2>() { b, c, d } : new List<Point2>() { b, d, c };
        if (!abd && !bcd && cad) return abc ? new List<Point2>() { c, a, d } : new List<Point2>() { c, d, a };

        throw new Exception("The computation of convex hull of 4 points should not be able to reach this part of code. Check that the provided points are valid.");
    }

    public List<Point2> JoinHulls(List<Point2> ch1, List<Point2> ch2)
    {
        //najdi bod p uvnitr ch1
        //zjisti zda p je uvnitr ch2
        //p je uvnitr ch2 - spoj ch1 a ch2 do seznamu, serazene vuci p podle uhlu, jdi na grahamovo prohledavani
        //p neni uvnitr ch2 - ch2 lezi vuci p v klinu s uhlem <= PI => 1. lze vyradit, 2. setridit vuci p spolu s ch1
        //grahamovo prohledavani serazenych vrcholu
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

    public static List<Point2> GrahamScan(Point2[] points)
    {
        var leftMostPoint = points[0];
        foreach (var p in points)
        {
            if (p.X < leftMostPoint.X)
            {
                leftMostPoint = p;
            }
        }
        
        var pts = SortPointsByAngle(leftMostPoint, points);

        var cHull = new List<Point2>();
        for (i = 0; i < pts.Length - 1; i++)
        {
            //traverse all of the sorted points, create convex hull
        }

        return cHull;
    }

    public static Point2[] SortPointsByAngle(Point2 innerPoint, Point2[] points)
    {
        var angle = new double[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            angle[i] = GetPointAngle((points[i] - innerPoint).Normalize());
        }
        Array.Sort(angle, points);

        return points;
    }

    public static double GetPointAngle(Point2 p)
    {
        if (p.X >= 0 && p.Y >= 0)
        {
            return Asin(p.Y);
        }

        if (p.X <= 0 && p.Y >= 0)
        {
            return PI - Asin(p.Y);
        }

        if (p.X <= 0 && p.Y <= 0)
        {
            return PI - Asin(p.Y);
        }

        if (p.X >= 0 && p.Y <= 0)
        {
            return 2 * PI + Asin(p.Y);
        }

        return NaN;
    }

    public static List<Point2> BruteForce(Point2[] points)
    {
        var chLines = new List<Tuple<int, int>>();
        
        for (var i = 0; i < points.Length; i++)
        {
            for (var j = i + 1; j < points.Length; j++)
            {
                var orientations = new bool[3];
                var oIndex = 0;
                for (var k = 0; k < points.Length; k++)
                {
                    if (k != i && k != j)
                    {
                        orientations[oIndex++] = Point2.OrientedCCW(points[i], points[j], points[k]);
                    }
                }

                if (orientations[0] == orientations[1] && orientations[0] == orientations[2])
                {
                    chLines.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        var cHull = new List<Point2>
        {
            points[chLines[0].Item1],
            points[chLines[0].Item2]
        };
        var lastIndex = chLines[0].Item2;
        chLines.RemoveAt(0);
        
        while (chLines.Count > 1)
        {
            var t = chLines.Single(l => l.Item1 == lastIndex || l.Item2 == lastIndex);
            lastIndex = t.Item1 == lastIndex ? t.Item2 : t.Item1;
            cHull.Add(points[lastIndex]);
            chLines.Remove(t);
        }

        if (!Point2.OrientedCCW(cHull[0], cHull[1], cHull[2]))
        {
            cHull.Reverse();
        }

        return cHull;
    }

    public override string ToString()
    {
        return string.Join(" ", Hull);
    }
}
