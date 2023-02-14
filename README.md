# Geometrica: A Library for Various Geometry-Related Tasks

[![on-push-do-docs](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml)

## Primitives

Basic information about the different types of primitives used by the algorithms can be found in [Primitives](Docs/Primitives.md).

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
- [ ] Some of the algorithms implemented in this library were inspired by the materials published by Prof. Ivana Kolingerova from Universy of West Bohemia, if you are interested in this stuff and speak Czech, go [czech them out](http://afrodita.zcu.cz/~kolinger/vyukaZCU.html#VAM).
