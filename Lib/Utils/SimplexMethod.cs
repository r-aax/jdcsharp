// Copyright Joy Developing.

using System;

namespace Lib.Utils
{
    /// <summary>
    /// Result.
    /// </summary>
    public enum SimplexMethodIterResult
    {
        /// <summary>
        /// Common iteration.
        /// </summary>
        Common,

        /// <summary>
        /// Optimal solution is found.
        /// </summary>
        Optimal,

        /// <summary>
        /// System is unsolvable.
        /// </summary>
        Unsolvable
    }

    /// <summary>
    /// Simplex method in standard form
    /// (all conditions are inequalities).
    /// </summary>
    public class SimplexMethod
    {
        /// <summary>
        /// Count of variables.
        /// </summary>
        private int _N;

        /// <summary>
        /// Count of variables access.
        /// </summary>
        public int N
        {
            get
            {
                return _N;
            }
            private set
            {
                _N = value;
            }
        }

        /// <summary>
        /// Count of inequalities (basis size).
        /// </summary>
        private int M;

        /// <summary>
        /// Matrix <c>A</c> of inequalities coefficients.
        /// </summary>
        private double[,] A;

        /// <summary>
        /// Vector <c>B</c> of free members.
        /// </summary>
        private double[] B;

        /// <summary>
        /// Vector <c>C</c> of targer function coefficients.
        /// </summary>
        private double[] C;

        /// <summary>
        /// Vector of basis vectors numbers.
        /// </summary>
        private int[] Bas;

        /// <summary>
        /// Vector <c>Delta</c>.
        /// </summary>
        private double[] D;

        /// <summary>
        /// Value <c>Z</c> of target function.
        /// </summary>
        private double Z;

        /// <summary>
        /// Solution.
        /// </summary>
        private double[] X;

        /// <summary>
        /// Initialize.
        /// </summary>
        private void InitT()
        {
            A = new double[M, N + M];
            B = new double[M];
            C = new double[N + M];
            Bas = new int[M];
            D = new double[N + M];
            X = new double[N];
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">Count of variables.</param>
        /// <param name="m">Basis size.</param>
        public SimplexMethod(int n, int m)
        {
            N = n;
            M = m;
            InitT();
        }

        /// <summary>
        /// Set coefficient <c>a</c>.
        /// </summary>
        /// <param name="i">row number</param>
        /// <param name="j">column number</param>
        /// <param name="v">value</param>
        public void SetA(int i, int j, double v)
        {
            A[i, j] = v;
        }

        /// <summary>
        /// Set all coefficients <c>a</c>.
        /// </summary>
        /// <param name="a">matrix</param>
        public void SetAllA(double[,] a)
        {
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    SetA(i, j, a[i, j]);
                }
            }
        }

        /// <summary>
        /// Set coefficient <c>b</c>.
        /// </summary>
        /// <param name="i">coefficient number</param>
        /// <param name="v">value</param>
        public void SetB(int i, double v)
        {
            B[i] = v;
        }

        /// <summary>
        /// Set all coefficients <c>b</c>.
        /// </summary>
        /// <param name="b">vector</param>
        public void SetAllB(double[] b)
        {
            for (int i = 0; i < M; i++)
            {
                SetB(i, b[i]);
            }
        }

        /// <summary>
        /// Set coefficient <c>c</c>.
        /// </summary>
        /// <param name="j">coefficient number</param>
        /// <param name="v">value</param>
        public void SetC(int j, double v)
        {
            C[j] = v;
        }

        /// <summary>
        /// Set all coefficients <c>c</c>.
        /// </summary>
        /// <param name="c">vector</param>
        public void SetAllC(double[] c)
        {
            for (int j = 0; j < N; j++)
            {
                SetC(j, c[j]);
            }
        }

        /// <summary>
        /// Get <c>Z</c>.
        /// </summary>
        /// <returns>value</returns>
        public double GetZ()
        {
            return Z;
        }

        /// <summary>
        /// Get value of result element.
        /// </summary>
        /// <param name="i">element number</param>
        /// <returns>value</returns>
        public double GetX(int i)
        {
            return X[i];
        }

