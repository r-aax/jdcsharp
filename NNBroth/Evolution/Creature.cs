﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNBroth.Evolution
{
    /// <summary>
    /// Creature.
    /// </summary>
    class Creature
    {
        /// <summary>
        /// Cortex;
        /// </summary>
        public Cortex Cortex
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sensor_dimension">sensor dimension</param>
        /// <param name="actuator_dimension">actuator dimension</param>
        public Creature(int sensor_dimension, int actuator_dimension)
        {
            Cortex = new Cortex(sensor_dimension, actuator_dimension);
        }
    }
}
