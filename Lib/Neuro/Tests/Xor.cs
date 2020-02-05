﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Neuro.Tests
{
    /// <summary>
    /// Xor test.
    /// </summary>
    public class Xor : Batch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Xor() : base()
        {
            BaseName = "Xor";
            SuffName = "full";

            // Cases.
            AddTestCase(new double[] { 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 0.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 1.0 }, 0, 2);
        }
    }
}
