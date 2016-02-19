// Author: Alexey Rybakov

using System;
using System.Diagnostics;

using Lib.Maths.Bits;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using Lib.Maths.Numbers;
using Lib.Utils;
using Lib.Draw;
using Lib.DataStruct.Graph.DrawProperties;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Point3D = Lib.Maths.Geometry.Geometry3D.Point;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;
using Triangle3D = Lib.Maths.Geometry.Geometry3D.Triangle;
using Line2D = Lib.Maths.Geometry.Geometry2D.Line;

namespace Lib.DataStruct.Graph
{
    /// <summary>
    /// Graph creator.
    /// </summary>
    public static class GraphCreator
    {
        /// <summary>
        /// Initial graph.
        /// </summary>
        /// <param name="dim">dimensionality</param>
        /// <param name="n">order</param>
        /// <returns>graph</returns>
        private static Graph InitialGraph(GraphDimensionality dim, int n)
        {
            Graph g = new Graph(dim);

            for (int i = 0; i < n; i++)
            {
                g.AddNode();
            }

            return g;
        }

        /// <summary>
        /// Null graph.
        /// </summary>
        /// <param name="dim">dimensionality</param>
        /// <returns>graph</returns>
        public static Graph NullGraph(GraphDimensionality dim)
        {
            return InitialGraph(dim, 0);
        }

