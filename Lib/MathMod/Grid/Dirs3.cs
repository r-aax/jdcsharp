using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// 3 dirs.
    /// </summary>
    public class Dirs3
    {
        /// <summary>
        /// Dirs.
        /// </summary>
        public Dir[] Dirs;

        /// <summary>
        /// I direction.
        /// </summary>
        public Dir I
        {
            get
            {
                return Dirs[Dir.IN];
            }

            set
            {
                Dirs[Dir.IN] = value;
            }
        }

        /// <summary>
        /// J direction.
        /// </summary>
        public Dir J
        {
            get
            {
                return Dirs[Dir.JN];
            }

            set
            {
                Dirs[Dir.JN] = value;
            }
        }

        /// <summary>
        /// K direction.
        /// </summary>
        public Dir K
        {
            get
            {
                return Dirs[Dir.KN];
            }

            set
            {
                Dirs[Dir.KN] = value;
            }
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Dirs3()
        {
            Dirs = new Dir[Dir.GenCount];
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="i">I direction</param>
        /// <param name="j">J direction</param>
        /// <param name="k">K direction</param>
        public Dirs3(Dir i, Dir j, Dir k)
            : this()
        {
            I = i;
            J = j;
            K = k;
        }

        /// <summary>
        /// Set relation d1 -> d2.
        /// </summary>
        /// <param name="d1">first direction</param>
        /// <param name="d2">second direction</param>
        public void Set(Dir d1, Dir d2)
        {
            if (d1.IsGen)
            {
                Dirs[d1.N] = d2;
            }
            else
            {
                Dirs[(!d1).N] = !d2;
            }
        }
    }
}
