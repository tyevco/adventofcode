﻿using System.Collections.Generic;

namespace Advent.Utilities
{
    public static partial class CollisonDetection
    {
        public static bool IsIntersecting(IAABoundingBox box, ITriangle triangle)
        {
            double triangleMin, triangleMax;
            double boxMin, boxMax;

            // Test the box normals (x-, y- and z-axes)
            var boxNormals = new IVector[] {
                new Vector(1,0,0),
                new Vector(0,1,0),
                new Vector(0,0,1)
            };

            for (int i = 0; i < 3; i++)
            {
                IVector n = boxNormals[i];
                Project(triangle.Vertices, boxNormals[i], out triangleMin, out triangleMax);
                if (triangleMax < box.Start.Coords[i] || triangleMin > box.End.Coords[i])
                    return false; // No intersection possible.
            }

            // Test the triangle normal
            double triangleOffset = triangle.Normal.Dot(triangle.A);
            Project(box.Vertices, triangle.Normal, out boxMin, out boxMax);
            if (boxMax < triangleOffset || boxMin > triangleOffset)
                return false; // No intersection possible.

            // Test the nine edge cross-products
            IVector[] triangleEdges = new IVector[] {
                triangle.A.Minus(triangle.B),
                triangle.B.Minus(triangle.C),
                triangle.C.Minus(triangle.A)
            };

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    // The box normals are the same as it's edge tangents
                    IVector axis = triangleEdges[i].Cross(boxNormals[j]);
                    Project(box.Vertices, axis, out boxMin, out boxMax);
                    Project(triangle.Vertices, axis, out triangleMin, out triangleMax);
                    if (boxMax < triangleMin || boxMin > triangleMax)
                        return false; // No intersection possible
                }

            // No separating axis found.
            return true;
        }

        public static void Project(IEnumerable<IVector> points, IVector axis,
                out double min, out double max)
        {
            min = double.PositiveInfinity;
            max = double.NegativeInfinity;

            foreach (var p in points)
            {
                double val = axis.Dot(p);
                if (val < min) min = val;
                if (val > max) max = val;
            }
        }
    }
}
