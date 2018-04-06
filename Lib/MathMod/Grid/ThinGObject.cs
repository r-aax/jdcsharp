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
    /// Thin grid object.
    /// </summary>
    public class ThinGObject : GObject
    {
        /// <summary>
        /// Canvas.
        /// </summary>
        public ThinDescartesObject3D Canvas
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I direction nodes interval</param>
        /// <param name="j">J direction nodes interval</param>
        /// <param name="k">K direction nodes interval </param>
        public ThinGObject(int id, Block b, IntervalI i, IntervalI j, IntervalI k)
            : base(id, b)
        {
            Canvas = new ThinDescartesObject3D(i, j, k);
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        public ThinGObject(int id, Block b, DescartesObject3D canvas)
            : this(id, b, canvas.I, canvas.J, canvas.K)
        {
        }
    }
}
