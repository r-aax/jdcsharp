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
            Console.WriteLine("Start");
            PredramseyCircularGraphsAnalizer an = new PredramseyCircularGraphsAnalizer(102);

            DateTime from = DateTime.Now;
            List<UInt64> list = an.MultiangleMasksList(6);
            DateTime to = DateTime.Now;

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine("{0}", list[i].ToString("x16"));
            //}

            XmlSerializer serializer = new XmlSerializer(list.GetType());
            TextWriter writer = new StreamWriter("base_masks_multiangle_6.xml");
            serializer.Serialize(writer, list);
            writer.Flush();
            writer.Close();

            Console.WriteLine("Masks count = {0}", list.Count);
            Console.WriteLine("Seconds = {0}", (to - from).Seconds.ToString());

            Console.ReadKey();
        }
    }
}
