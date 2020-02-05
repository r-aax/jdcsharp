﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using Lib.Maths.Numbers;
using Lib.Utils;

namespace Lib.Neuro.Net
{
    /// <summary>
    /// Cortex.
    /// </summary>
    public class Cortex : ICloneable
    {
        /// <summary>
        /// Current id.
        /// </summary>
        public static int CurId = 0;

        /// <summary>
        /// Own id.
        /// </summary>
        public int Id;

        /// <summary>
        /// Min border of random neuron bias.
        /// </summary>
        public static readonly double RandomNeuronBiasMin = -0.1;

        /// <summary>
        /// Max border of random neuron bias.
        /// </summary>
        public static readonly double RandomNeuronBiasMax = 0.1;

        /// <summary>
        /// Min border of random link weight.
        /// </summary>
        public static readonly double RandomLinkWeightMin = -0.1;

        /// <summary>
        /// Max border of random link weight.
        /// </summary>
        public static readonly double RandomLinkWeightMax = 0.1;

        /// <summary>
        /// Random neuron bias.
        /// </summary>
        public static double RandomNeuronBias
        {
            get
            {
                return Randoms.RandomInInterval(RandomNeuronBiasMin, RandomNeuronBiasMax);
            }
        }

        /// <summary>
        /// Random link weight.
        /// </summary>
        public static double RandomLinkWeight
        {
            get
            {
                return Randoms.RandomInInterval(RandomLinkWeightMin, RandomLinkWeightMax);
            }
        }

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
        public List<Neuron> Neurons;

        /// <summary>
        /// Links.
        /// </summary>
        public List<Link> Links;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cortex()
        {
            Id = CurId;
            CurId++;

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
            Neuron neuron = new Neuron(RandomNeuronBias);

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
        private void Link(Neuron src, Neuron dst, double weight)
        {
            Link link = new Link(src, dst, weight);

            // Insert it into links list.
            Links.Add(link);

            src.AddOutLink(link);
            dst.AddInLink(link);
        }

        /// <summary>
        /// Check if two neurons are linked.
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dst">destination</param>
        /// <returns>link</returns>
        private Link FindLink(Neuron src, Neuron dst)
        {
            foreach (Link link in Links)
            {
                if ((link.Src == src) && (link.Dst == dst))
                {
                    return link;
                }
            }

            return null;
        }

        /// <summary>
        /// Delete link.
        /// </summary>
        /// <param name="link">link</param>
        private void Unlink(Link link)
        {
            link.Src.OutLinks.Remove(link);
            link.Dst.InLinks.Remove(link);
            Links.Remove(link);
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
                    Link(src, dst, RandomLinkWeight);
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
        /// Reorder neurons.
        /// </summary>
        private void ReorderNeurons()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.PredAdded = 0;
            }

            List<Neuron> new_neurons = new List<Neuron>();
            Queue<Neuron> queue = new Queue<Neuron>();

            foreach (Neuron neuron in Sensor.Neurons)
            {
                queue.Enqueue(neuron);
            }

            while (queue.Count > 0)
            {
                Neuron qh = queue.Dequeue();

                new_neurons.Add(qh);

                foreach (Link link in qh.OutLinks)
                {
                    Neuron dst = link.Dst;

                    dst.PredAdded++;

                    if (dst.PredAdded == dst.InLinks.Count)
                    {
                        queue.Enqueue(dst);
                    }
                }
            }

            foreach (Neuron neuron in Neurons)
            {
                if (new_neurons.IndexOf(neuron) < 0)
                {
                    while (neuron.InLinks.Count > 0)
                    {
                        Unlink(neuron.InLinks[0]);
                    }

                    while (neuron.OutLinks.Count > 0)
                    {
                        Unlink(neuron.OutLinks[0]);
                    }
                }
            }

            Neurons = new_neurons;
        }

        /// <summary>
        /// Order elements.
        /// </summary>
        public void OrderElements()
        {
            ReorderNeurons();

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
        /// <returns>answer</returns>
        public double[] SenseForward(double[] inputs)
        {
            // Invalidate signals for debug.
            InvalidateData();

            Sensor.Sense(inputs);

            // Process neurons in right order.
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].PropagateSignalForward();
            }

            return Actuator.GetOutputs();
        }

