using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

using Lib.Maths;

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
            NVector v = new NVector(5);
            Console.WriteLine(v);
           
            Console.ReadKey();
        }
    }
}
