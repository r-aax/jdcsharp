// Copyright Joy Developing.

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

using Lib.Maths.Geometry.Geometry2D;
using LPoint = Lib.Maths.Geometry.Geometry2D.Point;
using LColor = Lib.Draw.Color;
using SDColor = System.Drawing.Color;

namespace Lib.Draw.WindowsForms
{
    /// <summary>
    /// Drawing in arbitrary rectangle scope.
    /// This scope is linked with <c>Graphics</c> object.
    /// </summary>
    public class RectDrawer : Lib.Draw.RectDrawer
    {
        /// <summary>
        /// <c>Graphics</c> object.
        /// </summary>
        private Graphics Graphics = null;

        /// <summary>
        /// Painting scope.
        /// </summary>
        private PictureBox _PictureBox = null;

        /// <summary>
        /// Painting scope access.
        /// </summary>
        public PictureBox PictureBox
        {
            get
            {
                return _PictureBox;
            }

            private set
            {
                _PictureBox = value;
            }
        }

        /// <summary>
        /// Last bitmap.
        /// </summary>
        private Bitmap LastBitmap = null;

        /// <summary>
        /// Current bitmap.
        /// </summary>
        private Bitmap CurBitmap = null;

        /// <summary>
        /// Background color.
        /// </summary>
        private SDColor BackColor = SDColor.White;

        /// <summary>
        /// Pen.
        /// </summary>
        private Pen Pen = null;

        /// <summary>
        /// Brush.
        /// </summary>
        private Brush Brush = null;

        /// <summary>
        /// Create scaler master.
        /// </summary>
        protected override void MakeScaler()
        {
            Scaler = new RectScaler(Rect, new Rect(PictureBox.Width, PictureBox.Height),
                                    false, true);
        }

        /// <summary>
        /// Create draw master for painting in scope <c>rect</c>,
        /// wich will be displayed in <c>pb</c>.
        /// </summary>
        /// <param name="rect">real scope</param>
        /// <param name="picture_box">picture box</param>
        /// <param name="is_x_invert">inverted flag <c>x</c></param>
        /// <param name="is_y_invert">inverted flag <c>y</c></param>
        public RectDrawer(Rect rect, PictureBox picture_box,
                          bool is_x_invert, bool is_y_invert)
        {
            PictureBox = picture_box;
            Rect = rect;
            MakeScaler();

            // Event handler for resize of PictureBox.
            PictureBox.Resize += new EventHandler((sender, e) => MakeScaler());

            // Event handler for resize initial scope.
            Rect.OnChange += new EventHandler((sender, e) => MakeScaler());

            // Default values.
            BackColor = SDColor.White;
            Pen = new Pen(SDColor.Black);
            Brush = new SolidBrush(SDColor.Black);
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
                                 PictureBox.Width, PictureBox.Height);
        }

        /// <summary>
        /// Begin draw.
        /// Create new picture and clear it.
        /// </summary>
        public override void BeginDraw()
        {
            CurBitmap = new Bitmap(PictureBox.Width, PictureBox.Height);
            Graphics = Graphics.FromImage(CurBitmap);
            Graphics.Clear(BackColor);

            // We always need anti alias.
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        /// <summary>
        /// End draw.
        /// Show picture in picture box.
        /// </summary>
        public override void EndDraw()
        {
            PictureBox.Image = CurBitmap;
            LastBitmap = CurBitmap;
        }

        /// <summary>
        /// Convert color <c>System.Drawing.Color</c>.
        /// </summary>
        /// <param name="color">color</param>
        /// <returns>color <c>System.Drawing.Color</c></returns>
        private SDColor ConvertColor(LColor color)
        {
            return SDColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Set pen color.
        /// </summary>
        /// <param name="color">color</param>
        public override void SetPenColor(Color color)
        {
            Pen.Color = ConvertColor(color);
        }

        /// <summary>
        /// Set pen thickness.
        /// </summary>
        /// <param name="thickness">tickness</param>
        public override void SetPenThickness(double thickness)
        {
            Pen.Width = (float)thickness;
        }

        /// <summary>
        /// Set brush.
        /// </summary>
        /// <param name="color">color</param>
        public override void SetBrush(Color color)
        {
            Brush = new SolidBrush(ConvertColor(color));
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
            Graphics.DrawLine(Pen,
                              (int)ta.X, (int)ta.Y, (int)tb.X, (int)tb.Y);
        }

        /// <summary>
        /// Draw margined line.
        /// </summary>
        /// <param name="a">begin</param>
        /// <param name="b">end</param>
        /// <param name="margin">margin</param>
        public override void DrawMarginedLine(LPoint a, LPoint b, double margin)
        {
            Debug.Assert(false);

            LPoint ta = Scaler.T(a);
            LPoint tb = Scaler.T(b);
            Graphics.DrawLine(Pen,
                              (int)ta.X, (int)ta.Y, (int)tb.X, (int)tb.Y);
        }

        /// <summary>
        /// Draw point (circle with fixes radius).
        /// </summary>
        /// <param name="p">point</param>
        /// <param name="r">radius</param>
        public override void DrawPoint(LPoint p, double r)
        {
            LPoint tp = Scaler.T(p);
            Graphics.DrawEllipse(Pen,
                                 (int)(tp.X - r), (int)(tp.Y - r),
                                 (int)(2 * r), (int)(2 * r));
        }

        /// <summary>
        /// Fill point.
        /// </summary>
        /// <param name="p">center</param>
        /// <param name="r">radius</param>
        public override void FillPoint(LPoint p, double r)
        {
            LPoint tp = Scaler.T(p);
            Graphics.FillEllipse(Brush,
                                 (int)(tp.X - r), (int)(tp.Y - r),
                                 (int)(2 * r), (int)(2 * r));
        }

        /// <summary>
        /// Draw ellipse.
        /// </summary>
        /// <param name="p">center</param>
        /// <param name="rx">radius <c>x</c></param>
        /// <param name="ry">radius <c>y</c></param>
        public override void DrawEllipse(LPoint p, double rx, double ry)
        {
            Vector v = new Vector(rx, ry);
            LPoint p1 = p - v;
            LPoint p2 = p + v;
            LPoint tp1 = Scaler.T(p1);
            LPoint tp2 = Scaler.T(p2);
            Graphics.DrawEllipse(Pen, (int)tp1.X, (int)tp1.Y, (int)tp2.X, (int)tp2.Y);
        }

        /// <summary>
        /// Fill ellipse.
        /// </summary>
        /// <param name="p">center</param>
        /// <param name="rx">radius <c>x</c></param>
        /// <param name="ry">radius <c>y</c></param>
        public override void FillEllipse(LPoint p, double rx, double ry)
        {
            Vector v = new Vector(rx, ry);
            LPoint p1 = p - v;
            LPoint p2 = p + v;
            LPoint tp1 = Scaler.T(p1);
            LPoint tp2 = Scaler.T(p2);
            Graphics.FillEllipse(Brush, (int)tp1.X, (int)tp1.Y, (int)tp2.X, (int)tp2.Y);
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="p">poit</param>
        /// <param name="text">text</param>
        /// <param name="size">size</param>
        /// <param name="family">font</param>
        public override void DrawText(LPoint p, string text, double size, string family)
        {
            throw new NotImplementedException();
        }
    }
}
