using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Maths.Geometry
{
    /// <summary>
    /// Segment with two ends.
    /// </summary>
    public class Segm<T>
    {
        /// <summary>
        /// Values.
        /// </summary>
        private T[] V;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Segm()
        {
            V = new T[2];
        }

        /// <summary>
        /// Low value.
        /// </summary>
        public T L
        {
            get
            {
                return V[0];
            }

            set
            {
                V[0] = value;
            }
        }

        /// <summary>
        /// High value.
        /// </summary>
        public T H
        {
            get
            {
                return V[1];
            }

            set
            {
                V[1] = value;
            }
        }

        /// <summary>
        /// Constructor by ends values.
        /// </summary>
        /// <param name="l">low value</param>
        /// <param name="h">high value</param>
        public Segm(T l, T h)
            : this()
        {
            L = l;
            H = h;
        }

        /// <summary>
        /// Constructor by other segment.
        /// </summary>
        /// <param name="s">segment</param>
        public Segm(Segm<T> s)
            : this()
        {
            L = s.L;
            H = s.H;
        }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">number</param>
        /// <returns>Element of segment.</returns>
        public T this[int i]
        {
            get
            {
                return V[i];
            }

            set
            {
                V[i] = value;
            }
        }
    }
}
