// Author: Alexey Rybakov

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.Maths.Bits
{
    /// <summary>
    /// Bit mask.
    /// </summary>
    public class Mask
    {
        /// <summary>
        /// Bit array.
        /// </summary>
        private BitArray Bits;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="length">length</param>
        private Mask(int length)
        {
            Bits = new BitArray(length);
        }

        /// <summary>
        /// Empty mask.
        /// </summary>
        /// <param name="length">length</param>
        /// <returns>mask</returns>
        public static Mask MakeEmpty(int length)
        {
            Mask mask = new Mask(length);
            mask.SetAll(false);
            return mask;
        }

        /// <summary>
        /// Full mask.
        /// </summary>
        /// <param name="length">length</param>
        /// <returns>mask</returns>
        public static Mask MakeFull(int length)
        {
            Mask mask = new Mask(length);
            mask.SetAll(true);
            return mask;
        }

        /// <summary>
        /// Length.
        /// </summary>
        public int Length
        {
            get
            {
                return Bits.Length;
            }
        }

        /// <summary>
        /// Check if index is correct.
        /// </summary>
        /// <param name="index">index</param>
        [Conditional("DEBUG")]
        private void CheckIndex(int index)
        {
            if ((index < 0) || (index >= Length))
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Iterator.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>value</returns>
        public bool this[int index]
        {
            get
            {
                CheckIndex(index);
                return Bits[index];
            }

            set
            {
                CheckIndex(index);
                Bits[index] = value;
            }
        }

        /// <summary>
        /// Set given value for all bits.
        /// </summary>
        /// <param name="value">value</param>
        public void SetAll(bool value)
        {
            Bits.SetAll(value);
        }

        /// <summary>
        /// Get range.
        /// </summary>
        /// <returns>range</returns>
        private IEnumerable<int> Range()
        {
            return Enumerable.Range(0, Length);
        }

        /// <summary>
        /// Check if mask is empty.
        /// </summary>
        /// <returns><c>true</c> - if mask is empty, <c>false</c> - in another case.</returns>
        public bool IsEmpty()
        {
            return !Range().Any((i) => this[i]);
        }

        /// <summary>
        /// Check if mask if full.
        /// </summary>
        /// <returns><c>true</c> - if mask is full, <c>false</c> - in another case.</returns>
        public bool IsFull()
        {
            return Range().All((i) => this[i]);
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return (from i in Range()
                    select (this[i] ? '1' : '0')).ToString();
        }

        /// <summary>
        /// Get next mask.
        /// </summary>
        /// <param name="start">number of position to shift</param>
        /// <returns><c>true</c> - if next mask exists, <c>false</c> - if next mask does not exist.</returns>
        public bool Next(int start)
        {
            for (int i = start; i < Length; i++)
            {
                this[i] = !this[i];

                // If we set element to true then it is next mask.
                if (this[i])
                {
                    break;
                }

                if (i == Length - 1)
                {
                    return false;
                }
            }

            // All elements before we have to zero.
            for (int i = 0; i < start; i++)
            {
                this[i] = false;
            }

            return true;
        }

        /// <summary>
        /// Next mask.
        /// </summary>
        /// <returns><c>true</c> - if next mask exists, <c>false</c> - if next mask does not exist.</returns>
        public bool Next()
        {
            return Next(0);
        }

        /// <summary>
        /// Invert mask.
        /// </summary>
        /// <returns>inverted mask</returns>
        public Mask Invert()
        {
            Mask mask = MakeEmpty(Length);

            foreach (int i in Range())
            {
                if (!this[i])
                {
                    mask[i] = true;
                }
            }

            return mask;
        }

        /// <summary>
        /// Print
        /// </summary>
        /// <param name="shift">shift</param>
        public void Print(int shift)
        {
            Console.Write("[");

            for (int i = 0; i < Length; i++)
            {
                if (this[i])
                {
                    Console.Write(" {0}", i + shift);
                }
            }

            Console.WriteLine(" ] - {0}", Popcnt());
        }

        /// <summary>
        /// Count of 1 bits.
        /// </summary>
        /// <returns>count of 1 bits</returns>
        public int Popcnt()
        {
            int c = 0;

            for (int i = 0; i < Length; i++)
            {
                if (this[i])
                {
                    c++;
                }
            }

            return c;
        }

        /// <summary>
        /// Check if mask contains another mask.
        /// </summary>
        /// <param name="m">another mask</param>
        /// <returns>true - if this contans m, false - otherwise</returns>
        public bool IsContain(Mask m)
        {
            for (int i = 0; i < m.Length; i++)
            {
                if (m[i])
                {
                    if (i >= Length)
                    {
                        return false;
                    }

                    if (!this[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
