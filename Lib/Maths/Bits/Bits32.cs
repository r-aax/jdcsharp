// Copyright Joy Developing.

using System;
using System.Diagnostics;

namespace Lib.Maths.Bits
{
    /// <summary>
    /// Bit field of 32 size.
    /// </summary>
    public class Bits32
    {
        /// <summary>
        /// Size.
        /// </summary>
        public static readonly int Size = 32;

        /// <summary>
        /// Bytes count.
        /// </summary>
        public static readonly int BytesCount = 4;

        /// <summary>
        /// Full mask.
        /// </summary>
        public static readonly UInt32 Full = 0xFFFFFFFF;

        /// <summary>
        /// Data.
        /// </summary>
        protected UInt32 B = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="b">data</param>
        public Bits32(UInt32 b)
        {
            B = b;
        }

        /// <summary>
        /// Cast to integer.
        /// </summary>
        /// <returns>integer</returns>
        public static implicit operator UInt32(Bits32 bits)
        {
            return bits.B;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("0x{0,8:X8}", B);
        }

        /// <summary>
        /// Bit mask.
        /// </summary>
        /// <param name="pos">position</param>
        /// <returns>mask</returns>
        public static UInt32 BitMask(int pos)
        {
            Debug.Assert(pos < Size);

            return 1u << pos;
        }

        /// <summary>
        /// Check if bit is set.
        /// </summary>
        /// <param name="pos">position</param>
        /// <returns>check result</returns>
        public bool IsBitSet(int pos)
        {
            return (B & BitMask(pos)) != 0;
        }

        /// <summary>
        /// Set bit value.
        /// </summary>
        /// <param name="pos">position</param>
        /// <param name="value">value</param>
        public void SetBit(int pos, bool value)
        {
            if (value)
            {
                FillBit(pos);
            }
            else
            {
                ClearBit(pos);
            }
        }

        /// <summary>
        /// Fill bit with 1.
        /// </summary>
        /// <param name="pos">position</param>
        public void FillBit(int pos)
        {
            B |= BitMask(pos);
        }

        /// <summary>
        /// Clear bit.
        /// </summary>
        /// <param name="pos">position</param>
        public void ClearBit(int pos)
        {
            B &= ~BitMask(pos);
        }

        /// <summary>
        /// Invert bit.
        /// </summary>
        /// <param name="pos">position</param>
        public void InvertBit(int pos)
        {
            B ^= BitMask(pos);
        }

        /// <summary>
        /// Get tail mask.
        /// </summary>
        /// <param name="len">tail length</param>
        /// <returns>mask</returns>
        public static UInt32 TailMask(int len)
        {
            Debug.Assert((len > 0) && (len <= Size));

            return Full >> (Size - len);
        }

        /// <summary>
        /// Get range mask.
        /// </summary>
        /// <param name="r">range</param>
        /// <returns>range mask</returns>
        public static UInt32 RangeMask(Range r)
        {
            Debug.Assert(r.MSB < Size);

            return TailMask(r.Length) << r.LSB;
        }

        /// <summary>
        /// Get tail.
        /// </summary>
        /// <param name="len">tail length</param>
        /// <returns>tail</returns>
        public UInt32 GetTail(int len)
        {
            return B & TailMask(len);
        }

        /// <summary>
        /// Get range value.
        /// </summary>
        /// <param name="r">range</param>
        /// <returns>range value</returns>
        public UInt32 GetRange(Range r)
        {
            return (B & RangeMask(r)) >> r.LSB;
        }

        /// <summary>
        /// Set range value.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="value">value</param>
        public void SetRange(Range r, UInt32 value)
        {
            // We have to check that value not intersect
            // bounds of range (upper bound).
            Debug.Assert(new Bits32(value).GetTail(r.Length) == value);

            ClearRange(r);
            B |= (value << r.LSB);
        }

        /// <summary>
        /// Fill range with 1..1.
        /// </summary>
        /// <param name="r">range</param>
        public void FillRange(Range r)
        {
            B |= RangeMask(r);
        }

        /// <summary>
        /// Clear range.
        /// </summary>
        /// <param name="r">range</param>
        public void ClearRange(Range r)
        {
            B &= ~RangeMask(r);
        }

