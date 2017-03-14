using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Pair of adjacent interfaces.
    /// </summary>
    public class IfacesPair
    {
        /// <summary>
        /// Base interface.
        /// </summary>
        public Iface If;

        /// <summary>
        /// Mirror (adjacent interface).
        /// </summary>
        public Iface Mirror;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="iface">base interface</param>
        /// <param name="mirror">mirror</param>
        public IfacesPair(Iface iface, Iface mirror)
        {
            If = iface;
            Mirror = mirror;
        }

        /// <summary>
        /// Flip interfaces.
        /// </summary>
        public void Flip()
        {
            Iface tmp = If;

            If = Mirror;
            Mirror = tmp;
        }

        /// <summary>
        /// Set block.
        /// </summary>
        /// <param name="b">block</param>
        public void SetB(Block b)
        {
            If.B = b;
            Mirror.NB = b;
        }
    }
}
