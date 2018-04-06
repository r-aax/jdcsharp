using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Grid object.
    /// </summary>
    public class GObject
    {
        /// <summary>
        /// Grid.
        /// </summary>
        public StructuredGrid Grid;

        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">grid reference</param>
        /// <param name="id">identifier</param>
        public GObject(StructuredGrid g, int id)
        {
            Grid = g;
            Id = id;
        }
    }
}
