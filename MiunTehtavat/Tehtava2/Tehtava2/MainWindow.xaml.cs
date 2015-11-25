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

namespace Tehtava2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BLLotto LotteryMachine;

        public MainWindow()
        {
            InitializeComponent();
            LotteryMachine = new BLLotto();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)

        {
            int LotteryRows = 0;
            bool TryRowCount = int.TryParse(txtDraws.Text, out LotteryRows); //monta drawia vedetään

            for (int i = 0; i < LotteryRows; i++)
            {
                List<int> LottoRivi = LotteryMachine.LottoRivi(cmbGame.Text); //katsotaan comboboxin arvosta mitä peliä pelataan
                txtNumbers.Text += cmbGame.Text + ": \n";
                for (int j = 0; j < LottoRivi.Count(); j++) //oliolla laskeskellaan numerot
                {
                    txtNumbers.Text += LottoRivi[j].ToString() + " "; 
                }
                txtNumbers.Text += "\n \n";

                //eurojackpotin tähtinumerot
                if (cmbGame.Text == "EuroJackpot")
                {
                    List<int> TahtiRivi = LotteryMachine.LottoRivi("Tahtinumero");
                    txtNumbers.Text += "Tähtinumerot: \n";
                    for (int j = 0; j < TahtiRivi.Count(); j++)
                    {
                        txtNumbers.Text += TahtiRivi[j].ToString() + " ";
                    }
                    txtNumbers.Text += "\n \n";
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtNumbers.Text = "";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
