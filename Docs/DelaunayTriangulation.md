# Delaunay Triangulation

Delaunay triangulation over a set of points _S_ is such a triangulation which satisfies the condition that no point of _S_ lies inside a circumcircle of any of the triangles in the triangulation. It is often denoted _DT(S)_.

## Delaunay Criterium

Delaunay criterium or an In-Circle test is perhaps the most essential test of any of the construction algorithms. It determines if a given point is outside the circumcircle of a given triangle, therefore confirming the basic condition of the Delaunay triangulation.

If you need to determine if a point _p4_ lies inside the triangle _p1, p2, p3_, you can do so by calling this method. Note that the triangle needs to be oriented counter-clockwise to get the expected result.

<!-- snippet: IncircleTest -->
```cs
DelaunayTriangulation.InCircle(p1, p2, p3, p4);
```
<!-- endSnippet -->

This method returns _true_ if the point lies outside the circumcircle and the Delaunay criterium is therefore satisfied.

# Creation Algorithms

## Incremental Construction

## Divide and Conquer

# Operational Algorightms

## Point Location

### Orthogonal Walk

This is a very simple walking algorithm that traverses the triangulation first in the x-direction and then in the y-direction, each time stopping when a vertex of the current triangle is located beyond the query point in the respective axis and direction. Note that the final triangle of this walking algorithm is not necessarily the triangle containing the query point. After this algorithm has stopped, it is therefore necessary to perform another type of walk to traverse into the correct triangle.

### Remembering Stochastic Walk

# Triangle Operators

In order to make the work with triangles in the triangulation easier, there are a couple of helper extension methods that allow the user to easily determine if two triangles are neighbors in the triangulation, or to set this relationships among them. Note that the neighbors are indexed according to the triangle vertices, i.e., the neighbor at index 0 is the neighbor over the edge that is opposite to the point with index 0, etc.

<!-- snippet: TriangleNeighbors -->
```cs
var t1 = new Triangle(p1, p2, p3);
var t2 = new Triangle(p4, p3, p2);
var t3 = new Triangle(p5, p1, p3);
var t4 = new Triangle(p6, p2, p1);

t1.SetNeighbor(0, t2);  //sets neighbor across the side opposite to 0th vertex
t1.GetNeighbor(0);  //returns t2
t1.SetNeighbors(t2, t3, t4);
```
<!-- endSnippet -->

# Relation to Convex Hull