        /// <summary>
        /// Set edges for random graph in Erdos - Renyi model (binomial case).
        /// </summary> 
        /// <param name="graph">graph</param>
        /// <param name="p">edge probability</param>
        private static void SetErdosRenyiBinomialRandomEdges(Graph graph, double p)
        {
            // Add edges.
            for (int i = 0; i < graph.Order; i++)
            {
                for (int j = i + 1; j < graph.Order; j++)
                {
                    if (Randoms.Random01() <= p)
                    {
                        graph.AddEdge(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Set edges for random graph in Erdos - Renyi model (uniform case).
        /// </summary>
        /// <param name="graph">graph</param>
        /// <param name="m">edges count</param>
        private static void SetErdosRenyiUniformRandomEdges(Graph graph, int m)
        {
            int n = graph.Order;
            int k = 0;

            // Generate random vector.
            Bits b = new Bits(n * (n - 1) / 2);
            b.SetRandomSet(m);

            // Add edges.
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (b[k])
                    {
                        graph.AddEdge(i, j);
                    }

                    k++;
                }
            }
        }

        /// <summary>
        /// Add edges from single node to set of nodes.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index">index</param>
        /// <param name="indices">set of indices</param>
        public static void AddEdges(Graph g, int index, int[] indices)
        {
            foreach (int i in indices)
            {
                g.AddEdge(index, i);
            }
        }

        /// <summary>
        /// Add edges from single node to set of nodes.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index">index</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        public static void AddEdges(Graph g, int index, int index_from, int index_to)
        {
            AddEdges(g, index, Arrays.Range(index_from, index_to));
        }

        /// <summary>
        /// Add edges for each pair in given set of nodes.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">set of indices</param>
        public static void AddEdges(Graph g, int[] indices)
        {
            foreach (int i in indices)
            {
                foreach (int j in indices)
                {
                    if (i < j)
                    {
                        g.AddEdge(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Add edges for each pair in given set of nodes.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        public static void AddEdges(Graph g, int index_from, int index_to)
        {
            AddEdges(g, Arrays.Range(index_from, index_to));
        }

        /// <summary>
        /// Add chain.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">set of indices</param>
        public static void AddChain(Graph g, int[] indices)
        {
            for (int i = 0; i < indices.Length - 1; i++)
            {
                g.AddEdge(indices[i], indices[i + 1]);
            }
        }

        /// <summary>
        /// Add chain.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        public static void AddChain(Graph g, int index_from, int index_to)
        {
            AddChain(g, Arrays.Range(index_from, index_to));
        }

        /// <summary>
        /// Add chain.
        /// </summary>
        /// <param name="g">graph</param>
        public static void AddChain(Graph g)
        {
            AddChain(g, 0, g.Order - 1);
        }

        /// <summary>
        /// Add cycle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">set of indices</param>
        public static void AddCycle(Graph g, int[] indices)
        {
            AddShiftedCycle(g, indices, 1);
        }

        /// <summary>
        /// Add cycle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        public static void AddCycle(Graph g, int index_from, int index_to)
        {
            AddShiftedCycle(g, index_from, index_to, 1);
        }

        /// <summary>
        /// Add cycle.
        /// </summary>
        /// <param name="g">graph</param>
        public static void AddCycle(Graph g)
        {
            AddShiftedCycle(g, 1);
        }

        /// <summary>
        /// Add shifted cycle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">set of indices</param>
        /// <param name="k">chord length</param>
        public static void AddShiftedCycle(Graph g, int[] indices, int k)
        {
            int n = indices.Length;

            Debug.Assert((k > 0) && (k < n));

            for (int i = 0; i < n; i++)
            {
                if ((2 * k != n) || (i < n / 2))
                {
                    g.AddEdge(indices[i], indices[(i + k) % n]);
                }
            }
        }

        /// <summary>
        /// Add shifted cycle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="k">chord length</param>
        public static void AddShiftedCycle(Graph g, int index_from, int index_to, int k)
        {
            AddShiftedCycle(g, Arrays.Range(index_from, index_to), k);
        }

        /// <summary>
        /// Add shifted cycle.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="k">chord length</param>
        public static void AddShiftedCycle(Graph g, int k)
        {
            AddShiftedCycle(g, 0, g.Order - 1, k);
        }

        /// <summary>
        /// Add chords in LCF notation.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="indices">set of indices</param>
        /// <param name="lenghts">chords lengths</param>
        public static void AddLCFChords(Graph g, int[] indices, int[] lenghts)
        {
            // List of indices determines "cycle".
            // The chord of elemen indices[0] has length length[0] and so on.

            int n = indices.Length;
            int k = lenghts.Length;

            // For deterministic result.
            Debug.Assert((k > 0) && (n >= k) && ((n % k) == 0));

            for (int i = 0; i < n; i++)
            {
                int ai = i;
                int len = lenghts[i % k];

                Debug.Assert(len != 0);

                int bi = (i + ((len > 0) ? len : (n + len))) % n;

                if (!g.IsEdge(ai, bi))
                {
                    g.AddEdge(ai, bi);
                }
            }
        }

        /// <summary>
        /// Add chords in LCF notation.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="index_from">from index</param>
        /// <param name="index_to">to index</param>
        /// <param name="lenghts">chirds lengths</param>
        public static void AddLCFChords(Graph g, int index_from, int index_to, int[] lenghts)
        {
            AddLCFChords(g, Arrays.Range(index_from, index_to), lenghts);
        }

        /// <summary>
        /// Add chords in LCF notation.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="lengths">chirds lenghts</param>
        public static void AddLCFChords(Graph g, int[] lengths)
        {
            AddLCFChords(g, 0, g.Order - 1, lengths);
        }

        /// <summary>
        /// Add hatch.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="double_mid">middle multiplied by 2</param>
        public static void AddHatch(Graph g, int double_mid)
        {
            CyclicArithmetic arith = new CyclicArithmetic(g.Order);

            Debug.Assert((double_mid >= 0) && (double_mid < g.Order * 2));

            if ((double_mid % 2) == 0)
            {
                // Hatch from node.
                int index = double_mid / 2;

                // Count of hatches is (Order - 1) / 2.
                for (int i = 0; i < (g.Order - 1) / 2; i++)
                {
                    int i1 = i + 1;
                    int ai = arith.Sub(index, i1);
                    int bi = arith.Add(index, i1);

                    g.AddEdge(ai, bi);
                }
            }
            else
            {
                // Hatch from edge.
                int index_a = double_mid / 2;
                int index_b = arith.Add(index_a, 1);

                if (index_a > index_b)
                {
                    int tmp = index_a;

                    index_a = index_b;
                    index_b = tmp;
                }

                // Count of hatches is Order / 2.
                for (int i = 0; i < g.Order / 2; i++)
                {
                    int i1 = i + 1;
                    int ai = arith.Sub(index_a, i1);
                    int bi = arith.Add(index_b, i1);

                    g.AddEdge(ai, bi);
                }
            }
        }

        /// <summary>
        /// Random graph in rectangle.
        /// </summary>
        /// <param name="n">nodes count</param>
        /// <param name="p">edge probability</param>
        /// <param name="rect">rectangle</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiBinomialRandomGraph(int n, double p, Rect rect)
        {
            // 2D graph.
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            // Random layout.
            GraphLayoutManager.SetLayoutRandom(g, rect);

            // Add edges.
            SetErdosRenyiBinomialRandomEdges(g, p);

            return g;
        }

        /// <summary>
        /// Random graph on circle.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="p">edge probability</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiBinomialRandomGraph(int n, double p, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);
            SetErdosRenyiBinomialRandomEdges(g, p);

            return g;
        }

        /// <summary>
        /// Random graph in parallelepiped.
        /// </summary>
        /// <param name="n">nodes count</param>
        /// <param name="p">edge probability</param>
        /// <param name="par">parallelepiped</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiBinomialRandomGraph(int n, double p, Parallelepiped par)
        {
            // 3D graph.
            Graph g = InitialGraph(GraphDimensionality.D3, n);

            // Random layout.
            GraphLayoutManager.SetLayoutRandom(g, par);

            // Add edges.
            SetErdosRenyiBinomialRandomEdges(g, p);

            return g;
        }

        /// <summary>
        /// Random graph in rectangle.
        /// </summary>
        /// <param name="n">nodes count</param>
        /// <param name="m">edges count</param>
        /// <param name="rect">rectangle</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiUniformRandomGraph(int n, int m, Rect rect)
        {
            // 2D graph.
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            // Random layout.
            GraphLayoutManager.SetLayoutRandom(g, rect);

            // Add edges.
            SetErdosRenyiUniformRandomEdges(g, m);

            return g;
        }

        /// <summary>
        /// Random graph on circle.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="m">edges count</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiUniformRandomGraph(int n, int m, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);
            SetErdosRenyiUniformRandomEdges(g, m);

            return g;
        }

        /// <summary>
        /// Random graph in parallelepiped.
        /// </summary>
        /// <param name="n">nodes count</param>
        /// <param name="m">edges count</param>
        /// <param name="par">parallelepiped</param>
        /// <param name="graph">graph</param>
        public static Graph ErdosRenyiUniformRandomGraph(int n, int m, Parallelepiped par)
        {
            // 3D graph.
            Graph g = InitialGraph(GraphDimensionality.D3, n);

            // Random layout.
            GraphLayoutManager.SetLayoutRandom(g, par);

            // Add edges.
            SetErdosRenyiUniformRandomEdges(g, m);

            return g;
        }

        /// <summary>
        /// Empty graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph EmptyGraph(int n, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);

            return g;
        }

        /// <summary>
        /// Full graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph FullGraph(int n, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);
            AddEdges(g, 0, n - 1);

            return g;
        }

        /// <summary>
        /// Cycle.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Cycle(int n, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);
            AddCycle(g, 0, n - 1);

            return g;
        }

        /// <summary>
        /// Star.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Star(int n, Circle circle)
        {
            Debug.Assert(n > 0);

            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, 0, n - 2, circle, Math.PI / 2.0);
            g.Nodes[n - 1].Point2D = circle.Center;

            for (int i = 0; i < n - 1; i++)
            {
                g.AddEdge(i, n - 1);
            }

            return g;
        }

        /// <summary>
        /// Wheel.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="circle">outer circle</param>
        /// <returns>wheel</returns>
        public static Graph Wheel(int n, Circle circle)
        {
            Graph g = Star(n, circle);

            AddCycle(g, 0, n - 2);

            return g;
        }

        /// <summary>
        /// Tetrahedron.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">escribed sphere radius</param>
        /// <returns>tetrahedron</returns>
        public static Graph Tetrahedron(Point3D p, double r)
        {
            const int order = 4;
            Graph g = InitialGraph(GraphDimensionality.D3, order);

            // Edge length.
            double a = r * (4 / Math.Sqrt(6));

            // Projection of height on bottom plane.
            Point3D hp = p - new Vector3D(0.0, Math.Sqrt(2.0 / 3.0) * a - r, 0.0);

            // Nodes coordinates.
            for (int i = 0; i < order; i++)
            {
                g.Nodes[i].Point3D = p + new Vector3D(0.0, r, 0.0);
            }
            for (int i = 1; i < order; i++)
            {
                g.Nodes[i].Point3D.RotZ(p, -Math.Acos(1.0 - 0.5 * ((a * a) / (r * r))));
            }
            g.Nodes[2].Point3D.RotY(hp, 2.0 * Math.PI / 3.0);
            g.Nodes[3].Point3D.RotY(hp, -2.0 * Math.PI / 3.0);

            // Create edges.
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (i != j)
                    {
                        g.AddEdge(i, j);
                    }
                }
            }

            return g;
        }

