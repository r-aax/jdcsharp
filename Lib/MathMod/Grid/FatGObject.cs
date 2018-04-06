using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.Grid.DescartesObjects;
using Lib.Maths.Geometry;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Fat grid object.
    /// </summary>
    public class FatGObject : GObject
    {
        /// <summary>
        /// Canvas.
        /// </summary>
        public DescartesObject3D Canvas
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i"><c>I</c> direction nodes interval</param>
        /// <param name="j"><c>J</c> direction nodes interval</param>
        /// <param name="k"><c>K</c> direction nodes interval </param>
        public FatGObject(StructuredGrid g, int id, Block b, IntervalI i, IntervalI j, IntervalI k)
            : base(g, id, b)
        {
            Canvas = new DescartesObject3D(i, j, k);
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        public FatGObject(StructuredGrid g, int id, Block b, DescartesObject3D canvas)
            : this(g, id, b, canvas.I, canvas.J, canvas.K)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i"><c>I</c> direction nodes interval</param>
        /// <param name="j"><c>J</c> direction nodes interval</param>
        /// <param name="k"><c>K</c> direction nodes interval </param>
        public FatGObject(int id, Block b, IntervalI i, IntervalI j, IntervalI k)
            : this(b.Grid, id, b, i, j, k)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        public FatGObject(int id, Block b, DescartesObject3D do3)
            : this(b.Grid, id, b, do3)
        {
        }
    }
}
