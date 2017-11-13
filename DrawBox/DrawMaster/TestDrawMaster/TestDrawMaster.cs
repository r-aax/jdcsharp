using System.Windows.Controls;

using Lib.Draw.WPF;
using Lib.Maths.Geometry;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;

namespace DrawBox.DrawMaster.TestDrawMaster
{
    /// <summary>
    /// Test drawer.
    /// </summary>
    class TestDrawMaster
    {
        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cnv">canvas</param>
        public static void Draw(Canvas cnv)
        {
            RectDrawer d = new RectDrawer(new Rect2D(100.0, 100.0), cnv, false, true);
            d.BeginDraw();

            d.DrawLine(new Point(0.0, 0.0), new Point(100.0, 100.0));
            d.DrawLine(new Point(0.0, 100.0), new Point(100.0, 0.0));

            d.EndDraw();
        }
    }
}