        /// <summary>
        /// Sense back.
        /// </summary>
        /// <param name="ys">right answer vector</param>
        public void SenseBack(double[] ys)
        {
            Actuator.SenseBackInitLastLayer(ys);

            // Process neurons in reversed order.
            for (int i = Neurons.Count - 1; i >= 0; i--)
            {
                Neurons[i].PropagateErrorBack();
            }
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

        /// <summary>
        /// Get string with intervals of weights and biases.
        /// </summary>
        /// <returns>string</returns>
        public string WeightsBiasesIntervalsStr()
        {
            double min_weight = Links.Min(link => link.Weight);
            double max_weight = Links.Max(link => link.Weight);
            double min_bias = Neurons.Min(neuron => neuron.Bias);
            double max_bias = Neurons.Max(neuron => neuron.Bias);

            return String.Format("W [{0:F10}, {1:F10}], B [{2:F10}, - {3:F10}]", min_weight, max_weight, min_bias, max_bias);
        }

        /// <summary>
        /// Reset neuronet parameters : neurons biases and links weights.
        /// </summary>
        public void ResetNeuronsBiasesAndLinksWeights()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.Bias = RandomNeuronBias;
            }

            foreach (Link link in Links)
            {
                link.Weight = RandomLinkWeight;
            }
        }

        /// <summary>
        /// Add new neuron mutate.
        /// </summary>
        public void MutateAddNeuronOnLink()
        {
            // Select link.
            int link_num = Randoms.RandomInt(Links.Count - 1);
            Link link = Links[link_num];

            // Get link src/dst.
            Neuron src = link.Src;
            Neuron dst = link.Dst;

            // New neuron.
            Neuron new_neuron = NewNeuron();

            // Relink.
            Unlink(link);
            Link(src, new_neuron, 1.0);
            Link(new_neuron, dst, 1.0);            
        }

        /// <summary>
        /// Add one neuron and two links.
        /// </summary>
        public void MutateAddNeuronAndTwoLinks()
        {
            // Select pair of neurons.
            int n1 = Randoms.RandomInt(Neurons.Count - 1);
            int n2 = Randoms.RandomInt(Neurons.Count - 1);
            int src, dst;

            if (n1 != n2)
            {
                src = Math.Min(n1, n2);
                dst = Math.Max(n1, n2);
                Neuron neuron_src = Neurons[src];
                Neuron neuron_dst = Neurons[dst];

                if (!(neuron_src.IsFirstLayer && neuron_dst.IsFirstLayer)
                    && !(neuron_src.IsLastLayer && neuron_dst.IsLastLayer))
                {
                    Neuron new_neuron = NewNeuron();

                    Link(neuron_src, new_neuron, 1.0);
                    Link(new_neuron, neuron_dst, 1.0);
                }
            }
        }

        /// <summary>
        /// Add new link mutate.
        /// </summary>
        public void MutateAddLink()
        {
            // Select pair of neurons.
            int n1 = Randoms.RandomInt(Neurons.Count - 1);
            int n2 = Randoms.RandomInt(Neurons.Count - 1);
            int src, dst;

            if (n1 != n2)
            {
                src = Math.Min(n1, n2);
                dst = Math.Max(n1, n2);
                Neuron neuron_src = Neurons[src];
                Neuron neuron_dst = Neurons[dst];

                if ((!neuron_src.IsFirstLayer && !neuron_src.IsLastLayer)
                    || (!neuron_dst.IsFirstLayer && !neuron_dst.IsLastLayer))
                {
                    Link link = FindLink(neuron_src, neuron_dst);

                    if (link == null)
                    {
                        Link(neuron_src, neuron_dst, 1.0);
                    }
                }
            }
        }
 
        /// <summary>
        /// Delete link mutate.
        /// </summary>
        public void MutateDeleteLink()
        {
            // Select pair of neurons.
            int n1 = Randoms.RandomInt(Neurons.Count - 1);
            int n2 = Randoms.RandomInt(Neurons.Count - 1);
            int src, dst;

            if (n1 != n2)
            {
                src = Math.Min(n1, n2);
                dst = Math.Max(n1, n2);
                Neuron neuron_src = Neurons[src];
                Neuron neuron_dst = Neurons[dst];
                Link link = FindLink(neuron_src, neuron_dst);

                if (link != null)
                {
                    Unlink(link);
                }
            }
        }

        /// <summary>
        /// Mutate.
        /// </summary>
        public void Mutate()
        {
            double r = Randoms.RandomDouble(4.0);

            if (r < 1.0)
            {
                MutateAddNeuronOnLink();
            }
            else if (r < 2.0)
            {
                MutateAddNeuronAndTwoLinks();
            }
            else if (r < 3.0)
            {
                MutateAddLink();
            }
            else
            {
                MutateDeleteLink();
            }

            OrderElements();
        }
    }
}
