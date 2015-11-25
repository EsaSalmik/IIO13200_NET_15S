using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Xml.Linq;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace Tehtava3
{

    //VAIHE 2
    //tietoja voidaan tallennella vaikka tietokantoihin, tekstitiedostoihin, xml/json yms..
    //tässä käytän xmllää


    public partial class MainWindow : Window
    {
        private ObservableCollection<Pelaaja> pelaajat;
        string filePath;


        public MainWindow()
        {
            InitializeComponent();
            pelaajat = new ObservableCollection<Pelaaja>();   //tyypitetty lista
            lataaPelaajatTiedostosta();
            this.lsbLista.ItemsSource = this.PelaajaLista;
            this.lsbLista.DisplayMemberPath = "KokoNimi";
        }

        private ObservableCollection<Pelaaja> PelaajaLista
        {
            get
            {
                return this.pelaajat;
            }
        }

        private void cmbSeura_Initialized(object sender, EventArgs e)
        {
            List<string> seurat = new List<string>();
            seurat.Add("Blues");
            seurat.Add("HIFK");
            seurat.Add("HPK");
            seurat.Add("Ilves");
            seurat.Add("JYP");
            seurat.Add("Kalpa");
            seurat.Add("KooKoo");
            seurat.Add("Karpat");
            seurat.Add("Lukko");
            seurat.Add("Pelicans");
            seurat.Add("Saipa");
            seurat.Add("Sport");
            seurat.Add("Tappara");
            seurat.Add("TPS");
            seurat.Add("Assat");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = seurat;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void lataaPelaajatTiedostosta()
        {
            //ladataan pelaajat xml-tiedostosta (VAIHE 2)

            try
            {
                filePath = ConfigurationManager.AppSettings.Get("Path");   //polku appconfista
            }
            catch
            {
                filePath = "pelaajat.xml";
            }


            if (File.Exists(filePath))
            {
                XDocument document = XDocument.Load(filePath);
                var tempPelaajat = from p in document.Descendants("pelaaja")
                                   select new
                                   {
                                       Etunimi = p.Element("etunimi").Value,
                                       Sukunimi = p.Element("sukunimi").Value,
                                       Siirtohinta = p.Element("siirtohinta").Value,
                                       Seura = p.Element("joukkue").Value,
                                   };

                foreach (var p in tempPelaajat)
                {
                    pelaajat.Add(new Pelaaja(p.Etunimi, p.Sukunimi, p.Seura, double.Parse(p.Siirtohinta)));
                }

                lblCursorPosition.Text = "Pelaajat ladattu onnistuneesti";
            }
            else
            {
                lblCursorPosition.Text = "Vanhoja pelaajia ei löytynyt";
            }
        }

        private void btnUusiPelaaja_Click(object sender, RoutedEventArgs e)
        {
            double hinta;

            //tekstilootista arvot irti
            if (txbEtunimi.Text != "" && txbSukunimi.Text != "" && double.TryParse(txbSiirtohinta.Text, out hinta) && double.Parse(txbSiirtohinta.Text) >= 0)
            {
                //arvot uuteen olioon
                Pelaaja tempPelaaja = new Pelaaja(txbEtunimi.Text, txbSukunimi.Text, cmbSeura.Text, double.Parse(txbSiirtohinta.Text));

                //tuplan tarkistus
                bool duplikaatti = false;
                for (int i = 0; i < pelaajat.Count(); i++)
                {
                    if (pelaajat[i].EtuNimi + " " + pelaajat[i].SukuNimi == tempPelaaja.EtuNimi + " " + tempPelaaja.SukuNimi)
                    {
                        duplikaatti = true;
                        break;
                    }
                }
                if (!duplikaatti)
                {
                    pelaajat.Add(tempPelaaja);

                    txbEtunimi.Text = "";
                    txbSukunimi.Text = "";
                    txbSiirtohinta.Text = "";
                    cmbSeura.Text = "Blues";
                    lblCursorPosition.Text = "Pelaaja luotu";
                }
                else
                {
                    
                    lblCursorPosition.Text = "Error: Pelaaja on jo lisätty";
                }
            }
            else
            {
                lblCursorPosition.Text = "Error: Hinta väärässä muodossa";
            }
        }


        private void btnLopetus_Click(object sender, RoutedEventArgs e)
        {

            //alla tallenellaan tiedot xmllään (VAIHE 2)

            XDocument doc = new XDocument(
                new XElement("pelaajat",
                    from pelaaja in pelaajat
                    select new XElement("pelaaja",
                        new XElement("etunimi", pelaaja.EtuNimi),
                        new XElement("sukunimi", pelaaja.SukuNimi),
                        new XElement("siirtohinta", pelaaja.Siirtohinta.ToString()),
                        new XElement("joukkue", pelaaja.Seura))));

            try
            {
                doc.Save(filePath);
            }
            catch (InvalidCastException )
            {
                MessageBox.Show("Tiedostojen tallennus epäonnistui");
                Environment.Exit(0);
            }

            Environment.Exit(0);
        }

        private void lsbLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbLista.Items.Count > 0 && lsbLista.SelectedIndex != -1)
            {
                int pelaajaIndex = (sender as ListBox).SelectedIndex;

                txbEtunimi.Text = pelaajat[pelaajaIndex].EtuNimi;
                txbSukunimi.Text = pelaajat[pelaajaIndex].SukuNimi;
                txbSiirtohinta.Text = pelaajat[pelaajaIndex].Siirtohinta.ToString();
                cmbSeura.Text = pelaajat[pelaajaIndex].Seura;

                lblCursorPosition.Text = "";
            }
        }

        private void btnTalletaPelaaja_Click(object sender, RoutedEventArgs e)
        {
            int pelaajaIndex = lsbLista.SelectedIndex;
            double hinta;

            if (double.TryParse(txbSiirtohinta.Text, out hinta) && double.Parse(txbSiirtohinta.Text) >= 0)
            {

                pelaajat[pelaajaIndex].EtuNimi = txbEtunimi.Text;
                pelaajat[pelaajaIndex].SukuNimi = txbSukunimi.Text;
                pelaajat[pelaajaIndex].Siirtohinta = Double.Parse(txbSiirtohinta.Text);
                pelaajat[pelaajaIndex].Seura = cmbSeura.Text;

                this.lsbLista.DisplayMemberPath = "";
                this.lsbLista.DisplayMemberPath = "KokoNimi";

                lblCursorPosition.Text = "Muutokset tallennettu!";
            }
            else
            {
                lblCursorPosition.Text = "Error: Hinta väärässä muodossa";
            }

        }

        private void btnPoistaPelaaja_Click(object sender, RoutedEventArgs e)
        {
            int pelaajaIndex = lsbLista.SelectedIndex;

            pelaajat.RemoveAt(pelaajaIndex);

            txbEtunimi.Text = "";
            txbSukunimi.Text = "";
            txbSiirtohinta.Text = "";
            cmbSeura.Text = "Blues";

            lblCursorPosition.Text = "Pelaaja poistettu!";
        }

        //tekstitiedostoon tallentelu
        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog textDialog = new SaveFileDialog();
            textDialog.Filter = "Text Files | *.txt";
            textDialog.DefaultExt = "txt";

            bool? result = textDialog.ShowDialog();
            if (result == true)
            {
                System.IO.Stream fileStream = textDialog.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);

                for (int i = 0; i < lsbLista.Items.Count; i++)
                {
                    sw.WriteLine(lsbLista.Items[i].ToString());
                }

                sw.Flush();
                sw.Close();

                lblCursorPosition.Text = "Tallennus onnistui";
            }
            else
            {
                lblCursorPosition.Text = "Error: Tiedoston tallennus ei onnistunna";
            }
        }
    }
}
