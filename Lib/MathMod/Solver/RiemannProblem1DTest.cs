using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.Maths.Numbers;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Test for Riemann problem.
    /// </summary>
    public class RiemannProblem1DTest
    {
        /// <summary>
        /// Length of test scope.
        /// </summary>
        public double XLength;

        /// <summary>
        /// Cells count.
        /// </summary>
        public int CellsCount;

        /// <summary>
        /// rho on the left side.
        /// </summary>
        public double rho_l;

        /// <summary>
        /// <c>X</c> component of velocity on the left side.
        /// </summary>
        public double vX_l;

        /// <summary>
        /// Pressure on the left side.
        /// </summary>
        public double p_l;

        /// <summary>
        /// rho on the right side.
        /// </summary>
        public double rho_r;

        /// <summary>
        /// <c>X</c> component of velocity on the right side.
        /// </summary>
        public double vX_r;

        /// <summary>
        /// Pressure on the right side.
        /// </summary>
        public double p_r;

        /// <summary>
        /// Interval for density.
        /// </summary>
        public Interval rho_int;

        /// <summary>
        /// Interval for <c>X</c> component of velocity.
        /// </summary>
        public Interval vX_int;

        /// <summary>
        /// Interval for pressure.
        /// </summary>
        public Interval p_int;

        /// <summary>
        /// Interval for inner energy.
        /// </summary>
        public Interval eps_int;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <param name="rho_l_">left side density</param>
        /// <param name="vX_l_">left side velocity</param>
        /// <param name="p_l_">left side pressure</param>
        /// <param name="rho_r_">right side density</param>
        /// <param name="vX_r_">right side velocity</param>
        /// <param name="p_r_">right side pressure</param>
        /// <param name="rho_int_lo">density low value</param>
        /// <param name="rho_int_hi">density high value</param>
        /// <param name="vX_int_lo">velocity low value</param>
        /// <param name="vX_int_hi">velocity high value</param>
        /// <param name="p_int_lo">pressure low value</param>
        /// <param name="p_int_hi">pressure high value</param>
        /// <param name="eps_int_lo">inner energy low value</param>
        /// <param name="eps_int_hi">inner energy high value</param>
        public RiemannProblem1DTest(double x_length, int cells_count,
                                    double rho_l_, double vX_l_, double p_l_,
                                    double rho_r_, double vX_r_, double p_r_,
                                    double rho_int_lo, double rho_int_hi,
                                    double vX_int_lo, double vX_int_hi,
                                    double p_int_lo, double p_int_hi,
                                    double eps_int_lo, double eps_int_hi)
        {
            XLength = x_length;
            CellsCount = cells_count;
            rho_l = rho_l_;
            vX_l = vX_l_;
            p_l = p_l_;
            rho_r = rho_r_;
            vX_r = vX_r_;
            p_r = p_r_;
            rho_int = new Interval(rho_int_lo, rho_int_hi);
            vX_int = new Interval(vX_int_lo, vX_int_hi);
            p_int = new Interval(p_int_lo, p_int_hi);
            eps_int = new Interval(eps_int_lo, eps_int_hi);
        }

        /// <summary>
        /// Sod problem.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Sod(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            1.0, 0.0, 1.0, 0.125, 0.0, 0.1,
                                            0.0, 1.0,
                                            0.0, 1.0,
                                            0.0, 1.0,
                                            0.0, 1.0);
        }

        /// <summary>
        /// Modified Sod problem.
        /// </summary>
        /// <param name="xlength">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest ModifiedSod(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            1.0, 0.75, 1.0, 0.125, 0.0, 0.1,
                                            0.0, 1.0,
                                            0.0, 1.0,
                                            0.0, 1.0,
                                            0.0, 1.0);
        }

        /// <summary>
        /// 123 problem.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Problem123(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            1.0, -2.0, 0.4, 1.0, 2.0, 0.4,
                                            0.0, 1.0,
                                            -2.0, 2.0,
                                            0.0, 0.4,
                                            0.0, 1.0);
        }

        /// <summary>
        /// Woodward & Collella left side.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Woodward_Colella_Left(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            1.0, 0.0, 1000.0, 1.0, 0.0, 0.01,
                                            0.0, 6.0,
                                            0.0, 25.0,
                                            0.0, 1000.0,
                                            0.0, 2600.0);
        }

        /// <summary>
        /// Woodward & Collella right side.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Woodward_Collela_Right(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            1.0, 0.0, 0.01, 1.0, 0.0, 100.0,
                                            0.0, 6.0,
                                            -7.0, 0.0,
                                            10.0, 100.0,
                                            0.0, 260.0);
        }

        /// <summary>
        /// Woodward & Collella collisio.
        /// </summary>
        /// <param name="x_length">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Woodward_Collella_Collision(double xlength, int cells_count)
        {
            return new RiemannProblem1DTest(xlength, cells_count,
                                            5.99924, 19.5975, 460.894, 5.99242, -6.19633, 46.0950,
                                            0.0, 40.0,
                                            0.0, 25.0,
                                            0.0, 2000.0,
                                            0.0, 400.0);
        }

        /// <summary>
        /// Random test.
        /// </summary>
        /// <param name="xlength">scope length</param>
        /// <param name="cells_count">cells count</param>
        /// <returns>test</returns>
        public static RiemannProblem1DTest Random(double xlength, int cells_count)
        {
            Interval it = new Interval(0.0, 5.0);

            return new RiemannProblem1DTest(xlength, cells_count,
                                            Randoms.RandomInInterval(it), 0.0, Randoms.RandomInInterval(it),
                                            Randoms.RandomInInterval(it), 0.0, Randoms.RandomInInterval(it),
                                            it.L, it.H, it.L, it.H, it.L, it.H, it.L, it.H);
        }
    }
}
