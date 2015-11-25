using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
   Pelaaja-luokka kuntoon
*/

namespace Tehtava3
{
    class Pelaaja
    {
        public String etunimi;
        private String sukunimi;
        private String seura;
        private double siirtohinta;

        public String EtuNimi
        {
            get { return this.etunimi; }
            set { this.etunimi = value; }
        }
        public String SukuNimi
        {
            get { return this.sukunimi; }
            set { this.sukunimi = value; }
        }
        public String Seura
        {
            get { return this.seura; }
            set { this.seura = value; }
        }
        public double Siirtohinta
        {
            get { return this.siirtohinta; }
            set { this.siirtohinta = value; }
        }
        public String KokoNimi
        {
            get { return this.etunimi + " " + this.sukunimi + ", " + this.seura; }
        }

        public override string ToString()
        {
            return this.KokoNimi;
        }
        public Pelaaja(String etu, String suku, String seura, double hinta)
        {
            this.etunimi = etu;
            this.sukunimi = suku;
            this.siirtohinta = hinta;
            this.seura = seura;
        }
    }
}
