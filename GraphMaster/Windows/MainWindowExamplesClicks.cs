// Author: Alexey Rybakov

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Threading;

using Lib.Maths.Numbers;
using Lib.Draw;
using Lib.DataStruct.Graph;
using Lib.DataStruct.Graph.DrawProperties;
using Lib.DataStruct.Graph.Load;
using Lib.DataStruct.Graph.Partitioning;
using Lib.Maths.Geometry.Geometry2D;
using Lib.Maths.Geometry.Geometry3D;
using SWPoint = System.Windows.Point;
using SWRect = System.Windows.Rect;
using Rect2D = Lib.Maths.Geometry.Geometry2D.Rect;
using LVector = Lib.Maths.Geometry.Vector;
using LPoint = Lib.Maths.Geometry.Point;
using RectDrawerWPF = Lib.Draw.WPF.RectDrawer;
using GraphMaster.Tools;
using Lib.GUI.WPF;

namespace GraphMaster.Windows
{
    public partial class MainWindow : Window
    {
        //------------------------------------------------------------------------------------------
        // Common graphs.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Empty graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleEmpty_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.EmptyGraph(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Full graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleFull_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.FullGraph(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Random graph in Erdos - Renyi model (binomial).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleErdosRenyiBinomialRandom_Click(object sender, RoutedEventArgs e)
        {
            EditIntDoubleWindow w = new EditIntDoubleWindow(RandomOrder, 0.5,
                                                            "Erdos - Renyi binomial random graph parameters",
                                                            "Order", "Edge probability");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.ErdosRenyiBinomialRandomGraph(w.IntV, w.DoubleV, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Random graph in Erdos - Renyi model (uniform).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleErdosRenyiUniformRandom_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Erdos - Renyi uniform random graph parameters",
                                                      "Order", "Edges count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.ErdosRenyiUniformRandomGraph(w.Int1V, w.Int2V, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Cycle.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCycle_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Cycle(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Star.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleStar_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Star(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Wheel.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleWheel_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Wheel(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Dutch windmill.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleDutchWindmill_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(5, "Enter blades count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.DutchWindmill(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// French windmill.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleFrenchWindmill_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(5, "Enter blades count");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.FrenchWindmill(w.Result, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Circular graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCircular_Click(object sender, RoutedEventArgs e)
        {
            OrderAndIntsWindow w = new OrderAndIntsWindow(RandomOrder, new int[] { 1, 2 }, "Order and chords", "Chords");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.CircularGraph(w.Order, w.Ints, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Hatch graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleHatch_Click(object sender, RoutedEventArgs e)
        {
            OrderAndIntsWindow w = new OrderAndIntsWindow(RandomOrder, new int[] { 1, 2 }, "Order and doubled middles", "Doubled middles");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.HatchGraph(w.Order, w.Ints, Circle);
            }

            Paint();
        }

        //------------------------------------------------------------------------------------------
        // Grids.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// 1D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid1_Click(object sender, RoutedEventArgs e)
        {
            EditIntWindow w = new EditIntWindow(RandomOrder, "Enter order");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid1D(w.Result, Rect);
            }

            Paint();
        }

        /// <summary>
        /// 2D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid2_Click(object sender, RoutedEventArgs e)
        {
            Grid2DSizesWindow w = new Grid2DSizesWindow(RandomOrder, RandomOrder);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid2D(w.XSize, w.YSize, Rect);
            }

            Paint();
        }

        /// <summary>
        /// 3D grid.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleGrid3_Click(object sender, RoutedEventArgs e)
        {
            Grid3DSizesWindow w = new Grid3DSizesWindow(RandomOrder, RandomOrder, RandomOrder);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.Grid3D(w.XSize, w.YSize, w.ZSize,
                                            Rect.Extended(Rect.YInterval));
            }

            Paint();
        }

        /// <summary>
        /// Grid in circle with center point.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleGridCircleWithCenter_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Grid in circle with center point parameters",
                                                      "Count of radiuses", "Points on radius");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GridCicle(w.Int1V, w.Int2V, true, Circle);
            }

