using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.Grid;

namespace Lib.MathMod.Grid.Delete
{
    /// <summary>
    /// Class that manages grid cleaning.
    /// </summary>
    public class GridCleaner
    {
        /// <summary>
        /// Grid.
        /// </summary>
        public StructuredGrid Grid;

        /// <summary>
        /// Create object.
        /// </summary>
        /// <param name="g">grid</param>
        public GridCleaner(StructuredGrid g)
        {
            Grid = g;
        }

        /// <summary>
        /// Delete linked to block objects.
        /// </summary>
        /// <param name="b">block</param>
        public void DeleteLinkedToBlockObjects(Block b)
        {
            Grid.IfacesPairs.RemoveAll(ip => ip.IsIncident(b));
            Grid.BConds.RemoveAll(bc => bc.IsIncident(b));
            Grid.BCondsLinks.RemoveAll(bcl => bcl.IsIncident(b));
            Grid.Scopes.RemoveAll(s => s.IsIncident(b));
        }

        /// <summary>
        /// Delete block.
        /// </summary>
        /// <param name="b">block</param>
        public void DeleteBlock(Block b)
        {
            if (b != null)
            {
                DeleteLinkedToBlockObjects(b);
                Grid.Blocks.Remove(b);
            }
        }
    }
}
