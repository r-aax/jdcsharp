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
    public class MNIST : Batch
    {
        /// <summary>
        /// In dimension.
        /// </summary>
        public readonly int ImagePixels = 784;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="images_file">images file</param>
        /// <param name="labels_file">labels file</param>
        public MNIST(string images_file, string labels_file) : base()
        {
            Debug.Assert(File.Exists(images_file));
            Debug.Assert(File.Exists(labels_file));

            // Read images file.
            using (BinaryReader reader = new BinaryReader(File.Open(images_file, FileMode.Open)))
            {
                int head = IntBigEndian(reader);

                Debug.Assert(head == 0x803);

                int count = IntBigEndian(reader);

                for (int i = 0; i < count; i++)
                {
                    double[] image = new double[ImagePixels];

                    for (int j = 0; j < ImagePixels; j++)
                    {
                        image[j] = (double)reader.ReadByte() / 255.0;
                    }

                    Inputs.Add(image);
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

                    Outputs.Add(WrapInt(label, 10));
                }
            }

            Debug.Assert(Inputs.Count == Outputs.Count);
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
    }
}
