using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;

namespace NNBroth.Tests
{
    /// <summary>
    /// MNIST tests.
    /// </summary>
    public class MNIST
    {
        /// <summary>
        /// In dimension.
        /// </summary>
        public readonly int InDimension = 784;

        /// <summary>
        /// Out dimension.
        /// </summary>
        public readonly int OutDimension = 10;

        /// <summary>
        /// Images list.
        /// </summary>
        private List<int[]> Images;

        /// <summary>
        /// Labels list.
        /// </summary>
        private List<int> Labels;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="images_file">images file</param>
        /// <param name="labels_file">labels file</param>
        public MNIST(string images_file, string labels_file)
        {
            Debug.Assert(File.Exists(images_file));
            Debug.Assert(File.Exists(labels_file));

            // Empty lists of test data.
            Images = new List<int[]>();
            Labels = new List<int>();

            // Read images file.
            using (BinaryReader reader = new BinaryReader(File.Open(images_file, FileMode.Open)))
            {
                int head = IntBigEndian(reader);

                Debug.Assert(head == 0x803);

                int count = IntBigEndian(reader);

                for (int i = 0; i < count; i++)
                {
                    int image_pixels = InDimension;
                    int[] image = new int[image_pixels];

                    for (int j = 0; j < image_pixels; j++)
                    {
                        image[j] = (int)reader.ReadByte();
                    }

                    Images.Add(image);
                }
            }

            // Read labels file.
            using (BinaryReader reader = new BinaryReader(File.Open(labels_file, FileMode.Open)))
            {
                int head = IntBigEndian(reader);

                Debug.Assert(head == 0x801);

                int count = IntBigEndian(reader);

                for (int i = 0; i < count; i++)
                {
                    int label = (int)reader.ReadByte();

                    Labels.Add(label);
                }
            }

            Debug.Assert(Images.Count == Labels.Count);
        }

        /// <summary>
        /// Read integer from binary reader.
        /// </summary>
        /// <param name="reader">reader</param>
        /// <returns>integer value</returns>
        private int IntBigEndian(BinaryReader reader)
        {
            var data = reader.ReadBytes(4);

            Array.Reverse(data);

            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Test cases count.
        /// </summary>
        public int TestCasesCount
        {
            get
            {
                return Images.Count;
            }
        }

        /// <summary>
        /// Get test case.
        /// </summary>
        /// <param name="n">test case number</param>
        /// <returns>test case</returns>
        public double[] GetTestCase(int n)
        {
            int[] tc = Images[n];
            double[] res = new double[tc.Length];

            for (int i = 0; i < tc.Length; i++)
            {
                res[i] = (double)tc[i] / 255.0;
            }

            return res;
        }

        /// <summary>
        /// Get test case answer.
        /// </summary>
        /// <param name="n">number</param>
        /// <returns>answer</returns>
        public double[] GetTestCaseAnswer(int n)
        {
            double[] res = new double[OutDimension];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 0.0;
            }

            res[Labels[n]] = 1.0;

            return res;
        }
    }
}
