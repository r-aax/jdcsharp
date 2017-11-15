using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct
{
    /// <summary>
    /// Pair of object.
    /// </summary>
    public class Pair<T> : ICloneable
    {
        /// <summary>
        /// Objects.
        /// </summary>
        private T[] V;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Pair()
        {
            V = new T[2];
        }

        /// <summary>
        /// Constructor by two objects.
        /// </summary>
        /// <param name="t1">first object</param>
        /// <param name="t2">second object</param>
        public Pair(T t1, T t2)
            : this()
        {
            V[0] = t1;
            V[1] = t2;
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

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            return new Pair<T>(V[0], V[1]);
        }
    }
}
