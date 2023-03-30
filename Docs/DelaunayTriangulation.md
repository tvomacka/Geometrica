# Delaunay Triangulation

Delaunay triangulation over a set of points _S_ is such a triangulation which satisfies the condition that no point of _S_ lies inside a circumcircle of any of the triangles in the triangulation. It is often denoted _DT(S)_.

# Creation Algorithms

## Incremental Construction

## Divide and Conquer

# Triangle Operators

In order to make the work with triangles in the triangulation easier, there are a couple of helper extension methods that allow the user to easily determine if two triangles are neighbors in the triangulation, or to set this relationships among them. Note that the neighbors are indexed according to the triangle vertices, i.e., the neighbor at index 0 is the neighbor over the edge that is opposite to the point with index 0, etc.

<!-- snippet: TriangleNeighbors -->
<!-- endSnippet -->

# Relation to Convex Hull
