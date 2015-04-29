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

        /// <summary>
        /// Delegate for popcnt_4 calculation.
        /// </summary>
        /// <param name="b">bits</param>
        /// <returns>count of 1 bits</returns>
        delegate int Popcnt4(Byte b);

        /// <summary>
        /// Count of 1 bits.
        /// </summary>
        /// <returns>count of 1 bits</returns>
        public int Popcnt()
        {
            Popcnt4 popcnt_4 = b => (int)((0x4332322132212110 >> (b << 2)) & 0xF);

            return popcnt_4((Byte)((B >> 4) & 0xF)) + popcnt_4((Byte)(B & 0xF));
        }
    }
}
