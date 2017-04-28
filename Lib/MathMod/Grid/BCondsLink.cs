using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.MathMod.Grid.Cut;

namespace Lib.MathMod.Grid
{
    /// <summary>
    /// Link between two border conditions.
    /// </summary>
    public class BCondsLink
    {
        /// <summary>
        /// Kind of link.
        /// </summary>
        public string Kind
        {
            get;
            set;
        }

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
        public BCondsLink(BCond bcond1, BCond bcond2)
        {
            if ((bcond1 == null) || (bcond2 == null))
            {
                throw new Exception("null border condition in BCondsLink constructor");
            }

            BCond1 = bcond1;
            BCond2 = bcond2;

            LDirs12 = new Dir[Dir.Count];
            LDirs21 = new Dir[Dir.Count];
        }

        /// <summary>
        /// Border condition links.
        /// </summary>
        /// <param name="bcond1">first border condition</param>
        /// <param name="bcond2">second border condition</param>
        /// <param name="i1"><c>I1</c> direction</param>
        /// <param name="j1"><c>J1</c> direction</param>
        /// <param name="k1"><c>K1</c> direction</param>
        public BCondsLink(BCond bcond1, BCond bcond2,
                          Dir i1, Dir j1, Dir k1)
            : this(bcond1, bcond2)
        {
            // LDirs12
            LDirs12[Dir.I1N] = i1;
            LDirs12[Dir.J1N] = j1;
            LDirs12[Dir.K1N] = k1;
            LDirs12[Dir.I0N] = !i1;
            LDirs12[Dir.J0N] = !j1;
            LDirs12[Dir.K0N] = !k1;

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
        /// <param name="width">position</param>
        public void TruncLinkedBCond(BCond bcond, Dir d, int width)
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

            BCond new_bcond = BCondCutter.Trunc(link, link_d, width);
            g.BConds.Add(new_bcond);

            // Add two last border conditions to BCondsLinks list.
            if (bcond == BCond1)
            {
                int ind = g.BCondsCount;
                BCondsLink bcl = new BCondsLink(g.BConds[ind - 2], g.BConds[ind - 1],
                                                LDirs12[0], LDirs12[1], LDirs12[2]);
                bcl.Kind = Kind;
                g.BCondsLinks.Add(bcl);
                bcl.AddNameSuffixIfPERI();
            }
            else if (bcond == BCond2)
            {
                int ind = g.BCondsCount;
                BCondsLink bcl = new BCondsLink(g.BConds[ind - 1], g.BConds[ind - 2],
                                                LDirs12[0], LDirs12[1], LDirs12[2]);
                bcl.Kind = Kind;
                g.BCondsLinks.Add(bcl);
                bcl.AddNameSuffixIfPERI();
            }
            else
            {
                throw new Exception("border condition is not found in BCondsLink");
            }

            AddNameSuffixIfPERI();
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("    : {0, 4} -- {1, 4} [{2}, {3}, {4}] {5, -12} {6, -12}",
                                 BCond1.Id, BCond2.Id,
                                 LDirs12[0], LDirs12[1], LDirs12[2], Kind, BCond1.Label.Name);
        }

        /// <summary>
        /// If name of border condition is PERI_C* add suffix.
        /// </summary>
        public void AddNameSuffixIfPERI()
        {
            string nm = BCond1.Label.Name;

            Debug.Assert(nm == BCond2.Label.Name, "both bconds of bconds link must have the same name");

            if (nm == "PERI_C")
            {
                // Do not rename if it is bcond of one block.
                if (BCond1.B == BCond2.B)
                {
                    return;
                }
            }

            if (nm.Length > 5)
            {
                nm = nm.Substring(0, 6);

                if (nm == "PERI_C")
                {
                    nm = String.Format("{0}-{1}", nm, BCond1.Id);
                    BCond1.Label.Name = nm;
                    BCond2.Label.Name = nm;
                }
            }
        }

        /// <summary>
        /// Set pair of directions.
        /// </summary>
        /// <param name="d">first border condition direction</param>
        /// <param name="nd">second border condition direction</param>
        public void SetNDir(Dir d, Dir nd)
        {
            LDirs12[d.N] = nd;
            LDirs12[(!d).N] = !nd;
            LDirs21[nd.N] = d;
            LDirs21[(!nd).N] = !d;
        }
    }
}
