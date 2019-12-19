using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Utils;
using Lib.Maths.Numbers;

using NNBroth.Evolution;

namespace NNBroth.Tests
{
    /// <summary>
    /// Base test case.
    /// From doubles to integer.
    /// </summary>
    public class Batch
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
        protected Batch()
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
        public double[] GetInput(int n)
        {
            return Inputs[n];
        }

        /// <summary>
        /// Get output.
        /// </summary>
        /// <param name="n">number</param>
        /// <returns>output</returns>
        public double[] GetOutput(int n)
        {
            return Outputs[n];
        }

        /// <summary>
        /// Wrap int value with doubles array.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="count">count of array elements</param>
        /// <returns>array</returns>
        protected static double[] WrapInt(int value, int count)
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
        /// <param name="output">output</param>
        private void AddTestCase(double[] input, double[] output)
        {
            Inputs.Add(input);
            Outputs.Add(output);
        }

        /// <summary>
        /// Add test case.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="int_output">integer output</param>
        /// <param name="outputs_count">outputs count</param>
        protected void AddTestCase(double[] input, int int_output, int outputs_count)
        {
            AddTestCase(input, WrapInt(int_output, outputs_count));
        }

        /// <summary>
        /// Check if answers are same.
        /// </summary>
        /// <param name="answer1">first answer</param>
        /// <param name="answer2">second answer</param>
        /// <returns>check result</returns>
        public static bool IsAnswersEq(double[] answer1, double[] answer2)
        {
            return Arrays.MaxIndex(answer1) == Arrays.MaxIndex(answer2);
        }

        /// <summary>
        /// Test cortex for test case.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="n">test case number</param>
        /// <returns>check result</returns>
        public bool Check(Cortex cortex, int n)
        {
            double[] cortex_output = cortex.SenseForward(GetInput(n));
            double[] right_output = GetOutput(n);

            return IsAnswersEq(cortex_output, right_output);
        }

        /// <summary>
        /// Calculate percent of right answers.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <returns>right answers percent</returns>
        public double RightAnswersPart(Cortex cortex)
        {
            int c = 0;

            for (int i = 0; i < TestCasesCount; i++)
            {
                if (Check(cortex, i))
                {
                    c++;
                }
            }

            return (double)c / (double)TestCasesCount;
        }

        /// <summary>
        /// Difference between cortex answer and right answer.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <param name="n">test number</param>
        /// <returns>difference</returns>
        public double Cost(Cortex cortex, int n)
        {
            cortex.SenseForward(GetInput(n));

            return cortex.Cost(GetOutput(n));
        }

        /// <summary>
        /// Total cost for batch.
        /// </summary>
        /// <param name="cortex">cortex</param>
        /// <returns>total cost</returns>
        public double TotalCost(Cortex cortex)
        {
            double tc = 0.0;

            for (int i = 0; i < TestCasesCount; i++)
            {
                tc += Cost(cortex, i);
            }

            return tc / TestCasesCount;
        }

        /// <summary>
        /// Create mini batch.
        /// </summary>
        /// <param name="n">test cases count</param>
        /// <returns>mini batch</returns>
        public Batch RandomMiniBatch(int n)
        {
            Batch test = new Batch();

            for (int i = 0; i < n; i++)
            {
                int index = Randoms.RandomInt(TestCasesCount - 1);

                test.AddTestCase(GetInput(index), GetOutput(index));
            }

            return test;
        }
    }
}