        /// <summary>
        /// Cube
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">escribed sphere radius</param>
        /// <returns>cube</returns>
        public static Graph Cube(Point3D p, double r)
        {
            const int order = 8;
            Graph g = InitialGraph(GraphDimensionality.D3, order);

            // Radius of inscribed sphere.
            double rr = r / Math.Sqrt(3);

            // Nodes coordinates.
            for (int i = 0; i < order; i++)
            {
                Bits32 bi = new Bits32((UInt32)i);

                g.Nodes[i].Point3D = p + new Vector3D(bi.IsBitSet(0) ? rr : -rr,
                                                      bi.IsBitSet(1) ? rr : -rr,
                                                      bi.IsBitSet(2) ? rr : -rr);
            }

            // Create edges.
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    Bits32 b = new Bits32((UInt32)i ^ (UInt32)j);

                    if (b.Popcnt() == 1)
                    {
                        g.AddEdge(i, j);
                    }
                }
            }

            return g;
        }

        /// <summary>
        /// Octahedron.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">escribed sphere radius</param>
        /// <returns>octahedron</returns>
        public static Graph Octahedron(Point3D p, double r)
        {
            const int order = 6;
            Graph g = InitialGraph(GraphDimensionality.D3, order);

            // Nodes coordinates.
            g.Nodes[0].Point3D = p + new Vector3D(0.0, r, 0.0);
            g.Nodes[1].Point3D = p + new Vector3D(r, 0.0, 0.0);
            g.Nodes[2].Point3D = p + new Vector3D(0.0, 0.0, r);
            g.Nodes[3].Point3D = p + new Vector3D(-r, 0.0, 0.0);
            g.Nodes[4].Point3D = p + new Vector3D(0.0, 0.0, -r);
            g.Nodes[5].Point3D = p + new Vector3D(0.0, -r, 0.0);

            // Create edges.
            AddEdges(g, 0, 1, 4);
            AddEdges(g, 5, 1, 4);
            AddCycle(g, 1, 4);

            return g;
        }

        /// <summary>
        /// Dodecahedron.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">escribed sphere radius</param>
        /// <returns>dodecahedron</returns>
        public static Graph Dodecahedron(Point3D p, double r)
        {
            const int order = 20;
            Graph g = InitialGraph(GraphDimensionality.D3, order);

            // Calibrate radius.
            double d = r
                       * (((1.0 / 4.0) * Math.Sqrt(2.0 * (5 + Math.Sqrt(5.0))))
                          / ((1.0 / (4.0 * Math.Sqrt(3.0))) * (3.0 + Math.Sqrt(5.0))));

            // Base icosahedron.
            Graph ico = Icosahedron(p, d);

            // Dodecahedron nodes.
            for (int i = 0; i < order; i++)
            {
                g.Nodes[i].Point3D = p;
            }
            for (int i = 0; i < 5; i++)
            {
                g.Nodes[i].Point3D = new Triangle3D(ico.Nodes[0].Point3D,
                                                    ico.Nodes[i + 1].Point3D,
                                                    ico.Nodes[(i + 1) % 5 + 1].Point3D).Barycenter();
                g.Nodes[i + 5].Point3D = new Triangle3D(ico.Nodes[i + 1].Point3D,
                                                        ico.Nodes[(i + 1) % 5 + 1].Point3D,
                                                        ico.Nodes[i + 6].Point3D).Barycenter();
                g.Nodes[i + 10].Point3D = new Triangle3D(ico.Nodes[(i + 1) % 5 + 1].Point3D,
                                                         ico.Nodes[i + 6].Point3D,
                                                         ico.Nodes[(i + 1) % 5 + 6].Point3D).Barycenter();
                g.Nodes[i + 15].Point3D = new Triangle3D(ico.Nodes[i + 6].Point3D,
                                                         ico.Nodes[(i + 1) % 5 + 6].Point3D,
                                                         ico.Nodes[ico.Order - 1].Point3D).Barycenter();
            }

            // Create edges.
            AddCycle(g, 0, 4);
            for (int i = 0; i < 5; i++)
            {
                g.AddEdge(i, i + 5);
                g.AddEdge(i + 5, i + 10);
                g.AddEdge(i + 5, (i + 4) % 5 + 10);
                g.AddEdge(i + 10, i + 15);
            }
            AddCycle(g, 15, 19);

            return g;
        }

        /// <summary>
        /// Icosahedron.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">escribed sphere radius</param>
        /// <returns>icosahedron</returns>
        public static Graph Icosahedron(Point3D p, double r)
        {
            const int order = 12;
            Graph g = InitialGraph(GraphDimensionality.D3, order);

            // Calibrate radius.
            double d = r * 2.0 / Math.Sqrt(5.0);

            // Nodes coordinates.
            g.Nodes[0].Point3D = p + new Vector3D(0.0, d * Math.Sqrt(5.0) / 2.0, 0.0);
            g.Nodes[11].Point3D = p - new Vector3D(0.0, d * Math.Sqrt(5.0) / 2.0, 0.0);
            for (int i = 1; i <= 5; i++)
            {
                g.Nodes[i].Point3D = p + new Vector3D(0.0, 0.5 * d, 0.0);
                g.Nodes[i].Point3D += new Vector3D(d, 0.0, 0.0);
                g.Nodes[i].Point3D.RotY(p, 2.0 * Math.PI / 5.0 * ((double)(i - 1)));
            }
            for (int i = 6; i <= 10; i++)
            {
                g.Nodes[i].Point3D = p - new Vector3D(0.0, 0.5 * d, 0.0);
                g.Nodes[i].Point3D += new Vector3D(d, 0.0, 0.0);
                g.Nodes[i].Point3D.RotY(p, 2.0 * Math.PI / 5.0 * ((double)(i - 1) + 0.5));
            }

            // Create edges.
            AddEdges(g, 0, 1, 5);
            AddEdges(g, 11, 6, 10);
            AddCycle(g, 1, 5);
            AddCycle(g, 6, 10);
            for (int i = 1; i <= 5; i++)
            {
                int i4 = i + 4;

                g.AddEdge(i, i + 5);
                g.AddEdge(i, (i4 > 5) ? i4 : (i4 + 5));
            }

            return g;
        }

        /// <summary>
        /// LCF notation graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="lengths">lengths array</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph LCFGraph(int n, int[] lengths, Circle circle)
        {
            Graph g = Cycle(n, circle);

            AddLCFChords(g, lengths);

            return g;
        }

        /// <summary>
        /// (3-4)-cage.
        /// It is K(4, 4), but layout differs.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Cage_3_4(Circle circle)
        {
            return LCFGraph(6, new int[] { 3 }, circle);
        }

        /// <summary>
        /// Wagner graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph WagnerGraph(Circle circle)
        {
            return LCFGraph(8, new int[] { 4 }, circle);
        }

        /// <summary>
        /// Franklin graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph FranklinGraph(Circle circle)
        {
            return LCFGraph(12, new int[] { 5, -5 }, circle);
        }

        /// <summary>
        /// Frucht graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph FruchtGraph(Circle circle)
        {
            return LCFGraph(12, new int[] { -5, -2, -4, 2, 5, -2, 2, 5, -2, -5, 4, 2 }, circle);
        }

        /// <summary>
        /// Heawood graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph HeawoodGraph(Circle circle)
        {
            return LCFGraph(14, new int[] { 5, -5 }, circle);
        }

        /// <summary>
        /// McGee graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph McGeeGraph(Circle circle)
        {
            return LCFGraph(24, new int[] { 12, 7, -7 }, circle);
        }

        /// <summary>
        /// Tutte-Coxeter graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph TutteCoxeterGraph(Circle circle)
        {
            return LCFGraph(30, new int[] { -13, -9, 7, -7, 9, 13 }, circle);
        }

        /// <summary>
        /// Pappus graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph PappusGraph(Circle circle)
        {
            return LCFGraph(18, new int[] { 5, 7, -7, 7, -7, -5 }, circle);
        }

        /// <summary>
        /// Dyck graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph DyckGraph(Circle circle)
        {
            return LCFGraph(32, new int[] { 5, -5, 13, -13 }, circle);
        }

        /// <summary>
        /// Gray graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph GrayGraph(Circle circle)
        {
            return LCFGraph(54, new int[] { -25, 7, -7, 13, -13, 25 }, circle);
        }

        /// <summary>
        /// Harries graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph HarriesGraph(Circle circle)
        {
            return LCFGraph(70, new int[] { -29, -19, -13, 13, 21, -27, 27, 33, -13, 13, 19, -21, -33, 29 }, circle);
        }

        /// <summary>
        /// Harries-Wong graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph HarriesWongGraph(Circle circle)
        {
            return LCFGraph(70,
                            new int[]
                            {
                                9, 25, 31, -17, 17, 33, 9, -29, -15, -9, 9, 25, -25, 29, 17, -9, 9, -27, 35, -9, 9,
                                -17, 21, 27, -29, -9, -25, 13, 19, -9, -33, -17, 19, -31, 27, 11, -25, 29, -33, 13,
                                -13, 21, -29, -21, 25, 9, -11, -19, 29, 9, -27, -19, -13, -35, -9, 9, 17, 25, -9, 9,
                                27, -27, -21, 15, -9, 29, -29, 33, -9, -25
                            },
                            circle);
        }

        /// <summary>
        /// (3-10) Balaban cage.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Balaban_3_10_Cage(Circle circle)
        {
            return LCFGraph(70,
                            new int[]
                            {
                                -9, -25, -19, 29, 13, 35, -13, -29, 19, 25, 9, -29, 29, 17, 33, 21, 9,-13, -31, -9,
                                25, 17, 9, -31, 27, -9, 17, -19, -29, 27, -17, -9, -29, 33, -25,25, -21, 17, -17, 29,
                                35, -29, 17, -17, 21, -25, 25, -33, 29, 9, 17, -27, 29, 19, -17, 9, -27, 31, -9, -17,
                                -25, 9, 31, 13, -9, -21, -33, -17, -29, 29
                            },
                            circle);
        }

        /// <summary>
        /// Foster graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph FosterGraph(Circle circle)
        {
            return LCFGraph(90, new int[] { 17, -9, 37, -37, 9, -17 }, circle);

        }

        /// <summary>
        /// Biggs-Smith graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph BiggsSmithGraph(Circle circle)
        {
            return LCFGraph(102,
                            new int[]
                            {
                                16, 24, -38, 17, 34, 48, -19, 41, -35, 47, -20, 34, -36, 21, 14, 48, -16, -36, -43, 28,
                                -17, 21, 29, -43, 46, -24, 28, -38, -14, -50, -45, 21, 8, 27, -21, 20, -37, 39, -34,
                                -44, -8, 38, -21, 25, 15, -34, 18, -28, -41, 36, 8, -29, -21, -48, -28, -20, -47, 14,
                                -8, -15, -27, 38, 24, -48, -18, 25, 38, 31, -25, 24, -46, -14, 28, 11, 21, 35, -39, 43,
                                36, -38, 14, 50, 43, 36, -11, -36, -24, 45, 8, 19, -25, 38, 20, -24, -14, -21, -8, 44,
                                -31, -38, -28, 37
                            },
                            circle);
        }

        /// <summary>
        /// (3-11) Balaban cage.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Balaban_3_11_Cage(Circle circle)
        {
            return LCFGraph(112,
                            new int[]
                            {
                                44, 26, -47, -15, 35, -39, 11, -27, 38, -37, 43, 14, 28, 51, -29, -16, 41, -11, -26, 15, 22,
                                -51, -35, 36, 52, -14, -33, -26, -46, 52, 26, 16, 43, 33, -15, 17, -53, 23, -42, -35, -28, 30,
                                -22, 45, -44, 16, -38, -16, 50, -55, 20, 28, -17, -43, 47, 34, -26, -41, 11, -36, -23, -16, 41,
                                17, -51, 26, -33, 47, 17, -11, -20, -30, 21, 29, 36, -43, -52, 10, 39, -28, -17, -52, 51, 26, 37,
                                -17, 10, -10, -45, -34, 17, -26, 27, -21, 46, 53, -10, 29, -50, 35, 15, -47, -29, -41, 26, 33,
                                55, -17, 42, -26, -36, 16
                            },
                            circle);
        }

        /// <summary>
        /// Ljubljana graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph LjubljanaGraph(Circle circle)
        {
            return LCFGraph(112,
                            new int[]
                            {
                                47, -23, -31, 39, 25, -21, -31, -41, 25, 15, 29, -41, -19, 15, -49, 33, 39, -35, -21, 17, -33,
                                49, 41, 31, -15, -29, 41, 31, -15, -25, 21, 31, -51, -25, 23, 9, -17, 51, 35, -29, 21, -51, -39,
                                33, -9, -51, 51, -47, -33, 19, 51, -21, 29, 21, -31, -39
                            },
                            circle);
        }

        /// <summary>
        /// (3-12) Tutte cage.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Tutte_3_12_Cage(Circle circle)
        {
            return LCFGraph(126, new int[] { 17, 27, -13, -59, -35, 35, -11, 13, -53, 53, -27, 21, 57, 11, -21, -57, 59, -17 }, circle);
        }

        /// <summary>
        /// Generalized Petersen graph.
        /// </summary>
        /// <param name="n">half order</param>
        /// <param name="k">length of chord in star</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph GeneralizedPetersenGraph(int n, int k, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, 2 * n);

            // Nodes layout.
            GraphLayoutManager.SetLayoutCircle(g, 0, n - 1, circle, Math.PI / 2.0);
            GraphLayoutManager.SetLayoutCircle(g, n, 2 * n - 1, circle.Scaled(0.5), Math.PI / 2.0);

            // Edges.
            AddCycle(g, 0, n - 1);
            AddShiftedCycle(g, n, 2 * n - 1, k);
            for (int i = 0; i < n; i++)
            {
                g.AddEdge(i, i + n);
            }

            return g;
        }

        /// <summary>
        /// Prism.
        /// </summary>
        /// <param name="n">half order</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph Prism(int n, Circle circle)
        {
            return GeneralizedPetersenGraph(n, 1, circle);
        }

        /// <summary>
        /// Petersen graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph PerersenGraph(Circle circle)
        {
            return GeneralizedPetersenGraph(5, 2, circle);
        }

        /// <summary>
        /// Durer graph.
        /// </summary>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph DurerGraph(Circle circle)
        {
            return GeneralizedPetersenGraph(6, 2, circle);
        }

        /// <summary>
        /// Mobius-Kantor graph.
        /// </summary>
        /// <param name="notation">notation</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph MobiusKantorGraph(Notation notation,
                                              Circle circle)
        {
            switch (notation)
            {
                case Notation.GeneralizedPetersen:
                    return GeneralizedPetersenGraph(8, 3, circle);

                case Notation.LederbergCoxeterFrucht:
                    return LCFGraph(16, new int[] { 5, -5 }, circle);

                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Desargues graph.
        /// </summary>
        /// <param name="notation">notation</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph DesarguesGraph(Notation notation,
                                           Circle circle)
        {
            switch (notation)
            {
                case Notation.GeneralizedPetersen:
                    return GeneralizedPetersenGraph(10, 3, circle);

                case Notation.LederbergCoxeterFrucht:
                    return LCFGraph(20, new int[] { 5, -5, 9, -5 }, circle);

                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Nauru graph.
        /// </summary>
        /// <param name="notation">notation</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph NauruGraph(Notation notation,
                                       Circle circle)
        {
            switch (notation)
            {
                case Notation.GeneralizedPetersen:
                    return GeneralizedPetersenGraph(12, 4, circle);

                case Notation.LederbergCoxeterFrucht:
                    return LCFGraph(24, new int[] { 5, -9, 7, -7, 9, -5 }, circle);

                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// 1D grid.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="rect">rectangle</param>
        /// <returns>graph</returns>
        public static Graph Grid1D(int n, Rect rect)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutLine2D(g,
                                               new Line2D(new Point2D(rect.Left,
                                                                      rect.Center.Y),
                                                          new Vector2D(rect.Width, 0.0)));
            AddChain(g, 0, g.Order - 1);

            return g;
        }

        /// <summary>
        /// 2D grid.
        /// </summary>
        /// <param name="xn">count of nodes <c>x</c></param>
        /// <param name="yn">count of nodes <c>y</c></param>
        /// <param name="rect">outer rectangle</param>
        /// <returns>graph</returns>
        public static Graph Grid2D(int xn, int yn, Rect rect)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, xn * yn);

            GraphLayoutManager.SetLayoutRectGrid(g, xn, yn, rect);

            // Edges.
            for (int y = 0; y < yn; y++)
            {
                for (int x = 0; x < xn; x++)
                {
                    int n = y * xn + x;

                    if (y != 0)
                    {
                        g.AddEdge(n, n - xn);
                    }

                    if (x != 0)
                    {
                        g.AddEdge(n, n - 1);
                    }
                }
            }

            return g;
        }

        /// <summary>
        /// 3D grid.
        /// </summary>
        /// <param name="xn">nodes count <c>x</c></param>
        /// <param name="yn">nodes count <c>y</c></param>
        /// <param name="zn">nodes count <c>z</c></param>
        /// <param name="par">parallelepiped</param>
        /// <returns>graph</returns>
        public static Graph Grid3D(int xn, int yn, int zn, Parallelepiped par)
        {
            Graph g = InitialGraph(GraphDimensionality.D3, xn * yn * zn);

            GraphLayoutManager.SetLayoutParallelepipedGrid(g, xn, yn, zn, par);

            // Edges.
            for (int z = 0; z < zn; z++)
            {
                for (int y = 0; y < yn; y++)
                {
                    for (int x = 0; x < xn; x++)
                    {
                        int n = z * yn * xn + y * xn + x;

                        if (z != 0)
                        {
                            g.AddEdge(n, n - xn * yn);
                        }

                        if (y != 0)
                        {
                            g.AddEdge(n, n - xn);
                        }

                        if (x != 0)
                        {
                            g.AddEdge(n, n - 1);
                        }
                    }
                }
            }

            return g;
        }

        /// <summary>
        /// Circular graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="lengths">chords lengths</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph CircularGraph(int n, int[] lengths, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);

            foreach (int length in lengths)
            {
                AddShiftedCycle(g, length);
            }

            return g;
        }

        /// <summary>
        /// Dual circular graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="color1">first color</param>
        /// <param name="color1_lengths">cholds lengths for first color</param>
        /// <param name="color2">seconds color</param>
        /// <param name="color2_lengths">chords lenghts for second color</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph DualCircularGraph(int n,
                                              Color color1, int[] color1_lengths,
                                              Color color2, int[] color2_lengths,
                                              Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);

            g.IsCopyEdgeDrawProperties = true;

            g.DrawProperties.DefaultEdgeDrawProperties = new EdgeDrawProperties(color1);

            foreach (int length in color1_lengths)
            {
                AddShiftedCycle(g, length);
            }

            g.DrawProperties.DefaultEdgeDrawProperties = new EdgeDrawProperties(color2);

            foreach (int length in color2_lengths)
            {
                AddShiftedCycle(g, length);
            }

            return g;
        }

        /// <summary>
        /// Circular graph of maximum order than do not contain red clique of
        /// order <c>r</c> and blue clique of order <c>b</c> for Ramsey number <c>R(r, b)</c>.
        /// </summary>
        /// <param name="r">red clique order</param>
        /// <param name="b">blue clique order</param>
        /// <param name="is_draw_blue">show blue edges</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph MaxCircularGraphWithoutCliquesForRamseyNumber(int r, int b,
                                                                          bool is_draw_blue, Circle circle)
        {
            int n = 0;
            int[] red_lengths = null;
            int[] blue_lengths = null;

            switch (r)
            {
                case 3:

                    switch (b)
                    {
                        case 3:
                            n = 5;
                            red_lengths = new int[] { 1 };
                            blue_lengths = new int[] { 2 };
                            break;

                        case 4:
                            n = 8;
                            red_lengths = new int[] { 1, 4 };
                            blue_lengths = new int[] { 2, 3 };
                            break;

                        case 5:
                            n = 13;
                            red_lengths = new int[] { 1, 5 };
                            blue_lengths = new int[] { 2, 3, 4, 6 };
                            break;

                        default:
                            Debug.Assert(false);
                            break;
                    }

                    break;

                case 4:

                    if (b == 4)
                    {
                        n = 17;
                        red_lengths = new int[] { 1, 2, 4, 8 };
                        blue_lengths = new int[] { 3, 5, 6, 7 };
                    }
                    else if (b == 5)
                    {
                        n = 24;
                        red_lengths = new int[] { 1, 2, 4, 8, 9 };
                        blue_lengths = new int[] { 3, 5, 6, 7, 10, 11, 12 };
                    }
                    else
                    {
                        Debug.Assert(false);
                    }

                    break;

                case 5:

                    if (b == 5)
                    {
                        n = 41;
                        red_lengths = new int[] { 1, 2, 3, 5, 7, 10, 13, 15, 16, 17 };
                        blue_lengths = new int[] { 4, 6, 8, 9, 11, 12, 14, 18, 19, 20 };
                    }
                    else
                    {
                        Debug.Assert(false);
                    }

                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            if (!is_draw_blue)
            {
                blue_lengths = new int[0];
            }

            Graph g = DualCircularGraph(n,
                                        new Color(System.Windows.Media.Colors.Blue), blue_lengths,
                                        new Color(System.Windows.Media.Colors.Red), red_lengths,
                                        circle);

            return g;
        }

        /// <summary>
        /// Hatch graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="double_mids">doubled middles for hatches begin</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph HatchGraph(int n, int[] double_mids, Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);

            foreach (int double_mid in double_mids)
            {
                AddHatch(g, double_mid);
            }

            return g;
        }

        /// <summary>
        /// Dual hatch graph.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="color1">first color</param>
        /// <param name="color1_double_mids">doubled middles for first color</param>
        /// <param name="color2">second color</param>
        /// <param name="color2_double_mids">doubled middles for second color</param>
        /// <param name="circle">outer circle</param>
        /// <param name="graph">graph</param>
        public static Graph DualHatchGraph(int n,
                                           Color color1, int[] color1_double_mids,
                                           Color color2, int[] color2_double_mids,
                                           Circle circle)
        {
            Graph g = InitialGraph(GraphDimensionality.D2, n);

            GraphLayoutManager.SetLayoutCircle(g, circle, Math.PI / 2.0);

            g.IsCopyEdgeDrawProperties = true;

            g.DrawProperties.DefaultEdgeDrawProperties = new EdgeDrawProperties(color1);

            foreach (int index in color1_double_mids)
            {
                AddHatch(g, index);
            }

            g.DrawProperties.DefaultEdgeDrawProperties = new EdgeDrawProperties(color2);

            foreach (int index in color2_double_mids)
            {
                AddHatch(g, index);
            }

            return g;
        }
    }
}
