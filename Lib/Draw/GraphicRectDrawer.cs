using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Lib.Draw
{
    /// <summary>
    /// Drawer for graphic.
    /// </summary>
    public class GraphicRectDrawer
    {
        /// <summary>
        /// Drawer.
        /// </summary>
        private RectDrawer Drawer = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="drawer">drawer</param>
        public GraphicRectDrawer(RectDrawer drawer)
        {
            Debug.Assert(drawer != null);
            Drawer = drawer;
        }
    }
}
