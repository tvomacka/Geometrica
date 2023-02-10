# Geometrica: A Library for Various Geometry-Related Tasks

[![on-push-do-docs](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml)

## Point Operations

You can check the orientation of three points using an orientation test:

<!-- snippet: OrientationTest -->
```cs
var p = new Point2(0, 0);
var q = new Point2(0, 1);
var r = new Point2(1, 0);

Assert.IsTrue(Point2.Orientation(p, q, r) < 0);
```
<!-- endSnippet -->

or

<!-- snippet: CCWOrientationTest -->
```cs
var p = new Point2(0, 0);
var q = new Point2(1, 0);
var r = new Point2(0, 1);

Assert.IsTrue(Point2.OrientedCCW(p, q, r));
```
<!-- endSnippet -->

## Convex Hull

Convex hull of a set of point can be created using the constructor like this:

<!-- snippet: CreateConvexHull -->
```cs
var pts = new List<Point2>() 
{
    new(0, 0),
    new(1, 0),
    new(0, 1),
    new(0.1, 0.1)
};

var ch = new ConvexHull(pts);
```
<!-- endSnippet -->

Which uses a divide and conquer algorightm. There are other algorithms you can use, detailed in [Convex Hull Algorithms](Docs/ConvexHull.md).

# Credits

- [ ] Refactoring the code with [ReSharper](https://jb.gg/OpenSourceSupport).
