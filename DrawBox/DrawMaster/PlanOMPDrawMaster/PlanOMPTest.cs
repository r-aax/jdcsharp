using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBox.DrawMaster.PlanOMPDrawMaster
{
    /// <summary>
    /// Type of Plan OMP test.
    /// </summary>
    enum PlanOMPTest
    {
        /// <summary>
        /// 4 threads, 3 procedures, 2 width
        /// </summary>
        Test4th3p2w_1,

        /// <summary>
        /// 4 threads, 3 procedures, 2 width
        /// </summary>
        Test4th3p2w_2,

        /// <summary>
        /// 4 threads, 3 procedures, 2 width
        /// </summary>
        Test4th3p2w_3,

        /// <summary>
        /// 4 threads, 3 procedures, 2 width
        /// </summary>
        Test4th3p2w_4,

        /// <summary>
        /// 4 threads, 3 procedures, 2 width
        /// </summary>
        Test4th3p2w_5,

        /// <summary>
        /// 8 threads, 4 procedures, 3, width
        /// </summary>
        Test8th4p3w,

        /// <summary>
        /// 16 threads, 7 procedures, 4 width
        /// </summary>
        Test16th7p4w,

        /// <summary>
        /// 16 threads, 8 procedures, 5 width
        /// </summary>
        Test16th8p5w,

        /// <summary>
        /// 244 threads, 34 procedures, 21 width
        /// </summary>
        Test244th34p21w,

        /// <summary>
        /// 244 threads, test based on ground grid
        /// </summary>
        Test244thGround
    }
}
