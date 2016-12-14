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
        /// Direction type.
        /// </summary>
        public enum Type
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
            I1N = 0,

            /// <summary>
            /// I direction.
            /// </summary>
            IN = 0,

            /// <summary>
            /// Positive J direction.
            /// </summary>
            J1N = 1,

            /// <summary>
            /// J direction.
            /// </summary>
            JN = 1,

            /// <summary>
            /// Positive K direction.
            /// </summary>
            K1N = 2,

            /// <summary>
            /// K direction.
            /// </summary>
            KN = 2,

            /// <summary>
            /// General directions count.
            /// </summary>
            GenCount = 3,

            /// <summary>
            /// Negative I direction.
            /// </summary>
            I0N = 3,

            /// <summary>
            /// Negative J direction.
            /// </summary>
            J0N = 4,

            /// <summary>
            /// Negative K direction.
            /// </summary>
            K0N = 5,

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
        /// Type.
        /// </summary>
        private Type T;

        /// <summary>
        /// Dir number - cast direction type to integer.
        /// </summary>
        private int N
        {
            get
            {
                return (int)T;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dir()
        {
            T = Type.None;
        }

        /// <summary>
        /// Constructor by type.
        /// </summary>
        /// <param name="t">type</param>
        public Dir(Type t)
        {
            T = t;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="d"></param>
        public Dir(Dir d)
        {
            T = d.T;
        }

        /// <summary>
        /// Name of direction.
        /// </summary>
        /// <returns>name</returns>
        public string Name()
        {
            string[] str = { "I+", "J+", "K+", "I-", "J-", "K-" };

            return str[N];
        }

        /// <summary>
        /// General direction.
        /// </summary>
        /// <returns>general direction</returns>
        public Dir Gen()
        {
            return new Dir((Type)(N % (int)Type.GenCount));
        }

        /// <summary>
        /// Comparison.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>true - if equal, false - if not equal</returns>
        public static bool operator ==(Dir d1, Dir d2)
        {
            return d1.T == d2.T;
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
            return (obj is Dir) && ((obj as Dir).T == T);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// General direction I.
        /// </summary>
        public static Dir I = new Dir(Type.IN);

        /// <summary>
        /// General direction J.
        /// </summary>
        public static Dir J = new Dir(Type.IN);

        /// <summary>
        /// General direction K.
        /// </summary>
        public static Dir K = new Dir(Type.KN);

        /// <summary>
        /// Positive I direction.
        /// </summary>
        public static Dir I1 = new Dir(Type.I1N);

        /// <summary>
        /// Positive J direction.
        /// </summary>
        public static Dir J1 = new Dir(Type.I1N);

        /// <summary>
        /// Positive K direction.
        /// </summary>
        public static Dir K1 = new Dir(Type.K1N);

        /// <summary>
        /// Negative I direction.
        /// </summary>
        public static Dir I0 = new Dir(Type.I0N);

        /// <summary>
        /// Negative J direction.
        /// </summary>
        public static Dir J0 = new Dir(Type.I0N);

        /// <summary>
        /// Negative K direction.
        /// </summary>
        public static Dir K0 = new Dir(Type.K0N);

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
    }
}
