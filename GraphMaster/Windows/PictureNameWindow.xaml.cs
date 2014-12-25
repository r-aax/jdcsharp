// Copyright Joy Developing.

using System.Windows;

namespace GraphMaster.Windows
{
    /// <summary>
    /// PictureNameWindow.xaml logic.
    /// </summary>
    public partial class PictureNameWindow : Window
    {
        /// <summary>
        /// Picture name.
        /// </summary>
        public string PictureName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="picture_name">picture name</param>
        public PictureNameWindow(string picture_name)
        {
            InitializeComponent();

            PictureName = picture_name;
            PictureNameTB.Text = PictureName;
        }

        /// <summary>
        /// Accept picture name.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, RoutedEventArgs e)
        {
            PictureName = PictureNameTB.Text;
            Close();
        }

        /// <summary>
        /// Cancel picture name.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
