using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Numbers;
using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid.DescartesObjects
{
    /// <summary>
    /// Thin descartes object.
    /// </summary>
    public class ThinDescartesObject3D : DescartesObject3D
    {
        /// <summary>
        /// Direction of object (orthogonal to object).
        /// </summary>
        public Dir D
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="i">nodes count in I direction</param>
        /// <param name="j">nodes count in J direction</param>
        /// <param name="k">nodes count in K direction</param>
        public ThinDescartesObject3D(IntervalI i, IntervalI j, IntervalI k)
            : base (i, j, k)
        {
            SetD();
        }

        /// <summary>
        /// Set direction.
        /// </summary>
        private void SetD()
        {
            if (I.Length == 0)
            {
                D = (I0 == 0) ? Dir.I0 : Dir.I1;
            }
            else if (J.Length == 0)
            {
                D = (J0 == 0) ? Dir.J0 : Dir.J1;
            }
            else if (K.Length == 0)
            {
                D = (K0 == 0) ? Dir.K0 : Dir.K1;
            }
            else
            {
                throw new Exception("wrong sizes of ThisDescartesObject");
            }
        }

        /// <summary>
        /// Check if thin descartes object has I direction.
        /// </summary>
        public bool IsI
        {
            get
            {
                return D.IsI;
            }
        }

        /// <summary>
        /// Check if thin descartes object has J direction.
        /// </summary>
        public bool IsJ
        {
            get
            {
                return D.IsJ;
            }
        }

        /// <summary>
        /// Check if thin descartes object has K direction.
        /// </summary>
        public bool IsK
        {
            get
            {
                return D.IsK;
            }
        }

        /// <summary>
        /// Measure of border.
        /// </summary>
        public override int Measure
        {
            get
            {
                return SurfaceArea / 2;
            }
        }
    }
}
