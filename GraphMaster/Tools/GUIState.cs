// Author: Alexey Rybakov

namespace GraphMaster.Tools
{
    /// <summary>
    /// <c>GUI</c> state.
    /// </summary>
    public enum GUIState
    {
        /// <summary>
        /// No special sense.
        /// But we can capture single node in this mode.
        /// </summary>
        Common,

        /// <summary>
        /// Mode for select multi nodes and edges.
        /// </summary>
        Select,

        /// <summary>
        /// Move all selected elements.
        /// </summary>
        Move
    }
}
