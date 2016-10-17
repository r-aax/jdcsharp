using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

using Lib.Maths.Bits;
using Lib.Maths.Numbers.RamseyNumbers;

namespace Test
{
    /// <summary>
    /// Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Enter point.
        /// </summary>
        /// <param name="args">arguments</param>
        static void Main(string[] args)
        {
            TestWeightsDistribution();
        }

        /// <summary>
        /// Test for weights distribution.
        /// </summary>
        static void TestWeightsDistribution()
        {
            List<double> x = new List<double>(new double[] { 6.0, 3.0, 4.0, 2.0, 0.0, 2.0, 0.0, 1.0 });
            List<double> x1 = new List<double>();
            List<double> x2 = new List<double>();

            for (int i = 0; i < 200; i++)
            {
                //x.Add((double)i * (double)i);
            }

            Lib.Maths.WeightsDistribution.DistributeDelta2(x, x1, x2);
            Console.WriteLine("Delta2");
            Console.Write("X1 :");
            foreach (double d in x1)
            {
                Console.Write(" " + d.ToString());
            }
            Console.WriteLine(" : " + x1.Sum());
            Console.Write("X2 :");
            foreach (double d in x2)
            {
                Console.Write(" " + d.ToString());
            }
            Console.WriteLine(" : " + x2.Sum());

            Lib.Maths.WeightsDistribution.DistributeGreedy2(x, x1, x2);
            Console.WriteLine("Greedy2");
            Console.Write("X1 :");
            foreach (double d in x1)
            {
                Console.Write(" " + d.ToString());
            }
            Console.WriteLine(" : " + x1.Sum());
            Console.Write("X2 :");
            foreach (double d in x2)
            {
                Console.Write(" " + d.ToString());
            }
            Console.WriteLine(" : " + x2.Sum());

            Console.ReadKey();
        }
    }
}
