// Author: Alexey Rybakov

using System.Diagnostics;

using Lib.Maths.Bits;

namespace Lib.Maths.Numbers.RamseyNumbers
{
    /// <summary>
    /// Analizer of predsamsey circular graphs.
    /// This class considers circular graphs of order <c>N</c>,
    /// less than Ramsey number for <c>Kr</c>, <c>Kb</c> and looks for graphs,
    /// which do not contain full red cliques of <c>Kr</c> nodes and
    /// full blue cliques of <c>Kb</c> nodes.
    /// </summary>
    public class PredramseyCircularGraphsAnalizer
    {
        /// <summary>
        /// Order.
        /// </summary>
        private int N;

        /// <summary>
        /// Cliques sizes.
        /// </summary>
        private int Kr, Kb;

        /// <summary>
        /// Red edges mask.
        /// </summary>
        private Mask RedMask;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">order</param>
        /// <param name="k_r">red clique size</param>
        /// <param name="k_b">blue clique size</param>
        public PredramseyCircularGraphsAnalizer(int n, int k_r, int k_b)
        {
            // Set sizes.
            N = n;
            Kr = k_r;
            Kb = k_b;

            // Set empty red mask.
            RedMask = Mask.MakeEmpty(N / 2);
        }

        /// <summary>
        /// Get next red coloring.
        /// </summary>
        /// <returns>
        /// <c>true</c> - if we have next red coloring,
        /// <c>false</c> - if we do not have next red coloring
        /// </returns>
        public bool NextColoring()
        {
            return RedMask.Next();
        }

        /// <summary>
        /// Check for red clique.
        /// </summary>
        /// <returns>
        /// <c>true</c> - if graph has red clique,
        /// <c>false</c> - if graph do not have red clique
        /// </returns>
        public bool HasRed()
        {
            return HasFull(RedMask, Kr);
        }

        /// <summary>
        /// Check for blue clique.
        /// </summary>
        /// <returns>
        /// <c>true</c> - if graph has blue clique,
        /// <c>false</c> - if graph do not have blue clique
        /// </returns>
        public bool HasBlue()
        {
            return HasFull(RedMask.Invert(), Kb);
        }

        /// <summary>
        /// Check for clique.
        /// </summary>
        /// <param name="mask">red mask</param>
        /// <param name="k">clique size</param>
        /// <returns>
        /// <c>true</c> - if clique is found,
        /// <c>false</c> - if clique is not found
        /// </returns>
        private bool HasFull(Mask mask, int k)
        {
            switch (k)
            {
                case 3:
                    return Has3(mask);

                case 4:
                    return Has4(mask);

                case 5:
                    return Has5(mask);

                case 6:
                    return Has6(mask);

                default:
                    Debug.Assert(false);
                    break;
            }

            return false;
        }

