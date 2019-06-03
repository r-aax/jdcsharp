using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Lib.MathMod.Grid.Cut;
using Lib.MathMod.Grid.DescartesObjects;

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
        }

        /// <summary>
        /// Border conditions link.
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
            bcond1.SetNDirs(bcond2, new Dirs3(i1, j1, k1));
        }

        /// <summary>
        /// Border conditions link.
        /// </summary>
        /// <param name="bcond1">first border condition</param>
        /// <param name="bcond2">second border condition</param>
        /// <param name="dirs">directions</param>
        public BCondsLink(BCond bcond1, BCond bcond2, Dirs3 dirs)
            : this(bcond1, bcond2, dirs.I, dirs.J, dirs.K)
        {
        }

        /// <summary>
        /// Corder conditions link.
        /// </summary>
        /// <param name="bcond1">first border condition</param>
        /// <param name="bcond2">second border condition</param>
        /// <param name="dirs">directions</param>
        public BCondsLink(BCond bcond1, BCond bcond2, Dir[] dirs)
            : this(bcond1, bcond2, dirs[0], dirs[1], dirs[2])
        {
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("    : {0, 4} -- {1, 4} [{2}, {3}, {4}] {5, -12} {6, -12}",
                                 BCond1.Id, BCond2.Id,
                                 BCond1.NDirs[0], BCond1.NDirs[1], BCond1.NDirs[2],
                                 Kind, BCond1.Label.Name);
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
        /// Get border condition belongs to given block.
        /// </summary>
        /// <param name="b">block</param>
        /// <returns>border condition</returns>
        public BCond Get(Block b)
        {
            if (BCond1.B == b)
            {
                return BCond1;
            }
            else if (BCond2.B == b)
            {
                return BCond2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get adjacent border condition.
        /// </summary>
        /// <param name="bcond">border condition</param>
        /// <returns>adjacent border condition</returns>
        public BCond Adjacent(BCond bcond)
        {
            if (bcond == BCond1)
            {
                return BCond2;
            }
            else if (bcond == BCond2)
            {
                return BCond1;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if linked condition is inci
        /// </summary>
        /// <param name="b"></param>
        /// <returns><c>true</c> - if incident, <c>false</c> - otherwise</returns>
        public bool IsIncident(Block b)
        {
            return BCond1.IsIncident(b) || BCond2.IsIncident(b);
        }
    }
}
