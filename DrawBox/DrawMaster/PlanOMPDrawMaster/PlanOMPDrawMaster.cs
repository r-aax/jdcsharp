using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;

using Lib.Draw.WPF;
using Lib.Maths.Geometry;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;

namespace DrawBox.DrawMaster.PlanOMPDrawMaster
{
    /// <summary>
    /// Plan OMP drawer.
    /// </summary>
    class PlanOMPDrawMaster
    {
        /// <summary>
        /// Draw test case.
        /// </summary>
        /// <param name="cnv">canvas</param>
        /// <param name="test">test</param>
        public static void Draw(Canvas cnv, PlanOMPTest test)
        {
            int master_threads = 0;
            int max_threads = 0;
            ThreadsCountChangeElement[] els = null;

            switch (test)
            {
                case PlanOMPTest.Test4th3p2w_1:
                    
                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(0, 99.9965474170, +1, true),
                        new ThreadsCountChangeElement(0, 100.1967713920, -1, false),
                        new ThreadsCountChangeElement(1, 100.1970040560, +1, true),
                        new ThreadsCountChangeElement(0, 100.1972496420, +0, true),
                        new ThreadsCountChangeElement(0, 100.2974233870, -0, false),
                        new ThreadsCountChangeElement(1, 100.4122099690, -1, false),
                        new ThreadsCountChangeElement(2, 100.4128559300, +1, true),
                        new ThreadsCountChangeElement(1, 100.4140073230, +0, true),
                        new ThreadsCountChangeElement(1, 100.5150017770, -0, false),
                        new ThreadsCountChangeElement(2, 100.6141368290, -1, false),
                        new ThreadsCountChangeElement(1, 100.6156012270, +1, true),
                        new ThreadsCountChangeElement(2, 100.6158018340, +0, true),
                        new ThreadsCountChangeElement(2, 100.7164478100, -0, false),
                        new ThreadsCountChangeElement(1, 100.8159082510, -1, false),
                        new ThreadsCountChangeElement(2, 100.8165636810, +1, true),
                        new ThreadsCountChangeElement(1, 100.8170282610, +0, true),
                        new ThreadsCountChangeElement(1, 100.9180189840, -0, false),
                        new ThreadsCountChangeElement(2, 101.0171621240, -1, false),
                        new ThreadsCountChangeElement(1, 101.0172855970, +1, true),
                        new ThreadsCountChangeElement(2, 101.0178267080, +0, true),
                        new ThreadsCountChangeElement(2, 101.1191302920, -0, false),
                        new ThreadsCountChangeElement(1, 101.2179372310, -1, false),
                        new ThreadsCountChangeElement(2, 101.2190863610, +1, true),
                        new ThreadsCountChangeElement(2, 101.4201765140, -1, false),
                        new ThreadsCountChangeElement(0, 101.4203294890, +1, true),
                        new ThreadsCountChangeElement(0, 101.6205578260, -1, false),
                        new ThreadsCountChangeElement(0, 101.6207151240, +0, true),
                        new ThreadsCountChangeElement(0, 101.7208217800, -0, false),
                        new ThreadsCountChangeElement(0, 101.7210477190, +1, true),
                        new ThreadsCountChangeElement(0, 101.9213306570, -1, false)
                    };

                    break;

                case PlanOMPTest.Test4th3p2w_2:

                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(1, 115.7610124800, +1, true),
                        new ThreadsCountChangeElement(1, 115.9616963860, -1, false),
                        new ThreadsCountChangeElement(0, 115.9628593740, +1, true),
                        new ThreadsCountChangeElement(1, 115.9630008240, +0, true),
                        new ThreadsCountChangeElement(1, 116.0630965040, -0, false),
                        new ThreadsCountChangeElement(0, 116.1631693760, -1, false),
                        new ThreadsCountChangeElement(0, 116.1637211950, +0, true),
                        new ThreadsCountChangeElement(2, 116.1639352590, +1, true),
                        new ThreadsCountChangeElement(0, 116.2639420640, -0, false),
                        new ThreadsCountChangeElement(2, 116.3799643710, -1, false),
                        new ThreadsCountChangeElement(1, 116.3810250540, +1, true),
                        new ThreadsCountChangeElement(2, 116.3811268440, +0, true),
                        new ThreadsCountChangeElement(2, 116.4819341730, -0, false),
                        new ThreadsCountChangeElement(1, 116.5812822600, -1, false),
                        new ThreadsCountChangeElement(0, 116.5817902650, +1, true),
                        new ThreadsCountChangeElement(1, 116.5819047580, +0, true),
                        new ThreadsCountChangeElement(1, 116.6820167530, -0, false),
                        new ThreadsCountChangeElement(0, 116.7820773160, -1, false),
                        new ThreadsCountChangeElement(1, 116.7827080450, +1, true),
                        new ThreadsCountChangeElement(0, 116.7831525540, +0, true),
                        new ThreadsCountChangeElement(0, 116.8832749750, -0, false),
                        new ThreadsCountChangeElement(1, 116.9832790620, -1, false),
                        new ThreadsCountChangeElement(2, 116.9839876140, +1, true),
                        new ThreadsCountChangeElement(2, 117.1846813060, -1, false),
                        new ThreadsCountChangeElement(0, 117.1857212290, +1, true),
                        new ThreadsCountChangeElement(2, 117.1858267300, +0, true),
                        new ThreadsCountChangeElement(2, 117.2859573580, -0, false),
                        new ThreadsCountChangeElement(0, 117.3860143550, -1, false),
                        new ThreadsCountChangeElement(2, 117.3861753050, +1, true),
                        new ThreadsCountChangeElement(2, 117.5896080800, -1, false)
                    };

                    break;

                case PlanOMPTest.Test4th3p2w_3:

                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(2, 131.7322500060, +1, true),
                        new ThreadsCountChangeElement(2, 131.9371930740, -1, false),
                        new ThreadsCountChangeElement(1, 131.9383551260, +1, true),
                        new ThreadsCountChangeElement(2, 131.9393075610, +0, true),
                        new ThreadsCountChangeElement(2, 132.0398739230, -0, false),
                        new ThreadsCountChangeElement(1, 132.1394848340, -1, false),
                        new ThreadsCountChangeElement(2, 132.1398915320, +1, true),
                        new ThreadsCountChangeElement(1, 132.1400740030, +0, true),
                        new ThreadsCountChangeElement(1, 132.2402561480, -0, false),
                        new ThreadsCountChangeElement(2, 132.3402267710, -1, false),
                        new ThreadsCountChangeElement(1, 132.3410009960, +1, true),
                        new ThreadsCountChangeElement(2, 132.3414815620, +0, true),
                        new ThreadsCountChangeElement(2, 132.4415951570, -0, false),
                        new ThreadsCountChangeElement(1, 132.5415929100, -1, false),
                        new ThreadsCountChangeElement(2, 132.5417301630, +1, true),
                        new ThreadsCountChangeElement(1, 132.5418311010, +0, true),
                        new ThreadsCountChangeElement(1, 132.6419995870, -0, false),
                        new ThreadsCountChangeElement(2, 132.7419716420, -1, false),
                        new ThreadsCountChangeElement(1, 132.7428907440, +1, true),
                        new ThreadsCountChangeElement(1, 132.9439333710, -1, false),
                        new ThreadsCountChangeElement(0, 132.9441613050, +1, true),
                        new ThreadsCountChangeElement(0, 133.1443946920, -1, false),
                        new ThreadsCountChangeElement(0, 133.1445912270, +0, true),
                        new ThreadsCountChangeElement(0, 133.2447330400, -0, false),
                        new ThreadsCountChangeElement(0, 133.2448886580, +1, true),
                        new ThreadsCountChangeElement(0, 133.4451184370, -1, false),
                        new ThreadsCountChangeElement(0, 133.4452664910, +0, true),
                        new ThreadsCountChangeElement(0, 133.5453697440, -0, false),
                        new ThreadsCountChangeElement(0, 133.5455431310, +1, true),
                        new ThreadsCountChangeElement(0, 133.7457926950, -1, false)
                    };

                    break;

