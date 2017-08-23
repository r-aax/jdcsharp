using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Riemann solver.
    /// Not modified.
    /// </summary>
    class RiemannToroOrig
    {
        // Exact Riemann solver for the Euler equations in one dimension
        // Translated from the Fortran code er1pex.f and er1pex.ini
        // by Dr.E.F.Toro downloaded from
        // http://www.numeritek.com/numerica_software.html#freesample
        // With some modifications for C# code.

        /// <summary>
        /// 
        /// </summary>
        public static double gama = 1.4;

        /// <summary>
        /// 
        /// </summary>
        public static double g1 = (gama - 1.0) / (2.0 * gama);

        /// <summary>
        /// 
        /// </summary>
        public static double g2 = (gama + 1.0) / (2.0 * gama);

        /// <summary>
        /// 
        /// </summary>
        public static double g3 = 2.0 * gama / (gama - 1.0);

        /// <summary>
        /// 
        /// </summary>
        public static double g4 = 2.0 / (gama - 1.0);

        /// <summary>
        /// 
        /// </summary>
        public static double g5 = 2.0 / (gama + 1.0);

        /// <summary>
        /// 
        /// </summary>
        public static double g6 = (gama - 1.0) / (gama + 1.0);

        /// <summary>
        /// 
        /// </summary>
        public static double g7 = (gama - 1.0) / 2.0;

        /// <summary>
        /// 
        /// </summary>
        public static double g8 = gama - 1.0;

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
                    double dr, double ur, double pr, double cr, out double pm)
        {
            // purpose: to provide a guessed value for pressure
            //          pm in the Star Region. The choice is made
            //          according to adaptive Riemann solver using
            //          the PVRS, TRRS and TSRS approximate
            //          Riemann solvers. See Sect. 9.5 of Chapt. 9 of Ref. 1

            double cup, gel, ger, pmax, pmin, ppv, pq, ptl, ptr,
                   qmax, quser, um;

            quser = 2.0;

            // compute guess pressure from PVRS Riemann solver
            cup = 0.25 * (dl + dr) * (cl + cr);
            ppv = 0.5 * (pl + pr) + 0.5 * (ul - ur) * cup;
            ppv = Math.Max(0.0, ppv);
            pmin = Math.Min(pl, pr);
            pmax = Math.Max(pl, pr);
            qmax = pmax / pmin;

            if (qmax <= quser && (pmin <= ppv && ppv <= pmax))
                pm = ppv;     // select PVRS Riemann solver
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
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        public static void prefun(
            out double f,
            out double fd,
            double p,
            double dk,
            double pk,
            double ck)
        {
            // purpose: to evaluate the pressure functions
            //          fl and fr in exact Riemann solver
            //          and their first derivatives

            double ak, bk, pratio, qrt;

            if (p <= pk)
            {
                // rarefaction wave
                pratio = p / pk;
                f = g4 * ck * (Math.Pow(pratio, g1) - 1.0);
                fd = (1.0 / (dk * ck)) * Math.Pow(pratio, -g2);
            }
            else
            {
                //  shock wave
                ak = g5 / dk;
                bk = g6 * pk;
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
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        public static void starpu(double dl, double ul, double pl, double cl,
                    double dr, double ur, double pr, double cr,
            out double p,
            out double u)
        {
            // purpose: to compute the solution for pressure and
            //          velocity in the Star Region

            const int nriter = 20;
            const double tolpre = 1.0e-6;
            double change, fl, fld, fr, frd, pold, pstart, udiff;

            fl = 0.0;
            fr = 0.0;
            p = 0.0;

            // guessed value pstart is computed
            guessp(dl, ul, pl, cl, dr, ur, pr, cr, out pstart);
            pold = pstart;
            udiff = ur - ul;

            int i = 1;
            for (; i <= nriter; i++)
            {
                prefun(out fl, out fld, pold, dl, pl, cl);
                prefun(out fr, out frd, pold, dr, pr, cr);
                p = pold - (fl + fr + udiff) / (fld + frd);
                change = 2.0 * Math.Abs((p - pold) / (p + pold));
                if (change <= tolpre)
                    break;
                if (p < 0.0)
                    p = tolpre;
                pold = p;
            }
            if (i > nriter)
            {
                throw new Exception("divergence in Newton-Raphson iteration");
            }

            // compute velocity in star region
            u = 0.5 * (ul + ur + fr - fl);
        }

        public static void sample(double pm, double um, double s,
                    double dl, double ul, double pl, double cl,
                    double dr, double ur, double pr, double cr,
                    out double d, out double u, out double p)
{
    // purpose: to sample the solution throughout the wave
    //          pattern. Pressure pm and velocity um in the
    //          star region are known. Sampling is performed
    //          in terms of the 'speed' s = x/t. Sampled
    //          values are d, u, p

    double c, cml, cmr, pml, pmr, shl, shr, sl, sr, stl, str;

    if (s <= um) {
        // sampling point lies to the left of the contact discontinuity
        if (pm <= pl) {
            // left rarefaction
            shl = ul - cl;
            if (s <= shl) {
                // sampled point is left data state
                d = dl;
                u = ul;
                p = pl;
            } else {
                cml = cl* Math.Pow(pm/pl, g1);
    stl = um - cml;
                if (s > stl) {
                    // sampled point is star left state
                    d = dl* Math.Pow(pm/pl, 1.0/gama);
    u = um;
                    p = pm;
                } else {
                    // sampled point is inside left fan
                    u = g5*(cl + g7* ul + s);
                    c = g5*(cl + g7*(ul - s));
                    d = dl* Math.Pow(c/cl, g4);
p = pl* Math.Pow(c/cl, g3);
                }
            }
        } else {
            // left shock
            pml = pm/pl;
            sl = ul - cl* Math.Sqrt(g2* pml + g1);
            if (s <= sl) {
                // sampled point is left data state
                d = dl;
                u = ul;
                p = pl;
            } else {
                // sampled point is star left state
                d = dl*(pml + g6)/(pml* g6 + 1.0);
                u = um;
                p = pm;
            }
        }
    } else {
        // sampling point lies to the right of the contact discontinuity
        if (pm > pr) {
            // right shock
            pmr = pm/pr;
            sr  = ur + cr* Math.Sqrt(g2* pmr + g1);
            if (s >= sr) {
                // sampled point is right data state
                d = dr;
                u = ur;
                p = pr;
            } else {
                // sampled point is star right state
                d = dr*(pmr + g6)/(pmr* g6 + 1.0);
                u = um;
                p = pm;
            }
        } else {
            // right rarefaction
            shr = ur + cr;
            if (s >= shr) {
                // sampled point is right data state
                d = dr;
                u = ur;
                p = pr;
            } else {
                cmr = cr* Math.Pow(pm/pr, g1);
str = um + cmr;
                if (s <= str) {
                    // sampled point is star right state
                    d = dr* Math.Pow(pm/pr, 1.0/gama);
u = um;
                    p = pm;
                } else {
                    // sampled point is inside left fan
                    u = g5*(-cr + g7* ur + s);
                    c = g5*(cr - g7*(ur - s));
                    d = dr* Math.Pow(c/cr, g4);
p = pr* Math.Pow(c/cr, g3);
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
            double pm, um;

            // compute sound speeds
            double cl = Math.Sqrt(gama * pl / dl);
            double cr = Math.Sqrt(gama * pr / dr);

            // the pressure positivity condition is tested for
            if (g4 * (cl + cr) <= (ur - ul))
            {

                throw new Exception("the initial data is such that vacuum is generated");
            }

            // exact solution for pressure and velocity in star region is found
            starpu(dl, ul, pl, cl, dr, ur, pr, cr, out pm, out um);
            sample(pm, um, 0.0, dl, ul, pl, cl, dr, ur, pr, cr, out d, out u, out p);
        }

        /// <summary>
        /// Stub for Riemann solver.
        /// </summary>
        /// <param name="u1">left side vector</param>
        /// <param name="u2">right side vector</param>
        /// <returns>result</returns>
        public static U Stub(U u1, U u2)
        {
            U ru = new U();

            ru.rho = 0.5 * (u1.rho + u2.rho);
            ru.v = 0.5 * (u1.v + u2.v);
            ru.p = 0.5 * (u1.p + u2.p);

            return ru;
        }

        /// <summary>
        /// Riemann solver for <c>X</c> direction.
        /// </summary>
        /// <param name="u_lo">lower side</param>
        /// <param name="u_hi">higher side</param>
        /// <returns>result</returns>
        public static U X(U u_lo, U u_hi)
        {
            double d, u, p;
            U ru = new U();

            riemann(u_lo.rho, u_lo.v.X, u_lo.p, u_hi.rho, u_hi.v.X, u_hi.p, out d, out u, out p);

            ru.rho = d;
            ru.v = new Maths.Geometry.Geometry2D.Vector(u, 0.0);
            ru.p = p;

            return ru;
        }

        /// <summary>
        /// Riemann solver for <c>X</c> direction.
        /// </summary>
        /// <param name="u_lo">lower side</param>
        /// <param name="u_hi">higher side</param>
        /// <returns>result</returns>
        public static U Y(U u_lo, U u_hi)
        {
            return Stub(u_lo, u_hi);
        }
    }
}
