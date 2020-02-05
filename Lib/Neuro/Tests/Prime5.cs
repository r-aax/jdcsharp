using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Neuro.Tests
{
    /// <summary>
    /// Xor with 5 variables.
    /// </summary>
    public class Prime5 : Batch
    {
        /// <summary>
        /// Constructor by default.
        /// </summary>
        public Prime5() : base()
        {
            BaseName = "Prime5";
            SuffName = "full";

            // Cases.
            AddTestCase(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 0.0, 0.0, 0.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 0.0, 0.0, 1.0, 0.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 0.0, 0.0, 1.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 0.0, 1.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 0.0, 1.0, 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 0.0, 1.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 0.0, 1.0, 1.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 1.0, 0.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0, 0.0, 0.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0, 0.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0, 0.0, 1.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 1.0, 1.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0, 1.0, 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 0.0, 1.0, 1.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0, 1.0, 1.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 0.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 0.0, 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 0.0, 0.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 0.0, 1.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 0.0, 1.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 1.0, 0.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 1.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 0.0, 1.0, 1.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 1.0, 0.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 0.0, 0.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 0.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 0.0, 1.0, 1.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 1.0, 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 1.0, 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 1.0, 1.0, 1.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0 }, 1, 2);
        }
    }
}
