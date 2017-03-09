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
        /// No number.
        /// </summary>
        public static readonly int NoN = -1;

        /// <summary>
        /// First direction number.
        /// </summary>
        public static readonly int FirstN = 0;

        /// <summary>
        /// Positive I direction number.
        /// </summary>
        public static readonly int I1N = 0;

        /// <summary>
        /// I direction number.
        /// </summary>
        public static readonly int IN = 0;

        /// <summary>
        /// Positive J direction number.
        /// </summary>
        public static readonly int J1N = 1;

        /// <summary>
        /// J direction number.
        /// </summary>
        public static readonly int JN = 1;

        /// <summary>
        /// Positive K direction number.
        /// </summary>
        public static readonly int K1N = 2;

        /// <summary>
        /// K direction number.
        /// </summary>
        public static readonly int KN = 2;

        /// <summary>
        /// Count of general directions (I, J, K).
        /// </summary>
        public static readonly int GenCount = 3;

        /// <summary>
        /// Negative I direction number.
        /// </summary>
        public static readonly int I0N = 3;

        /// <summary>
        /// Negative J direction number.
        /// </summary>
        public static readonly int J0N = 4;

        /// <summary>
        /// Negative K direction number.
        /// </summary>
        public static readonly int K0N = 5;

        /// <summary>
        /// Last direction number.
        /// </summary>
        public static readonly int LastN = 5;

        /// <summary>
        /// Directions count (I1, J1, K1, I0, J0, K0).
        /// </summary>
        public static readonly int Count = 6;

        /// <summary>
        /// Direction number.
        /// </summary>
        public int N
        {
            get;
            private set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dir()
        {
            N = NoN;
        }

        /// <summary>
        /// Constructor by direction number.
        /// </summary>
        /// <param name="n">number</param>
        public Dir(int n)
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
        /// General direction I.
        /// </summary>
        public static readonly Dir I = new Dir(IN);

        /// <summary>
        /// General direction J.
        /// </summary>
        public static readonly Dir J = new Dir(JN);

        /// <summary>
        /// General direction K.
        /// </summary>
        public static readonly Dir K = new Dir(KN);

        /// <summary>
        /// Positive I direction.
        /// </summary>
        public static readonly Dir I1 = I;

        /// <summary>
        /// Positive J direction.
        /// </summary>
        public static readonly Dir J1 = J;

        /// <summary>
        /// Positive K direction.
        /// </summary>
        public static readonly Dir K1 = K;

        /// <summary>
        /// Negative I direction.
        /// </summary>
        public static readonly Dir I0 = new Dir(I0N);

        /// <summary>
        /// Negative J direction.
        /// </summary>
        public static readonly Dir J0 = new Dir(J0N);

        /// <summary>
        /// Negative K direction.
        /// </summary>
        public static readonly Dir K0 = new Dir(K0N);

        /// <summary>
        /// Directions array.
        /// </summary>
        public static readonly Dir[] Dirs = new Dir[] { I1, J1, K1, I0, J0, K0 };

        /// <summary>
        /// Name of direction.
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            string[] str = { "I+", "J+", "K+", "I-", "J-", "K-" };

            return str[N];
        }

        /// <summary>
        /// General direction.
        /// </summary>
        public Dir Gen
        {
            get
            {
                return Dirs[N % GenCount];
            }
        }

        /// <summary>
        /// Comparison.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <returns>true - if equal, false - if not equal</returns>
        public static bool operator ==(Dir d1, Dir d2)
        {
            if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
            {
                return ReferenceEquals(d1, d2);
            }
            else
            { 
                return d1.N == d2.N;
            }
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
        /// Reverse direction.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>reversed direction</returns>
        public static Dir operator !(Dir d)
        {
            return Dirs[(d.N + GenCount) % Count];
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
        /// Override of get hash code.
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Check if is I direction.
        /// </summary>
        public bool IsI
        {
            get
            {
                return Gen == I;
            }
        }

        /// <summary>
        /// Check if is J direction.
        /// </summary>
        public bool IsJ
        {
            get
            {
                return Gen == J;
            }
        }

        /// <summary>
        /// Check if is K direction.
        /// </summary>
        public bool IsK
        {
            get
            {
                return Gen == K;
            }
        }

        /// <summary>
        /// Check if direction correct.
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return (N >= FirstN) && (N <= LastN);
            }
        }

        /// <summary>
        /// Check if positive direction.
        /// </summary>
        public bool IsPos
        {
            get
            {
                return (N >= I1N) && (N <= K1N);
            }
        }

        /// <summary>
        /// Check if negative direction.
        /// </summary>
        public bool IsNeg
        {
            get
            {
                return (N >= I0N) && (N <= K0N);
            }
        }

        /// <summary>
        /// Check if three directions produce the basis.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        /// <param name="d3">third direction</param>
        public static bool IsBasis(Dir d1, Dir d2, Dir d3)
        {
            Dir gd1 = d1.Gen;
            Dir gd2 = d2.Gen;
            Dir gd3 = d3.Gen;

            return ((1 << gd1.N) | (1 << gd2.N) | (1 << gd3.N)) == 0x7;
        }

        /// <summary>
        /// Pair of orthogonal directions.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        public void GetPairOfOrthogonalDirs(out Dir d1, out Dir d2)
        {
            int n = Gen.N;
            int[] d1nums = new int[] { J1N, I1N, I1N };
            int[] d2nums = new int[] { K1N, K1N, J1N };

            d1 = Dir.Dirs[d1nums[n]];
            d2 = Dir.Dirs[d2nums[n]];
        }

        /// <summary>
        /// Orthogonal rotation.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        public static void OrthogonalRot(ref Dir d1, ref Dir d2)
        {
            Dir oldd1 = d1;

            d1 = d2;
            d2 = !oldd1;
        }
    }
}
