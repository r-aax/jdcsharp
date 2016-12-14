// Author: Alexey Rybakov

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using Lib.Maths.Geometry.Geometry2D;
using LVector = Lib.Maths.Geometry.Geometry2D.Vector;
using LPoint = Lib.Maths.Geometry.Geometry2D.Point;
using LRect = Lib.Maths.Geometry.Geometry2D.Rect;
using LColor = Lib.Draw.Color;
using SWPoint = System.Windows.Point;
using SWRect = System.Windows.Rect;
using SWMColor = System.Windows.Media.Color;

namespace Lib.Draw.WPF
{
    /// <summary>
    /// Draw in arbitrary scope.
    /// Drawer is linked with <c>DrawingVisual</c> object.
    /// </summary>
    public class RectDrawer : Lib.Draw.RectDrawer
    {
        /// <summary>
        /// Drawing object.
        /// </summary>
        private DrawingVisual Visual = null;

        /// <summary>
        /// Drawing context.
        /// </summary>
        private DrawingContext Context = null;

        /// <summary>
        /// Drawing canvas.
        /// </summary>
        private Canvas Canvas = null;

        /// <summary>
        /// Background color.
        /// </summary>
        private SWMColor BackColor = Colors.White;

        /// <summary>
        /// Pen.
        /// </summary>
        private Pen Pen = null;

        /// <summary>
        /// Brush.
        /// </summary>
        private Brush Brush = null;

        /// <summary>
        /// Create scale master.
        /// </summary>
        protected override void MakeScaler()
        {
            Scaler = new RectScaler(Rect, new LRect(Canvas.ActualWidth, Canvas.ActualHeight), false, true);
        }

