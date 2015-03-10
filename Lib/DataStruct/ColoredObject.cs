// Author: Alexey Rybakov

namespace Lib.DataStruct
{
    /// <summary>
    /// Object that can has color (set of colors).
    /// </summary>
    public class ColoredObject
    {
        /// <summary>
        /// Mask of colors set.
        /// </summary>
        private int ColoresMask;

        /// <summary>
        /// Initialization.
        /// </summary>
        public ColoredObject()
        {
            ClearColores();
        }

        /// <summary>
        /// Color mask.
        /// </summary>
        /// <param name="n">color number</param>
        /// <returns>color mask</returns>
        static int ColorMask(int n)
        {
            return 1 << n;
        }

        /// <summary>
        /// Check if color is set.
        /// </summary>
        /// <param name="n">color number</param>
        /// <returns>true - if color is set, false - in another case</returns>
        public bool IsColored(int n)
        {
            return (ColoresMask & ColorMask(n)) != 0;
        }

        /// <summary>
        /// Check if color is not set.
        /// </summary>
        /// <param name="n">color number</param>
        /// <returns>true - if color is not set, false - in another case</returns>
        public bool IsNotColored(int n)
        {
            return !IsColored(n);
        }

        /// <summary>
        /// Set color.
        /// </summary>
        /// <param name="n">color number</param>
        public void SetColor(int n)
        {
            ColoresMask |= ColorMask(n);
        }

        /// <summary>
        /// Clear color.
        /// </summary>
        /// <param name="n">color number</param>
        public void ClearColor(int n)
        {
            ColoresMask &= ~ColorMask(n);
        }

        /// <summary>
        /// Switch color.
        /// </summary>
        /// <param name="n">color number</param>
        public void SwitchColor(int n)
        {
            ColoresMask ^= ColorMask(n);
        }

        /// <summary>
        /// Clear all colors.
        /// </summary>
        public void ClearColores()
        {
            ColoresMask = 0;
        }
    }
}
