# Geometrica: A Library for Various Geometry-Related Tasks

[![on-push-do-docs](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Geometrica/actions/workflows/on-push-do-docs.yml)

## Point Operations

You can check the orientation of three points using an orientation test:

snippet: OrientationTest

or

snippet: CCWOrientationTest

## Convex Hull

This module contains different algorithms for convex hull creation from a given point set.

- [ ] Direct hull from 3 or 4 points
- [ ] Brute force
- [ ] Graham Scan
- [ ] Divide and Conquer

snippet: CreateConvexHull

Uses a divide and conquer algorightm to create convex hulls by default.

```mermaid
flowchart TD
  A[p = set of points] --> B{"size(p) < 6?"}
  B -- Yes --> C["Return CH(p)"]
  B -- No --> D[Split p in half to p_1 and p_2]
  D --> E["CH(p_1)"] & F["CH(p_2)"]
  E --> G["Join CH(p_1) and CH(p_2)"]
  F --> G
  G --> H["Return CH(p)"]
```

The divide and conquer algorithm uses a brute force approach to construct the convex hulls of 5 point subsets. It is however possible to use this brute force approach for larger sets of points directly if desired.


snippet: BruteForceConvexHull
