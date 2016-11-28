using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.DataStruct
{
    /// <summary>
    /// Object that can be seleceted.
    /// </summary>
    public class SelectableObject
    {
        /// <summary>
        /// Selection flag.
        /// </summary>
        private bool _IsSelected;

        /// <summary>
        /// Check if object is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
        }

        /// <summary>
        /// Set selection.
        /// </summary>
        /// <param name="is_selected">selection flag</param>
        private void SetSelection(bool is_selected)
        {
            _IsSelected = is_selected;
        }

        /// <summary>
        /// Set selction to true.
        /// </summary>
        public void Select()
        {
            SetSelection(true);
        }

        /// <summary>
        /// Set selection to false.
        /// </summary>
        public void UnSelect()
        {
            SetSelection(false);
        }
    }
}
