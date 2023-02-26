using System.Collections;
using Geometrica.Primitives;
using static System.Double;
using static System.Math;

namespace Geometrica.DataStructures;

public class ConvexHull : IEnumerable<Point2>, IPolygon
{
    private readonly List<Point2> _points = new();
    private readonly List<Point2> _hull = new();

    public Point2 this[int key] => _hull[key];

    public ConvexHull()
    {
    }

    public ConvexHull(List<Point2> pts)
    {
        _points = pts;
        var ch = CreateConvexHull(pts);
        _hull = ch._hull;
    }

    public int Count => _hull.Count;

    public bool IsConvex => true;
    public bool Contains(Point2 point)
    {
        for (int i = 0; i < _hull.Count; i++)
        {
            if (!Point2.OrientedCcw(point, _hull[i], _hull[(i + 1) % _hull.Count]))
                return false;
        }

        return true;
    }

    public ConvexHull CreateConvexHull(List<Point2> pts)
    {
        if (pts == null)
        {
            throw new ArgumentNullException($"The provided list of points cannot be null, please provide a non-null list with some points to create a convex hull. Argument name: ${nameof(pts)}");
        }

        if (pts.Count < 6)
        {
            return CreateSimpleHull(pts);
        }

        var half = pts.Count / 2;
        var ch1 = new ConvexHull();
        ch1._points.AddRange(pts.GetRange(0, half));
        ch1._hull.AddRange(CreateConvexHull(ch1._points));

        var ch2 = new ConvexHull();
        ch2._points.AddRange(pts.GetRange(half, pts.Count - half));
        ch2._hull.AddRange(ch2._points);
        return JoinHulls(ch1, ch2);
    }

    public static ConvexHull CreateSimpleHull(List<Point2> pts)
    {
        if (pts == null)
        {
            throw new ArgumentNullException($"The argument {nameof(pts)} must not be null. Please provide a list points.");
        }

        return pts.Count switch
        {
            3 when Point2.Orientation(pts[0], pts[1], pts[2]) > 0 => new ConvexHull()
            {
                _hull = { pts[0], pts[1], pts[2] }, _points = { pts[0], pts[1], pts[2] }
            },
            3 => new ConvexHull() { _hull = { pts[0], pts[2], pts[1] }, _points = { pts[0], pts[2], pts[1] } },
            4 => ConvexHull4(pts[0], pts[1], pts[2], pts[3]),
            5 => BruteForce(pts.ToArray()),
            _ => throw new ArgumentException(
                $"This method must only be used for 3 to 5 points. You provided a list of {pts.Count} points."),
        };
    }

    public static ConvexHull ConvexHull4(Point2 a, Point2 b, Point2 c, Point2 d)
    {
        //https://stackoverflow.com/questions/2122305/convex-hull-of-4-points
        var abc = Point2.OrientedCcw(a, b, c);
        var abd = Point2.OrientedCcw(a, b, d);
        var bcd = Point2.OrientedCcw(b, c, d);
        var cad = Point2.OrientedCcw(c, a, d);

        abd = abd == abc;
        bcd = bcd == abc;
        cad = cad == abc;

        List<Point2> hull = null;

        if (abd && bcd && cad) hull = abc ? new List<Point2>() { a, b, c } : new List<Point2> { a, c, b };
        if (abd && bcd && !cad) hull = abc ? new List<Point2>() { a, b, c, d } : new List<Point2>() { a, d, c, b };
        if (abd && !bcd && cad) hull = abc ? new List<Point2>() { a, b, d, c } : new List<Point2>() { a, c, d, b };
        if (abd && !bcd && !cad) hull = abc ? new List<Point2>() { a, b, d } : new List<Point2>() { a, d, b };
        if (!abd && bcd && cad) hull = abc ? new List<Point2>() { a, d, b, c } : new List<Point2>() { a, c, b, d };
        if (!abd && bcd && !cad) hull = abc ? new List<Point2>() { b, c, d } : new List<Point2>() { b, d, c };
        if (!abd && !bcd && cad) hull = abc ? new List<Point2>() { c, a, d } : new List<Point2>() { c, d, a };

        var ch = new ConvexHull()
        {
            _points = { a, b, c, d }
        };
        if (hull != null) ch._hull.AddRange(hull);

        return ch;
    }

