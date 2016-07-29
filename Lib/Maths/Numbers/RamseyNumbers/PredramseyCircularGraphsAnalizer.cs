// Author: Alexey Rybakov

using System.Diagnostics;
using System.Collections.Generic;
using System;

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
        /// Order / 2.
        /// </summary>
        private int Nd2;

        /// <summary>
        /// Red edges mask.
        /// </summary>
        private Mask RedMask;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="n">order</param>
        public PredramseyCircularGraphsAnalizer(int n)
        {
            // Set sizes.
            N = n;
            Nd2 = N / 2;

            // Set empty red mask.
            RedMask = Mask.MakeEmpty(Nd2);
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
        /// <param name="kr">red clique size</param>
        /// <returns>
        /// <c>true</c> - if graph has red clique,
        /// <c>false</c> - if graph do not have red clique
        /// </returns>
        public bool HasRed(int kr)
        {
            return HasFull(RedMask, kr);
        }

        /// <summary>
        /// Check for blue clique.
        /// </summary>
        /// <param name="kb">blue clique size</param>
        /// <returns>
        /// <c>true</c> - if graph has blue clique,
        /// <c>false</c> - if graph do not have blue clique
        /// </returns>
        public bool HasBlue(int kb)
        {
            return HasFull(RedMask.Invert(), kb);
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
        /// <param name="kr">red clique size</param>
        /// <param name="kb">blue clique size</param>
        /// <returns>paintings count</returns>
        public int PaintingsCount(int kr, int kb)
        {
            int count = 0;

            do
            {
                if (!HasRed(kr) && !HasBlue(kb))
                {
                    RedMask.Print(1);
                    count++;
                }
            } while (NextColoring());

            return count;
        }

        /// <summary>
        /// Get list of masks, generated from multiangle chords.
        /// </summary>
        /// <param name="k">count of angles</param>
        /// <returns>list of masks</returns>
        public List<UInt64> MultiangleMasksList(int k)
        {
            switch (k)
            {
                case 6:
                    return For6AngleMasksList();

                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Get list of masks, generated from 4-angle chords.
        /// </summary>
        /// <returns>list of masks</returns>
        public List<Mask> For4AngleMasksList()
        {
            List<Mask> list = new List<Mask>();
            CyclicArithmetic ar = new CyclicArithmetic(N);

            // First node is always 0.
            int i0 = 0;

            // Second node (from i0 + 1 to N - 1).
            for (int i1 = i0 + 1; i1 <= N - 1; i1++)
            {
                // Third node (from i1 + 1 to N - 1).
                for (int i2 = i1 + 1; i2 <= N - 1; i2++)
                {
                    // Fourth node (from i2 + 1 to N - 1).
                    for (int i3 = i2 + 1; i3 <= N - 1; i3++)
                    {
                        // Create mask.
                        Mask m = Mask.MakeEmpty(Nd2);
                        m[ar.Dist(i0, i1) - 1] = true;
                        m[ar.Dist(i0, i2) - 1] = true;
                        m[ar.Dist(i0, i3) - 1] = true;
                        m[ar.Dist(i1, i2) - 1] = true;
                        m[ar.Dist(i1, i3) - 1] = true;
                        m[ar.Dist(i2, i3) - 1] = true;

                        // Add mask to list.
                        list.RemoveAll(e => e.IsContain(m));
                        if (!list.Exists(e => m.IsContain(e)))
                        {
                            list.Add(m);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get list of masks, generated from 5-angle chords.
        /// </summary>
        /// <returns>list of masks</returns>
        public List<Mask> For5AngleMasksList()
        {
            List<Mask> list = new List<Mask>();
            CyclicArithmetic ar = new CyclicArithmetic(N);

            // First node is always 0.
            int i0 = 0;

            // Second node (from i0 + 1 to N - 1).
            for (int i1 = i0 + 1; i1 <= N - 1; i1++)
            {
                // Third node (from i1 + 1 to N - 1).
                for (int i2 = i1 + 1; i2 <= N - 1; i2++)
                {
                    // Fourth node (from i2 + 1 to N - 1).
                    for (int i3 = i2 + 1; i3 <= N - 1; i3++)
                    {
                        // Fifth node (from i3 + 1 to N - 1).
                        for (int i4 = i3 + 1; i4 <= N - 1; i4++)
                        {
                            // Create mask.
                            Mask m = Mask.MakeEmpty(Nd2);
                            m[ar.Dist(i0, i1) - 1] = true;
                            m[ar.Dist(i0, i2) - 1] = true;
                            m[ar.Dist(i0, i3) - 1] = true;
                            m[ar.Dist(i0, i4) - 1] = true;
                            m[ar.Dist(i1, i2) - 1] = true;
                            m[ar.Dist(i1, i3) - 1] = true;
                            m[ar.Dist(i1, i4) - 1] = true;
                            m[ar.Dist(i2, i3) - 1] = true;
                            m[ar.Dist(i2, i4) - 1] = true;
                            m[ar.Dist(i3, i4) - 1] = true;

                            // Add mask to list.
                            list.RemoveAll(e => e.IsContain(m));
                            if (!list.Exists(e => m.IsContain(e)))
                            {
                                list.Add(m);
                            }
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get list of masks, generated from 6-angle chords.
        /// </summary>
        /// <returns>list of masks</returns>
        public List<UInt64> For6AngleMasksList()
        {
            HashSet<UInt64> hs = new HashSet<UInt64>();
            CyclicArithmetic ar = new CyclicArithmetic(N);

            // First node is always 0.
            int i0 = 0;

            // Second node (from i0 + 1 to N - 1).
            for (int i1 = i0 + 1; i1 <= N - 1; i1++)
            {
                int d01 = ar.Dist(i0, i1) - 1;
                UInt64 m1 = (1ul << d01);

                // d01 - minimal chord.

                // Third node (from i1 + 1 to N - 1).
                for (int i2 = i1 + 1; i2 <= N - 1; i2++)
                {
                    int d12 = ar.Dist(i1, i2) - 1;

                    if (d12 > d01)
                    {
                        continue;
                    }

                    int d02 = ar.Dist(i0, i2) - 1;
                    UInt64 m2 = m1 | (1ul << d02) | (1ul << d12);

                    // Fourth node (from i2 + 1 to N - 1).
                    for (int i3 = i2 + 1; i3 <= N - 1; i3++)
                    {
                        int d23 = ar.Dist(i2, i3) - 1;

                        if (d23 > d01)
                        {
                            continue;
                        }

                        int d03 = ar.Dist(i0, i3) - 1;
                        int d13 = ar.Dist(i1, i3) - 1;
                        UInt64 m3 = m2 | (1ul << d03) | (1ul << d13) | (1ul << d23);

                        // Fifth node (from i3 + 1 to N - 1).
                        for (int i4 = i3 + 1; i4 <= N - 1; i4++)
                        {
                            int d34 = ar.Dist(i3, i4) - 1;

                            if (d34 > d01)
                            {
                                continue;
                            }

                            int d04 = ar.Dist(i0, i4) - 1;
                            int d14 = ar.Dist(i1, i4) - 1;
                            int d24 = ar.Dist(i2, i4) - 1;
                            UInt64 m4 = m3 | (1ul << d04) | (1ul << d14) | (1ul << d24) | (1ul << d34);

                            // Sixth node (from i4 + 1 to N - 1).
                            for (int i5 = i4 + 1; i5 <= N - 1; i5++)
                            {
                                int d05 = ar.Dist(i0, i5) - 1;
                                int d45 = ar.Dist(i4, i5) - 1;

                                if ((d05 > d01) || (d45 > d01))
                                {
                                    continue;
                                }

                                int d15 = ar.Dist(i1, i5) - 1;
                                int d25 = ar.Dist(i2, i5) - 1;
                                int d35 = ar.Dist(i3, i5) - 1;
                                UInt64 m5 = m4
                                            | (1ul << d05) | (1ul << d15) | (1ul << d25)
                                            | (1ul << d35) | (1ul << d45);

                                // Add mask to list.
                                hs.Add(m5);
                            }
                        }
                    }
                }
            }

            // Now we need to delete dublicates and overlapped elements and return the list.

            List<UInt64> list = new List<UInt64>(hs as ICollection<UInt64>);
            list.Sort();

            // It it too long.

            //int i = 0;
            //while (i < list.Count)
            //{
            //    if ((i % 100) == 0)
            //    {
            //        Console.WriteLine("Count = {0}, i = {1}", list.Count, i);
            //    }
            //    
            //    UInt64 u = list[i];
            //
            //    if (list.RemoveAll(e => (e != u) && ((e & u) == u)) == 0)
            //    {
            //        i++;
            //    }
            //}
            //
            //for (int ii = 0; ii < list.Count; ii++)
            //{
            //    for (int jj = ii + 1; jj < list.Count; jj++)
            //    {
            //        Debug.Assert(!(((list[ii] & list[jj]) == list[ii])
            //                       || ((list[ii] & list[jj]) == list[jj])));
            //    }
            //}

            return list;
        }
    }
}
