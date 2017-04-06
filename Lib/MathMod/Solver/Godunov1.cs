using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.SolidGrid;

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
        public static double gamma = 1.0;

        /// <summary>
        /// Flow in X direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <param name="p">pressure</param>
        /// <returns>flow in X direction</returns>
        public static Q FlowX(U u, double p)
        {
            double q_rho = u.rho * u.vx;
            double q_vx = u.rho * u.vx * u.vx + p;
            double q_vy = u.rho * u.vx * u.vy;
            double q_vz = u.rho * u.vx * u.vz;
            double q_E = u.rho * u.vx * (u.eps + 0.5 * (u.vx * u.vx + u.vy * u.vy + u.vz * u.vz)) + p * u.vx;

            return new Q(q_rho, q_vx, q_vy, q_vz, q_E);
        }

        /// <summary>
        /// Flow in Y direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <param name="p">pressure</param>
        /// <returns>flow in Y direction</returns>
        public static Q FlowY(U u, double p)
        {
            double q_rho = u.rho * u.vy;
            double q_vx = u.rho * u.vy * u.vx;
            double q_vy = u.rho * u.vy * u.vy + p;
            double q_vz = u.rho * u.vy * u.vz;
            double q_E = u.rho * u.vy * (u.eps + 0.5 * (u.vx * u.vx + u.vy * u.vy + u.vz * u.vz)) + p * u.vy;

            return new Q(q_rho, q_vx, q_vy, q_vz, q_E);
        }

        /// <summary>
        /// Flow in Z direction.
        /// </summary>
        /// <param name="u">U</param>
        /// <param name="p">pressure</param>
        /// <returns>flow in Z direction</returns>
        public static Q FlowZ(U u, double p)
        {
            double q_rho = u.rho * u.vz;
            double q_vx = u.rho * u.vz * u.vx;
            double q_vy = u.rho * u.vz * u.vy;
            double q_vz = u.rho * u.vz * u.vz + p;
            double q_E = u.rho * u.vz * (u.eps + 0.5 * (u.vx * u.vx + u.vy * u.vy + u.vz * u.vz)) + p * u.vz;

            return new Q(q_rho, q_vx, q_vy, q_vz, q_E);
        }

        /// <summary>
        /// One iteration of the method.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="dt">time step</param>
        public static void Iter(SolidGrid.SolidGrid g, double dt)
        {
            // Calculate U to D for all.
            for (int i = 0; i < g.XISize; i++)
            {
                for (int j = 0; j < g.YISize; j++)
                {
                    for (int k = 0; k < g.ZISize; k++)
                    {
                        g.Cells[i, j, k].UtoD(g.CellV);
                    }
                }
            }

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
                        Q q = FlowX(u, (gamma - 1.0) * u.rho * u.eps);

                        q.Mul(g.CellFacetS * dt);
                        c1.D.SubQ(q);
                        c2.D.AddQ(q);
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
                        Q q = FlowY(u, (gamma - 1.0) * u.rho * u.eps);

                        q.Mul(g.CellFacetS * dt);
                        c1.D.SubQ(q);
                        c2.D.AddQ(q);
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
                        Q q = FlowZ(u, (gamma - 1.0) * u.rho * u.eps);

                        q.Mul(g.CellFacetS * dt);
                        c1.D.SubQ(q);
                        c2.D.AddQ(q);
                    }
                }
            }

            // Calculate D to U for all.
            for (int i = 0; i < g.XISize; i++)
            {
                for (int j = 0; j < g.YISize; j++)
                {
                    for (int k = 0; k < g.ZISize; k++)
                    {
                        g.Cells[i, j, k].DtoU(g.CellV);
                    }
                }
            }
        }
    }
}
