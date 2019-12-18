﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Tests
{
    /// <summary>
    /// Xor test.
    /// </summary>
    public class Xor : Test
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Xor() : base()
        {
            AddTestCase(new double[] { 0.0, 0.0 }, 0, 2);
            AddTestCase(new double[] { 0.0, 1.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 0.0 }, 1, 2);
            AddTestCase(new double[] { 1.0, 1.0 }, 0, 2);
        }
    }
}
