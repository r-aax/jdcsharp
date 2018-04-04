using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths.Geometry;
using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Block scope.
    /// </summary>
    public class Scope : ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Block.
        /// </summary>
        public Block B
        {
            get;
            set;
        }

        /// <summary>
        /// Canvas.
        /// </summary>
        public DescartesObject3D Canvas
        {
            get;
            private set;
        }

        /// <summary>
        /// Label of object.
        /// </summary>
        public NamedObject Label;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="i">I sizes</param>
        /// <param name="j">J sizes</param>
        /// <param name="k">K sizes</param>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public Scope(int id, Block b, IntervalI i, IntervalI j, IntervalI k,
                     string type, string subtype, string name)
        {
            Id = id;
            B = b;
            Canvas = new DescartesObject3D(i, j, k);
            Label = new NamedObject(type, subtype, name);
        }

        /// <summary>
        /// Constructor from canvas.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="b">block</param>
        /// <param name="canvas">canvas</param>
        /// <param name="type">type</param>
        /// <param name="subtype">subtype</param>
        /// <param name="name">name</param>
        public Scope(int id, Block b, DescartesObject3D canvas,
                     string type, string subtype, string name)
            : this(id, b, canvas.I, canvas.J, canvas.K, type, subtype, name)
        {
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy</returns>
        public object Clone()
        {
            return new Scope(Id, B, Canvas,             
                             Label.Type, Label.Subtype, Label.Name);
        }

        /// <summary>
        /// Clone scope with given identifier and block.
        /// </summary>
        /// <param name="id">new identifier</param>
        /// <param name="b">new block</param>
        /// <returns>new scope</returns>
        public Scope Clone(int id, Block b)
        {
            Scope scope = Clone() as Scope;

            scope.Id = id;
            scope.B = b;

            return scope;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0,4}: {1,4} [{2,3} - {3,3}, {4,3} - {5,3}, {6,3} - {7,3}] {8,-12} {9,-12} {10,-12}",
                                 Id, B.Id,
                                 Canvas.I0, Canvas.I1,
                                 Canvas.J0, Canvas.J1,
                                 Canvas.K0, Canvas.K1,
                                 Label.Type, Label.Subtype, Label.Name);
        }
    }
}
