using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBox.DrawMaster.PlanOMPDrawMaster
{
    /// <summary>
    /// Element of threads count change.
    /// </summary>
    class ThreadsCountChangeElement
    {
        /// <summary>
        /// Thread number (parent thread).
        /// </summary>
        public int ThreadNum;

        /// <summary>
        /// World (global) time.
        /// </summary>
        public double WTime;

        /// <summary>
        /// Threads count change.
        /// </summary>
        public int ChangeCount;

        /// <summary>
        /// Check if we allocate threads.
        /// </summary>
        public bool IsAlloc;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="thread_num">thread number</param>
        /// <param name="wtime">world time</param>
        /// <param name="change_count">count of threads (change)</param>
        /// <param name="is_alloc">if we allocate threads</param>
        public ThreadsCountChangeElement(int thread_num, double wtime, int change_count, bool is_alloc)
        {
            ThreadNum = thread_num;
            WTime = wtime;
            ChangeCount = change_count;
            IsAlloc = is_alloc;
        }
    };
}
