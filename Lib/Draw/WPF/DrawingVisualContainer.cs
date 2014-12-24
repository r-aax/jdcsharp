// Copyright Joy Developing.

using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace Lib.Draw.WPF
{
    /// <summary>
    /// Container for <c>Drawing Visual</c>.
    /// </summary>
    public class DrawingVisualContainer : FrameworkElement
    {
        /// <summary>
        /// Drawing object.
        /// </summary>
        private DrawingVisual Visual;

        /// <summary>
        /// Constructor from drawing object.
        /// </summary>
        /// <param name="visual">drawing object</param>
        public DrawingVisualContainer(DrawingVisual visual)
        {
            Visual = visual;
            AddVisualChild(Visual);
            AddLogicalChild(Visual);
        }

        /// <summary>
        /// Visual elements count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Get visual object.
        /// </summary>
        /// <param name="index">number</param>
        /// <returns>visual object</returns>
        protected override Visual GetVisualChild(int index)
        {
            Debug.Assert(index == 0);
            return Visual;
        }
    }
}