            Paint();
        }

        /// <summary>
        /// Grid in circle without center point.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void ExampleGridCircleWithoutCenter_Click(object sender, RoutedEventArgs e)
        {
            int n = RandomOrder;

            EditIntIntWindow w = new EditIntIntWindow(n, n * (n - 1) / 4,
                                                      "Grid in circle with center point parameters",
                                                      "Count of radiuses", "Points on radius");
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GridCicle(w.Int1V, w.Int2V, false, Circle);
            }

            Paint();
        }

        //------------------------------------------------------------------------------------------
        // 3D Shapes.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Tetrahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DTetrahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tetrahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Cube.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DCube_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Cube(Sphere);
            Paint();
        }

        /// <summary>
        /// Octahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DOctahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Octahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Dodecahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DDodecahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Dodecahedron(Sphere);
            Paint();
        }

        /// <summary>
        /// Icosahedron.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void Example3DIcosahedron_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Icosahedron(Sphere);
            Paint();
        }

        //------------------------------------------------------------------------------------------
        // Cages.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Example. (3-3)-cage (K(4)).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FullGraph(4, Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-4)-cage (K(3,3)).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_4_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Cage_3_4(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-5)-cage (Petersen graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_5_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PerersenGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-6)-cage (Heawood graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_6_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-7)-cage (McGee graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_7_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.McGeeGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Example. (3-8)-cage (Tutte-Coxeter graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_8_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_1_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_10_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10)-cage (Harries graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10)-cage (Harries-Wong graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_10_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesWongGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-11) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_11_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_11_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// (3-12) Tutte graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleCage_3_12_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tutte_3_12_Cage(Circle);
            Paint();
        }

        //------------------------------------------------------------------------------------------
        // Levi's graphs.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Desargues graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Desargues_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Heawood graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Heawood_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Mobius-Kantor graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_MobiusKantor_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Pappus graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Pappus_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PappusGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Gray graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_Gray_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GrayGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Tutte-Coxeter graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLevi_TutteCoxeter_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        //------------------------------------------------------------------------------------------
        // Petersen graphs.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 1)</c> (10-prism).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_1_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Prism(10, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(5, 2)</c> (Petersen graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_5_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PerersenGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(6, 2)</c> (Durer graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_6_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DurerGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(8, 3)</c> (Mobius-Kantor graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_8_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 2)</c> (dodecahedron).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_2_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GeneralizedPetersenGraph(10, 2, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(10, 3)</c> (Desargues graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_10_3_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Generalized Petersen graph <c>GP(12, 5)</c> (Nauru graph).
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_12_5_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.NauruGraph(Notation.GeneralizedPetersen, Circle);
            Paint();
        }

        /// <summary>
        /// Random generalized Petersen graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExamplePetersen_n_k_Click(object sender, RoutedEventArgs e)
        {
            int n = Randoms.RandomInInterval(Math.Max(2 + 1, MinOrder), MaxOrder - 1);
            int k = Randoms.RandomInInterval(1, n / 2);
            PetersenGraphParametersWindow w = new PetersenGraphParametersWindow(n, k);
            w.ShowDialog();

            if (w.IsAccepted)
            {
                Graph = GraphCreator.GeneralizedPetersenGraph(w.HalfOrder, w.InnerChord, Circle);
            }

            Paint();
        }

        //------------------------------------------------------------------------------------------
        // LCF notation graphs.
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Wagner graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Wagner_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.WagnerGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Franklin graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Franklin_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FranklinGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Frucht graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Frucht_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FruchtGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Heawood graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Heawood_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HeawoodGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Mobius-Kantor graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_MobiusKantor_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MobiusKantorGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Pappus graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Pappus_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.PappusGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Desargues graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Desargues_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DesarguesGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// McGee graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_McGee_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.McGeeGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Nauru graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Nauru_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.NauruGraph(Notation.LederbergCoxeterFrucht, Circle);
            Paint();
        }

        /// <summary>
        /// Tutte-Coxeter graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_TutteCoxeter_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.TutteCoxeterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Dyke graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Dyck_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.DyckGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Gray graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Gray_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.GrayGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Harries graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Harries_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Harries-Wong graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_HarriesWong_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.HarriesWongGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-10) Balaban cage.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_10_Balaban_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_10_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// Foster graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Foster_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.FosterGraph(Circle);
            Paint();
        }

        /// <summary>
        /// Biggs-Smith graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_BiggsSmith_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.BiggsSmithGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-11) Balaban graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_11_Balaban_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Balaban_3_11_Cage(Circle);
            Paint();
        }

        /// <summary>
        /// Ljubljana graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_Ljubljana_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.LjubljanaGraph(Circle);
            Paint();
        }

        /// <summary>
        /// (3-12) Tutte graph.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void ExampleLCF_3_12_Tutte_Cage_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.Tutte_3_12_Cage(Circle);
            Paint();
        }

        //------------------------------------------------------------------------------------------
        // Ramsey numbers (related graphs).
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Circular graph of order 5 without red r-cliques and blue b-cluques for R(3, 3).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC5R33All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 3, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 5 without red r-cliques and blue b-cluques for R(3, 3).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC5R33Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 3, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 8 without red r-cliques and blue b-cluques for R(3, 4).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC8R34All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 4, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 8 without red r-cliques and blue b-cluques for R(3, 4).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC8R34Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 4, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 13 without red r-cliques and blue b-cluques for R(3, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC13R35All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 13 without red r-cliques and blue b-cluques for R(3, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC13R35Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(3, 5, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 17 without red r-cliques and blue b-cluques for R(4, 4).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC17R44All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 4, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 17 without red r-cliques and blue b-cluques for R(4, 4).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC17R44Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 4, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 24 without red r-cliques and blue b-cluques for R(4, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC24R45All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 24 without red r-cliques and blue b-cluques for R(4, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC24R45Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(4, 5, false, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 41 without red r-cliques and blue b-cluques for R(5, 5).
        /// All edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC41R55All_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(5, 5, true, Circle);
            Paint();
        }

        /// <summary>
        /// Circular graph of order 41 without red r-cliques and blue b-cluques for R(5, 5).
        /// Only red edges.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">paremeters</param>
        private void RamseyCircC41R55Red_Click(object sender, RoutedEventArgs e)
        {
            Graph = GraphCreator.MaxCircularGraphWithoutCliquesForRamseyNumber(5, 5, false, Circle);
            Paint();
        }

        //------------------------------------------------------------------------------------------
    }
}
