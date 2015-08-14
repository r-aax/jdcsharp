// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Utils;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Point3D = Lib.Maths.Geometry.Geometry3D.Point;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Line2D = Lib.Maths.Geometry.Geometry2D.Line;
using Line3D = Lib.Maths.Geometry.Geometry3D.Line;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Graph layouts manager (arrange coordinates).
    /// </summary>
    public class GraphLayoutManager
    {
        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="rect">rectangle</param>
        public static void SetLayoutRandom(Graph g, int index_from, int index_to, Rect rect)
        {
            Debug.Assert(g.Is2D);

            for (int i = index_from; i <= index_to; i++)
            {
                g.Nodes[i].Point2D = Point2D.Random(rect);
            }
        }

        /// <summary>
        /// Set random graph nodes coordinates.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">indices array</param>
        /// <param name="rect">rectangle</param>
        public static void SetLayoutRandom(Graph g, int[] indices, Rect rect)
        {
            Debug.Assert(g.Is2D);

            foreach (int i in indices)
            {
                g.Nodes[indices[i]].Point2D = Point2D.Random(rect);
            }
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
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="par">parallelepiped</param>
        public static void SetLayoutRandom(Graph g, int index_from, int index_to, Parallelepiped par)
        {
            Debug.Assert(g.Is3D);

            for (int i = index_from; i <= index_to; i++)
            {
                g.Nodes[i].Point3D = Point3D.Random(par);
            }
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

            foreach (int i in indices)
            {
                g.Nodes[indices[i]].Point3D = Point3D.Random(par);
            }
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

                g.Nodes[i].Point2D = circle.Center + new Vector2D(circle.Radius, 0.0);
                g.Nodes[i].Point2D.Rot(circle.Center, angle);
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
                g.Nodes[indices[i]].Point2D = line.P + line.V * ((double)i / (indices.Length - 1));
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
                g.Nodes[indices[i]].Point3D = line.P + line.V * ((double)i / (indices.Length - 1));
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

                    g.Nodes[n].Point2D = new Point2D(rect.Left + rect.Width * ((double)x / (xn - 1)),
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

                        g.Nodes[n].Point3D = new Point3D(par.Left + par.Width * ((double)x / (xn - 1)),
                                                         par.Top - par.Height * ((double)y / (yn - 1)),
                                                         par.Front - par.Depth * ((double)z / (zn - 1)));
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
    }
}
