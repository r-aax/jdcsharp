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
    }
}
