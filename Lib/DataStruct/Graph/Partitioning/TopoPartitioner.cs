using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.Maths;

namespace Lib.DataStruct.Graph.Partitioning
{
    /// <summary>
    /// Partitioner of graph usinf cluster topology.
    /// </summary>
    public class TopoPartitioner
    {
        /// <summary>
        /// Task graph.
        /// </summary>
        public Graph G;

        /// <summary>
        /// Cluster graph.
        /// </summary>
        public Graph H;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        public TopoPartitioner(Graph g, Graph h)
        {
            G = g;
            H = h;
        }
        
        /// <summary>
        /// Get function value.
        /// </summary>
        /// <param name="v">distribution vector</param>
        /// <returns>function value</returns>
        public double F(NVector v)
        {
            for (int i = 0; i < v.Size; i++)
            {
                G.Nodes[i].Partition = (int)(Math.Round(v.E[i]));
            }

            return PartitioningStatistics.TopoQualityValue(G, H);
        }

        /// <summary>
        /// Partial derivate of <c>F</c> function in <c>v</c> point of <c>i</c>-th variable.
        /// </summary>
        /// <param name="v">point</param>
        /// <param name="i">number of variable</param>
        /// <returns>derivate value</returns>
        public double FPartialDer(NVector v, int i)
        {
            if (v[i] == 0)
            {
                NVector vp = v.Copy;
                vp[i]++;

                double f_vp = F(vp);
                double f_v = F(v);

                return f_vp - f_v;
            }
            else if (v[i] == H.Order - 1)
            {
                NVector vm = v.Copy;
                vm[i]--;

                double f_v = F(v);
                double f_vm = F(vm);

                return f_v - f_vm;
            }
            else
            {
                NVector vp = v.Copy;
                NVector vm = v.Copy;
                vp[i]++;
                vm[i]--;

                double f_vp = F(vp);
                double f_vm = F(vm);

                return 0.5 * (f_vp - f_vm);
            }
        }

        /// <summary>
        /// Gradient of <c>F</c> function in <c>v</c> point.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public NVector FGrad(NVector v)
        {
            NVector grad = new NVector(v.Size);

            for (int i = 0; i < grad.Size; i++)
            {
                grad[i] = FPartialDer(v, i);
            }

            return grad;
        }

        /// <summary>
        /// Partition graph <c>g</c> using cluster graph <c>h</c>
        /// with Gradient Descent Method.
        /// </summary>
        /// <param name="g">task graph</param>
        /// <param name="h">cluster graph</param>
        public void PartitionGradientDescentMethod()
        {
            int iter = 0;
            NVector v = new NVector(G.Order);

            Console.WriteLine("distr : {0}", v);
            Console.WriteLine(" iter : N = {0}, F = {1}", iter, F(v));

            for (int i = 0; i < 10; i++)
            {
                NVector grad = FGrad(v);
                Console.WriteLine("grad : {0}", grad);
                int ind_max_mod_el = grad.IndexOfMaxModElement;
                double max_mod_el = Math.Abs(grad[ind_max_mod_el]);

                // Zero all elements of gradient but ind_max_mod_el-th.
                for (int j = 0; j < v.Size; j++)
                {
                    if (j != ind_max_mod_el)
                    {
                        grad[j] = 0.0;
                    }
                }

                if (max_mod_el == 0.0)
                {
                    Console.WriteLine("break 1");
                    break;
                }

                double alpha = 1.0 / max_mod_el;
                grad = grad * alpha;

                NVector new_v = v - grad;
                double new_v_min = new_v.E.Min();
                double new_v_max = new_v.E.Max();

                if ((new_v_min < 0) || (new_v_max >= H.Order))
                {
                    Console.WriteLine("break 2");
                    break;
                }

                v = new_v.Round;

                iter++;
                Console.WriteLine("distr : {0}", v);
                Console.WriteLine(" iter : N = {0}, F = {1}", iter, F(v));
            }
        }
    }
}
