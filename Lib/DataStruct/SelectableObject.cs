using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

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
        [XmlIgnore]
        public bool IsSelected
        {
            get;
            private set;
        }

        /// <summary>
        /// Set selction to true.
        /// </summary>
        public void Select()
        {
            IsSelected = true;
        }

        /// <summary>
        /// Set selection to false.
        /// </summary>
        public void UnSelect()
        {
            IsSelected = false;
        }

        /// <summary>
        /// Change selection.
        /// </summary>
        public void SwitchSelection()
        {
            IsSelected = !IsSelected;
        }
    }
}
