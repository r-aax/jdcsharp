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
        /// Thin object direction.
        /// </summary>
        public Dir D
        {
            get
            {
                return Canvas.D;
            }
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

        /// <summary>
        /// Check if thin object is opposite to another thin object.
        /// </summary>
        /// <param name="obj">another object</param>
        /// <returns><c>true</c> - if opposite, <c>false</c> - otherwise</returns>
        public bool IsOppositeOnOneBlock(ThinGObject obj)
        {
            // Check for one block.
            if (B.Id != obj.B.Id)
            {
                return false;
            }

            // Check for opposite.
            if (!D.IsOpposite(obj.D))
            {
                return false;
            }

            // Check for coordinates.
            Dir d1, d2;
            D.GetPairOfOrthogonalDirs(out d1, out d2);

            return (Canvas.Coord(d1) == obj.Canvas.Coord(d1))
                   && (Canvas.Coord(d2) == obj.Canvas.Coord(d2));
        }
    }
}
