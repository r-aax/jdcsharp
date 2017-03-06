using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry.Geometry3D;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Block.
    /// </summary>
    public class Block : DescartesObject
    {
        public Point[,,] Nodes;
    }
}
