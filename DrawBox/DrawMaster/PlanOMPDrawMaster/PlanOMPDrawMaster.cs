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
        /// <param name="threads">threads in exmaple</param>
        public static void Draw(Canvas cnv, int threads_in_example)
        {
            int master_threads = 0;
            int max_threads = 0;
            ThreadsCountChangeElement[] els = null;

            switch (threads_in_example)
            {
                case 4:
                    
                    // 4 threads.
                    // 3 procesures with parallel part 2.
                    master_threads = 3;
                    max_threads = 4;
                    els = new ThreadsCountChangeElement[]
                    {
                        new ThreadsCountChangeElement(2, 388.114, +1, true),
                        new ThreadsCountChangeElement(2, 388.364, -1, false),
                        new ThreadsCountChangeElement(1, 388.365, +1, true),
                        new ThreadsCountChangeElement(2, 388.365, +0, true),
                        new ThreadsCountChangeElement(2, 388.465, -0, false),
                        new ThreadsCountChangeElement(1, 388.566, -1, false),
                        new ThreadsCountChangeElement(0, 388.567, +1, true),
                        new ThreadsCountChangeElement(1, 388.568, +0, true),
                        new ThreadsCountChangeElement(1, 388.669, -0, false),
                        new ThreadsCountChangeElement(0, 388.768, -1, false),
                        new ThreadsCountChangeElement(1, 388.768, +1, true),
                        new ThreadsCountChangeElement(0, 388.769, +0, true),
                        new ThreadsCountChangeElement(0, 388.869, -0, false),
                        new ThreadsCountChangeElement(1, 388.969, -1, false),
                        new ThreadsCountChangeElement(2, 388.97, +1, true),
                        new ThreadsCountChangeElement(1, 388.97, +0, true),
                        new ThreadsCountChangeElement(1, 389.07, -0, false),
                        new ThreadsCountChangeElement(2, 389.22, -1, false),
                        new ThreadsCountChangeElement(0, 389.22, +1, true),
                        new ThreadsCountChangeElement(2, 389.221, +0, true),
                        new ThreadsCountChangeElement(2, 389.321, -0, false),
                        new ThreadsCountChangeElement(0, 389.421, -1, false),
                        new ThreadsCountChangeElement(1, 389.422, +1, true),
                        new ThreadsCountChangeElement(0, 389.423, +0, true),
                        new ThreadsCountChangeElement(0, 389.524, -0, false),
                        new ThreadsCountChangeElement(1, 389.623, -1, false),
                        new ThreadsCountChangeElement(2, 389.624, +1, true),
                        new ThreadsCountChangeElement(2, 389.874, -1, false),
                        new ThreadsCountChangeElement(0, 389.875, +1, true),
                        new ThreadsCountChangeElement(0, 390.075, -1, false)
                    };

                    break;

                case 8:

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

                case 16:

                    // 16 threads.
                    // 8 procedures with parallel part 5.
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