        /// <summary>
        /// Create draw master for drawing in <c>rect</c> scope,
        /// picture will be displayed in  <c>canv</c>.
        /// </summary>
        /// <param name="rect">real scope</param>
        /// <param name="canvas">scope of drawing</param>
        /// <param name="is_x_invert">invert flag <c>x</c></param>
        /// <param name="is_y_invert">invert flag <c>y</c></param>
        public RectDrawer(LRect rect, Canvas canvas,
                          bool is_x_invert, bool is_y_invert)
        {
            Canvas = canvas;
            SetRect(rect);

            // Event handler for PictureBox resize.
            Canvas.SizeChanged += new SizeChangedEventHandler((sender, e) => MakeScaler());

            // Default values.
            BackColor = Colors.White;
            Pen = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            Brush = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Bijection string.
        /// </summary>
        /// <returns>string</returns>
        public override string GetBijectionString()
        {
            return String.Format("[{0:0.##} - {1:0.##}] x [{2:0.##} - {3:0.##}]"
                                 + " -> [0 - {4:0.##}] x [0 - {5:0.##}]",
                                 Rect.XInterval.L, Rect.XInterval.H,
                                 Rect.YInterval.L, Rect.YInterval.H,
                                 Canvas.ActualWidth, Canvas.ActualHeight);
        }

        /// <summary>
        /// Begin draw.
        /// Create picture and clear it.
        /// </summary>
        public override void BeginDraw()
        {
            Visual = new DrawingVisual();
            Context = Visual.RenderOpen();

            // Clear draw scope.
            Canvas.Children.Clear();
        }

        /// <summary>
        /// End draw.
        /// </summary>
        public override void EndDraw()
        {
            Context.Close();

            // Put picture on canvas.
            Canvas.Children.Add(new DrawingVisualContainer(Visual));
        }

        /// <summary>
        /// Convert color to <c>System.Drawing.Color</c>.
        /// </summary>
        /// <param name="color">color</param>
        /// <returns>color <c>System.Drawing.Color</c></returns>
        private SWMColor ConvertColor(LColor color)
        {
            return SWMColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Set pen clolor.
        /// </summary>
        /// <param name="color">color</param>
        public override void SetPenColor(LColor color)
        {
            Pen = new Pen(new SolidColorBrush(ConvertColor(color)), Pen.Thickness);
        }

        /// <summary>
        /// Set pen thickness.
        /// </summary>
        /// <param name="thickness">thickness</param>
        public override void SetPenThickness(double thickness)
        {
            Pen.Thickness = thickness;
        }

        /// <summary>
        /// Set brush.
        /// </summary>
        /// <param name="color">color</param>
        public override void SetBrush(LColor color)
        {
            Brush = new SolidColorBrush(ConvertColor(color));
        }

        /// <summary>
        /// Draw line.
        /// </summary>
        /// <param name="a">begin</param>
        /// <param name="b">end</param>
        public override void DrawLine(LPoint a, LPoint b)
        {
            LPoint ta = Scaler.T(a);
            LPoint tb = Scaler.T(b);
            Context.DrawLine(Pen, new SWPoint(ta.X, ta.Y), new SWPoint(tb.X, tb.Y));
        }

        /// <summary>
        /// Draw margined line.
        /// </summary>
        /// <param name="a">begin</param>
        /// <param name="b">end</param>
        /// <param name="margin">margin value</param>
        public override void DrawMarginedLine(LPoint a, LPoint b, double margin)
        {
            LPoint ta = Scaler.T(a);
            LPoint tb = Scaler.T(b);
            LVector dir = tb - ta;

            dir.Normalize(margin);
            ta += dir;
            tb -= dir;

            Context.DrawLine(Pen, new SWPoint(ta.X, ta.Y), new SWPoint(tb.X, tb.Y));
        }

        /// <summary>
        /// Draw point
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">radius</param>
        public override void DrawPoint(LPoint p, double r)
        {
            LPoint tp = Scaler.T(p);
            Context.DrawEllipse(null, Pen, new SWPoint(tp.X, tp.Y), r, r);
        }

        /// <summary>
        /// Fill point.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">radius</param>
        public override void FillPoint(LPoint p, double r)
        {
            LPoint tp = Scaler.T(p);
            Context.DrawEllipse(Brush, null, new SWPoint(tp.X, tp.Y), r, r);
        }

        /// <summary>
        /// Draw rectangle.
        /// </summary>
        /// <param name="r">rectangle</param>
        public override void DrawRect(LRect r)
        {
            LPoint tlb = Scaler.T(r.LB);
            LPoint trt = Scaler.T(r.RT);
            Context.DrawRectangle(null, Pen,
                                  new SWRect(new SWPoint(tlb.X, tlb.Y), new SWPoint(trt.X, trt.Y)));
        }

        /// <summary>
        /// Fill rectangle.
        /// </summary>
        /// <param name="r">rectangle</param>
        public override void FillRect(LRect r)
        {
            LPoint tlb = Scaler.T(r.LB);
            LPoint trt = Scaler.T(r.RT);
            Context.DrawRectangle(Brush, null,
                                  new SWRect(new SWPoint(tlb.X, tlb.Y), new SWPoint(trt.X, trt.Y)));
        }

        /// <summary>
        /// Draw ellipse.
        /// </summary>
        /// <param name="p">center</param>
        /// <param name="rx">radius <c>x</c></param>
        /// <param name="ry">radius <c>y</c></param>
        public override void DrawEllipse(LPoint p, double rx, double ry)
        {
            LVector v = new LVector(rx, ry);
            LPoint p1 = p - v;
            LPoint p2 = p + v;
            LPoint tp1 = Scaler.T(p1);
            LPoint tp2 = Scaler.T(p2);
            LVector mid_v = LVector.Mid(tp1, tp2);
            Context.DrawEllipse(null, Pen,
                                new SWPoint(mid_v.X, mid_v.Y),
                                0.5 * tp1.DistX(tp2), 0.5 * tp1.DistY(tp2));
        }

        /// <summary>
        /// Fill ellipse.
        /// </summary>
        /// <param name="p">center</param>
        /// <param name="rx">radius <c>x</c></param>
        /// <param name="ry">radius <c>y</c></param>
        public override void FillEllipse(LPoint p, double rx, double ry)
        {
            LVector v = new LVector(rx, ry);
            LPoint p1 = p - v;
            LPoint p2 = p + v;
            LPoint tp1 = Scaler.T(p1);
            LPoint tp2 = Scaler.T(p2);
            LVector mid_v = LVector.Mid(tp1, tp2);
            Context.DrawEllipse(Brush, null,
                                new SWPoint(mid_v.X, mid_v.Y),
                                0.5 * tp1.DistX(tp2), 0.5 * tp1.DistY(tp2));
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="text">text</param>
        /// <param name="size">size</param>
        /// <param name="family">font</param>
        public override void DrawText(LPoint p, string text, double size, string family)
        {
            LPoint tp = Scaler.T(p);
            FormattedText ftext = new FormattedText(text,
                                                    System.Globalization.CultureInfo.CurrentCulture,
                                                    FlowDirection.LeftToRight,
                                                    new Typeface(family),
                                                    size,
                                                    Brushes.Black);

            Context.DrawText(ftext, new SWPoint(tp.X, tp.Y));
        }
    }
}
