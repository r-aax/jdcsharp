// Author: Alexey Rybakov

using System;

namespace Lib.Maths.Bits
{
    /// <summary>
    /// Bit field of 8 bits.
    /// </summary>
    public class Bits8
    {
        /// <summary>
        /// Data.
        /// </summary>
        private Byte B = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="b">data</param>
        public Bits8(Byte b)
        {
            B = b;
        }

        /// <summary>
        /// Cast to byte.
        /// </summary>
        /// <returns>integer</returns>
        public static implicit operator Byte(Bits8 bits)
        {
            return bits.B;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("0x{0,2:X2}", B);
        }

        /// <summary>
        /// Reverse.
        /// </summary>
        public void Reverse()
        {
            B = (Byte)(((B & 0x0F) << 4) | ((B & 0xF0) >> 4));
            B = (Byte)(((B & 0x33) << 2) | ((B & 0xCC) >> 2));
            B = (Byte)(((B & 0x55) << 1) | ((B & 0xAA) >> 1));
        }
    }
}
