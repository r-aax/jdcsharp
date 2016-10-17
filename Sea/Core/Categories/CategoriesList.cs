// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lib.DataStruct;

namespace Sea.Core.Categories
{
    /// <summary>
    /// List of categories.
    /// </summary>
    public class CategoriesList : ICloneable
    {
        /// <summary>
        /// Items.
        /// </summary>
        public List<MPTTTree> Items { get; set; }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>category</returns>
        public MPTTTree this[int i]
        {
            get
            {
                return Items[i];
            }

            set
            {
                Items[i] = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CategoriesList()
        {
            Items = new List<MPTTTree>();
        }

        /// <summary>
        /// Categories count.
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Check if is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy of categories list</returns>
        public object Clone()
        {
            CategoriesList categories_list = new CategoriesList();

            for (int i = 0; i < Count; i++)
            {
                categories_list.Items.Add(Items[i].Clone() as MPTTTree);
            }

            return categories_list;
        }

        /// <summary>
        /// Fill listbox with categories information.
        /// </summary>
        /// <param name="lb">list box</param>
        public void ToListBox(ListBox lb)
        {
            lb.Items.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                lb.Items.Add(Items[i].Data as string);
            }
        }

        /// <summary>
        /// Find category by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>category</returns>
        public MPTTTree Find(int id)
        {
            foreach (MPTTTree tree in Items)
            {
                MPTTTree subtree = tree.Find(id);

                if (subtree != null)
                {
                    return subtree;
                }
            }

            return null;
        }

        /// <summary>
        /// Delete extra categories.
        /// Categories list must be a forest.
        /// </summary>
        public void DeleteExtra()
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                for (int j = Items.Count - 1; j > i; j--)
                {
                    MPTTTree ti = Items[i];
                    MPTTTree tj = Items[j];

                    if (ti.IsInner(tj))
                    {
                        Items.RemoveAt(i);
                        break;
                    }
                    else if (ti.IsOuter(tj))
                    {
                        Items.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// Clear categories.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// Check intersection between two categories lists.
        /// </summary>
        /// <param name="list">list of categories</param>
        /// <returns><c>true</c> - if there is intersection, <c>false</c> - otherwise</returns>
        public bool IsIntersection(CategoriesList list)
        {
            for (int i = 0; i < Count; i++)
            {
                for (int listi = 0; listi < list.Count; listi++)
                {
                    if (this[i].IsIntersection(list[listi]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
