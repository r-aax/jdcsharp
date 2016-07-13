using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            PredramseyCircularGraphsAnalizer an = new PredramseyCircularGraphsAnalizer(17);

            Console.WriteLine(an.PaintingsCount(4, 4));

            Console.ReadKey();
        }
    }
}