    public static ConvexHull JoinHulls(ConvexHull ch1, ConvexHull ch2)
    {
        var p = GetPointInside(ch1);
        Point2[] sortedPts;

        if (IsPointInside(p, ch2))
        {
            var allPoints = new List<Point2>();
            allPoints.AddRange(ch1);
            allPoints.AddRange(ch2);
            sortedPts = SortPointsByAngle(p, allPoints.ToArray());
        }
        else
        {
            var angle = new double[ch2.Count];
            var minAngleIndex = -1;
            var maxAngleIndex = -1;
            var minAngle = double.MaxValue;
            var maxAngle = double.MinValue;
            for (var i = 0; i < ch2.Count; i++)
            {
                angle[i] = GetPointAngle((ch2[i] - p).Normalize());
                if (angle[i] < minAngle)
                {
                    minAngle = angle[i];
                    minAngleIndex = i;
                }

                if (maxAngle < angle[i])
                {
                    maxAngle = angle[i];
                    maxAngleIndex = i;
                }
            }

            var allPoints = new List<Point2>();
            allPoints.AddRange(ch1);
            if (minAngleIndex <= maxAngleIndex)
            {
                allPoints.AddRange(ch2._hull.GetRange(minAngleIndex, maxAngleIndex - minAngleIndex + 1));
            }
            else
            {
                allPoints.AddRange(ch2._hull.GetRange(minAngleIndex, ch2.Count - minAngleIndex));
                allPoints.AddRange(ch2._hull.GetRange(0, maxAngleIndex + 1));
            }

            sortedPts = SortPointsByAngle(p, allPoints.ToArray());
        }

        return GrahamScan(sortedPts, true);
    }

    public static bool IsPointInside(Point2 p, ConvexHull convexHull)
    {
        for (var i = 0; i < convexHull.Count; i++)
        {
            if (!Point2.OrientedCcw(p, convexHull[i], convexHull[(i + 1) % convexHull.Count]))
                return false;
        }
        return true;
    }

    public static Point2 GetPointInside(ConvexHull convexHull)
    {
        return new Point2()
        {
            X = (convexHull[0].X + convexHull[1].X + convexHull[2].X) / 3,
            Y = (convexHull[0].Y + convexHull[1].Y + convexHull[2].Y) / 3
        };
    }

    public void Add(Point2 point)
    {
        _points.Add(point);
        UpdateConvexHull();
    }

    private void UpdateConvexHull()
    {
        switch (_points.Count)
        {
            case < 3:
                _hull.Clear();
                return;
            case 3:
                _hull.AddRange(_points);
                break;
            default:
                {
                    if (!Contains(_points.Last()))
                    {
                        _hull.Clear();
                        _hull.AddRange(GrahamScan(_points.ToArray()));
                    }

                    break;
                }
        }
    }

    public static ConvexHull GrahamScan(Point2[] points, bool pointsSorted = false)
    {
        var pts = !pointsSorted ? SortByAngleToLeftmostPoint(points) : points;

        var stack = new Stack<Point2>();

        foreach (var p in pts)
        {
            while (stack.Count > 1 && !Ccw(p, stack))
            {
                stack.Pop();
            }

            bool Ccw(Point2 q, Stack<Point2> s)
            {
                return Point2.OrientedCcw(s.ElementAt(1), s.ElementAt(0), q);
            }

            stack.Push(p);
        }


        var cHull = new ConvexHull();
        cHull._points.AddRange(points);

        var hullPoints = stack.ToList();
        hullPoints.Reverse();
        cHull._hull.AddRange(hullPoints);

        return cHull;
    }

    private static Point2[] SortByAngleToLeftmostPoint(Point2[] points)
    {
        var leftMostPoint = points[0];
        foreach (var p in points)
        {
            if (p.X <= leftMostPoint.X)
            {
                leftMostPoint = p.Y < leftMostPoint.Y ? p : leftMostPoint;
            }
        }

        return SortPointsByAngle(leftMostPoint, points);
    }

    public static Point2[] SortPointsByAngle(Point2 innerPoint, Point2[] points)
    {
        var angle = new double[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            angle[i] = Equals(points[i], innerPoint) ? 0 : GetPointAngle((points[i] - innerPoint).Normalize());
        }
        Array.Sort(angle, points);

        return points;
    }

    public static double GetPointAngle(Point2 p)
    {
        return p.X switch
        {
            >= 0 when p.Y >= 0 => Asin(p.Y),
            <= 0 when p.Y >= 0 => PI - Asin(p.Y),
            <= 0 when p.Y <= 0 => PI - Asin(p.Y),
            _ => 2 * PI + Asin(p.Y)
        };
    }

    public static ConvexHull BruteForce(Point2[] points)
    {
        var chLines = GetLinesOnConvexHull(points);

        return ConstructConvexHull(points, chLines);
    }

    public static ConvexHull ConstructConvexHull(Point2[] points, List<Tuple<int, int>> chLines)
    {
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

        if (!Point2.OrientedCcw(cHull[0], cHull[1], cHull[2]))
        {
            cHull.Reverse();
        }

        var ch = new ConvexHull();
        ch._points.AddRange(points);
        ch._hull.AddRange(cHull);
        return ch;
    }

    public static List<Tuple<int, int>> GetLinesOnConvexHull(Point2[] points)
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
                        orientations[oIndex++] = Point2.OrientedCcw(points[i], points[j], points[k]);
                    }
                }

                if (orientations[0] == orientations[1] && orientations[0] == orientations[2])
                {
                    chLines.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        return chLines;
    }

    public IEnumerator<Point2> GetEnumerator()
    {
        return _hull.GetEnumerator();
    }

    public override string ToString()
    {
        return string.Join(" ", _hull);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_hull).GetEnumerator();
    }
}
