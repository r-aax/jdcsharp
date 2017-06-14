using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.SolidGrid;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Godunov's method 1 order of precision.
    /// </summary>
    public class Godunov1
    {
        /// <summary>
        /// Do several iterations of Godunov's method.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">delta time</param>
        /// <param name="n">iterations count</param>
        public static void Iters(SolidGrid.SolidGrid g, double dt, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Iter(g, dt);
            }
        }

        /// <summary>
        /// Solid grid iteration.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">delta time</param>
        public static void Iter(SolidGrid.SolidGrid g, double dt)
        {
            g.UtoD();

            for (int xi = 0; xi < g.NX; xi++)
            {
                for (int yi = 0; yi < g.NY; yi++)
                {
                    for (int zi = 0; zi < g.NZ; zi++)
                    {
                        Iter(g, xi, yi, zi, dt);
                    }
                }
            }

            g.DtoU();
        }

        /// <summary>
        /// Iteration for one cell.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="xi">index in X direction</param>
        /// <param name="yi">index in Y direction</param>
        /// <param name="zi">index in Z direction</param>
        /// <param name="dt">delta time</param>
        public static void Iter(SolidGrid.SolidGrid g, int xi, int yi, int zi, double dt)
        {
            U u = g.Cells[xi, yi, zi].U;
            D d = g.Cells[xi, yi, zi].D;
            U ru;
            D flow;
            double k = g.CellS * dt;

            // X+
            ru = Riemann.Stub(u, g.XNextU(xi, yi, zi));
            flow = ru.FlowX;
            d -= flow * k;
            
            // X-
            ru = Riemann.Stub(g.XPrevU(xi, yi, zi), u);
            flow = ru.FlowX;
            d += flow * k;

            // Y+
            //ru = Riemann.Stub(u, g.YNextU(xi, yi, zi));
            //flow = ru.FlowY;
            //d -= flow * k;

            // Y-
            //ru = Riemann.Stub(g.YPrevU(xi, yi, zi), u);
            //flow = ru.FlowY;
            //d += flow * k;

            // Z+
            //ru = Riemann.Stub(u, g.ZNextU(xi, yi, zi));
            //flow = ru.FlowZ;
            //d -= flow * k;

            // Z-
            //ru = Riemann.Stub(g.ZNextU(xi, yi, zi), u);
            //flow = ru.FlowZ;
            //d += flow * k;

            g.Cells[xi, yi, zi].D = d;
        }
    }
}
