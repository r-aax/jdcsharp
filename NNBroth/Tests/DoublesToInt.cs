using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NNBroth.Evolution;

namespace NNBroth.Tests
{
    /// <summary>
    /// Base test case.
    /// From doubles to integer.
    /// </summary>
    public abstract class DoublesToInt
    {
        /// <summary>
        /// List of inputs.
        /// </summary>
        protected List<double[]> Inputs;

        /// <summary>
        /// List of outputs.
        /// </summary>
        protected List<int> Outputs;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DoublesToInt()
        {
            Inputs = new List<double[]>();
            Outputs = new List<int>();
        }

        /// <summary>
        /// Count of test cases.
        /// </summary>
        public int TestCasesCount
        {
            get
            {
                return Inputs.Count;
            }
        }

        /// <summary>
        /// Dimension of input.
        /// </summary>
        public int InputDimension
        {
            get
            {
                return Inputs[0].Length;
            }
        }

        /// <summary>
        /// Dimension of output.
        /// </summary>
        public int OutputDimension
        {
            get
            {
                return Outputs.Max() + 1;
            }
        }

        /// <summary>
        /// Get input.
        /// </summary>
        /// <param name="n">number</param>
        /// <returns>input</returns>
        private double[] GetInput(int n)
        {
            return Inputs[n];
        }

        /// <summary>
        /// Get output.
        /// </summary>
        /// <param name="n">number</param>
        /// <returns>output</returns>
        private int GetOutput(int n)
        {
            return Outputs[n];
        }

        /// <summary>
        /// Add test case.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="output">output</param>
        protected void AddTestCase(double[] input, int output)
        {
            Inputs.Add(input);
            Outputs.Add(output);
        }

        /// <summary>
        /// Test cortex for test case.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="n">test case number</param>
        /// <returns></returns>
        public bool Test(Cortex cortex, int n)
        {
            return cortex.Sense(GetInput(n)) == GetOutput(n);
        }
    }
}
