﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using Lib.Maths.Numbers;

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
        /// Constructor.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public Cortex(int sensor_dimension, int actuator_dimension) : this()
        {
            List<Neuron> first_layer = CreateNeuronsLayer(sensor_dimension);
            List<Neuron> last_layer = CreateNeuronsLayer(actuator_dimension);

            LinkSensorToLayer(first_layer);
            LinkLayers(first_layer, last_layer);
            LinkLayerToActuator(last_layer);

            // Set right order of nodes.
            OrderNodes();
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
        /// Link to nnodes.
        /// </summary>
        /// <param name="src">source node</param>
        /// <param name="dst">destination node</param>
        /// <param name="weight">weight</param>
        private void Link(Node src, Node dst, double weight = 1.0)
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
                Link(Sensor, dst);
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
                Link(src, Actuator);
            }
        }

        /// <summary>
        /// Invalidate all signals.
        /// </summary>
        public void InvalidateSignals()
        {
            foreach (Link link in Links)
            {
                link.Signal = double.NaN;
            }
        }

        /// <summary>
        /// Order nodes.
        /// </summary>
        public void OrderNodes()
        {
            // TODO: set right order of nodes.
        }

        /// <summary>
        /// Sense signals.
        /// </summary>
        /// <param name="in_signals">signals</param>
        /// <returns>answer</returns>
        public double[] Sense(double[] in_signals)
        {
            // Invalidate signals for debug.
            InvalidateSignals();

            Sensor.Sense(in_signals);

            // Process neurons in right order.
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].PropagateSignalForward();
            }

            return Actuator.GetOutputs();
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

                if (link.Src is Sensor)
                {
                    cortex.Sensor.OutLinks.Add(cortex_link);
                    cortex_link.Src = cortex.Sensor;
                }
                else
                {
                    int src_index = Neurons.IndexOf(link.Src as Neuron);

                    cortex.Neurons[src_index].OutLinks.Add(cortex_link);
                    cortex_link.Src = cortex.Neurons[src_index];
                }

                if (link.Dst is Actuator)
                {
                    cortex.Actuator.InLinks.Add(cortex_link);
                    cortex_link.Dst = cortex.Actuator;
                }
                else
                {
                    int dst_index = Neurons.IndexOf(link.Dst as Neuron);

                    cortex.Neurons[dst_index].InLinks.Add(cortex_link);
                    cortex_link.Dst = cortex.Neurons[dst_index];
                }
            }

            return cortex;
        }
    }
}
