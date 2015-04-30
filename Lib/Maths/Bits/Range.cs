// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Bits
{
    /// <summary>
    /// Range.
    /// </summary>
    public class Range
    {
        /// <summary>
        /// Least significant bit.
        /// </summary>
        public int LSB { get; private set; }

        /// <summary>
        /// Most significant bit.
        /// </summary>
        public int MSB { get; private set; }

        /// <summary>
        /// Length.
        /// </summary>
        public int Length
        {
            get
            {
                return MSB - LSB + 1;
            }
        }

        /// <summary>
        /// Values count.
        /// </summary>
        public int ValuesCount
        {
            get
            {
                return 1 << Length;
            }
        }

        /// <summary>
        /// Maximum value in range.
        /// </summary>
        public UInt32 MaxValue
        {
            get
            {
                return (UInt32)(1 << Length) - 1;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lsb">least significant bit</param>
        /// <param name="msb">most significatn bit</param>
        public Range(int lsb, int msb)
        {
            Debug.Assert(msb >= lsb);

            LSB = lsb;
            MSB = msb;
        }

        /// <summary>
        /// Range without <c>LSB</c>.
        /// </summary>
        /// <returns>new range</returns>
        public Range WithoutLSB()
        {
            return new Range(LSB + 1, MSB);
        }

        /// <summary>
        /// Range without <c>MSB</c>.
        /// </summary>
        /// <returns>new range</returns>
        public Range WithoutMSB()
        {
            return new Range(LSB, MSB - 1);
        }

        /// <summary>
        /// Range without <c>LSB</c> and <c>MSB</c>.
        /// </summary>
        /// <returns></returns>
        public Range WithoutEdges()
        {
            return new Range(LSB + 1, MSB - 1);
        }
    }
}
