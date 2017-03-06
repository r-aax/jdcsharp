using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lib.MathMod.Grid.Load
{
    /// <summary>
    /// Grid loader from PFG/IBC format.
    /// </summary>
    public class GridLoaderPFG
    {
        /// <summary>
        /// Load block from PFG file.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        /// <param name="is_blank">isblank feature</param>
        public static void LoadBlock(StructuredGrid g, StreamReader sr, bool is_blank)
        {
            ;
        }

        /// <summary>
        /// Load blocks from PFG file.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="sr">stream reader</param>
        /// <param name="is_blank">isblank feature</param>
        public static void LoadBlocks(StructuredGrid g, StreamReader sr, bool is_blank)
        {
            string line;

            if ((line = sr.ReadLine()) != null)
            {
                int bc = Int32.Parse(line);

                for (int i = 0; i < bc; i++)
                {
                    LoadBlock(g, sr, is_blank);
                }
            }
        }

        /// <summary>
        /// Load structured grid from PFG/IBC files.
        /// </summary>
        /// <param name="g">grid</param>
        /// <param name="file_name">PFG file name</param>
        /// <param name="is_iblank">isblank feature</param>
        /// <returns><c>true</c> - if grid is loaded, <c>false</c> - otherwise</returns>
        public static bool Load(StructuredGrid g, string pfg_file_name, bool is_iblank)
        {
            bool is_succ = true;

            try
            {
                using (StreamReader pfg_sr = new StreamReader(pfg_file_name))
                {
                    LoadBlocks(g, pfg_sr, is_iblank);
                }
            }
            catch (Exception)
            {
                is_succ = false;
            }

            return is_succ;
        }
    }
}
