using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Riemann solver.
    /// </summary>
    public class Riemann
    {
        /// <summary>
        /// Gamma value.
        /// </summary>
        public const double gama = 1.4;

        /// <summary>
        /// g1
        /// </summary>
        public const double g1 = (gama - 1.0) / (2.0 * gama);

        /// <summary>
        /// g2
        /// </summary>
        public const double g2 = (gama + 1.0) / (2.0 * gama);

        /// <summary>
        /// g3
        /// </summary>
        public const double g3 = 2.0 * gama / (gama - 1.0);

        /// <summary>
        /// g4
        /// </summary>
        public const double g4 = 2.0 / (gama - 1.0);

        /// <summary>
        /// g5
        /// </summary>
        public const double g5 = 2.0 / (gama + 1.0);

        /// <summary>
        /// g6
        /// </summary>
        public const double g6 = (gama - 1.0) / (gama + 1.0);

        /// <summary>
        /// g7
        /// </summary>
        public const double g7 = (gama - 1.0) / 2.0;

        /// <summary>
        /// g8
        /// </summary>
        public const double g8 = gama - 1.0;

        // Exact Riemann solver for the Euler equations in one dimension
        // Translated from the Fortran code er1pex.f and er1pex.ini
        // by Dr.E.F.Toro downloaded from
        // http://www.numeritek.com/numerica_software.html#freesample
        // With some modifications for C# code.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="ul"></param>
        /// <param name="pl"></param>
        /// <param name="cl"></param>
        /// <param name="dr"></param>
        /// <param name="ur"></param>
        /// <param name="pr"></param>
        /// <param name="cr"></param>
        /// <param name="pm"></param>
        public static void guessp(double dl, double ul, double pl, double cl,
                                  double dr, double ur, double pr, double cr,
                                  out double pm)
        {
            double cup, gel, ger, pmax, pmin, ppv, pq, ptl, ptr, qmax, quser, um;
            quser = 2.0;

            // compute guess pressure from PVRS Riemann solver
            cup = 0.25 * (dl + dr) * (cl + cr);
            ppv = 0.5 * (pl + pr) + 0.5 * (ul - ur) * cup;
            ppv = Math.Max(0.0, ppv);
            pmin = Math.Min(pl, pr);
            pmax = Math.Max(pl, pr);
            qmax = pmax / pmin;

            if ((qmax <= quser) && (pmin <= ppv) && (ppv <= pmax))
            {
                pm = ppv; // select PVRS Riemann solver
            }
            else
            {
                if (ppv < pmin)
                {
                    // select Two-Rarefaction Riemann solver
                    pq = Math.Pow(pl / pr, g1);
                    um = (pq * ul / cl + ur / cr + g4 * (pq - 1.0)) / (pq / cl + 1.0 / cr);
                    ptl = 1.0 + g7 * (ul - um) / cl;
                    ptr = 1.0 + g7 * (um - ur) / cr;
                    pm = 0.5 * (Math.Pow(pl * ptl, g3) + Math.Pow(pr * ptr, g3));
                }
                else
                {
                    // select Two-Shock Riemann solver with PVRS as estimate
                    gel = Math.Sqrt((g5 / dl) / (g6 * pl + ppv));
                    ger = Math.Sqrt((g5 / dr) / (g6 * pr + ppv));
                    pm = (gel * pl + ger * pr - (ur - ul)) / (gel + ger);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="fd"></param>
        /// <param name="p"></param>
        /// <param name="dk"></param>
        /// <param name="pk"></param>
        /// <param name="ck"></param>
        public static void prefun(out double f, out double fd, double p, double dk, double pk, double ck)
        {
            double ak, bk, pratio, qrt;

            if (p <= pk)
            {
                // rarefaction wave
                pratio = p / pk;
                f = g4 * ck * (Math.Pow(pratio, g1) - 1.0);
                fd = (1.0 / (dk* ck)) * Math.Pow(pratio, -g2);
            }
            else
            {
                //  shock wave
                ak = g5 / dk;
                bk = g6* pk;
                qrt = Math.Sqrt(ak / (bk + p));
                f = (p - pk) * qrt;
                fd = (1.0 - 0.5 * (p - pk) / (bk + p)) * qrt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="ul"></param>
        /// <param name="pl"></param>
        /// <param name="cl"></param>
        /// <param name="dr"></param>
        /// <param name="ur"></param>
        /// <param name="pr"></param>
        /// <param name="cr"></param>
        /// <param name="p"></param>
        /// <param name="u"></param>
        public static void starpu(double dl, double ul, double pl, double cl,
                                  double dr, double ur, double pr, double cr,
                                  ref double p, out double u)
        {
            const int nriter = 20;
            const double tolpre = 1.0e-6;
            double change, fl, fld, fr, frd, pold, pstart, udiff;
            int i;

            fl = 0.0;
            fr = 0.0;

            // guessed value pstart is computed
            pstart = 0.0;
            guessp(dl, ul, pl, cl, dr, ur, pr, cr, out pstart);
            pold = pstart;
            udiff = ur - ul;

            for (i = 1 ; i <= nriter; i++)
            {
                prefun(out fl, out fld, pold, dl, pl, cl);
                prefun(out fr, out frd, pold, dr, pr, cr);
                p = pold - (fl + fr + udiff) / (fld + frd);
                change = 2.0 * Math.Abs((p - pold) / (p + pold));

                if (change <= tolpre)
                {
                    break;
                }

                if (p < 0.0)
                {
                    p = tolpre;
                }

                pold = p;
            }

            if (i > nriter)
            {
                throw new Exception("divergence in Newton-Raphson iteration");
            }

            // compute velocity in star region
            u = 0.5 * (ul + ur + fr - fl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="ul"></param>
        /// <param name="pl"></param>
        /// <param name="cl"></param>
        /// <param name="dr"></param>
        /// <param name="ur"></param>
        /// <param name="pr"></param>
        /// <param name="cr"></param>
        /// <param name="pm"></param>
        /// <param name="um"></param>
        /// <param name="d"></param>
        /// <param name="u"></param>
        /// <param name="p"></param>
        public static void sample(double dl, double ul, double pl, double cl,
                                  double dr, double ur, double pr, double cr,
                                  double pm, double um, out double d, out double u, out double p)
        {
            double c, cml, cmr, pml, pmr, shl, shr, sl, sr, stl, str;

            if (um >= 0.0)
            {
                // sampling point lies to the left of the contact discontinuity
                if (pm <= pl)
                {
                    // left rarefaction
                    shl = ul - cl;

                    if (shl >= 0.0)
                    {
                        // sampled point is left data state
                        d = dl;
                        u = ul;
                        p = pl;
                    }
                    else
                    {
                        cml = cl * Math.Pow(pm / pl, g1);
                        stl = um - cml;

                        if (stl< 0.0)
                        {
                            // sampled point is star left state
                            d = dl * Math.Pow(pm / pl, 1.0 / gama);
                            u = um;
                            p = pm;
                        }
                        else
                        {
                            // sampled point is inside left fan
                            u = g5 * (cl + g7 * ul);
                            c = g5 * (cl + g7 * ul);
                            d = dl * Math.Pow(c / cl, g4);
                            p = pl * Math.Pow(c / cl, g3);
                        }
                    }
                }
                else
                {
                    // left shock
                    pml = pm / pl;
                    sl = ul - cl * Math.Sqrt(g2 * pml + g1);

                    if (sl >= 0.0)
                    {
                        // sampled point is left data state
                        d = dl;
                        u = ul;
                        p = pl;
                    }
                    else
                    {
                        // sampled point is star left state
                        d = dl * (pml + g6) / (pml * g6 + 1.0);
                        u = um;
                        p = pm;
                    }
                }
            }
            else
            {
                // sampling point lies to the right of the contact discontinuity
                if (pm > pr)
                {
                    // right shock
                    pmr = pm / pr;
                    sr  = ur + cr * Math.Sqrt(g2 * pmr + g1);

                    if (sr <= 0.0)
                    {
                        // sampled point is right data state
                        d = dr;
                        u = ur;
                        p = pr;
                    }
                    else
                    {
                        // sampled point is star right state
                        d = dr * (pmr + g6) / (pmr * g6 + 1.0);
                        u = um;
                        p = pm;
                    }
                }
                else
                {
                    // right rarefaction
                    shr = ur + cr;

                    if (shr <= 0.0)
                    {
                        // sampled point is right data state
                        d = dr;
                        u = ur;
                        p = pr;
                    }
                    else
                    {
                        cmr = cr * Math.Pow(pm / pr, g1);
                        str = um + cmr;

                        if (str >= 0.0)
                        {
                            // sampled point is star right state
                            d = dr * Math.Pow(pm / pr, 1.0 / gama);
                            u = um;
                            p = pm;
                        }
                        else
                        {
                            // sampled point is inside left fan
                            u = g5 * (-cr + g7 * ur);
                            c = g5 * (cr - g7 * ur);
                            d = dr * Math.Pow(c / cr, g4);
                            p = pr * Math.Pow(c / cr, g3);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="ul"></param>
        /// <param name="pl"></param>
        /// <param name="dr"></param>
        /// <param name="ur"></param>
        /// <param name="pr"></param>
        /// <param name="d"></param>
        /// <param name="u"></param>
        /// <param name="p"></param>
        public static void riemann(double dl, double ul, double pl,
                                   double dr, double ur, double pr,
                                   out double d, out double u, out double p)
        {
            double pm, um, cl, cr;

            pm = 0.0;

            // compute sound speeds
            cl = Math.Sqrt(gama * pl / dl);
            cr = Math.Sqrt(gama * pr / dr);

            // the pressure positivity condition is tested for
            if (g4 * (cl + cr) <= (ur - ul))
            {
                throw new Exception("the initial data is such that vacuum is generated");
            }

            // exact solution for pressure and velocity in star region is found
            starpu(dl, ul, pl, cl, dr, ur, pr, cr, ref pm, out um);
            sample(dl, ul, pl, cl, dr, ur, pr, cr, pm, um, out d, out u, out p);
        }

        /// <summary>
        /// Stub for Riemann task.
        /// </summary>
        /// <param name="u1">left side of Riemann task</param>
        /// <param name="u2">right side of Riemann task</param>
        /// <returns><c>U</c> data (simple case)</returns>
        public static U Stub(U u1, U u2)
        {
            U ru = new U();

            ru.rho = 0.5 * (u1.rho + u2.rho);
            ru.v = 0.5 * (u1.v + u2.v);
            ru.p = 0.5 * (u1.p + u2.p);

            return ru;
        }

        /// <summary>
        /// One-dimensiona E. F. Toro solution.
        /// </summary>
        /// <param name="ul">left side U</param>
        /// <param name="ur">right side U</param>
        /// <returns>U on the cells common border</returns>
        public static U X_Toro(U ul, U ur)
        {
            double d, u, p;
            U ru = new U();

            riemann(ul.rho, ul.v.X, ul.p, ur.rho, ur.v.X, ur.p, out d, out u, out p);

            ru.rho = d;
            ru.v = new Maths.Geometry.Geometry3D.Vector(u, 0.0, 0.0);
            ru.p = p;

            return ru;
        }
    }
}
