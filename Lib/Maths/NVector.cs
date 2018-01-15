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
        /// Extended element.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>value</returns>
        private double XE(int i)
        {
            return ((i >= 0) && (i < Size)) ? E[i] : 0.0;
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

        /// <summary>
        /// Module square.
        /// </summary>
        public double Mod2
        {
            get
            {
                double d = 0.0;

                for (int i = 0; i < Size; i++)
                {
                    d += E[i] * E[i];
                }

                return d;
            }
        }

        /// <summary>
        /// Module.
        /// </summary>
        public double Mod
        {
            get
            {
                return Math.Sqrt(Mod2);
            }
        }

        /// <summary>
        /// Multiplication on given value.
        /// </summary>
        /// <param name="k">value</param>
        public void Mult(double k)
        {
            for (int i = 0; i < Size; i++)
            {
                E[i] *= k;
            }
        }

        /// <summary>
        /// Invert elements of vector.
        /// </summary>
        public void Invert()
        {
            Mult(-1.0);
        }    

        /// <summary>
        /// Sum of vectors.
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>vector (sum)</returns>
        public static NVector operator +(NVector a, NVector b)
        {
            int size = Math.Max(a.Size, b.Size);
            NVector v = new NVector(size);
            
            for (int i = 0; i < size; i++)
            {
                v.E[i] = a.XE(i) + b.XE(i);
            }

            return v;           
        }

        /// <summary>
        /// Unary minus.
        /// </summary>
        /// <param name="a">vector</param>
        /// <returns>inverted vector</returns>
        public static NVector operator -(NVector a)
        {
            NVector v = new NVector(a);

            v.Invert();

            return v;
        }

        /// <summary>
        /// Binary minus.
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>result</returns>
        public static NVector operator -(NVector a, NVector b)
        {
            int size = Math.Max(a.Size, b.Size);
            NVector v = new NVector(size);

            for (int i = 0; i < size; i++)
            {
                v.E[i] = a.XE(i) - b.XE(i);
            }

            return v;
        }

        /// <summary>
        /// Multiplication on given value.
        /// </summary>
        /// <param name="a">vactor</param>
        /// <param name="k">value</param>
        /// <returns>result</returns>
        public static NVector operator *(NVector a, double k)
        {
            NVector v = new NVector(a);

            v.Mult(k);

            return v;
        }

        /// <summary>
        /// Multiplication on the given value.
        /// </summary>
        /// <param name="k">value</param>
        /// <param name="a">vector</param>
        /// <returns>result</returns>
        public static NVector operator *(double k, NVector a)
        {
            return a * k;
        }

        /// <summary>
        /// Division on value.
        /// </summary>
        /// <param name="a">vector</param>
        /// <param name="k">value</param>
        /// <returns>result</returns>
        public static NVector operator /(NVector a, double k)
        {
            return a * (1.0 / k);
        }
    }
}
