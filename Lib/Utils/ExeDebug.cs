using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lib.Utils
{
    /// <summary>
    /// Debug class.
    /// </summary>
    public class ExeDebug
    {
        /// <summary>
        /// Report error with current file and line.
        /// </summary>
        /// <param name="msg">message</param>
        /// <returns>message with file and line information</returns>
        public static String ReportError(string msg)
        {
            // Get the frame one step up the call tree.
            StackFrame st = new StackFrame(1, true);

            // These will now show the file and line number of the ReportError.
            string file = st.GetFileName();
            int line = st.GetFileLineNumber();

            return "Error: " + msg + " (file: " + file + ", line: " + line.ToString() + ")";
        }

        /// <summary>
        /// Current line number.
        /// </summary>
        public static int __LINE__
        {
            get
            {
                StackFrame st = new StackFrame(1, true);
                int line = new int();
                line += st.GetFileLineNumber();

                return line;
            }
        }

        /// <summary>
        /// Current file.
        /// </summary>
        public static string __FILE__
        {
            get
            {
                StackFrame st = new StackFrame(1, true);
                string temp = st.GetFileName();
                String file = String.Copy(String.IsNullOrEmpty(temp) ? "" : temp);

                return String.IsNullOrEmpty(file) ? "" : file;
            }
        }
    }
}
