# Primitives
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

Assert.IsTrue(Point2.OrientedCcw(p, q, r));
```
<!-- endSnippet -->