                case PlanOMPTest.Test4th3p2w_4:

                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(2, 147.5658680350, +1, true),
                        new ThreadsCountChangeElement(2, 147.7669098160, -1, false),
                        new ThreadsCountChangeElement(0, 147.7681327080, +1, true),
                        new ThreadsCountChangeElement(2, 147.7682845010, +0, true),
                        new ThreadsCountChangeElement(2, 147.8684401300, -0, false),
                        new ThreadsCountChangeElement(0, 147.9684786910, -1, false),
                        new ThreadsCountChangeElement(2, 147.9695044390, +1, true),
                        new ThreadsCountChangeElement(0, 147.9722455360, +0, true),
                        new ThreadsCountChangeElement(0, 148.0725755760, -0, false),
                        new ThreadsCountChangeElement(2, 148.1802346190, -1, false),
                        new ThreadsCountChangeElement(1, 148.1809498020, +1, true),
                        new ThreadsCountChangeElement(2, 148.1814305990, +0, true),
                        new ThreadsCountChangeElement(2, 148.2819340880, -0, false),
                        new ThreadsCountChangeElement(1, 148.3815607580, -1, false),
                        new ThreadsCountChangeElement(2, 148.3821822360, +1, true),
                        new ThreadsCountChangeElement(1, 148.3826666090, +0, true),
                        new ThreadsCountChangeElement(1, 148.4836408500, -0, false),
                        new ThreadsCountChangeElement(2, 148.5852330730, -1, false),
                        new ThreadsCountChangeElement(1, 148.5854041500, +1, true),
                        new ThreadsCountChangeElement(1, 148.7878671420, -1, false),
                        new ThreadsCountChangeElement(0, 148.7882491850, +1, true),
                        new ThreadsCountChangeElement(1, 148.7883483510, +0, true),
                        new ThreadsCountChangeElement(1, 148.8885002870, -0, false),
                        new ThreadsCountChangeElement(0, 148.9884810140, -1, false),
                        new ThreadsCountChangeElement(1, 148.9886194590, +1, true),
                        new ThreadsCountChangeElement(0, 148.9890136680, +0, true),
                        new ThreadsCountChangeElement(0, 149.0891372360, -0, false),
                        new ThreadsCountChangeElement(1, 149.1891747040, -1, false),
                        new ThreadsCountChangeElement(0, 149.1898249700, +1, true),
                        new ThreadsCountChangeElement(0, 149.3900729970, -1, false)
                    };

                    break;

                case PlanOMPTest.Test4th3p2w_5:

                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(1, 163.8589496460, +1, true),
                        new ThreadsCountChangeElement(1, 164.0604088320, -1, false),
                        new ThreadsCountChangeElement(2, 164.0622796620, +1, true),
                        new ThreadsCountChangeElement(1, 164.0624566900, +0, true),
                        new ThreadsCountChangeElement(1, 164.1642983600, -0, false),
                        new ThreadsCountChangeElement(2, 164.2626308410, -1, false),
                        new ThreadsCountChangeElement(1, 164.2640085810, +1, true),
                        new ThreadsCountChangeElement(2, 164.2805069460, +0, true),
                        new ThreadsCountChangeElement(2, 164.3823697480, -0, false),
                        new ThreadsCountChangeElement(1, 164.4689733090, -1, false),
                        new ThreadsCountChangeElement(2, 164.4710099150, +1, true),
                        new ThreadsCountChangeElement(1, 164.4719727710, +0, true),
                        new ThreadsCountChangeElement(1, 164.5731169320, -0, false),
                        new ThreadsCountChangeElement(2, 164.6720945050, -1, false),
                        new ThreadsCountChangeElement(1, 164.6722307740, +1, true),
                        new ThreadsCountChangeElement(2, 164.6732432820, +0, true),
                        new ThreadsCountChangeElement(2, 164.7742119200, -0, false),
                        new ThreadsCountChangeElement(1, 164.8741701180, -1, false),
                        new ThreadsCountChangeElement(2, 164.8746373380, +1, true),
                        new ThreadsCountChangeElement(2, 165.0764463110, -1, false),
                        new ThreadsCountChangeElement(0, 165.0772002390, +1, true),
                        new ThreadsCountChangeElement(0, 165.2774213280, -1, false),
                        new ThreadsCountChangeElement(0, 165.2775853440, +0, true),
                        new ThreadsCountChangeElement(0, 165.3776730950, -0, false),
                        new ThreadsCountChangeElement(0, 165.3778506720, +1, true),
                        new ThreadsCountChangeElement(0, 165.5781044270, -1, false),
                        new ThreadsCountChangeElement(0, 165.5782835140, +0, true),
                        new ThreadsCountChangeElement(0, 165.6783866210, -0, false),
                        new ThreadsCountChangeElement(0, 165.6785532580, +1, true),
                        new ThreadsCountChangeElement(0, 165.8788184040, -1, false)
                    };

                    break;

                case PlanOMPTest.Test8th4p3w:

                    // 8 threads.
                    // 4 procedures with parallel part 3.
                    master_threads = 4;
                    max_threads = 8;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(2, 408.677, +2, true),
                        new ThreadsCountChangeElement(1, 408.679, +2, true),
                        new ThreadsCountChangeElement(2, 409.007, -2, false),
                        new ThreadsCountChangeElement(3, 409.008, +2, true),
                        new ThreadsCountChangeElement(2, 409.008, +0, true),
                        new ThreadsCountChangeElement(1, 409.052, -2, false),
                        new ThreadsCountChangeElement(1, 409.053, +0, true),
                        new ThreadsCountChangeElement(0, 409.055, +2, true),
                        new ThreadsCountChangeElement(2, 409.112, -0, false),
                        new ThreadsCountChangeElement(1, 409.16, -0, false),
                        new ThreadsCountChangeElement(3, 409.309, -2, false),
                        new ThreadsCountChangeElement(3, 409.311, +0, true),
                        new ThreadsCountChangeElement(1, 409.312, +2, true),
                        new ThreadsCountChangeElement(0, 409.385, -2, false),
                        new ThreadsCountChangeElement(0, 409.387, +0, true),
                        new ThreadsCountChangeElement(2, 409.389, +2, true),
                        new ThreadsCountChangeElement(3, 409.412, -0, false),
                        new ThreadsCountChangeElement(0, 409.66, -0, false),
                        new ThreadsCountChangeElement(1, 409.68, -2, false),
                        new ThreadsCountChangeElement(1, 409.688, +0, true),
                        new ThreadsCountChangeElement(0, 409.688, +2, true),
                        new ThreadsCountChangeElement(2, 409.751, -2, false),
                        new ThreadsCountChangeElement(2, 409.753, +0, true),
                        new ThreadsCountChangeElement(3, 409.76, +2, true),
                        new ThreadsCountChangeElement(1, 409.789, -0, false),
                        new ThreadsCountChangeElement(2, 409.856, -0, false),
                        new ThreadsCountChangeElement(0, 410.065, -2, false),
                        new ThreadsCountChangeElement(1, 410.067, +2, true),
                        new ThreadsCountChangeElement(0, 410.07, +0, true),
                        new ThreadsCountChangeElement(3, 410.138, -2, false),
                        new ThreadsCountChangeElement(3, 410.139, +0, true),
                        new ThreadsCountChangeElement(0, 410.17, -0, false),
                        new ThreadsCountChangeElement(0, 410.17, +2, true),
                        new ThreadsCountChangeElement(3, 410.241, -0, false),
                        new ThreadsCountChangeElement(1, 410.448, -2, false),
                        new ThreadsCountChangeElement(2, 410.448, +2, true),
                        new ThreadsCountChangeElement(0, 410.54, -2, false),
                        new ThreadsCountChangeElement(3, 410.544, +2, true),
                        new ThreadsCountChangeElement(2, 410.849, -2, false),
                        new ThreadsCountChangeElement(3, 410.853, -2, false),
                    };

                    break;

                case PlanOMPTest.Test16th7p4w:

                    // 16 threads.
                    // 7 procedures with parallel part 4.
                    master_threads = 7;
                    max_threads = 16;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(0, 5584338.6986027937, +3, true),
                        new ThreadsCountChangeElement(1, 5584338.6986286817, +3, true),
                        new ThreadsCountChangeElement(2, 5584338.6986447843, +3, true),
                        new ThreadsCountChangeElement(2, 5584339.0994339418, -3, false),
                        new ThreadsCountChangeElement(3, 5584339.0994496057, +3, true),
                        new ThreadsCountChangeElement(0, 5584339.0994650768, -3, false),
                        new ThreadsCountChangeElement(2, 5584339.0994770797, +0, true),
                        new ThreadsCountChangeElement(0, 5584339.0994817158, +0, true),
                        new ThreadsCountChangeElement(6, 5584339.0994872032, +3, true),
                        new ThreadsCountChangeElement(1, 5584339.1145869130, -3, false),
                        new ThreadsCountChangeElement(5, 5584339.1145994421, +3, true),
                        new ThreadsCountChangeElement(1, 5584339.1146230251, +0, true),
                        new ThreadsCountChangeElement(2, 5584339.1994845001, -0, false),
                        new ThreadsCountChangeElement(0, 5584339.1994939568, -0, false),
                        new ThreadsCountChangeElement(1, 5584339.2146322243, -0, false),
                        new ThreadsCountChangeElement(3, 5584339.5002140021, -3, false),
                        new ThreadsCountChangeElement(0, 5584339.5002256120, +3, true),
                        new ThreadsCountChangeElement(3, 5584339.5002315817, +0, true),
                        new ThreadsCountChangeElement(6, 5584339.5141646070, -3, false),
                        new ThreadsCountChangeElement(1, 5584339.5141764544, +3, true),
                        new ThreadsCountChangeElement(6, 5584339.5141882813, +0, true),
                        new ThreadsCountChangeElement(5, 5584339.5327054383, -3, false),
                        new ThreadsCountChangeElement(4, 5584339.5327331200, +3, true),
                        new ThreadsCountChangeElement(5, 5584339.5327465804, +0, true),
                        new ThreadsCountChangeElement(3, 5584339.6002388503, -0, false),
                        new ThreadsCountChangeElement(6, 5584339.6141995369, -0, false),
                        new ThreadsCountChangeElement(5, 5584339.6327551370, -0, false),
                        new ThreadsCountChangeElement(0, 5584339.9340282157, -3, false),
                        new ThreadsCountChangeElement(3, 5584339.9340405418, +3, true),
                        new ThreadsCountChangeElement(0, 5584339.9340523807, +0, true),
                        new ThreadsCountChangeElement(1, 5584339.9440223901, -3, false),
                        new ThreadsCountChangeElement(2, 5584339.9440333312, +3, true),
                        new ThreadsCountChangeElement(1, 5584339.9440442929, +0, true),
                        new ThreadsCountChangeElement(4, 5584339.9541647388, -3, false),
                        new ThreadsCountChangeElement(5, 5584339.9541900018, +3, true),
                        new ThreadsCountChangeElement(4, 5584339.9541979367, +0, true),
                        new ThreadsCountChangeElement(0, 5584340.0340613928, -0, false),
                        new ThreadsCountChangeElement(1, 5584340.0540202605, -0, false),
                        new ThreadsCountChangeElement(4, 5584340.0542054409, -0, false),
                        new ThreadsCountChangeElement(3, 5584340.3440615591, -3, false),
                        new ThreadsCountChangeElement(0, 5584340.3440742008, +3, true),
                        new ThreadsCountChangeElement(3, 5584340.3440858424, +0, true),
                        new ThreadsCountChangeElement(2, 5584340.3541726656, -3, false),
                        new ThreadsCountChangeElement(1, 5584340.3541840212, +3, true),
                        new ThreadsCountChangeElement(2, 5584340.3541956889, +0, true),
                        new ThreadsCountChangeElement(5, 5584340.3608518662, -3, false),
                        new ThreadsCountChangeElement(4, 5584340.3608755302, +3, true),
                        new ThreadsCountChangeElement(5, 5584340.3608861910, +0, true),
                        new ThreadsCountChangeElement(3, 5584340.4441612810, -0, false),
                        new ThreadsCountChangeElement(2, 5584340.4542306382, -0, false),
                        new ThreadsCountChangeElement(5, 5584340.4608936599, -0, false),
                        new ThreadsCountChangeElement(0, 5584340.7544866353, -3, false),
                        new ThreadsCountChangeElement(3, 5584340.7544984128, +3, true),
                        new ThreadsCountChangeElement(1, 5584340.7640330410, -3, false),
                        new ThreadsCountChangeElement(6, 5584340.7640439160, +3, true),
                        new ThreadsCountChangeElement(4, 5584340.7668267116, -3, false),
                        new ThreadsCountChangeElement(5, 5584340.7668496557, +3, true),
                        new ThreadsCountChangeElement(4, 5584340.7668605493, +0, true),
                        new ThreadsCountChangeElement(4, 5584340.8668682883, -0, false),
                        new ThreadsCountChangeElement(5, 5584341.1677962122, -3, false),
                        new ThreadsCountChangeElement(4, 5584341.1678243466, +3, true),
                        new ThreadsCountChangeElement(3, 5584341.1840145160, -3, false),
                        new ThreadsCountChangeElement(2, 5584341.1840296453, +3, true),
                        new ThreadsCountChangeElement(6, 5584341.1940198895, -3, false),
                        new ThreadsCountChangeElement(6, 5584341.1940362751, +0, true),
                        new ThreadsCountChangeElement(6, 5584341.2940464569, -0, false),
                        new ThreadsCountChangeElement(6, 5584341.2940594489, +3, true),
                        new ThreadsCountChangeElement(4, 5584341.5679127760, -3, false),
                        new ThreadsCountChangeElement(2, 5584341.5844751438, -3, false),
                        new ThreadsCountChangeElement(6, 5584341.7095417399, -3, false)
                    };

                    break;

                case PlanOMPTest.Test16th8p5w:

                    // 16 threads.
                    // 8 procedures with parallel part 8.
                    master_threads = 8;
                    max_threads = 16;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(5, 5577833.0099753961, +4, true),
                        new ThreadsCountChangeElement(3, 5577833.0099943206, +4, true),
                        new ThreadsCountChangeElement(5, 5577833.5124745565, -4, false),
                        new ThreadsCountChangeElement(0, 5577833.5125041064, +4, true),
                        new ThreadsCountChangeElement(5, 5577833.5125268744, +0, true),
                        new ThreadsCountChangeElement(3, 5577833.5471619712, -4, false),
                        new ThreadsCountChangeElement(4, 5577833.5471749380, +4, true),
                        new ThreadsCountChangeElement(3, 5577833.5471887691, +0, true),
                        new ThreadsCountChangeElement(5, 5577833.6125374380, -0, false),
                        new ThreadsCountChangeElement(3, 5577833.6471966403, -0, false),
                        new ThreadsCountChangeElement(0, 5577834.0340576628, -4, false),
                        new ThreadsCountChangeElement(7, 5577834.0340704927, +4, true),
                        new ThreadsCountChangeElement(0, 5577834.0346367927, +0, true),
                        new ThreadsCountChangeElement(4, 5577834.0655096648, -4, false),
                        new ThreadsCountChangeElement(2, 5577834.0655218307, +4, true),
                        new ThreadsCountChangeElement(4, 5577834.0655408408, +0, true),
                        new ThreadsCountChangeElement(0, 5577834.1346462630, -0, false),
                        new ThreadsCountChangeElement(4, 5577834.1655496806, -0, false),
                        new ThreadsCountChangeElement(7, 5577834.5444746763, -4, false),
                        new ThreadsCountChangeElement(1, 5577834.5444871774, +4, true),
                        new ThreadsCountChangeElement(7, 5577834.5445210636, +0, true),
                        new ThreadsCountChangeElement(2, 5577834.5842119688, -4, false),
                        new ThreadsCountChangeElement(5, 5577834.5842235191, +4, true),
                        new ThreadsCountChangeElement(2, 5577834.5842359466, +0, true),
                        new ThreadsCountChangeElement(7, 5577834.6445294069, -0, false),
                        new ThreadsCountChangeElement(2, 5577834.6842435403, -0, false),
                        new ThreadsCountChangeElement(1, 5577835.0565129193, -4, false),
                        new ThreadsCountChangeElement(7, 5577835.0565260584, +4, true),
                        new ThreadsCountChangeElement(1, 5577835.0565369902, +0, true),
                        new ThreadsCountChangeElement(5, 5577835.1140246792, -4, false),
                        new ThreadsCountChangeElement(2, 5577835.1140343836, +4, true),
                        new ThreadsCountChangeElement(5, 5577835.1140469294, +0, true),
                        new ThreadsCountChangeElement(1, 5577835.1565456940, -0, false),
                        new ThreadsCountChangeElement(5, 5577835.2140535619, -0, false),
                        new ThreadsCountChangeElement(7, 5577835.5742434701, -4, false),
                        new ThreadsCountChangeElement(1, 5577835.5742751639, +4, true),
                        new ThreadsCountChangeElement(7, 5577835.5742879147, +0, true),
                        new ThreadsCountChangeElement(2, 5577835.6452569077, -4, false),
                        new ThreadsCountChangeElement(5, 5577835.6452702312, +4, true),
                        new ThreadsCountChangeElement(2, 5577835.6452785227, +0, true),
                        new ThreadsCountChangeElement(7, 5577835.6742966063, -0, false),
                        new ThreadsCountChangeElement(2, 5577835.7452854794, -0, false),
                        new ThreadsCountChangeElement(1, 5577836.1042396221, -4, false),
                        new ThreadsCountChangeElement(2, 5577836.1042530127, +4, true),
                        new ThreadsCountChangeElement(1, 5577836.1042645304, +0, true),
                        new ThreadsCountChangeElement(5, 5577836.1640223833, -4, false),
                        new ThreadsCountChangeElement(4, 5577836.1640571915, +4, true),
                        new ThreadsCountChangeElement(1, 5577836.2042734539, -0, false),
                        new ThreadsCountChangeElement(2, 5577836.6178896176, -4, false),
                        new ThreadsCountChangeElement(1, 5577836.6179036163, +4, true),
                        new ThreadsCountChangeElement(4, 5577836.6750670411, -4, false),
                        new ThreadsCountChangeElement(0, 5577836.6750839837, +4, true),
                        new ThreadsCountChangeElement(4, 5577836.6750964643, +0, true),
                        new ThreadsCountChangeElement(4, 5577836.7751029292, -0, false),
                        new ThreadsCountChangeElement(1, 5577837.1205102904, -4, false),
                        new ThreadsCountChangeElement(7, 5577837.1205306854, +4, true),
                        new ThreadsCountChangeElement(0, 5577837.1930591632, -4, false),
                        new ThreadsCountChangeElement(4, 5577837.1930715563, +4, true),
                        new ThreadsCountChangeElement(0, 5577837.1930795750, +0, true),
                        new ThreadsCountChangeElement(0, 5577837.2930873139, -0, false),
                        new ThreadsCountChangeElement(7, 5577837.6250944706, -4, false),
                        new ThreadsCountChangeElement(0, 5577837.6251213914, +4, true),
                        new ThreadsCountChangeElement(4, 5577837.6941649364, -4, false),
                        new ThreadsCountChangeElement(6, 5577837.6941757919, +4, true),
                        new ThreadsCountChangeElement(0, 5577838.1252781218, -4, false),
                        new ThreadsCountChangeElement(3, 5577838.1253044363, +4, true),
                        new ThreadsCountChangeElement(6, 5577838.1953132842, -4, false),
                        new ThreadsCountChangeElement(6, 5577838.1953254333, +0, true),
                        new ThreadsCountChangeElement(6, 5577838.2953344677, -0, false),
                        new ThreadsCountChangeElement(6, 5577838.2953495011, +4, true),
                        new ThreadsCountChangeElement(3, 5577838.6254296834, -4, false),
                        new ThreadsCountChangeElement(3, 5577838.6254535802, +0, true),
                        new ThreadsCountChangeElement(3, 5577838.7254626239, -0, false),
                        new ThreadsCountChangeElement(3, 5577838.7254729578, +4, true),
                        new ThreadsCountChangeElement(6, 5577838.7961322945, -4, false),
                        new ThreadsCountChangeElement(6, 5577838.7961454904, +0, true),
                        new ThreadsCountChangeElement(6, 5577838.8961538943, -0, false),
                        new ThreadsCountChangeElement(6, 5577838.8961666226, +4, true),
                        new ThreadsCountChangeElement(3, 5577839.2255727733, -4, false),
                        new ThreadsCountChangeElement(6, 5577839.4085708056, -4, false)
                    };

                    break;

                case PlanOMPTest.Test244th34p21w:

                    // 244 threads.
                    // 34 procedures with parallel part 21.
                    master_threads = 34;
                    max_threads = 244;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(16, 1473169472.2682309151, +20, true),
                        new ThreadsCountChangeElement(25, 1473169472.2683749199, +20, true),
                        new ThreadsCountChangeElement(24, 1473169472.2684988976, +20, true),
                        new ThreadsCountChangeElement(33, 1473169472.2686159611, +20, true),
                        new ThreadsCountChangeElement(0, 1473169472.2687449455, +20, true),
                        new ThreadsCountChangeElement(5, 1473169472.2688241005, +20, true),
                        new ThreadsCountChangeElement(27, 1473169472.2689390182, +20, true),
                        new ThreadsCountChangeElement(4, 1473169472.2690849304, +20, true),
                        new ThreadsCountChangeElement(26, 1473169472.2692320347, +20, true),
                        new ThreadsCountChangeElement(32, 1473169472.2693810463, +20, true),
                        new ThreadsCountChangeElement(16, 1473169474.3686571121, -20, false),
                        new ThreadsCountChangeElement(20, 1473169474.3690609932, +20, true),
                        new ThreadsCountChangeElement(25, 1473169474.3698260784, -20, false),
                        new ThreadsCountChangeElement(16, 1473169474.3702869415, +0, true),
                        new ThreadsCountChangeElement(33, 1473169474.3705570698, -20, false),
                        new ThreadsCountChangeElement(24, 1473169474.3709740639, -20, false),
                        new ThreadsCountChangeElement(0, 1473169474.3713068962, -20, false),
                        new ThreadsCountChangeElement(12, 1473169474.3716289997, +20, true),
                        new ThreadsCountChangeElement(11, 1473169474.3721029758, +20, true),
                        new ThreadsCountChangeElement(15, 1473169474.3725800514, +20, true),
                        new ThreadsCountChangeElement(2, 1473169474.3730459213, +20, true),
                        new ThreadsCountChangeElement(5, 1473169474.3736259937, -20, false),
                        new ThreadsCountChangeElement(27, 1473169474.3740720749, -20, false),
                        new ThreadsCountChangeElement(25, 1473169474.3742909431, +0, true),
                        new ThreadsCountChangeElement(4, 1473169474.3744559288, -20, false),
                        new ThreadsCountChangeElement(32, 1473169474.3746650219, -20, false),
                        new ThreadsCountChangeElement(26, 1473169474.3748559952, -20, false),
                        new ThreadsCountChangeElement(33, 1473169474.3750538826, +0, true),
                        new ThreadsCountChangeElement(24, 1473169474.3752191067, +0, true),
                        new ThreadsCountChangeElement(0, 1473169474.3753819466, +0, true),
                        new ThreadsCountChangeElement(22, 1473169474.3755729198, +20, true),
                        new ThreadsCountChangeElement(9, 1473169474.3758540154, +20, true),
                        new ThreadsCountChangeElement(21, 1473169474.3761520386, +20, true),
                        new ThreadsCountChangeElement(23, 1473169474.3764719963, +20, true),
                        new ThreadsCountChangeElement(3, 1473169474.3767681122, +20, true),
                        new ThreadsCountChangeElement(5, 1473169474.3771269321, +0, true),
                        new ThreadsCountChangeElement(27, 1473169474.3773140907, +0, true),
                        new ThreadsCountChangeElement(4, 1473169474.3773820400, +0, true),
                        new ThreadsCountChangeElement(32, 1473169474.3774499893, +0, true),
                        new ThreadsCountChangeElement(26, 1473169474.3775179386, +0, true),
                        new ThreadsCountChangeElement(16, 1473169474.4707520008, -0, false),
                        new ThreadsCountChangeElement(25, 1473169474.4744799137, -0, false),
                        new ThreadsCountChangeElement(33, 1473169474.4752490520, -0, false),
                        new ThreadsCountChangeElement(24, 1473169474.4756019115, -0, false),
                        new ThreadsCountChangeElement(0, 1473169474.4759490490, -0, false),
                        new ThreadsCountChangeElement(5, 1473169474.4773509502, -0, false),
                        new ThreadsCountChangeElement(27, 1473169474.4775459766, -0, false),
                        new ThreadsCountChangeElement(4, 1473169474.4777328968, -0, false),
                        new ThreadsCountChangeElement(32, 1473169474.4779200554, -0, false),
                        new ThreadsCountChangeElement(26, 1473169474.4781041145, -0, false),
                        new ThreadsCountChangeElement(20, 1473169476.4703609943, -20, false),
                        new ThreadsCountChangeElement(14, 1473169476.4708080292, +20, true),
                        new ThreadsCountChangeElement(20, 1473169476.4714260101, +0, true),
                        new ThreadsCountChangeElement(12, 1473169476.4723880291, -20, false),
                        new ThreadsCountChangeElement(7, 1473169476.4726269245, +20, true),
                        new ThreadsCountChangeElement(12, 1473169476.4729900360, +0, true),
                        new ThreadsCountChangeElement(11, 1473169476.4731669426, -20, false),
                        new ThreadsCountChangeElement(8, 1473169476.4734129906, +20, true),
                        new ThreadsCountChangeElement(15, 1473169476.4737670422, -20, false),
                        new ThreadsCountChangeElement(11, 1473169476.4739940166, +0, true),
                        new ThreadsCountChangeElement(27, 1473169476.4741640091, +20, true),
                        new ThreadsCountChangeElement(15, 1473169476.4744529724, +0, true),
                        new ThreadsCountChangeElement(2, 1473169476.4746379852, -20, false),
                        new ThreadsCountChangeElement(1, 1473169476.4748620987, +20, true),
                        new ThreadsCountChangeElement(2, 1473169476.4751889706, +0, true),
                        new ThreadsCountChangeElement(22, 1473169476.4760959148, -20, false),
                        new ThreadsCountChangeElement(6, 1473169476.4763100147, +20, true),
                        new ThreadsCountChangeElement(22, 1473169476.4767169952, +0, true),
                        new ThreadsCountChangeElement(9, 1473169476.4768919945, -20, false),
                        new ThreadsCountChangeElement(21, 1473169476.4771010876, -20, false),
                        new ThreadsCountChangeElement(16, 1473169476.4773020744, +20, true),
                        new ThreadsCountChangeElement(32, 1473169476.4775168896, +20, true),
                        new ThreadsCountChangeElement(9, 1473169476.4777369499, +0, true),
                        new ThreadsCountChangeElement(23, 1473169476.4779119492, -20, false),
                        new ThreadsCountChangeElement(21, 1473169476.4781050682, +0, true),
                        new ThreadsCountChangeElement(3, 1473169476.4782750607, -20, false),
                        new ThreadsCountChangeElement(4, 1473169476.4784860611, +20, true),
                        new ThreadsCountChangeElement(18, 1473169476.4786939621, +20, true),
                        new ThreadsCountChangeElement(23, 1473169476.4790339470, +0, true),
                        new ThreadsCountChangeElement(3, 1473169476.4792110920, +0, true),
                        new ThreadsCountChangeElement(20, 1473169476.5717198849, -0, false),
                        new ThreadsCountChangeElement(12, 1473169476.5731871128, -0, false),
                        new ThreadsCountChangeElement(11, 1473169476.5741949081, -0, false),
                        new ThreadsCountChangeElement(15, 1473169476.5746610165, -0, false),
                        new ThreadsCountChangeElement(2, 1473169476.5753960609, -0, false),
                        new ThreadsCountChangeElement(22, 1473169476.5770149231, -0, false),
                        new ThreadsCountChangeElement(9, 1473169476.5779318810, -0, false),
                        new ThreadsCountChangeElement(21, 1473169476.5782909393, -0, false),
                        new ThreadsCountChangeElement(23, 1473169476.5792360306, -0, false),
                        new ThreadsCountChangeElement(3, 1473169476.5794210434, -0, false),
                        new ThreadsCountChangeElement(14, 1473169478.5716269016, -20, false),
                        new ThreadsCountChangeElement(29, 1473169478.5720579624, +20, true),
                        new ThreadsCountChangeElement(14, 1473169478.5727760792, +0, true),
                        new ThreadsCountChangeElement(7, 1473169478.5731840134, -20, false),
                        new ThreadsCountChangeElement(33, 1473169478.5736510754, +20, true),
                        new ThreadsCountChangeElement(7, 1473169478.5740880966, +0, true),
                        new ThreadsCountChangeElement(8, 1473169478.5743970871, -20, false),
                        new ThreadsCountChangeElement(11, 1473169478.5747869015, +20, true),
                        new ThreadsCountChangeElement(27, 1473169478.5751919746, -20, false),
                        new ThreadsCountChangeElement(8, 1473169478.5754768848, +0, true),
                        new ThreadsCountChangeElement(2, 1473169478.5756490231, +20, true),
                        new ThreadsCountChangeElement(27, 1473169478.5759119987, +0, true),
                        new ThreadsCountChangeElement(1, 1473169478.5760951042, -20, false),
                        new ThreadsCountChangeElement(20, 1473169478.5762920380, +20, true),
                        new ThreadsCountChangeElement(1, 1473169478.5765430927, +0, true),
                        new ThreadsCountChangeElement(6, 1473169478.5771141052, -20, false),
                        new ThreadsCountChangeElement(28, 1473169478.5773379803, +20, true),
                        new ThreadsCountChangeElement(6, 1473169478.5776619911, +0, true),
                        new ThreadsCountChangeElement(16, 1473169478.5778489113, -20, false),
                        new ThreadsCountChangeElement(23, 1473169478.5780661106, +20, true),
                        new ThreadsCountChangeElement(32, 1473169478.5783119202, -20, false),
                        new ThreadsCountChangeElement(16, 1473169478.5785090923, +0, true),
                        new ThreadsCountChangeElement(25, 1473169478.5786900520, +20, true),
                        new ThreadsCountChangeElement(32, 1473169478.5789549351, +0, true),
                        new ThreadsCountChangeElement(4, 1473169478.5791339874, -20, false),
                        new ThreadsCountChangeElement(13, 1473169478.5793499947, +20, true),
                        new ThreadsCountChangeElement(18, 1473169478.5796890259, -20, false),
                        new ThreadsCountChangeElement(4, 1473169478.5798900127, +0, true),
                        new ThreadsCountChangeElement(21, 1473169478.5800659657, +20, true),
                        new ThreadsCountChangeElement(18, 1473169478.5802979469, +0, true),
                        new ThreadsCountChangeElement(14, 1473169478.6731040478, -0, false),
                        new ThreadsCountChangeElement(7, 1473169478.6744298935, -0, false),
                        new ThreadsCountChangeElement(8, 1473169478.6756761074, -0, false),
                        new ThreadsCountChangeElement(27, 1473169478.6761100292, -0, false),
                        new ThreadsCountChangeElement(1, 1473169478.6767320633, -0, false),
                        new ThreadsCountChangeElement(6, 1473169478.6778669357, -0, false),
                        new ThreadsCountChangeElement(16, 1473169478.6787118912, -0, false),
                        new ThreadsCountChangeElement(32, 1473169478.6792230606, -0, false),
                        new ThreadsCountChangeElement(4, 1473169478.6800849438, -0, false),
                        new ThreadsCountChangeElement(18, 1473169478.6804990768, -0, false),
                        new ThreadsCountChangeElement(29, 1473169480.6729440689, -20, false),
                        new ThreadsCountChangeElement(26, 1473169480.6733651161, +20, true),
                        new ThreadsCountChangeElement(29, 1473169480.6739680767, +0, true),
                        new ThreadsCountChangeElement(33, 1473169480.6743330956, -20, false),
                        new ThreadsCountChangeElement(16, 1473169480.6745510101, +20, true),
                        new ThreadsCountChangeElement(33, 1473169480.6748039722, +0, true),
                        new ThreadsCountChangeElement(11, 1473169480.6754009724, -20, false),
                        new ThreadsCountChangeElement(6, 1473169480.6756188869, +20, true),
                        new ThreadsCountChangeElement(11, 1473169480.6758699417, +0, true),
                        new ThreadsCountChangeElement(2, 1473169480.6761190891, -20, false),
                        new ThreadsCountChangeElement(8, 1473169480.6763210297, +20, true),
                        new ThreadsCountChangeElement(2, 1473169480.6765830517, +0, true),
                        new ThreadsCountChangeElement(20, 1473169480.6767709255, -20, false),
                        new ThreadsCountChangeElement(27, 1473169480.6769919395, +20, true),
                        new ThreadsCountChangeElement(20, 1473169480.6772511005, +0, true),
                        new ThreadsCountChangeElement(28, 1473169480.6778640747, -20, false),
                        new ThreadsCountChangeElement(18, 1473169480.6780660152, +20, true),
                        new ThreadsCountChangeElement(28, 1473169480.6783120632, +0, true),
                        new ThreadsCountChangeElement(23, 1473169480.6785159111, -20, false),
                        new ThreadsCountChangeElement(32, 1473169480.6787140369, +20, true),
                        new ThreadsCountChangeElement(23, 1473169480.6789400578, +0, true),
                        new ThreadsCountChangeElement(25, 1473169480.6792860031, -20, false),
                        new ThreadsCountChangeElement(3, 1473169480.6794929504, +20, true),
                        new ThreadsCountChangeElement(25, 1473169480.6797211170, +0, true),
                        new ThreadsCountChangeElement(13, 1473169480.6799149513, -20, false),
                        new ThreadsCountChangeElement(31, 1473169480.6801230907, +20, true),
                        new ThreadsCountChangeElement(13, 1473169480.6804640293, +0, true),
                        new ThreadsCountChangeElement(21, 1473169480.6806449890, -20, false),
                        new ThreadsCountChangeElement(9, 1473169480.6808800697, +20, true),
                        new ThreadsCountChangeElement(21, 1473169480.6811230183, +0, true),
                        new ThreadsCountChangeElement(29, 1473169480.7742629051, -0, false),
                        new ThreadsCountChangeElement(33, 1473169480.7750039101, -0, false),
                        new ThreadsCountChangeElement(11, 1473169480.7761371136, -0, false),
                        new ThreadsCountChangeElement(2, 1473169480.7767839432, -0, false),
                        new ThreadsCountChangeElement(20, 1473169480.7774469852, -0, false),
                        new ThreadsCountChangeElement(28, 1473169480.7785339355, -0, false),
                        new ThreadsCountChangeElement(23, 1473169480.7791368961, -0, false),
                        new ThreadsCountChangeElement(25, 1473169480.7799339294, -0, false),
                        new ThreadsCountChangeElement(13, 1473169480.7806649208, -0, false),
                        new ThreadsCountChangeElement(21, 1473169480.7813179493, -0, false),
                        new ThreadsCountChangeElement(26, 1473169482.7741489410, -20, false),
                        new ThreadsCountChangeElement(21, 1473169482.7745280266, +20, true),
                        new ThreadsCountChangeElement(26, 1473169482.7749280930, +0, true),
                        new ThreadsCountChangeElement(16, 1473169482.7752420902, -20, false),
                        new ThreadsCountChangeElement(12, 1473169482.7756330967, +20, true),
                        new ThreadsCountChangeElement(6, 1473169482.7762360573, -20, false),
                        new ThreadsCountChangeElement(2, 1473169482.7766890526, +20, true),
                        new ThreadsCountChangeElement(6, 1473169482.7771189213, +0, true),
                        new ThreadsCountChangeElement(8, 1473169482.7774250507, -20, false),
                        new ThreadsCountChangeElement(7, 1473169482.7777919769, +20, true),
                        new ThreadsCountChangeElement(27, 1473169482.7780649662, -20, false),
                        new ThreadsCountChangeElement(8, 1473169482.7782769203, +0, true),
                        new ThreadsCountChangeElement(17, 1473169482.7784640789, +20, true),
                        new ThreadsCountChangeElement(18, 1473169482.7788660526, -20, false),
                        new ThreadsCountChangeElement(29, 1473169482.7790579796, +20, true),
                        new ThreadsCountChangeElement(18, 1473169482.7793130875, +0, true),
                        new ThreadsCountChangeElement(32, 1473169482.7794899940, -20, false),
                        new ThreadsCountChangeElement(28, 1473169482.7796750069, +20, true),
                        new ThreadsCountChangeElement(3, 1473169482.7801349163, -20, false),
                        new ThreadsCountChangeElement(11, 1473169482.7803289890, +20, true),
                        new ThreadsCountChangeElement(3, 1473169482.7805690765, +0, true),
                        new ThreadsCountChangeElement(31, 1473169482.7807550430, -20, false),
                        new ThreadsCountChangeElement(13, 1473169482.7809720039, +20, true),
                        new ThreadsCountChangeElement(31, 1473169482.7811989784, +0, true),
                        new ThreadsCountChangeElement(9, 1473169482.7814381123, -20, false),
                        new ThreadsCountChangeElement(0, 1473169482.7816510201, +20, true),
                        new ThreadsCountChangeElement(9, 1473169482.7819020748, +0, true),
                        new ThreadsCountChangeElement(26, 1473169482.8752710819, -0, false),
                        new ThreadsCountChangeElement(6, 1473169482.8775029182, -0, false),
                        new ThreadsCountChangeElement(8, 1473169482.8784730434, -0, false),
                        new ThreadsCountChangeElement(18, 1473169482.8795089722, -0, false),
                        new ThreadsCountChangeElement(3, 1473169482.8807690144, -0, false),
                        new ThreadsCountChangeElement(31, 1473169482.8814630508, -0, false),
                        new ThreadsCountChangeElement(9, 1473169482.8821001053, -0, false),
                        new ThreadsCountChangeElement(21, 1473169484.8751270771, -20, false),
                        new ThreadsCountChangeElement(24, 1473169484.8755280972, +20, true),
                        new ThreadsCountChangeElement(12, 1473169484.8762769699, -20, false),
                        new ThreadsCountChangeElement(26, 1473169484.8765900135, +20, true),
                        new ThreadsCountChangeElement(12, 1473169484.8768599033, +0, true),
                        new ThreadsCountChangeElement(2, 1473169484.8773429394, -20, false),
                        new ThreadsCountChangeElement(31, 1473169484.8775529861, +20, true),
                        new ThreadsCountChangeElement(7, 1473169484.8782939911, -20, false),
                        new ThreadsCountChangeElement(4, 1473169484.8785040379, +20, true),
                        new ThreadsCountChangeElement(7, 1473169484.8787510395, +0, true),
                        new ThreadsCountChangeElement(17, 1473169484.8790369034, -20, false),
                        new ThreadsCountChangeElement(20, 1473169484.8792479038, +20, true),
                        new ThreadsCountChangeElement(17, 1473169484.8794980049, +0, true),
                        new ThreadsCountChangeElement(29, 1473169484.8796761036, -20, false),
                        new ThreadsCountChangeElement(1, 1473169484.8798780441, +20, true),
                        new ThreadsCountChangeElement(29, 1473169484.8801310062, +0, true),
                        new ThreadsCountChangeElement(28, 1473169484.8803129196, -20, false),
                        new ThreadsCountChangeElement(19, 1473169484.8805971146, +20, true),
                        new ThreadsCountChangeElement(28, 1473169484.8809249401, +0, true),
                        new ThreadsCountChangeElement(11, 1473169484.8811080456, -20, false),
                        new ThreadsCountChangeElement(8, 1473169484.8813040257, +20, true),
                        new ThreadsCountChangeElement(13, 1473169484.8815519810, -20, false),
                        new ThreadsCountChangeElement(5, 1473169484.8817520142, +20, true),
                        new ThreadsCountChangeElement(13, 1473169484.8819839954, +0, true),
                        new ThreadsCountChangeElement(0, 1473169484.8822419643, -20, false),
                        new ThreadsCountChangeElement(23, 1473169484.8824579716, +20, true),
                        new ThreadsCountChangeElement(0, 1473169484.8826780319, +0, true),
                        new ThreadsCountChangeElement(12, 1473169484.9770801067, -0, false),
                        new ThreadsCountChangeElement(7, 1473169484.9789481163, -0, false),
                        new ThreadsCountChangeElement(17, 1473169484.9796969891, -0, false),
                        new ThreadsCountChangeElement(29, 1473169484.9803349972, -0, false),
                        new ThreadsCountChangeElement(28, 1473169484.9811298847, -0, false),
                        new ThreadsCountChangeElement(13, 1473169484.9822549820, -0, false),
                        new ThreadsCountChangeElement(0, 1473169484.9828770161, -0, false),
                        new ThreadsCountChangeElement(24, 1473169486.9760899544, -20, false),
                        new ThreadsCountChangeElement(10, 1473169486.9765009880, +20, true),
                        new ThreadsCountChangeElement(24, 1473169486.9771978855, +0, true),
                        new ThreadsCountChangeElement(26, 1473169486.9775118828, -20, false),
                        new ThreadsCountChangeElement(9, 1473169486.9778649807, +20, true),
                        new ThreadsCountChangeElement(31, 1473169486.9783570766, -20, false),
                        new ThreadsCountChangeElement(3, 1473169486.9788210392, +20, true),
                        new ThreadsCountChangeElement(31, 1473169486.9792668819, +0, true),
                        new ThreadsCountChangeElement(4, 1473169486.9795839787, -20, false),
                        new ThreadsCountChangeElement(22, 1473169486.9799790382, +20, true),
                        new ThreadsCountChangeElement(20, 1473169486.9804790020, -20, false),
                        new ThreadsCountChangeElement(29, 1473169486.9806840420, +20, true),
                        new ThreadsCountChangeElement(1, 1473169486.9809141159, -20, false),
                        new ThreadsCountChangeElement(13, 1473169486.9811038971, +20, true),
                        new ThreadsCountChangeElement(1, 1473169486.9813261032, +0, true),
                        new ThreadsCountChangeElement(19, 1473169486.9815070629, -20, false),
                        new ThreadsCountChangeElement(6, 1473169486.9817140102, +20, true),
                        new ThreadsCountChangeElement(19, 1473169486.9819419384, +0, true),
                        new ThreadsCountChangeElement(8, 1473169486.9821228981, -20, false),
                        new ThreadsCountChangeElement(7, 1473169486.9823250771, +20, true),
                        new ThreadsCountChangeElement(5, 1473169486.9825530052, -20, false),
                        new ThreadsCountChangeElement(25, 1473169486.9827649593, +20, true),
                        new ThreadsCountChangeElement(5, 1473169486.9829800129, +0, true),
                        new ThreadsCountChangeElement(23, 1473169486.9831590652, -20, false),
                        new ThreadsCountChangeElement(14, 1473169486.9834280014, +20, true),
                        new ThreadsCountChangeElement(24, 1473169487.0775361061, -0, false),
                        new ThreadsCountChangeElement(31, 1473169487.0796120167, -0, false),
                        new ThreadsCountChangeElement(1, 1473169487.0815279484, -0, false),
                        new ThreadsCountChangeElement(19, 1473169487.0821459293, -0, false),
                        new ThreadsCountChangeElement(5, 1473169487.0831758976, -0, false),
                        new ThreadsCountChangeElement(10, 1473169489.0773880482, -20, false),
                        new ThreadsCountChangeElement(19, 1473169489.0778040886, +20, true),
                        new ThreadsCountChangeElement(10, 1473169489.0781381130, +0, true),
                        new ThreadsCountChangeElement(9, 1473169489.0785679817, -20, false),
                        new ThreadsCountChangeElement(12, 1473169489.0787799358, +20, true),
                        new ThreadsCountChangeElement(3, 1473169489.0795149803, -20, false),
                        new ThreadsCountChangeElement(24, 1473169489.0797340870, +20, true),
                        new ThreadsCountChangeElement(22, 1473169489.0807549953, -20, false),
                        new ThreadsCountChangeElement(1, 1473169489.0809779167, +20, true),
                        new ThreadsCountChangeElement(22, 1473169489.0811951160, +0, true),
                        new ThreadsCountChangeElement(29, 1473169489.0813798904, -20, false),
                        new ThreadsCountChangeElement(33, 1473169489.0815939903, +20, true),
                        new ThreadsCountChangeElement(13, 1473169489.0818209648, -20, false),
                        new ThreadsCountChangeElement(28, 1473169489.0820209980, +20, true),
                        new ThreadsCountChangeElement(6, 1473169489.0822510719, -20, false),
                        new ThreadsCountChangeElement(30, 1473169489.0824699402, +20, true),
                        new ThreadsCountChangeElement(7, 1473169489.0828680992, -20, false),
                        new ThreadsCountChangeElement(18, 1473169489.0830750465, +20, true),
                        new ThreadsCountChangeElement(25, 1473169489.0832960606, -20, false),
                        new ThreadsCountChangeElement(5, 1473169489.0834929943, +20, true),
                        new ThreadsCountChangeElement(14, 1473169489.0839569569, -20, false),
                        new ThreadsCountChangeElement(15, 1473169489.0841660500, +20, true),
                        new ThreadsCountChangeElement(14, 1473169489.0843749046, +0, true),
                        new ThreadsCountChangeElement(10, 1473169489.1784360409, -0, false),
                        new ThreadsCountChangeElement(22, 1473169489.1814029217, -0, false),
                        new ThreadsCountChangeElement(14, 1473169489.1845719814, -0, false),
                        new ThreadsCountChangeElement(19, 1473169491.1783430576, -20, false),
                        new ThreadsCountChangeElement(0, 1473169491.1787400246, +20, true),
                        new ThreadsCountChangeElement(19, 1473169491.1790690422, +0, true),
                        new ThreadsCountChangeElement(12, 1473169491.1794619560, -20, false),
                        new ThreadsCountChangeElement(10, 1473169491.1798439026, +20, true),
                        new ThreadsCountChangeElement(24, 1473169491.1802630424, -20, false),
                        new ThreadsCountChangeElement(22, 1473169491.1806790829, +20, true),
                        new ThreadsCountChangeElement(1, 1473169491.1814548969, -20, false),
                        new ThreadsCountChangeElement(31, 1473169491.1818120480, +20, true),
                        new ThreadsCountChangeElement(33, 1473169491.1821210384, -20, false),
                        new ThreadsCountChangeElement(14, 1473169491.1823139191, +20, true),
                        new ThreadsCountChangeElement(28, 1473169491.1825089455, -20, false),
                        new ThreadsCountChangeElement(17, 1473169491.1827099323, +20, true),
                        new ThreadsCountChangeElement(30, 1473169491.1831309795, -20, false),
                        new ThreadsCountChangeElement(30, 1473169491.1833350658, +0, true),
                        new ThreadsCountChangeElement(18, 1473169491.1835870743, -20, false),
                        new ThreadsCountChangeElement(5, 1473169491.1840929985, -20, false),
                        new ThreadsCountChangeElement(15, 1473169491.1847178936, -20, false),
                        new ThreadsCountChangeElement(15, 1473169491.1849100590, +0, true),
                        new ThreadsCountChangeElement(19, 1473169491.2794859409, -0, false),
                        new ThreadsCountChangeElement(19, 1473169491.2798249722, +20, true),
                        new ThreadsCountChangeElement(30, 1473169491.2836010456, -0, false),
                        new ThreadsCountChangeElement(30, 1473169491.2840468884, +20, true),
                        new ThreadsCountChangeElement(15, 1473169491.2850670815, -0, false),
                        new ThreadsCountChangeElement(15, 1473169491.2853920460, +20, true),
                        new ThreadsCountChangeElement(0, 1473169493.2793340683, -20, false),
                        new ThreadsCountChangeElement(10, 1473169493.2806520462, -20, false),
                        new ThreadsCountChangeElement(10, 1473169493.2809839249, +0, true),
                        new ThreadsCountChangeElement(22, 1473169493.2813000679, -20, false),
                        new ThreadsCountChangeElement(31, 1473169493.2824089527, -20, false),
                        new ThreadsCountChangeElement(14, 1473169493.2828550339, -20, false),
                        new ThreadsCountChangeElement(17, 1473169493.2832570076, -20, false),
                        new ThreadsCountChangeElement(17, 1473169493.2834339142, +0, true),
                        new ThreadsCountChangeElement(19, 1473169493.3804490566, -20, false),
                        new ThreadsCountChangeElement(10, 1473169493.3813209534, -0, false),
                        new ThreadsCountChangeElement(10, 1473169493.3815948963, +20, true),
                        new ThreadsCountChangeElement(17, 1473169493.3835740089, -0, false),
                        new ThreadsCountChangeElement(17, 1473169493.3837289810, +20, true),
                        new ThreadsCountChangeElement(30, 1473169493.3845930099, -20, false),
                        new ThreadsCountChangeElement(30, 1473169493.3847711086, +0, true),
                        new ThreadsCountChangeElement(15, 1473169493.3858819008, -20, false),
                        new ThreadsCountChangeElement(30, 1473169493.4849131107, -0, false),
                        new ThreadsCountChangeElement(30, 1473169493.4851689339, +20, true),
                        new ThreadsCountChangeElement(10, 1473169495.4820890427, -20, false),
                        new ThreadsCountChangeElement(17, 1473169495.4841420650, -20, false),
                        new ThreadsCountChangeElement(30, 1473169495.5856420994, -20, false)
                    };

                    break;

                case PlanOMPTest.Test244thGround:

                    // 244 threads.
                    // Test example is based on part of ground grid.
                    master_threads = 20;
                    max_threads = 244;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(16, 1473923495.1552760601, +19, true),
                        new ThreadsCountChangeElement(18, 1473923495.1554229259, +19, true),
                        new ThreadsCountChangeElement(0, 1473923495.1555340290, +111, true),
                        new ThreadsCountChangeElement(17, 1473923495.1556079388, +19, true),
                        new ThreadsCountChangeElement(19, 1473923495.1557331085, +15, true),
                        new ThreadsCountChangeElement(8, 1473923495.1558570862, +31, true),
                        new ThreadsCountChangeElement(19, 1473923498.0452909470, -15, false),
                        new ThreadsCountChangeElement(13, 1473923498.0458240509, +23, true),
                        new ThreadsCountChangeElement(19, 1473923498.0464479923, +0, true),
                        new ThreadsCountChangeElement(19, 1473923498.1467869282, -0, false),
                        new ThreadsCountChangeElement(18, 1473923498.8305180073, -19, false),
                        new ThreadsCountChangeElement(15, 1473923498.8310720921, +19, true),
                        new ThreadsCountChangeElement(18, 1473923498.8317019939, +0, true),
                        new ThreadsCountChangeElement(18, 1473923498.9320869446, -0, false),
                        new ThreadsCountChangeElement(17, 1473923499.0531439781, -19, false),
                        new ThreadsCountChangeElement(19, 1473923499.0536890030, +15, true),
                        new ThreadsCountChangeElement(17, 1473923499.0540499687, +0, true),
                        new ThreadsCountChangeElement(17, 1473923499.1544139385, -0, false),
                        new ThreadsCountChangeElement(16, 1473923499.6446518898, -19, false),
                        new ThreadsCountChangeElement(12, 1473923499.6451790333, +23, true),
                        new ThreadsCountChangeElement(16, 1473923499.6458239555, +0, true),
                        new ThreadsCountChangeElement(16, 1473923499.7461559772, -0, false),
                        new ThreadsCountChangeElement(19, 1473923501.9415550232, -15, false),
                        new ThreadsCountChangeElement(19, 1473923501.9419450760, +0, true),
                        new ThreadsCountChangeElement(19, 1473923502.0422000885, -0, false),
                        new ThreadsCountChangeElement(19, 1473923502.0425240993, +15, true),
                        new ThreadsCountChangeElement(8, 1473923502.0664880276, -31, false),
                        new ThreadsCountChangeElement(17, 1473923502.0670371056, +19, true),
                        new ThreadsCountChangeElement(8, 1473923502.0673849583, +0, true),
                        new ThreadsCountChangeElement(8, 1473923502.1677439213, -0, false),
                        new ThreadsCountChangeElement(13, 1473923503.0769879818, -23, false),
                        new ThreadsCountChangeElement(7, 1473923503.0773839951, +31, true),
                        new ThreadsCountChangeElement(13, 1473923503.0780160427, +0, true),
                        new ThreadsCountChangeElement(13, 1473923503.1783990860, -0, false),
                        new ThreadsCountChangeElement(15, 1473923503.4676620960, -19, false),
                        new ThreadsCountChangeElement(18, 1473923503.4680800438, +19, true),
                        new ThreadsCountChangeElement(15, 1473923503.4683809280, +0, true),
                        new ThreadsCountChangeElement(15, 1473923503.5687370300, -0, false),
                        new ThreadsCountChangeElement(19, 1473923504.9302890301, -15, false),
                        new ThreadsCountChangeElement(14, 1473923504.9306910038, +19, true),
                        new ThreadsCountChangeElement(12, 1473923505.1666650772, -23, false),
                        new ThreadsCountChangeElement(16, 1473923505.1670908928, +19, true),
                        new ThreadsCountChangeElement(12, 1473923505.1673860550, +0, true),
                        new ThreadsCountChangeElement(12, 1473923505.2677159309, -0, false),
                        new ThreadsCountChangeElement(17, 1473923505.9628949165, -19, false),
                        new ThreadsCountChangeElement(15, 1473923505.9632959366, +19, true),
                        new ThreadsCountChangeElement(17, 1473923505.9635879993, +0, true),
                        new ThreadsCountChangeElement(17, 1473923506.0639619827, -0, false),
                        new ThreadsCountChangeElement(18, 1473923507.1432819366, -19, false),
                        new ThreadsCountChangeElement(12, 1473923507.1436719894, +23, true),
                        new ThreadsCountChangeElement(18, 1473923507.1439650059, +0, true),
                        new ThreadsCountChangeElement(18, 1473923507.2443189621, -0, false),
                        new ThreadsCountChangeElement(16, 1473923509.6566019058, -19, false),
                        new ThreadsCountChangeElement(17, 1473923509.6569910049, +19, true),
                        new ThreadsCountChangeElement(16, 1473923509.6572759151, +0, true),
                        new ThreadsCountChangeElement(16, 1473923509.7575891018, -0, false),
                        new ThreadsCountChangeElement(14, 1473923509.8726809025, -19, false),
                        new ThreadsCountChangeElement(16, 1473923509.8731000423, +19, true),
                        new ThreadsCountChangeElement(14, 1473923509.8734400272, +0, true),
                        new ThreadsCountChangeElement(14, 1473923509.9738230705, -0, false),
                        new ThreadsCountChangeElement(7, 1473923510.4730899334, -31, false),
                        new ThreadsCountChangeElement(10, 1473923510.4735479355, +27, true),
                        new ThreadsCountChangeElement(7, 1473923510.4740450382, +0, true),
                        new ThreadsCountChangeElement(7, 1473923510.5743799210, -0, false),
                        new ThreadsCountChangeElement(15, 1473923510.5996611118, -19, false),
                        new ThreadsCountChangeElement(18, 1473923510.6000580788, +19, true),
                        new ThreadsCountChangeElement(15, 1473923510.6003949642, +0, true),
                        new ThreadsCountChangeElement(15, 1473923510.7007849216, -0, false),
                        new ThreadsCountChangeElement(12, 1473923512.6649301052, -23, false),
                        new ThreadsCountChangeElement(13, 1473923512.6653349400, +23, true),
                        new ThreadsCountChangeElement(12, 1473923512.6656320095, +0, true),
                        new ThreadsCountChangeElement(12, 1473923512.7660670280, -0, false),
                        new ThreadsCountChangeElement(17, 1473923513.5527958870, -19, false),
                        new ThreadsCountChangeElement(14, 1473923513.5531930923, +19, true),
                        new ThreadsCountChangeElement(18, 1473923514.2752940655, -19, false),
                        new ThreadsCountChangeElement(12, 1473923514.2756929398, +23, true),
                        new ThreadsCountChangeElement(16, 1473923514.3626730442, -19, false),
                        new ThreadsCountChangeElement(15, 1473923514.3630681038, +19, true),
                        new ThreadsCountChangeElement(10, 1473923516.5130720139, -27, false),
                        new ThreadsCountChangeElement(11, 1473923516.5134460926, +23, true),
                        new ThreadsCountChangeElement(10, 1473923516.5140759945, +0, true),
                        new ThreadsCountChangeElement(10, 1473923516.6144340038, -0, false),
                        new ThreadsCountChangeElement(13, 1473923517.6958699226, -23, false),
                        new ThreadsCountChangeElement(9, 1473923517.6962521076, +27, true),
                        new ThreadsCountChangeElement(13, 1473923517.6967120171, +0, true),
                        new ThreadsCountChangeElement(13, 1473923517.7970860004, -0, false),
                        new ThreadsCountChangeElement(14, 1473923518.4947240353, -19, false),
                        new ThreadsCountChangeElement(14, 1473923518.4951229095, +0, true),
                        new ThreadsCountChangeElement(14, 1473923518.5953769684, -0, false),
                        new ThreadsCountChangeElement(14, 1473923518.5957009792, +19, true),
                        new ThreadsCountChangeElement(15, 1473923518.9993729591, -19, false),
                        new ThreadsCountChangeElement(12, 1473923519.7968640327, -23, false),
                        new ThreadsCountChangeElement(3, 1473923519.7972869873, +43, true),
                        new ThreadsCountChangeElement(0, 1473923521.4296059608, -111, false),
                        new ThreadsCountChangeElement(7, 1473923521.4300649166, +31, true),
                        new ThreadsCountChangeElement(6, 1473923521.4303390980, +35, true),
                        new ThreadsCountChangeElement(13, 1473923521.4309051037, +23, true),
                        new ThreadsCountChangeElement(0, 1473923521.4313380718, +0, true),
                        new ThreadsCountChangeElement(0, 1473923521.5316839218, -0, false),
                        new ThreadsCountChangeElement(11, 1473923522.3742170334, -23, false),
                        new ThreadsCountChangeElement(8, 1473923522.3746199608, +31, true),
                        new ThreadsCountChangeElement(11, 1473923522.3749210835, +0, true),
                        new ThreadsCountChangeElement(11, 1473923522.4752669334, -0, false),
                        new ThreadsCountChangeElement(14, 1473923523.5371620655, -19, false),
                        new ThreadsCountChangeElement(10, 1473923523.5372729301, +27, true),
                        new ThreadsCountChangeElement(9, 1473923524.3248479366, -27, false),
                        new ThreadsCountChangeElement(11, 1473923524.3252859116, +23, true),
                        new ThreadsCountChangeElement(9, 1473923524.3255710602, +0, true),
                        new ThreadsCountChangeElement(9, 1473923524.4260199070, -0, false),
                        new ThreadsCountChangeElement(13, 1473923526.4616210461, -23, false),
                        new ThreadsCountChangeElement(9, 1473923526.4619829655, +27, true),
                        new ThreadsCountChangeElement(6, 1473923528.8176789284, -35, false),
                        new ThreadsCountChangeElement(5, 1473923528.8181350231, +39, true),
                        new ThreadsCountChangeElement(6, 1473923528.8211829662, +0, true),
                        new ThreadsCountChangeElement(7, 1473923528.8251450062, -31, false),
                        new ThreadsCountChangeElement(7, 1473923528.8254930973, +0, true),
                        new ThreadsCountChangeElement(6, 1473923528.9217031002, -0, false),
                        new ThreadsCountChangeElement(7, 1473923528.9257659912, -0, false),
                        new ThreadsCountChangeElement(7, 1473923528.9260680676, +31, true),
                        new ThreadsCountChangeElement(8, 1473923529.2835600376, -31, false),
                        new ThreadsCountChangeElement(8, 1473923529.2839589119, +0, true),
                        new ThreadsCountChangeElement(8, 1473923529.3842370510, -0, false),
                        new ThreadsCountChangeElement(8, 1473923529.3845500946, +31, true),
                        new ThreadsCountChangeElement(10, 1473923529.5766499043, -27, false),
                        new ThreadsCountChangeElement(10, 1473923529.5770490170, +0, true),
                        new ThreadsCountChangeElement(10, 1473923529.6773660183, -0, false),
                        new ThreadsCountChangeElement(10, 1473923529.6776928902, +27, true),
                        new ThreadsCountChangeElement(11, 1473923530.1857070923, -23, false),
                        new ThreadsCountChangeElement(11, 1473923530.1861059666, +0, true),
                        new ThreadsCountChangeElement(11, 1473923530.2863581181, -0, false),
                        new ThreadsCountChangeElement(11, 1473923530.2866508961, +23, true),
                        new ThreadsCountChangeElement(3, 1473923530.7104589939, -43, false),
                        new ThreadsCountChangeElement(4, 1473923530.7109200954, +39, true),
                        new ThreadsCountChangeElement(3, 1473923530.7114400864, +0, true),
                        new ThreadsCountChangeElement(3, 1473923530.8117809296, -0, false),
                        new ThreadsCountChangeElement(9, 1473923533.0904641151, -27, false),
                        new ThreadsCountChangeElement(9, 1473923533.0908629894, +0, true),
                        new ThreadsCountChangeElement(9, 1473923533.1911060810, -0, false),
                        new ThreadsCountChangeElement(9, 1473923533.1913878918, +27, true),
                        new ThreadsCountChangeElement(10, 1473923535.7169699669, -27, false),
                        new ThreadsCountChangeElement(11, 1473923536.1470379829, -23, false),
                        new ThreadsCountChangeElement(2, 1473923536.1474530697, +51, true),
                        new ThreadsCountChangeElement(8, 1473923536.2932260036, -31, false),
                        new ThreadsCountChangeElement(6, 1473923536.2936110497, +35, true),
                        new ThreadsCountChangeElement(7, 1473923536.3210361004, -31, false),
                        new ThreadsCountChangeElement(5, 1473923537.3056030273, -39, false),
                        new ThreadsCountChangeElement(1, 1473923537.3057549000, +67, true),
                        new ThreadsCountChangeElement(5, 1473923537.3058700562, +0, true),
                        new ThreadsCountChangeElement(5, 1473923537.4064290524, -0, false),
                        new ThreadsCountChangeElement(9, 1473923539.8194570541, -27, false),
                        new ThreadsCountChangeElement(4, 1473923540.0228970051, -39, false),
                        new ThreadsCountChangeElement(3, 1473923540.0232930183, +43, true),
                        new ThreadsCountChangeElement(4, 1473923540.0235989094, +0, true),
                        new ThreadsCountChangeElement(4, 1473923540.1240179539, -0, false),
                        new ThreadsCountChangeElement(6, 1473923543.6807889938, -35, false),
                        new ThreadsCountChangeElement(5, 1473923543.6812000275, +39, true),
                        new ThreadsCountChangeElement(6, 1473923543.6814839840, +0, true),
                        new ThreadsCountChangeElement(6, 1473923543.7819550037, -0, false),
                        new ThreadsCountChangeElement(2, 1473923549.0783190727, -51, false),
                        new ThreadsCountChangeElement(6, 1473923549.0787971020, +35, true),
                        new ThreadsCountChangeElement(4, 1473923549.0790879726, +39, true),
                        new ThreadsCountChangeElement(2, 1473923549.0794510841, +0, true),
                        new ThreadsCountChangeElement(2, 1473923549.1798770428, -0, false),
                        new ThreadsCountChangeElement(3, 1473923550.9335110188, -43, false),
                        new ThreadsCountChangeElement(3, 1473923550.9338669777, +0, true),
                        new ThreadsCountChangeElement(3, 1473923551.0341091156, -0, false),
                        new ThreadsCountChangeElement(3, 1473923551.0343680382, +43, true),
                        new ThreadsCountChangeElement(5, 1473923552.1658799648, -39, false),
                        new ThreadsCountChangeElement(5, 1473923552.1662549973, +0, true),
                        new ThreadsCountChangeElement(5, 1473923552.2665030956, -0, false),
                        new ThreadsCountChangeElement(5, 1473923552.2667889595, +39, true),
                        new ThreadsCountChangeElement(1, 1473923553.1705520153, -67, false),
                        new ThreadsCountChangeElement(2, 1473923553.1709868908, +51, true),
                        new ThreadsCountChangeElement(1, 1473923553.1712570190, +0, true),
                        new ThreadsCountChangeElement(1, 1473923553.2718389034, -0, false),
                        new ThreadsCountChangeElement(6, 1473923556.4656920433, -35, false),
                        new ThreadsCountChangeElement(4, 1473923558.3910679817, -39, false),
                        new ThreadsCountChangeElement(1, 1473923558.3915030956, +67, true),
                        new ThreadsCountChangeElement(4, 1473923558.3916029930, +0, true),
                        new ThreadsCountChangeElement(4, 1473923558.4922049046, -0, false),
                        new ThreadsCountChangeElement(5, 1473923560.7510709763, -39, false),
                        new ThreadsCountChangeElement(4, 1473923560.7514679432, +39, true),
                        new ThreadsCountChangeElement(3, 1473923561.9444110394, -43, false),
                        new ThreadsCountChangeElement(2, 1473923566.1014180183, -51, false),
                        new ThreadsCountChangeElement(0, 1473923566.1015310287, +111, true),
                        new ThreadsCountChangeElement(2, 1473923566.1016221046, +0, true),
                        new ThreadsCountChangeElement(2, 1473923566.2026369572, -0, false),
                        new ThreadsCountChangeElement(4, 1473923570.0630691051, -39, false),
                        new ThreadsCountChangeElement(1, 1473923574.2561800480, -67, false),
                        new ThreadsCountChangeElement(2, 1473923574.2566130161, +51, true),
                        new ThreadsCountChangeElement(1, 1473923574.2568929195, +0, true),
                        new ThreadsCountChangeElement(1, 1473923574.3573870659, -0, false),
                        new ThreadsCountChangeElement(2, 1473923587.1868910789, -51, false),
                        new ThreadsCountChangeElement(1, 1473923587.1873130798, +67, true),
                        new ThreadsCountChangeElement(0, 1473923592.3749649525, -111, false),
                        new ThreadsCountChangeElement(0, 1473923592.3754138947, +0, true),
                        new ThreadsCountChangeElement(0, 1473923592.4756500721, -0, false),
                        new ThreadsCountChangeElement(0, 1473923592.4759109020, +111, true),
                        new ThreadsCountChangeElement(1, 1473923603.0518081188, -67, false),
                        new ThreadsCountChangeElement(0, 1473923618.7495040894, -111, false)
                    };

                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Draw(cnv, master_threads, max_threads, els);
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cnv">canvas</param>
        /// <param name="master_threads">master threads count</param>
        /// <param name="max_threads">max threads count</param>
        /// <param name="els">threads count change elements array</param>
        public static void Draw(Canvas cnv, int master_threads, int max_threads, ThreadsCountChangeElement[] els)
        {
            double beg_time = els.First<ThreadsCountChangeElement>().WTime;
            double end_time = els.Last<ThreadsCountChangeElement>().WTime;

            RectDrawer d = new RectDrawer(new Rect2D(new Interval(beg_time, end_time), new Interval(max_threads)),
                                          cnv, false, true);

            d.BeginDraw();

            int[] ths = new int[master_threads];
            bool[] is_draw = new bool[master_threads];

            for (int i = 0; i < ths.Count<int>(); i++)
            {
                ths[i] = 1;
            }

            for (int i = 0; i < is_draw.Count<bool>(); i++)
            {
                is_draw[i] = false;
            }

            d.SetPenColor(new Lib.Draw.Color(Colors.DarkGray));

            for (int i = 0; i < els.Count<ThreadsCountChangeElement>(); i++)
            {
                ThreadsCountChangeElement e = els[i];

                if (i == 0)
                {
                    ths[e.ThreadNum] += e.ChangeCount;
                    is_draw[e.ThreadNum] = e.IsAlloc;
                }
                else if (i > 0)
                {
                    ThreadsCountChangeElement pe = els[i - 1];

                    // Draw rectangles.
                    double low = 0.0;
                    for (int j = 0; j < ths.Count<int>(); j++)
                    {
                        Color c;

                        switch (j)
                        {
                            case 0:
                                c = Colors.Blue;
                                break;

                            case 1:
                                c = Colors.Green;
                                break;

                            case 2:
                                c = Colors.Red;
                                break;

                            case 3:
                                c = Colors.Yellow;
                                break;

                            case 4:
                                c = Colors.SteelBlue;
                                break;

                            case 5:
                                c = Colors.Coral;
                                break;

                            case 6:
                                c = Colors.Khaki;
                                break;

                            case 7:
                                c = Colors.Orange;
                                break;
                                
                            case 8:
                                c = Colors.DarkSalmon;
                                break;

                            case 9:
                                c = Colors.Brown;
                                break;

                            case 10:
                                c = Colors.BurlyWood;
                                break;

                            case 11:
                                c = Colors.CadetBlue;
                                break;

                            case 12:
                                c = Colors.Chocolate;
                                break;

                            case 13:
                                c = Colors.Crimson;
                                break;

                            case 14:
                                c = Colors.Cyan;
                                break;

                            case 15:
                                c = Colors.DarkGoldenrod;
                                break;
                                
                            case 16:
                                c = Colors.DarkGreen;
                                break;

                            case 17:
                                c = Colors.DarkMagenta;
                                break;

                            case 18:
                                c = Colors.DarkOrchid;
                                break;

                            case 19:
                                c = Colors.ForestGreen;
                                break;

                            case 20:
                                c = Colors.DimGray;
                                break;

                            case 21:
                                c = Colors.DodgerBlue;
                                break;

                            case 22:
                                c = Colors.DarkRed;
                                break;

                            case 23:
                                c = Colors.Firebrick;
                                break;

                            case 24:
                                c = Colors.MediumAquamarine;
                                break;

                            case 25:
                                c = Colors.BlueViolet;
                                break;

                            case 26:
                                c = Colors.RosyBrown;
                                break;

                            case 27:
                                c = Colors.Maroon;
                                break;

                            case 28:
                                c = Colors.Sienna;
                                break;

                            case 29:
                                c = Colors.Olive;
                                break;

                            case 30:
                                c = Colors.Tomato;
                                break;

                            case 31:
                                c = Colors.DarkTurquoise;
                                break;

                            case 32:
                                c = Colors.Peru;                              
                                break;

                            case 33:
                                c = Colors.Plum;
                                break;

                            default:
                                c = Colors.Black;
                                break;
                        }

                        if (is_draw[j])
                        {
                            d.SetBrush(new Lib.Draw.Color(c));
                        }
                        else
                        {
                            d.SetBrush(new Lib.Draw.Color(Colors.LightGray));
                        }

                        d.FillRect(new Rect2D(new Interval(pe.WTime, e.WTime),
                                              new Interval(low, low + ths[j])));

                        if (low > 0.0)
                        {
                            d.DrawLine(new Point2D(pe.WTime, low), new Point2D(e.WTime, low));
                        }

                        low += ths[j];
                    }

                    ths[e.ThreadNum] += e.ChangeCount;
                    is_draw[e.ThreadNum] = e.IsAlloc;
                }
            }

            for (int i = 1; i < els.Count<ThreadsCountChangeElement>() - 1; i++)
            {
                ThreadsCountChangeElement e = els[i];
                d.DrawLine(new Point2D(e.WTime, 0.0), new Point2D(e.WTime, max_threads));
            }

            d.EndDraw();
        }
    }
}