        /// <summary>
        /// Write to range float value units count.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="uvalue">units count</param>
        /// <returns><c>true</c> - if we had to decrement units count by 1, <c>false</c> - in another case</returns>
        private bool WriteUDoubleUnitsCount(Range r, UInt32 uvalue)
        {
            UInt32 corrected_uvalue = uvalue;
            bool res = false;

            // If range of float values is X,
            // then MSB value corresponds to X / 2.0,
            // next bit value corresponds to X / 4.0 and so on.
            // We see that real range of float values can be written to range is X * (1 - 2^(-length)),
            // when length is length of given range.
            // So if we want to write value X, we intersect upper bound of range.
            // In this case we write value uvalue - 1.
            if (corrected_uvalue == r.MaxValue + 1)
            {
                corrected_uvalue--;
                res = true;
            }

            SetRange(r, corrected_uvalue);

            return res;
        }

        /// <summary>
        /// Read unsigned double.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="unit">unit value (value of LSB)</param>
        /// <returns>float value</returns>
        public double ReadUDouble(Range r, double unit)
        {
            return GetRange(r) * unit;
        }

        /// <summary>
        /// Write unsigned double.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="value">value</param>
        /// <param name="unit">unit value</param>
        public void WriteUDouble(Range r, double value, double unit)
        {
            Debug.Assert(value >= 0.0);

            UInt32 uvalue = (UInt32)Math.Round(value / unit);

            WriteUDoubleUnitsCount(r, uvalue);
        }

        /// <summary>
        /// Read double.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="unit">unit value</param>
        /// <returns>double value</returns>
        public double ReadDouble(Range r, double unit)
        {
            double value = ReadUDouble(r.WithoutMSB(), unit);

            return IsBitSet(r.MSB) ? (-value) : value;
        }

        /// <summary>
        /// Write double.
        /// </summary>
        /// <param name="r">range</param>
        /// <param name="value">value</param>
        /// <param name="unit">unit value</param>
        public void WriteDouble(Range r, double value, double unit)
        {
            WriteUDouble(r.WithoutMSB(), Math.Abs(value), unit);
            SetBit(r.MSB, value < 0.0);
        }

        /// <summary>
        /// Read double value from two words.
        /// </summary>
        /// <param name="bits1">first word</param>
        /// <param name="r1">first word range</param>
        /// <param name="bits2">second word</param>
        /// <param name="r2">second word range</param>
        /// <param name="unit">unit value</param>
        /// <returns>double value</returns>
        public static double DuplexReadDouble(Bits32 bits1, Range r1, Bits32 bits2, Range r2, double unit)
        {
            UInt32 utail = bits1.GetRange(r1);
            UInt32 uhead = bits2.GetRange(r2.WithoutMSB());
            UInt32 uvalue = utail | (uhead << r1.Length);
            double value = uvalue * unit;

            return bits2.IsBitSet(r2.MSB) ? (-value) : value;
        }

        /// <summary>
        /// Write double value to two words.
        /// </summary>
        /// <param name="bits1">first word</param>
        /// <param name="r1">first word range</param>
        /// <param name="bits2">second word</param>
        /// <param name="r2">second word range</param>
        /// <param name="value">value</param>
        /// <param name="unit">unit value</param>
        public static void DuplexWriteDouble(Bits32 bits1, Range r1, Bits32 bits2, Range r2, double value, double unit)
        {
            UInt32 uvalue = (UInt32)Math.Abs(Math.Round(value / unit));

            if (bits2.WriteUDoubleUnitsCount(r2.WithoutMSB(), uvalue >> r1.Length))
            {
                bits1.FillRange(r1);
            }
            else
            {
                bits1.SetRange(r1, new Bits32(uvalue).GetTail(r1.Length));
            }

            bits2.SetBit(r2.MSB, value < 0.0);
        }

        /// <summary>
        /// Count of 1 bits.
        /// </summary>
        /// <returns>count of 1 bits</returns>
        public int Popcnt()
        {
            int c = 0;

            for (int i = 0; i < Size; i++)
            {
                if (IsBitSet(i))
                {
                    c++;
                }
            }

            return c;
        }
    }
}
