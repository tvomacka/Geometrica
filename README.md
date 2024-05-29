# Geometrica: A Library for Various Geometry-Related Tasks

[![on-push-do-docs](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml)
[![Build and Run Unit Tests](https://github.com/tvomacka/Geometrica/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/tvomacka/Geometrica/actions/workflows/build-and-test.yml)

## Disclaimer

This is a work in progress and does not aim to be a time-optimal implementation. You will probably find stuff that is incomplete or broken, be aware and use at your own risk.

## Primitives

Basic information about the different types of primitives used by the algorithms can be found in [Primitives](Docs/Primitives.md).

## Convex Hull

> Detailed description in [Convex Hull Algorithms](Docs/ConvexHull.md).

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

Which uses a divide and conquer algorightm.

Once the convex hull is created, the points on the hull can either be accessed using an indexer or iterated through with IEnumerable.
<!-- snippet: AccessingPointsOnCH -->
```cs
var pt = ch[0]; //returns (0, 0)
foreach (var point in ch)
{
    //...
}
```
<!-- endSnippet -->

## Delaunay Triangulation

> Detailed description in [Delaunay Triangulation](Docs/DelaunayTriangulation.md).

Delaunay triangulation of set of points can be created using the constructor:

<!-- snippet: DelaunayTriangulationConstructor -->
```cs
var p1 = new Point2(0, 0);
var p2 = new Point2(1, 0);
var p3 = new Point2(0, 1);
var p4 = new Point2(1, 1);

var dt = new DelaunayTriangulation(new Point2[] { p1, p2, p3 });
```
<!-- endSnippet -->

If you provide an empty set of points, or create the class instance using a parameterless constructor, the resulting Delaunay triangulation will be empty.
Points can be added to a pre-existing Delaunay triangulation using a dedicated method. For adding a new point into a triangulation, it does not matter how it was created or if it contains any other points.

<!-- snippet: DelaunayTriangulationAddPoint -->
```cs
dt.Add(new Point2(0.1, 0.1));
```
<!-- endSnippet -->

# Credits

- [ ] Refactoring the code with [ReSharper](https://jb.gg/OpenSourceSupport).
- [ ] Some of the algorithms implemented in this library were inspired by the materials published by Prof. Ivana Kolingerova from Universy of West Bohemia, if you are interested in this stuff and speak Czech, go [czech them out](http://afrodita.zcu.cz/~kolinger/vyukaZCU.html#VAM).
