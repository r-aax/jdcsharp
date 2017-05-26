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
        /// Gamma parameter.
        /// </summary>
        public static double gamma = 1.66;

        /// <summary>
        /// Pressure from density and epsilon.
        /// </summary>
        /// <param name="rho">density</param>
        /// <param name="eps">epsilon</param>
        /// <returns>pressure</returns>
        public static double Pressure(double rho, double eps)
        {
            return (gamma - 1.0) * rho * eps;
        }

        /// <summary>
        /// Pressure from U vector.
        /// </summary>
        /// <param name="u">U vector</param>
        /// <returns>pressure</returns>
        public static double Pressure(U u)
        {
            return Pressure(u.rho, u.eps);
        }

        /// <summary>
        /// Flow in X direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <returns>flow in X direction</returns>
        public static Q FlowX(U u)
        {
            double p = Pressure(u);
            double q_rho = u.rho * u.v.X;
            Vector3D q_v = q_rho * u.v;
            q_v.X += p;
            double q_E = q_rho * u.E + p * u.v.X;

            return new Q(q_rho, q_v, q_E);
        }

        /// <summary>
        /// Flow in Y direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <returns>flow in Y direction</returns>
        public static Q FlowY(U u)
        {
            double p = Pressure(u);
            double q_rho = u.rho * u.v.Y;
            Vector3D q_v = q_rho * u.v;
            q_v.Y += p;
            double q_E = q_rho * u.E + p * u.v.Y;

            return new Q(q_rho, q_v, q_E);
        }

        /// <summary>
        /// Flow in Z direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <returns>flow in Z direction</returns>
        public static Q FlowZ(U u)
        {
            double p = Pressure(u);
            double q_rho = u.rho * u.v.Z;
            Vector3D q_v = q_rho * u.v;
            q_v.Z += p;
            double q_E = q_rho * u.E + p * u.v.Z;

            return new Q(q_rho, q_v, q_E);
        }

        /// <summary>
        /// Sub and add flow.
        /// </summary>
        /// <param name="d_sub">D vector for subtraction</param>
        /// <param name="d_add">D vector for addition</param>
        /// <param name="q">flow</param>
        /// <param name="dv">volume (delta)</param>
        public static void SubAddQ(D d_sub, D d_add, Q q, double dv)
        {
            q.Mul(dv);
            d_sub.SubQ(q);
            d_add.AddQ(q);
        }

        /// <summary>
        /// One iteration of the method.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">time step</param>
        public static void Iter(SolidGrid.SolidGrid g, double dt)
        {
            g.UtoD();

            // Calculate flows in X direction.
            for (int i = 0; i < g.XISize - 1; i++)
            {
                for (int j = 0; j < g.YISize; j++)
                {
                    for (int k = 0; k < g.ZISize; k++)
                    {
                        Cell c1 = g.Cells[i, j, k];
                        Cell c2 = g.Cells[i + 1, j, k];
                        U u = Riemann.Stub(c1.U, c2.U);
                        Q q = FlowX(u);

                        SubAddQ(c1.D, c2.D, q, g.CellFacetS * dt);
                    }
                }
            }

            // Calculate flows in Y direction.
            for (int i = 0; i < g.XISize; i++)
            {
                for (int j = 0; j < g.YISize - 1; j++)
                {
                    for (int k = 0; k < g.ZISize; k++)
                    {
                        Cell c1 = g.Cells[i, j, k];
                        Cell c2 = g.Cells[i, j + 1, k];
                        U u = Riemann.Stub(c1.U, c2.U);
                        Q q = FlowY(u);

                        SubAddQ(c1.D, c2.D, q, g.CellFacetS * dt);
                    }
                }
            }
            
            // Calculate flows in Z direction.
            for (int i = 0; i < g.XISize; i++)
            {
                for (int j = 0; j < g.YISize; j++)
                {
                    for (int k = 0; k < g.ZISize - 1; k++)
                    {
                        Cell c1 = g.Cells[i, j, k];
                        Cell c2 = g.Cells[i, j, k + 1];
                        U u = Riemann.Stub(c1.U, c2.U);
                        Q q = FlowZ(u);

                        SubAddQ(c1.D, c2.D, q, g.CellFacetS * dt);
                    }
                }
            }

            // All borders are hard.

            // X borders.
            for (int j = 0; j < g.YISize; j++)
            {
                for (int k = 0; k < g.ZISize; k++)
                {
                    Cell c;
                    U u;
                    Q q;

                    // Left border.
                    c = g.Cells[0, j, k];
                    u = Riemann.Stub(c.U.Mirror(AxisType.X), c.U);
                    q = FlowX(u);
                    q.Mul(g.CellFacetS * dt);
                    c.D.AddQ(q);

                    // Right border.
                    c = g.Cells[g.XISize - 1, j, k];
                    u = Riemann.Stub(c.U, c.U.Mirror(AxisType.X));
                    q = FlowX(u);
                    q.Mul(g.CellFacetS * dt);
                    c.D.SubQ(q);
                }
            }

            g.DtoU();
        }
    }
}