        /// <summary>
        /// Set first basis.
        /// </summary>
        private void SetFirstBasis()
        {
            // Singular mastix of rang M.
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    A[i, j + N] = ((i == j) ? 1.0 : 0.0);
                }
            }

            // First basis - additional variables.
            for (int i = 0; i < M; i++)
            {
                int j = i + N;
                Bas[i] = j;
                C[j] = 0.0;
            }

            // First solution - zero.
            for (int j = 0; j < N; j++)
            {
                X[j] = 0.0;
            }
        }

        /// <summary>
        /// Calculate <c>Delta</c>.
        /// </summary>
        /// <param name="j">coefficient number</param>
        private void CalcDelta(int j)
        {
            double v = 0.0;

            for (int i = 0; i < M; i++)
            {
                v += A[i, j] * C[Bas[i]];
            }

            D[j] = v - C[j];
        }

        /// <summary>
        /// Calculate all coefficients <c>Delta</c>.
        /// </summary>
        private void CalcDeltas()
        {
            for (int j = 0; j < N + M; j++)
            {
                CalcDelta(j);
            }
        }

        /// <summary>
        /// Calculate target function value.
        /// </summary>
        private void CalcZ()
        {
            double v = 0.0;

            for (int i = 0; i < M; i++)
            {
                v += B[i] * C[Bas[i]];
            }

            Z = v;
        }

        /// <summary>
        /// Find lead column.
        /// </summary>
        /// <returns>Lead column number or -1, if solution is optimal.</returns>
        private int FindJ0()
        {
            int j0 = -1;
            double min_d = 0.0;

            for (int j = 0; j < N + M; j++)
            {
                if (D[j] > 0.0)
                {
                    continue;
                }

                if ((j0 < 0) || (D[j] < min_d))
                {
                    min_d = D[j];
                    j0 = j;
                }
            }

            if (min_d >= 0.0)
            {
                // Solution is optimal.
                return -1;
            }

            return j0;
        }

        /// <summary>
        /// Find lead row.
        /// </summary>
        /// <param name="j0">lead column number</param>
        /// <returns>row number</returns>
        private int FindI0(int j0)
        {
            int i0 = -1;
            double ba, min_ba = 0.0;

            for (int i = 0; i < M; i++)
            {
                if (A[i, j0] <= 0.0)
                {
                    // Coefficient must be positive.
                    continue;
                }

                ba = B[i] / A[i, j0];

                if ((i0 < 0) || (ba < min_ba))
                {
                    min_ba = ba;
                    i0 = i;
                }
            }

            if (i0 < 0)
            {
                // System is unsolvable.
                return -1;
            }

            return i0;
        }

        /// <summary>
        /// Recalc by lead element.
        /// </summary>
        /// <param name="i0">lead row number</param>
        /// <param name="j0">lead column number</param>
        private void RecalcTable(int i0, int j0)
        {
            double a = A[i0, j0];

            // Row i0 normalization.
            B[i0] /= a;
            for (int j = 0; j < N + M; j++)
            {
                A[i0, j] /= a;
            }

            // Refresh all rows after this.
            for (int i = 0; i < M; i++)
            {
                if (i == i0)
                {
                    continue;
                }

                double k = A[i, j0];

                B[i] -= B[i0] * k;
                for (int j = 0; j < N + M; j++)
                {
                    A[i, j] -= A[i0, j] * k;
                }
            }
        }

        /// <summary>
        /// Push vector <c>j0</c> to the basis instead of <c>Bas[i0]</c>.
        /// </summary>
        /// <param name="j0">new vector number</param>
        /// <param name="i0">old vector position</param>
        private void ChangeBas(int j0, int i0)
        {
            Bas[i0] = j0;

            for (int j = 0; j < N; j++)
            {
                X[j] = 0.0;
            }

            for (int i = 0; i < M; i++)
            {
                if (Bas[i] < N)
                {
                    X[Bas[i]] = B[i];
                }
            }
        }

        /// <summary>
        /// One iteration.
        /// </summary>
        /// <returns>iteration result</returns>
        private SimplexMethodIterResult MakeIter()
        {
            int i0, j0;

            CalcDeltas();

            j0 = FindJ0();

            if (j0 < 0)
            {
                // Solution is optimal.
                return SimplexMethodIterResult.Optimal;
            }

            i0 = FindI0(j0);

            if (i0 < 0)
            {
                // System is unsolvable.
                return SimplexMethodIterResult.Unsolvable;
            }

            RecalcTable(i0, j0);
            ChangeBas(j0, i0);
            CalcZ();

            return SimplexMethodIterResult.Common;
        }

        /// <summary>
        /// Run simplex-method.
        /// </summary>
        /// <returns>calculation result</returns>
        public SimplexMethodIterResult Run()
        {
            SimplexMethodIterResult iter_res;

            SetFirstBasis();

            do
            {
                iter_res = MakeIter();
            } while (iter_res == SimplexMethodIterResult.Common);

            PrintX();

            return iter_res;
        }

        /// <summary>
        /// Print simplex table.
        /// </summary>
        public void PrintT()
        {
            Console.WriteLine("T : ");

            for (int i = 0; i < M; i++)
            {
                Console.Write("    {0} {1,5:f} |", Bas[i], B[i]);

                for (int j = 0; j < N + M; j++)
                {
                    Console.Write(" {0,5:f} ", A[i, j]);
                }

                Console.WriteLine();
            }

            Console.Write("             ");

            for (int j = 0; j < N + M; j++)
            {
                Console.Write(" {0,5:f} ", D[j]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Print target function value.
        /// </summary>
        public void PrintZ()
        {
            Console.WriteLine("Z : {0}", Z);
        }

        /// <summary>
        /// Print <c>X</c>.
        /// </summary>
        public void PrintX()
        {
            Console.Write("X : ");

            for (int j = 0; j < N; j++)
            {
                Console.Write(" {0} ", X[j]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Print basis.
        /// </summary>
        public void PrintBas()
        {
            Console.Write("Bas : ");

            for (int i = 0; i < M; i++)
            {
                Console.Write(" {0} ", Bas[i]);
            }

            Console.WriteLine();
        }
    }
}
