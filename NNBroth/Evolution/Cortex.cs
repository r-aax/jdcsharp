using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Utils;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Cortex.
    /// </summary>
    public class Cortex : ICloneable
    {
        /// <summary>
        /// Sensor.
        /// </summary>
        private Sensor Sensor;

        /// <summary>
        /// Actuator.
        /// </summary>
        private Actuator Actuator;

        /// <summary>
        /// List of neurons.
        /// </summary>
        private List<Neuron> Neurons;

        /// <summary>
        /// Links.
        /// </summary>
        private List<Link> Links;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cortex()
        {
            Sensor = new Sensor();
            Actuator = new Actuator();
            Neurons = new List<Neuron>();
            Links = new List<Link>();
        }

        /// <summary>
        /// Create multilayer cortex.
        /// </summary>
        /// <param name="layers">layers sizes</param>
        /// <returns>cortex</returns>
        public static Cortex CreateMultilayerCortex(int[] layers)
        {
            Debug.Assert(layers.Length >= 2);

            Cortex cortex = new Cortex();
            List<Neuron> dst_layer = null;
            List<Neuron> src_layer = null;

            // Sensor.
            dst_layer = cortex.CreateNeuronsLayer(layers[0]);
            cortex.LinkSensorToLayer(dst_layer);

            // Inner links.
            for (int i = 1; i < layers.Length; i++)
            {
                src_layer = dst_layer;
                dst_layer = cortex.CreateNeuronsLayer(layers[i]);
                cortex.LinkLayers(src_layer, dst_layer);
            }

            // Actuator.
            src_layer = dst_layer;
            cortex.LinkLayerToActuator(src_layer);

            cortex.OrderElements();

            return cortex;
        }

        /// <summary>
        /// Add new neuron.
        /// </summary>
        /// <returns>neuron</returns>
        private Neuron NewNeuron()
        {
            Neuron neuron = new Neuron();

            // Insert it into neurons list.
            Neurons.Add(neuron);

            return neuron;
        }

        /// <summary>
        /// Link two neurons.
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dst">destination</param>
        /// <param name="weight">weight</param>
        private void Link(Neuron src, Neuron dst, double weight = 1.0)
        {
            Link link = new Link(src, dst, weight);

            // Insert it into links list.
            Links.Add(link);

            src.AddOutLink(link);
            dst.AddInLink(link);
        }

        /// <summary>
        /// Create layer of neurons.
        /// </summary>
        /// <param name="count">count of neurons in the layer</param>
        /// <returns>layer</returns>
        private List<Neuron> CreateNeuronsLayer(int count)
        {
            List<Neuron> layer = new List<Neuron>();

            // Fill the layer.
            for (int i = 0; i < count; i++)
            {
                layer.Add(NewNeuron());
            }

            return layer;
        }

        /// <summary>
        /// Link two neurons layers.
        /// </summary>
        /// <param name="src_layer">source layer</param>
        /// <param name="dst_layer">destination layer</param>
        private void LinkLayers(List<Neuron> src_layer, List<Neuron> dst_layer)
        {
            foreach (Neuron src in src_layer)
            {
                foreach (Neuron dst in dst_layer)
                {
                    Link(src, dst);
                }
            }
        }

        /// <summary>
        /// Link sensor to the layer.
        /// </summary>
        /// <param name="layer">layer</param>
        private void LinkSensorToLayer(List<Neuron> layer)
        {
            foreach (Neuron dst in layer)
            {
                Sensor.Neurons.Add(dst);
            }
        }

        /// <summary>
        /// Link Layer to actuator.
        /// </summary>
        /// <param name="layer"></param>
        private void LinkLayerToActuator(List<Neuron> layer)
        {
            foreach (Neuron src in layer)
            {
                Actuator.Neurons.Add(src);
            }
        }

        /// <summary>
        /// Invalidate all signals.
        /// </summary>
        public void InvalidateData()
        {
            // Neurons data.
            foreach (Neuron neuron in Neurons)
            {
                neuron.Accumulator = double.NaN;
                neuron.Error = double.NaN;
            }

            // Links data.
            foreach (Link link in Links)
            {
                link.Signal = double.NaN;
                link.Error = double.NaN;
            }
        }

        /// <summary>
        /// Order elements.
        /// </summary>
        public void OrderElements()
        {
            // TODO: set right order of nodes.

            // Set neurons ids.
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Id = i;
            }

            // Set links ids.
            for (int i = 0; i < Links.Count; i++)
            {
                Links[i].Id = i;
            }
        }

        /// <summary>
        /// Sense signals.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public void SenseForward(double[] inputs)
        {
            // Invalidate signals for debug.
            InvalidateData();

            Sensor.Sense(inputs);

            // Process neurons in right order.
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].PropagateSignalForward();
            }
        }

        /// <summary>
        /// Sense back.
        /// </summary>
        /// <param name="outputs">outputs</param>
        public void SenseBack(double[] outputs)
        {
            throw new Exception("TODO");
        }

        /// <summary>
        /// Sense with outputs return.
        /// </summary>
        /// <param name="inputs">inputs</param>
        /// <returns>outputs</returns>
        public double[] Sense(double[] inputs)
        {
            SenseForward(inputs);

            return Actuator.GetOutputs();
        }

        /// <summary>
        /// Get cost after cortex sense.
        /// </summary>
        /// <param name="y">right answer</param>
        /// <returns>cost</returns>
        public double Cost(double[] y)
        {
            double[] a = Actuator.GetOutputs();

            return 0.5 * Arrays.SquareDifference(a, y);
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            Cortex cortex = new Cortex();

            // Clone neurons.
            for (int i = 0; i < Neurons.Count; i++)
            {
                cortex.Neurons.Add(Neurons[i].Clone() as Neuron);
            }

            // Clone links.
            for (int i = 0; i < Links.Count; i++)
            {
                cortex.Links.Add(Links[i].Clone() as Link);
            }

            // Restore links.
            foreach (Link link in Links)
            {
                int link_index = Links.IndexOf(link);
                Link cortex_link = cortex.Links[link_index];

                int src_index = Neurons.IndexOf(link.Src as Neuron);
                cortex.Neurons[src_index].OutLinks.Add(cortex_link);
                cortex_link.Src = cortex.Neurons[src_index];

                int dst_index = Neurons.IndexOf(link.Dst as Neuron);
                cortex.Neurons[dst_index].InLinks.Add(cortex_link);
                cortex_link.Dst = cortex.Neurons[dst_index];
            }

            // Restore sensor and actuator data.
            foreach (Neuron neuron in Sensor.Neurons)
            {
                cortex.Sensor.Neurons.Add(cortex.Neurons[neuron.Id]);
            }
            foreach (Neuron neuron in Actuator.Neurons)
            {
                cortex.Actuator.Neurons.Add(cortex.Neurons[neuron.Id]);
            }

            return cortex;
        }

        /// <summary>
        /// Zero delta biases for neurons and delta weights for links.
        /// </summary>
        public void ZeroDWeightsAndDBiases()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.dBias = 0.0;
            }

            foreach (Link link in Links)
            {
                link.dWeight = 0.0;
            }
        }

        /// <summary>
        /// Store delta biases for neurons and delta weights for links.
        /// </summary>
        public void StoreDWeightsAndDBiases()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.dBias += neuron.Error;

                foreach (Link link in neuron.InLinks)
                {
                    link.dWeight += link.Signal * neuron.Error;
                }
            }
        }

        /// <summary>
        /// Correct neurons biases and links weight.
        /// </summary>
        /// <param name="eta">learning rate</param>
        public void CorrectWeightsAndBiases(double eta)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.Bias -= eta * neuron.dBias;
            }

            foreach (Link link in Links)
            {
                link.Weight -= eta * link.dWeight;
            }
        }
    }
}