        /// <summary>
        /// Find <c>K_3</c>.
        /// </summary>
        /// <param name="mask">red mask</param>
        /// <returns>
        /// <c>true</c> - if <c>K_3</c> is found,
        /// <c>false</c> - if not
        /// </returns>
        private bool Has3(Mask mask)
        {
            CyclicArithmetic arith = new CyclicArithmetic(N);

            for (int i1 = 0; i1 < N; i1++)
            {
                for (int i2 = i1 + 1; i2 < N; i2++)
                {
                    if (!mask[arith.Dist(i2, i1) - 1])
                    {
                        continue;
                    }

                    for (int i3 = i2 + 1; i3 < N; i3++)
                    {
                        if (mask[arith.Dist(i3, i2) - 1]
                            && mask[arith.Dist(i3, i1) - 1])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Find <c>K_4</c>.
        /// </summary>
        /// <param name="mask">red mask</param>
        /// <returns>
        /// <c>true</c> - if <c>K_4</c> is found,
        /// <c>false</c> - if not
        /// </returns>
        private bool Has4(Mask mask)
        {
            CyclicArithmetic arith = new CyclicArithmetic(N);

            for (int i1 = 0; i1 < N; i1++)
            {
                for (int i2 = i1 + 1; i2 < N; i2++)
                {
                    if (!mask[arith.Dist(i2, i1) - 1])
                    {
                        continue;
                    }

                    for (int i3 = i2 + 1; i3 < N; i3++)
                    {
                        if (!mask[arith.Dist(i3, i2) - 1]
                            || !mask[arith.Dist(i3, i1) - 1])
                        {
                            continue;
                        }

                        for (int i4 = i3 + 1; i4 < N; i4++)
                        {
                            if (mask[arith.Dist(i4, i3) - 1]
                                && mask[arith.Dist(i4, i2) - 1]
                                && mask[arith.Dist(i4, i1) - 1])
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Find <c>K_5</c>.
        /// </summary>
        /// <param name="mask">red mask</param>
        /// <returns>
        /// <c>true</c> - if <c>K_5</c> is found,
        /// <c>false</c> - if not
        /// </returns>
        private bool Has5(Mask mask)
        {
            CyclicArithmetic arith = new CyclicArithmetic(N);

            for (int i1 = 0; i1 < N; i1++)
            {
                for (int i2 = i1 + 1; i2 < N; i2++)
                {
                    if (!mask[arith.Dist(i2, i1) - 1])
                    {
                        continue;
                    }

                    for (int i3 = i2 + 1; i3 < N; i3++)
                    {
                        if (!mask[arith.Dist(i3, i2) - 1]
                            || !mask[arith.Dist(i3, i1) - 1])
                        {
                            continue;
                        }

                        for (int i4 = i3 + 1; i4 < N; i4++)
                        {
                            if (!mask[arith.Dist(i4, i3) - 1]
                                || !mask[arith.Dist(i4, i2) - 1]
                                || !mask[arith.Dist(i4, i1) - 1])
                            {
                                continue;
                            }

                            for (int i5 = i4 + 1; i5 < N; i5++)
                            {
                                if (mask[arith.Dist(i5, i4) - 1]
                                    && mask[arith.Dist(i5, i3) - 1]
                                    && mask[arith.Dist(i5, i2) - 1]
                                    && mask[arith.Dist(i5, i1) - 1])
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Find <c>K_6</c>.
        /// </summary>
        /// <param name="mask">red mask</param>
        /// <returns>
        /// <c>true</c> - if <c>K_6</c> is found,
        /// <c>false</c> - if not
        /// </returns>
        private bool Has6(Mask mask)
        {
            CyclicArithmetic arith = new CyclicArithmetic(N);

            for (int i1 = 0; i1 < N; i1++)
            {
                for (int i2 = i1 + 1; i2 < N; i2++)
                {
                    if (!mask[arith.Dist(i2, i1) - 1])
                    {
                        continue;
                    }

                    for (int i3 = i2 + 1; i3 < N; i3++)
                    {
                        if (!mask[arith.Dist(i3, i2) - 1]
                            || !mask[arith.Dist(i3, i1) - 1])
                        {
                            continue;
                        }

                        for (int i4 = i3 + 1; i4 < N; i4++)
                        {
                            if (!mask[arith.Dist(i4, i3) - 1]
                                || !mask[arith.Dist(i4, i2) - 1]
                                || !mask[arith.Dist(i4, i1) - 1])
                            {
                                continue;
                            }

                            for (int i5 = i4 + 1; i5 < N; i5++)
                            {
                                if (!mask[arith.Dist(i5, i4) - 1]
                                    || !mask[arith.Dist(i5, i3) - 1]
                                    || !mask[arith.Dist(i5, i2) - 1]
                                    || !mask[arith.Dist(i5, i1) - 1])
                                {
                                    continue;
                                }

                                for (int i6 = i5 + 1; i6 < N; i6++)
                                {
                                    if (mask[arith.Dist(i6, i5) - 1]
                                        && mask[arith.Dist(i6, i4) - 1]
                                        && mask[arith.Dist(i6, i3) - 1]
                                        && mask[arith.Dist(i6, i2) - 1]
                                        && mask[arith.Dist(i6, i1) - 1])
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Count of predramsey graphs paintings.
        /// </summary>
        /// <returns>paintings count</returns>
        public int PaintingsCount()
        {
            int count = 0;

            do
            {
                if (!HasRed() && !HasBlue())
                {
                    RedMask.Print(1);
                    count++;
                }
            } while (NextColoring());

            return count;
        }
    }
}
