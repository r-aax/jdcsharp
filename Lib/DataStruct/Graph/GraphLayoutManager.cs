﻿// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Utils;
using Line2D = Lib.Maths.Geometry.Geometry2D.Line;
using Line3D = Lib.Maths.Geometry.Geometry3D.Line;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Graph layouts manager (arrange coordinates).
    /// </summary>
    public class GraphLayoutManager
    {
        //------------------------------------------------------------------------------------------
        // General functions.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Set points for nodes with given indices.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices</param>
        /// <param name="points">points</param>
        public static void SetPointsForNodes(Graph g, int[] indices, Point[] points)
        {
            Debug.Assert(indices.Length == points.Length);

            for (int i = 0; i < indices.Length; i++)
            {
                g.Nodes[indices[i]].P = points[i];
            }
        }

        //------------------------------------------------------------------------------------------
        // Layouts.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="rect">rectangle</param>
        public static void SetLayoutRandom(Graph g, int[] indices, Rect rect)
        {
            Debug.Assert(g.Is2D);

            SetPointsForNodes(g, indices, PointsGenerator.RandomPointsInRect(indices.Length, rect));
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="rect">rectangle</param>
        public static void SetLayoutRandom(Graph g, int index_from, int index_to, Rect rect)
        {
            SetLayoutRandom(g, Arrays.Range(index_from, index_to), rect);
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="rect">rectangle</param>
        public static void SetLayoutRandom(Graph g, Rect rect)
        {
            SetLayoutRandom(g, 0, g.Order - 1, rect);
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="par">parallelepiped</param>
        public static void SetLayoutRandom(Graph g, int[] indices, Parallelepiped par)
        {
            Debug.Assert(g.Is3D);

            SetPointsForNodes(g, indices, PointsGenerator.RandomPointsInParallelepiped(indices.Length, par));
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="par">parallelepiped</param>
        public static void SetLayoutRandom(Graph g, int index_from, int index_to, Parallelepiped par)
        {
            SetLayoutRandom(g, Arrays.Range(index_from, index_to), par);
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="par">parallelepiped</param>
        public static void SetLayoutRandom(Graph g, Parallelepiped par)
        {
            SetLayoutRandom(g, 0, g.Order - 1, par);
        }

        /// <summary>
        /// Set layout on circle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="circle">circle</param>
        /// <param name="n">circle parts count</param>
        /// <param name="start_angle">angle of first node on circle</param>
        /// <param name="dir">layout direction</param>
        public static void SetLayoutCircle(Graph g, int index_from, int index_to,
                                           Circle circle, int n, double start_angle,
                                           Direction dir)
        {
            Debug.Assert((dir == Direction.Clockwise) || (dir == Direction.ContraClockwise));
            Debug.Assert(g.Is2D);

            for (int i = index_from; i <= index_to; i++)
            {
                double angle = (2.0 * Math.PI / n) * (i - index_from);

                if (dir == Direction.Clockwise)
                {
                    angle *= -1.0;
                }

                angle += start_angle;

                g.Nodes[i].P = circle.Center + new Vector(circle.Radius, 0.0);
                g.Nodes[i].P.Rot(circle.Center, angle);
            }
        }

        /// <summary>
        /// Set layout on circle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="circle">circle</param>
        /// <param name="start_angle">angle of first node on circle</param>
        public static void SetLayoutCircle(Graph g, int index_from, int index_to,
                                           Circle circle, double start_angle)
        {
            SetLayoutCircle(g, index_from, index_to, circle, index_to - index_from + 1,
                            start_angle, Direction.Clockwise);
        }

        /// <summary>
        /// Set layout on circle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="circle">circle</param>
        /// <param name="start_angle">angle of first node on circle</param>
        public static void SetLayoutCircle(Graph g, Circle circle, double start_angle)
        {
            SetLayoutCircle(g, 0, g.Order - 1, circle, start_angle);
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine2D(Graph g, int[] indices, Line2D line)
        {
            Debug.Assert(indices.Length > 1);

            for (int i = 0; i < indices.Length; i++)
            {
                g.Nodes[indices[i]].P = line.P + line.V * ((double)i / (indices.Length - 1));
            }
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine2D(Graph g, int index_from, int index_to, Line2D line)
        {
            SetLayoutLine2D(g, Arrays.Range(index_from, index_to), line);
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine2D(Graph g, Line2D line)
        {
            SetLayoutLine2D(g, Arrays.Range(0, g.Order - 1), line);
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine3D(Graph g, int[] indices, Line3D line)
        {
            Debug.Assert(indices.Length > 1);

            for (int i = 0; i < indices.Length; i++)
            {
                g.Nodes[indices[i]].P = line.P + line.V * ((double)i / (indices.Length - 1));
            }
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine3D(Graph g, int index_from, int index_to, Line3D line)
        {
            SetLayoutLine3D(g, Arrays.Range(index_from, index_to), line);
        }

        /// <summary>
        /// Nodes layout on line.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="line">line</param>
        public static void SetLayoutLine3D(Graph g, Line3D line)
        {
            SetLayoutLine3D(g, Arrays.Range(0, g.Order - 1), line);
        }

        /// <summary>
        /// Set layout as grid in rectangle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="rect">outer rectangle</param>
        public static void SetLayoutRectGrid(Graph g, int[] indices, int xn, int yn, Rect rect)
        {
            Debug.Assert(indices.Length <= xn * yn);

            for (int y = 0; y < yn; y++)
            {
                for (int x = 0; x < xn; x++)
                {
                    int n = y * xn + x;

                    if (n >= indices.Length)
                    {
                        return;
                    }

                    n = indices[n];

                    g.Nodes[n].P = new Point(rect.Left + rect.Width * ((double)x / (xn - 1)),
                                             rect.Top - rect.Height * ((double)y / (yn - 1)));
                }
            }
        }

        /// <summary>
        /// Set layout as grid in rectangle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="rect">outer rectangle</param>
        public static void SetLayoutRectGrid(Graph g, int index_from, int index_to,
                                             int xn, int yn, Rect rect)
        {
            SetLayoutRectGrid(g, Arrays.Range(index_from, index_to), xn, yn, rect);
        }

        /// <summary>
        /// Set layout as grid in rectangle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="rect">outer rectangle</param>
        public static void SetLayoutRectGrid(Graph g, int xn, int yn, Rect rect)
        {
            SetLayoutRectGrid(g, 0, g.Order - 1, xn, yn, rect);
        }

        /// <summary>
        /// Set layout as grid in parallelepiped.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="zn">count of nodes on axis <c>z</c></param>
        /// <param name="par">outer parallelepiped</param>
        public static void SetLayoutParallelepipedGrid(Graph g, int[] indices,
                                                       int xn, int yn, int zn,
                                                       Parallelepiped par)
        {
            Debug.Assert(indices.Length <= xn * yn * zn);

            for (int z = 0; z < zn; z++)
            {
                for (int y = 0; y < yn; y++)
                {
                    for (int x = 0; x < xn; x++)
                    {
                        int n = z * yn * xn + y * xn + x;

                        if (n >= indices.Length)
                        {
                            return;
                        }

                        n = indices[n];

                        double xcoord = (xn > 1)
                                        ? par.Left + par.Width * ((double)x / (xn - 1))
                                        : 0.5 * (par.Left + par.Right);
                        double ycoord = (yn > 1)
                                        ? par.Top - par.Height * ((double)y / (yn - 1))
                                        : 0.5 * (par.Top + par.Bottom);
                        double zcoord = (zn > 1)
                                        ? par.Front - par.Depth * ((double)z / (zn - 1))
                                        : 0.5 * (par.Front + par.Back);

                        g.Nodes[n].P = new Point(xcoord, ycoord, zcoord);
                    }
                }
            }
        }

        /// <summary>
        /// Set layout as grid in parallelepiped.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="zn">count of nodes on axis <c>z</c></param>
        /// <param name="par">outer parallelepiped</param>
        public static void SetLayoutParallelepipedGrid(Graph g, int index_from, int index_to,
                                                       int xn, int yn, int zn,
                                                       Parallelepiped par)
        {
            SetLayoutParallelepipedGrid(g, Arrays.Range(index_from, index_to), xn, yn, zn, par);
        }

        /// <summary>
        /// Set layout as grid in parallelepiped.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="xn">count of nodes on axis <c>x</c></param>
        /// <param name="yn">count of nodes on axis <c>y</c></param>
        /// <param name="zn">count of nodes on axis <c>z</c></param>
        /// <param name="par">outer parallelepiped</param>
        public static void SetLayoutParallelepipedGrid(Graph g, int xn, int yn, int zn,
                                                       Parallelepiped par)
        {
            SetLayoutParallelepipedGrid(g, 0, g.Order - 1, xn, yn, zn, par);
        }

        /// <summary>
        /// Set layout for lateral surface of the cylinder.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">nodes indices</param>
        /// <param name="yn">points count in <c>OY</c> direction</param>
        /// <param name="cn">points count on single circle</param>
        /// <param name="cylinder">cylinder</param>
        public static void SetLayoutCylinderLateralSurfaceGrid(Graph g, int[] indices,
                                                               int yn, int cn, 
                                                               Cylinder cylinder)
        {
            Debug.Assert(indices.Length <= yn * cn);

            Point[] ps = PointsGenerator.PointsOnCircle(cylinder.Circle, cn);
            double dh = cylinder.Height / (yn - 1);

            for (int ih = 0; ih < yn; ih++)
            {
                for (int ic = 0; ic < ps.Length; ic++)
                {
                    g.Nodes[ih * cn + ic].P = ps[ic] + new Vector(0.0, 0.0, ih * dh);
                }
            }

            return;
        }

        /// <summary>
        /// Set layout for lateral surface of the cylinder.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">start index</param>
        /// <param name="index_to">stop index</param>
        /// <param name="yn">points count in <c>OY</c> direction</param>
        /// <param name="cn">points count on single circle</param>
        /// <param name="cylinder">cylinder</param>
        public static void SetLayoutCylinderLateralSurfaceGrid(Graph g, int index_from, int index_to,
                                                               int yn, int cn,
                                                               Cylinder cylinder)
        {
            SetLayoutCylinderLateralSurfaceGrid(g, Arrays.Range(index_from, index_to), yn, cn, cylinder);
        }

        /// <summary>
        /// Set layout for lateral surface of the cylinder.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="yn">points count in <c>OY</c> direction</param>
        /// <param name="cn">points count on single circle</param>
        /// <param name="cylinder">cylinder</param>
        public static void SetLayoutCylinderLateralSurfaceGrid(Graph g,
                                                               int yn, int cn,
                                                               Cylinder cylinder)
        {
            SetLayoutCylinderLateralSurfaceGrid(g, 0, g.Order - 1, yn, cn, cylinder);
        }
    }
}
