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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Lib.Maths.Geometry.Geometry3D;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;
using Lib.MathMod.SolidGrid;

namespace Hydro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Grid.
        /// </summary>
        private SolidGrid Grid = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start button click.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void StartB_Click(object sender, RoutedEventArgs e)
        {
            Grid = new SolidGrid(new Vector3D(10.0, 10.0, 1.0), 0.1);
        }
    }
}
