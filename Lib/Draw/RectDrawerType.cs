// Author: Alexey Rybakov

namespace Lib.Draw
{
    /// <summary>
    /// Draw master type.
    /// </summary>
    enum RectDrawerType
    {
        /// <summary>
        /// For <c>Windows Forms</c> we draw in <c>Graphics</c> object.
        /// </summary>
        WindowsFormsGraphics,

        /// <summary>
        /// For <c>WPF</c> we draw in <c>DrawingVisual</c> object.
        /// </summary>
        WPFDrawingVisual
    }
}
