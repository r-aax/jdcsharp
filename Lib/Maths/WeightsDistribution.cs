using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths
{
    /// <summary>
    /// Weights distribution.
    /// </summary>
    public class WeightsDistribution
    {
        /// <summary>
        /// Greedy algorithm of distribution.
        /// </summary>
        /// <param name="x">list of weights</param>
        /// <param name="x1">result list in the first basket</param>
        /// <param name="x2">result list in the second basket</param>
        public static void DistributeGreedy2(List<double> x, List<double> x1, List<double> x2)
        {
            x1.Clear();
            x2.Clear();

            x.Sort();

            double sum1 = 0.0;
            double sum2 = 0.0;
            for (int i = x.Count - 1; i >= 0; i--)
            {
                if (sum1 < sum2)
                {
                    x1.Add(x[i]);
                    sum1 += x[i];
                }
                else
                {
                    x2.Add(x[i]);
                    sum2 += x[i];
                }
            }
        }

        /// <summary>
        /// Recursive function for distribution with Delta method usage.
        /// </summary>
        /// <param name="x">list of weights</param>
        /// <param name="x1">result list in the first basket</param>
        /// <param name="x2">result list in the second basket</param>
        public static void DistributeDelta2(List<double> x, List<double> x1, List<double> x2)
        {
            x1.Clear();
            x2.Clear();

            if (x.Count < 2)
            {
                x1.AddRange(x);
            }
            else
            {
                // Split.
                List<double> tmp1 = new List<double>();
                List<double> tmp2 = new List<double>();
                for (int i = 0; i < x.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        tmp1.Add(x[i]);
                    }
                    else
                    {
                        tmp2.Add(x[i]);
                    }
                }
                if (tmp1.Sum() > tmp2.Sum())
                {
                    x1.AddRange(tmp1);
                    x2.AddRange(tmp2);
                }
                else
                {
                    x1.AddRange(tmp2);
                    x2.AddRange(tmp1);
                }

                // Find best solution.
                while (true)
                {
                    List<double> y1 = new List<double>();
                    List<double> z1 = new List<double>();
                    List<double> y2 = new List<double>();
                    List<double> z2 = new List<double>();

                    double delta_x = x1.Sum() - x2.Sum();

                    DistributeDelta2(x1, y1, z1);
                    DistributeDelta2(x2, y2, z2);

                    double delta_y = y1.Sum() - y2.Sum();
                    double delta_z = z1.Sum() - z2.Sum();

                    if ((delta_y > 0.0) && (delta_y < delta_x))
                    {
                        // Change Y.
                        x1.Clear();
                        x2.Clear();
                        x1.AddRange(y2);
                        x1.AddRange(z1);
                        x2.AddRange(y1);
                        x2.AddRange(z2);
                    }
                    else if ((delta_z > 0.0) && (delta_z < delta_x))
                    {
                        // Change Z.
                        x1.Clear();
                        x2.Clear();
                        x1.AddRange(y1);
                        x1.AddRange(z2);
                        x2.AddRange(y2);
                        x2.AddRange(z1);
                    }
                    else
                    {
                        bool is_stop = true;

                        for (int i = 0; i < x1.Count; i++)
                        {
                            if (x1[i] < delta_x)
                            {
                                x2.Add(x1[i]);
                                x1.RemoveAt(i);
                                is_stop = false;

                                break;
                            }
                        }

                        if (is_stop)
                        {
                            break;
                        }
                    }

                    if (x1.Sum() < x2.Sum())
                    {
                        List<double> tmp = new List<double>();

                        tmp.AddRange(x1);
                        x1.Clear();
                        x1.AddRange(x2);
                        x2.Clear();
                        x2.AddRange(tmp);
                    }
                }
            }
        }
    }
}
