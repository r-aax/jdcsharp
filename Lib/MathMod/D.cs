using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod
{
    /// <summary>
    /// Constant data.
    /// </summary>
    public class D
    {
        /// <summary>
        /// Mass.
        /// </summary>
        public double m;

        /// <summary>
        /// X component of impulse.
        /// </summary>
        public double Px;

        /// <summary>
        /// Y component of impulse.
        /// </summary>
        public double Py;

        /// <summary>
        /// Z componnent of impulse.
        /// </summary>
        public double Pz;

        /// <summary>
        /// Full energy.
        /// </summary>
        public double I;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="m_">mass</param>
        /// <param name="px">X component of impulse</param>
        /// <param name="py">Y component of impulse</param>
        /// <param name="pz">Z component of impulse</param>
        /// <param name="i">full energy</param>
        public D(double m_, double px, double py, double pz, double i)
        {
            m = m_;
            Px = px;
            Py = py;
            Pz = pz;
            I = i;
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public D()
            : this(0.0, 0.0, 0.0, 0.0, 0.0)
        {        
        }

        /// <summary>
        /// Add flow.
        /// </summary>
        /// <param name="q">flow</param>
        public void AddQ(Q q)
        {
            m += q.rho;
            Px += q.vx;
            Py += q.vy;
            Pz += q.vz;
            I += q.E;
        }

        /// <summary>
        /// Sub flow.
        /// </summary>
        /// <param name="q">flow</param>
        public void SubQ(Q q)
        {
            m -= q.rho;
            Px -= q.vx;
            Py -= q.vy;
            Pz -= q.vz;
            I -= q.E;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("D: {0}, {1}, {2}, {3}, {4}", m, Px, Py, Pz, I);
        }
    }
}
