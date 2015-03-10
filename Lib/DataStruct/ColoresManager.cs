// Author: Alexey Rybakov

namespace Lib.DataStruct
{
    /// <summary>
    /// Colors manager.
    /// </summary>
    public class ColorsManager : ColoredObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorsManager()
        {
            ClearColores();
        }

        /// <summary>
        /// Clear all colors.
        /// </summary>
        public void FreeColors()
        {
            ClearColores();
        }

        /// <summary>
        /// New color allocation.
        /// </summary>
        /// <returns>new color</returns>
        public int AllocColor()
        {
            int len = sizeof(int) * 8;

            for (int i = 0; i < len; i++)
            {
                if (IsNotColored(i))
                {
                    SetColor(i);

                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Free color.
        /// </summary>
        /// <param name="n">color number</param>
        public void FreeColor(int n)
        {
            ClearColor(n);
        }
    }
}
