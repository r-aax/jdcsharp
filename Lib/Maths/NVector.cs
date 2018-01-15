using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths
{
    /// <summary>
    /// Vector with components count greater than three.
    /// </summary>
    public class NVector
    {
        /// <summary>
        /// Array of elements.
        /// </summary>
        public double[] E = null;

        /// <summary>
        /// Vector size.
        /// </summary>
        public int Size
        {
            get
            {
                return (E == null) ? 0 : E.Count();
            }
        }

        /// <summary>
        /// Allocate array of elements.
        /// </summary>
        /// <param name="n">size</param>
        private void Alloc(int n)
        {
            E = new double[n];
        }

        /// <summary>
        /// Set all vector element to v.
        /// </summary>
        /// <param name="v">value</param>
        public void Set(double v)
        {
            for (int i = 0; i < Size; i++)
            {
                E[i] = v;
            }
        }

        /// <summary>
        /// Set all vector elements to 0.0.
        /// </summary>
        public void Clear()
        {
            Set(0.0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">size</param>
        /// <param name="v">value</param>
        public NVector(int n, double v)
        {
            Alloc(n);
            Set(v);
        }

        /// <summary>
        /// Constructro with zeroing of elements.
        /// </summary>
        /// <param name="n">size</param>
        public NVector(int n)
            : this(n, 0.0)
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="nv">vector</param>
        public NVector(NVector nv)
        {
            Alloc(nv.Size);

            for (int i = 0; i < Size; i++)
            {
                E[i] = nv.E[i];
            }
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string str = "";

            if (Size > 0)
            {
                str = E[0].ToString();

                if (Size > 1)
                {
                    for (int i = 1; i < Size; i++)
                    {
                        str = String.Format("{0},{1}", str, E[i]);
                    }
                }
            }

            return String.Format("({0})", str);
        }
    }
}
