using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Xml.Linq;

namespace Tehtava4
{
    /// <summary>
    /// Interaction logic for Kellari.xaml
    /// </summary>
    public partial class Kellari : Window
    {

        XDocument document;
        string filePath;

        public Kellari()
        {
            InitializeComponent();

            filePath = ConfigurationManager.AppSettings.Get("Path");
            document = XDocument.Load(filePath);

            DataGridTextColumn textColumn1 = new DataGridTextColumn();
            textColumn1.Header = "Viini";
            textColumn1.Binding = new Binding("nimi");

            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            textColumn2.Header = "Maa";
            textColumn2.Binding = new Binding("maa");

            DataGridTextColumn textColumn3 = new DataGridTextColumn();
            textColumn3.Header = "Arvio";
            textColumn3.Binding = new Binding("arvio");

            dbWines.Columns.Add(textColumn1);
            dbWines.Columns.Add(textColumn2);
            dbWines.Columns.Add(textColumn3);

            etsiViinit();
            etsiMaat();

            lblPath.Content = filePath;
        }

        private void etsiViinit()
        {
            //viiniä comboboxiin
            var tempViinit = from p in document.Descendants("wine")
                             select new
                             {
                                 nimi = p.Element("nimi").Value,
                                 maa = p.Element("maa").Value,
                                 arvio = p.Element("arvio").Value
                             };

            foreach (var p in tempViinit)
            {
                dbWines.Items.Add(p);
            }
        }

        private void etsiMaat()
        {
            //valtiot boxiin
            var tempViinit = from p in document.Descendants("wine")
                             select new
                             {
                                 nimi = p.Element("nimi").Value,
                                 maa = p.Element("maa").Value,
                                 arvio = p.Element("arvio").Value
                             };

            foreach (var p in tempViinit)
            {
                if (!cmbMaat.Items.Contains(p.maa))
                {
                    cmbMaat.Items.Add(p.maa.ToString());
                }
            }

            cmbMaat.SelectedIndex = 0;
        }

        private void btnHaeViinit_Click(object sender, RoutedEventArgs e)
        {
            //viinit tietystä valtiosta

            var tempViinit = from p in document.Descendants("wine")
                             select new
                             {
                                 nimi = p.Element("nimi").Value,
                                 maa = p.Element("maa").Value,
                                 arvio = p.Element("arvio").Value
                             };


            dbWines.Items.Clear();

            foreach (var p in tempViinit)
            {
                if (p.maa.ToString() == cmbMaat.SelectedItem.ToString())
                {
                    dbWines.Items.Add(p);
                }
            }
        }
    }
}
