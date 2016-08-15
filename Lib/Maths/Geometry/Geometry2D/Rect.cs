// Author: Alexey Rybakov

using System;
using System.Diagnostics;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// 2D rectangle.
    /// </summary>
    public class Rect : ICloneable
    {
        /// <summary>
        /// Interval <c>x</c>.
        /// </summary>
        public Interval XInterval { get; private set; }

        /// <summary>
        /// Interval <c>y</c>.
        /// </summary>
        public Interval YInterval { get; private set; }

        /// <summary>
        /// Change event.
        /// </summary>
        public event EventHandler OnChange = null;

        /// <summary>
        /// Raise change event.
        /// </summary>
        private void RaiseOnChange()
        {
            if (OnChange != null)
            {
                OnChange(this, null);
            }
        }

        /// <summary>
        /// Constructor by intervals.
        /// </summary>
        /// <param name="xi">first interval</param>
        /// <param name="yi">second interval</param>
        public Rect(Interval xi, Interval yi)
        {
            XInterval = xi;
            YInterval = yi;
        }

        /// <summary>
        /// Constructor by sizes.
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public Rect(double width, double height)
            : this(new Interval(width), new Interval(height))
        {
        }

        /// <summary>
        /// Constructor from intervals.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public Rect(Point p1, Point p2)
            : this(new Interval(Math.Min(p1.X, p2.X), Math.Max(p1.X, p2.X)),
                   new Interval(Math.Min(p1.Y, p2.Y), Math.Max(p1.Y, p2.Y)))
        {
        }

        /// <summary>
        /// Width (size <c>x</c>).
        /// </summary>
        public double Width
        {
            get
            {
                return XInterval.Length;
            }
        }

        /// <summary>
        /// Height (size <c>y</c>).
        /// </summary>
        public double Height
        {
            get
            {
                return YInterval.Length;
            }
        }

        /// <summary>
        /// Radius.
        /// </summary>
        public double Radius
        {
            get
            {
                return Math.Min(Width, Height) / 2.0;
            }
        }

        /// <summary>
        /// Left-Bottom point.
        /// </summary>
        public Point LB
        {
            get
            {
                return new Point(Left, Bottom);
            }
        }

        /// <summary>
        ///  Left-Top point.
        /// </summary>
        public Point LT
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        /// <summary>
        ///  Right-Bottom point.
        /// </summary>
        public Point RB
        {
            get
            {
                return new Point(Right, Bottom);
            }
        }

        /// <summary>
        /// Right-Top point.
        /// </summary>
        public Point RT
        {
            get
            {
                return new Point(Right, Top);
            }
        }

        /// <summary>
        /// Check if point in rectangle.
        /// </summary>
        /// <param name="p">point</param>
        /// <returns>result</returns>
        public bool IsIn(Point p)
        {
            return XInterval.IsIn(p.X) && YInterval.IsIn(p.Y);
        }

        /// <summary>
        /// Move.
        /// </summary>
        /// <param name="v">move vector</param>
        public void Move(Vector v)
        {
            XInterval.Move(v.X);
            YInterval.Move(v.Y);

            RaiseOnChange();
        }

        /// <summary>
        /// Size relative move.
        /// </summary>
        /// <param name="k">move vector</param>
        public void RelMove(Vector k)
        {
            XInterval.RelMove(k.X);
            YInterval.RelMove(k.Y);

            RaiseOnChange();
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="p">scaling point</param>
        /// <param name="kx">scaling coefficient <c>x</c></param>
        /// <param name="ky">scaling coefficient <c>y</c></param>
        public void Scale(Point p, double kx, double ky)
        {
            XInterval.Scale(p.X, kx);
            YInterval.Scale(p.Y, ky);

            RaiseOnChange();
        }

        /// <summary>
        /// Return of scaled rectangle.
        /// </summary>
        /// <param name="p">scaling point</param>
        /// <param name="kx">scaling coefficient <c>x</c></param>
        /// <param name="ky">scaling coefficient <c>y</c></param>
        /// <returns>scaled rectangle</returns>
        public Rect Scaled(Point p, double kx, double ky)
        {
            Rect rect = Clone() as Rect;

            rect.Scale(p, kx, ky);

            return rect;
        }

        /// <summary>
        /// Scaling.
        /// </summary>
        /// <param name="p">scaling point</param>
        /// <param name="k">scaling coefficient</param>
        public void Scale(Point p, double k)
        {
            Scale(p, k, k);
        }

        /// <summary>
        /// Return of scaled rectangle.
        /// </summary>
        /// <param name="p">scaling point</param>
        /// <param name="k">scaling coefficient</param>
        /// <returns>scaled rectangle</returns>
        public Rect Scaled(Point p, double k)
        {
            return Scaled(p, k, k);
        }

        /// <summary>
        /// Center scaling.
        /// </summary>
        /// <param name="kx">scaling coefficient <c>x</c></param>
        /// <param name="ky">scaling coefficient <c>y</c></param>
        public void CenterScale(double kx, double ky)
        {
            XInterval.CenterScale(kx);
            YInterval.CenterScale(ky);

            RaiseOnChange();
        }

        /// <summary>
        /// Return center scaled rectangle.
        /// </summary>
        /// <param name="kx">scaling coefficient <c>x</c></param>
        /// <param name="ky">scaling coefficient <c>y</c></param>
        /// <returns>scaled rectangle</returns>
        public Rect CenterScaled(double kx, double ky)
        {
            return Scaled(Center, kx, ky);
        }

        /// <summary>
        /// Center scaling.
        /// </summary>
        /// <param name="k">coefficient</param>
        public void CenterScale(double k)
        {
            CenterScale(k, k);
        }

        /// <summary>
        /// Return center scaling rectangle.
        /// </summary>
        /// <param name="k">coefficient</param>
        /// <returns>scaled rectangle</returns>
        public Rect CenterScaled(double k)
        {
            return CenterScaled(k, k);
        }

        /// <summary>
        /// Center.
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point(XInterval.Mid, YInterval.Mid);
            }
        }

        /// <summary>
        /// Centered circle.
        /// </summary>
        /// <param name="k">Margin coefficient.</param>
        /// <returns>circle</returns>
        public Circle CenteredCircle(double k)
        {
            Debug.Assert(k <= 1.0);

            return new Circle(Center, Radius * k);
        }

        /// <summary>
        /// Centered circle.
        /// </summary>
        /// <returns>circle</returns>
        public Circle CenteredCircle()
        {
            return CenteredCircle(1.0);
        }

        /// <summary>
        /// Left side.
        /// </summary>
        public double Left
        {
            get
            {
                return XInterval.L;
            }
        }

        /// <summary>
        /// Right side.
        /// </summary>
        public double Right
        {
            get
            {
                return XInterval.H;
            }
        }

        /// <summary>
        /// Bottom side.
        /// </summary>
        public double Bottom
        {
            get
            {
                return YInterval.L;
            }
        }

        /// <summary>
        /// Top side.
        /// </summary>
        public double Top
        {
            get
            {
                return YInterval.H;
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>clone</returns>
        public object Clone()
        {
            return new Rect(XInterval.Clone() as Interval,
                            YInterval.Clone() as Interval);
        }

        /// <summary>
        /// Expand to parallelepiped.
        /// </summary>
        /// <param name="z_interval">interval <c>z</c></param>
        /// <returns>parallelepiped</returns>
        public Lib.Maths.Geometry.Geometry3D.Parallelepiped Extended(Interval z_interval)
        {
            return new Geometry3D.Parallelepiped(XInterval.Clone() as Interval,
                                                 YInterval.Clone() as Interval,
                                                 z_interval.Clone() as Interval);
        }
    }
}
