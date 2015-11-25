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

namespace Tehtava1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)

            //logiikka buttosklikin alle
        {

            try
            {
                //haetaan syötetyt arvot
                int WindowHeight = int.Parse(txtWindowHeight.Text);
                int WindowWidth = int.Parse(txtWindowWidth.Text);
                int BorderWidth = int.Parse(txtBorderWidth.Text);

                //korkeampi matematiikka
                int WindowSize = WindowWidth * WindowHeight; //ikkunan ala
                int BorderLength = WindowWidth * 2 + WindowHeight * 2; //piiri
                int BorderSize = (WindowSize - ((WindowHeight - (2 * BorderWidth)) * (WindowWidth - (2 * BorderWidth)))); //karmin ala

                //isketään arvot kenttiin.
                txtWindowSize.Text = WindowSize.ToString();
                txtBorderLength.Text = BorderLength.ToString();
                txtBorderSize.Text = BorderSize.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Erroria pukkaa, kokeileppa oikeilla arvoilla");
            }

        }
    }
}
