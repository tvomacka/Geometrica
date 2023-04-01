# Delaunay Triangulation

Delaunay triangulation over a set of points _S_ is such a triangulation which satisfies the condition that no point of _S_ lies inside a circumcircle of any of the triangles in the triangulation. It is often denoted _DT(S)_.

# Creation Algorithms

## Incremental Construction

## Divide and Conquer

# Operational Algorightms

## Point Location

### Orthogonal Walk

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
