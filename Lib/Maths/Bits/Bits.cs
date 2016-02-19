using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

using Lib.Maths.Numbers;

namespace Lib.Maths.Bits
{
    /// <summary>
    /// Bits class.
    /// </summary>
    public class Bits
    {
        /// <summary>
        /// Array of bits.
        /// </summary>
        private BitArray A;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">length</param>
        public Bits(int n)
        {
            A = new BitArray(n);
        }

        /// <summary>
        /// Indexer overload.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>value of bit array in the given position</returns>
        public bool this[int i]
        {
            get
            {
                return A[i];
            }

            set
            {
                A[i] = value;
            }
        }

        /// <summary>
        /// Check if bit array has true value.
        /// </summary>
        public bool HasTrue
        {
            get
            {
                foreach (bool b in A)
                {
                    if (b)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Check if bit array has false value.
        /// </summary>
        public bool HasFalse
        {
            get
            {
                foreach (bool b in A)
                {
                    if (!b)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Check if bit array has only true values.
        /// </summary>
        public bool IsAllTrue
        {
            get
            {
                return !HasFalse;
            }
        }

        /// <summary>
        /// Check if bit array has only false values.
        /// </summary>
        public bool IsAllFalse
        {
            get
            {
                return !HasTrue;
            }
        }

        /// <summary>
        /// Set values to all elements of bit array.
        /// </summary>
        /// <param name="v">value</param>
        public void Set(bool v)
        {
            A.SetAll(v);
        }

        /// <summary>
        /// Count of elements.
        /// </summary>
        public int Count
        {
            get
            {
                return A.Count;
            }
        }

        /// <summary>
        /// Set random values.
        /// </summary>
        /// <param name="true_p">probability of true value</param>
        public void Randomize(double true_p)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] = Randoms.RandomBool(true_p);
            }
        }

        /// <summary>
        /// Set random bool values with probability 0.5.
        /// </summary>
        public void Randomize()
        {
            Randomize(0.5);
        }

        /// <summary>
        ///  Set random set of size m in true.
        /// </summary>
        /// <param name="m">set size</param>
        public void SetRandomSet(int m)
        {
            Debug.Assert(m <= Count);

            Set(false);

            for (int i = 0; i < m; i++)
            {
                int j = Randoms.RandomInInterval(0, Count - 1 - i);
                int k = 0;

                while (this[k] || (j > 0))
                {
                    if (!this[k])
                    {
                        j--;
                    }

                    k++;
                }

                this[k] = true;
            }
        }

        /// <summary>
        /// Count of true values.
        /// </summary>
        public int TrueCount
        {
            get
            {
                int c = 0;

                foreach (bool b in A)
                {
                    if (b)
                    {
                        c++;
                    }
                }

                return c;
            }
        }

        /// <summary>
        /// Count of false values.
        /// </summary>
        public int FalseCount
        {
            get
            {
                return Count - TrueCount;
            }
        }
    }
}
