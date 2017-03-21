using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Link between two border conditions.
    /// </summary>
    public class BCondsLink
    {
        /// <summary>
        /// First border condition.
        /// </summary>
        public BCond BCond1
        {
            get;
            private set;
        }

        /// <summary>
        /// Second border condition.
        /// </summary>
        public BCond BCond2
        {
            get;
            private set;
        }

        /// <summary>
        /// Matrix between <c>BCond1</c> coordinates and <c>BCond2</c>.
        /// </summary>
        public Dir[] LDirs12;

        /// <summary>
        /// Matrix between <c>BCond2</c> coordinates and <c>BCond1</c>.
        /// </summary>
        public Dir[] LDirs21;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get
            {
                return BCond1.Label.Name;
            }
        }

        /// <summary>
        /// Border condition links.
        /// </summary>
        /// <param name="bcond1">first border condition</param>
        /// <param name="bcond2">second border condition</param>
        /// <param name="i1"><c>I1</c> direction</param>
        /// <param name="j1"><c>J1</c> direction</param>
        /// <param name="k1"><c>K1</c> direction</param>
        /// <param name="i0"><c>I0</c> direction</param>
        /// <param name="j0"><c>J0</c> direction</param>
        /// <param name="k0"><c>K0</c> direction</param>
        public BCondsLink(BCond bcond1, BCond bcond2,
                          Dir i1, Dir j1, Dir k1, Dir i0, Dir j0, Dir k0)
        {
            BCond1 = bcond1;
            BCond2 = bcond2;

            LDirs12 = new Dir[Dir.Count];
            LDirs21 = new Dir[Dir.Count];

            // LDirs12
            LDirs12[Dir.I1N] = i1;
            LDirs12[Dir.J1N] = j1;
            LDirs12[Dir.K1N] = k1;
            LDirs12[Dir.I0N] = i0;
            LDirs12[Dir.J0N] = j0;
            LDirs12[Dir.K0N] = k0;

            // LDir21
            for (int n = 0; n < Dir.Count; n++)
            {
                LDirs21[LDirs12[n].N] = Dir.Dirs[n];
            }
        }

        /// <summary>
        /// Cut linked border condition.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <param name="d">direction</param>
        /// <param name="pos">position</param>
        public void CutLinkedBCond(BCond bcond, Dir d, int pos)
        {
            StructuredGrid g = bcond.B.Grid;
            BCond link;
            Dir link_d;

            if (bcond == BCond1)
            {
                link = BCond2;
                link_d = LDirs12[d.N];
            }
            else if (bcond == BCond2)
            {
                link = BCond1;
                link_d = LDirs21[d.N];
            }
            else
            {
                throw new Exception("border condition is not found in BCondsLink");
            }

            // Cut link in link_d direction in pos position.
            BCond new_bcond = link.Clone(g.MaxBCondId() + 1, link.B);

            // Coordinates change.
            // link: [d0 : d1] --> [d0 : d0 + pos]
            // new_bcond : [d0 + pos :  d1]
            int d0 = link.Coords[link_d.N][0];
            int d1 = link.Coords[link_d.N][1];
            link.Coords[link_d.N] = new Maths.Geometry.ISegm(d0, d0 + pos);
            new_bcond.Coords[link_d.N] = new Maths.Geometry.ISegm(d0 + pos, d1);
            g.BConds.Add(new_bcond);
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("    : {0, 4} -- {1, 4} [{2}, {3}, {4}, {5}, {6}, {7}]",
                                 BCond1.Id, BCond2.Id,
                                 LDirs12[0], LDirs12[1], LDirs12[2], LDirs12[3], LDirs12[4], LDirs12[5]);
        }
    }
}
