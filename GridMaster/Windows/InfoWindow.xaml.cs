using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Lib.MathMod.Grid;

namespace GridMaster.Windows
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InfoWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with title.
        /// </summary>
        /// <param name="title">title</param>
        public InfoWindow(string title)
            : this()
        {
            Title = title;
        }

        /// <summary>
        /// Add structured grid info to window.
        /// </summary>
        /// <param name="g">grid</param>
        public void AddGridInfo(StructuredGrid g)
        {
            if (g == null)
            {
                LinesLB.Items.Add("No grid");

                return;
            }

            // Base grid description.
            string[] descr = g.Description();
            for (int i = 0; i < descr.Count(); i++)
            {
                LinesLB.Items.Add(descr[i]);
            }

            // Blocks descriptions.
            LinesLB.Items.Add("Blocks:");
            for (int i = 0; i < g.BlocksCount; i++)
            {
                LinesLB.Items.Add(" " + g.Blocks[i].ToString());
            }

            // Ifaces descriptions.
            LinesLB.Items.Add("Ifaces:");
            for (int i = 0; i < g.IfacesCount; i += 2)
            {
                LinesLB.Items.Add("!" + g.Ifaces[i].ToString());
                LinesLB.Items.Add(" " + g.Ifaces[i + 1].ToString());
            }

            // Border conditions descriptions.
            LinesLB.Items.Add("BConds:");
            for (int i = 0; i < g.BCondsCount; i++)
            {
                LinesLB.Items.Add(" " + g.BConds[i].ToString());
            }

            // Scopes descriptions.
            LinesLB.Items.Add("Scopes:");
            for (int i = 0; i < g.ScopesCount; i++)
            {
                LinesLB.Items.Add(" " + g.Scopes[i].ToString());
            }
        }

        /// <summary>
        /// Add blocks distribution to information window.
        /// </summary>
        /// <param name="g">grid</param>
        public void AddBlocksDistribution(StructuredGrid g)
        {
            if (g == null)
            {
                LinesLB.Items.Add("No grid");

                return;
            }

            LinesLB.Items.Add("  Id PId");

            // Partition number for each block.
            for (int i = 0; i < g.BlocksCount; i++)
            {
                Lib.MathMod.Grid.Block b = g.Blocks[i];
                LinesLB.Items.Add(String.Format("{0,4} {1,3}", b.Id, b.PartitionNumber));
            }
        }
    }
}
