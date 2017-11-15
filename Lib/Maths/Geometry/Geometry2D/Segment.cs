using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry.Geometry2D
{
    /// <summary>
    /// Segment.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// First point.
        /// </summary>
        public Point A;

        /// <summary>
        /// Second point.
        /// </summary>
        public Point B;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a">first point</param>
        /// <param name="b">second point</param>
        public Segment(Point a, Point b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Check if two segments intersect.
        /// </summary>
        /// <param name="s1">first segment</param>
        /// <param name="s2">second segment</param>
        /// <returns><c>true</c> - if segments intersect, <c>false</c> - otherwise</returns>
        public static bool IsIntersect(Segment s1, Segment s2)
        {
            Point a = s1.A;
            Point b = s1.B;
            Point c = s2.A;
            Point d = s2.B;
            IntervalD s1x = IntervalD.SetFrom(a.X, b.X);
            IntervalD s1y = IntervalD.SetFrom(a.Y, b.Y);
            IntervalD s2x = IntervalD.SetFrom(c.X, d.X);
            IntervalD s2y = IntervalD.SetFrom(c.Y, d.Y);
            double abc = (new Triangle(a, b, c)).OrientedAreaSign;
            double abd = (new Triangle(a, b, d)).OrientedAreaSign;
            double cda = (new Triangle(c, d, a)).OrientedAreaSign;
            double cdb = (new Triangle(c, d, b)).OrientedAreaSign;

            return IntervalD.IsIntersect(s1x, s2x)
                   && IntervalD.IsIntersect(s1y, s2y)
                   && (abc * abd <= 0.0)
                   && (cda * cdb <= 0.0);
        }
        
        /// <summary>
        /// Check if two segments have common point.
        /// </summary>
        /// <param name="s1">first segment</param>
        /// <param name="s2">second segment</param>
        /// <returns><c>true</c> - if segments have common point, <c>false</c> - otherwise</returns>
        public static bool IsCommonPoint(Segment s1, Segment s2)
        {
            return s1.A.IsEq(s2.A) || s1.A.IsEq(s2.B) || s1.B.IsEq(s2.A) || s1.B.IsEq(s2.B);
        }
    }
}
