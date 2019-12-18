using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;

using NNBroth.Evolution;

namespace NNBroth.Tests
{
    /// <summary>
    /// Base test case.
    /// From doubles to integer.
    /// </summary>
    public abstract class Test
    {
        /// <summary>
        /// List of inputs.
        /// </summary>
        protected List<double[]> Inputs;

        /// <summary>
        /// List of outputs.
        /// </summary>
        protected List<double[]> Outputs;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected Test()
        {
            Inputs = new List<double[]>();
            Outputs = new List<double[]>();
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
                return Outputs[0].Length;
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
        private double[] GetOutput(int n)
        {
            return Outputs[n];
        }

        /// <summary>
        /// Wrap int value with doubles array.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="count">count of array elements</param>
        /// <returns>array</returns>
        protected double[] WrapInt(int value, int count)
        {
            double[] output = new double[count];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = 0.0;
            }

            output[value] = 1.0;

            return output;
        }

        /// <summary>
        /// Add test case.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="int_output">integer output</param>
        /// <param name="outputs_count">outputs count</param>
        protected void AddTestCase(double[] input, int int_output, int outputs_count)
        {
            Inputs.Add(input);
            Outputs.Add(WrapInt(int_output, outputs_count));
        }

        /// <summary>
        /// Test cortex for test case.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="n">test case number</param>
        /// <returns>check result</returns>
        public bool Check(Cortex cortex, int n)
        {
            double[] cortex_output = cortex.Sense(GetInput(n));
            double[] right_output = GetOutput(n);

            return Arrays.MaxIndex(cortex_output) == Arrays.MaxIndex(right_output);
        }

        /// <summary>
        /// Difference between cortex answer and right answer.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="n">test number</param>
        /// <returns>difference</returns>
        public double Diff(Cortex cortex, int n)
        {
            double[] cortex_output = cortex.Sense(GetInput(n));
            double[] right_output = GetOutput(n);

            return Arrays.MeanSquareDifference(cortex_output, right_output);
        }
    }
}
