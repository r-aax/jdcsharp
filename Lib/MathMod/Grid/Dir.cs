using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Grid direction.
    /// </summary>
    public class Dir
    {
        /// <summary>
        /// Direction number.
        /// </summary>
        public enum Num
        {
            /// <summary>
            /// No direction.
            /// </summary>
            None = -1,

            /// <summary>
            /// First value.
            /// </summary>
            First = 0,

            /// <summary>
            /// Positive I direction.
            /// </summary>
            I1 = 0,

            /// <summary>
            /// I direction.
            /// </summary>
            I = 0,

            /// <summary>
            /// Positive J direction.
            /// </summary>
            J1 = 1,

            /// <summary>
            /// J direction.
            /// </summary>
            J = 1,

            /// <summary>
            /// Positive K direction.
            /// </summary>
            K1 = 2,

            /// <summary>
            /// K direction.
            /// </summary>
            K = 2,

            /// <summary>
            /// General directions count.
            /// </summary>
            GenCount = 3,

            /// <summary>
            /// Negative I direction.
            /// </summary>
            I0 = 3,

            /// <summary>
            /// Negative J direction.
            /// </summary>
            J0 = 4,

            /// <summary>
            /// Negative K direction.
            /// </summary>
            K0 = 5,

            /// <summary>
            /// Last direction.
            /// </summary>
            Last = 5,

            /// <summary>
            /// Total directions count.
            /// </summary>
            Count = 6
        }

        /// <summary>
        /// Direction number.
        /// </summary>
        public Num N
        {
            get;
            private set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dir()
        {
            N = Num.None;
        }

        /// <summary>
        /// Constructor by direction number.
        /// </summary>
        /// <param name="n">number</param>
        public Dir(Num n)
        {
            N = n;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="d"></param>
        public Dir(Dir d)
        {
            N = d.N;
        }

        /// <summary>
        /// Name of direction.
        /// </summary>
        /// <returns>name</returns>
        public string Name()
        {
            string[] str = { "I+", "J+", "K+", "I-", "J-", "K-" };

            return str[(int)N];
        }

        /// <summary>
        /// General direction.
        /// </summary>
        /// <returns>general direction</returns>
        public Dir Gen()
        {
            return new Dir((Num)((int)N % (int)Num.GenCount));
        }

        /// <summary>
        /// Comparison.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>true - if equal, false - if not equal</returns>
        public static bool operator ==(Dir d1, Dir d2)
        {
            return d1.N == d2.N;
        }

        /// <summary>
        /// Comparison for not equal.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>true - if not equal, false - if equal</returns>
        public static bool operator !=(Dir d1, Dir d2)
        {
            return !(d1 == d2);
        }

        /// <summary>
        /// Check equal of two directions.
        /// </summary>
        /// <param name="obj">other direction</param>
        /// <returns><c>true</c> - if objects are equal, <c>false</c> - otherwise</returns>
        public override bool Equals(object obj)
        {
            return (obj is Dir) && ((obj as Dir).N == N);
        }

        /// <summary>
        /// General direction I.
        /// </summary>
        public static Dir I = new Dir(Num.I);

        /// <summary>
        /// General direction J.
        /// </summary>
        public static Dir J = new Dir(Num.J);

        /// <summary>
        /// General direction K.
        /// </summary>
        public static Dir K = new Dir(Num.K);

        /// <summary>
        /// Positive I direction.
        /// </summary>
        public static Dir I1 = I;

        /// <summary>
        /// Positive J direction.
        /// </summary>
        public static Dir J1 = J;

        /// <summary>
        /// Positive K direction.
        /// </summary>
        public static Dir K1 = K;

        /// <summary>
        /// Negative I direction.
        /// </summary>
        public static Dir I0 = new Dir(Num.I0);

        /// <summary>
        /// Negative J direction.
        /// </summary>
        public static Dir J0 = new Dir(Num.J0);

        /// <summary>
        /// Negative K direction.
        /// </summary>
        public static Dir K0 = new Dir(Num.K0);

        /// <summary>
        /// Check if is I direction.
        /// </summary>
        /// <returns>true - if I direction, false - othercase</returns>
        public bool IsI()
        {
            return Gen() == I;
        }

        /// <summary>
        /// Check if is J direction.
        /// </summary>
        /// <returns>true - if J direction, false - othercase</returns>
        public bool IsJ()
        {
            return Gen() == J;
        }

        /// <summary>
        /// Check if is K direction.
        /// </summary>
        /// <returns>true - if K direction, false - othercase</returns>
        public bool IsK()
        {
            return Gen() == K;
        }

        /// <summary>
        /// Check if three directions produce the basis.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="d3">third direction</param>
        public static bool IsBasis(Dir d1, Dir d2, Dir d3)
        {
            Dir gd1 = d1.Gen();
            Dir gd2 = d2.Gen();
            Dir gd3 = d3.Gen();

            return ((1 << (int)gd1.N) | (1 << (int)gd2.N) | (1 << (int)gd3.N)) == 0x7;
        }
    }
}
