using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava2
{
    class BLLotto
    {

        public string Tyyppi;
        private int SuurinNro;
        private int NumeroLkm;
        private int PieninNro;
        private Random rnd;

        public BLLotto()
        {
            rnd = new Random();
        }

        public List<int> LottoRivi() //listin alustus
        {
            List<int> Temp = new List<int>();

            return Temp;
        }

        public List<int> LottoRivi(string Tyyppi)
        {

            List<int> Temp = new List<int>();
            int randomCounter = 0;
            int maxNumber = 0;

            switch (Tyyppi)
            {
                case "Lotto":
                    randomCounter = 7;
                    maxNumber = 40;
                    break;
                case "VikingLotto":
                    randomCounter = 6;
                    maxNumber = 49;
                    break;
                case "EuroJackpot":
                    randomCounter = 5;
                    maxNumber = 51;
                    break;
                case "Tahtinumero":
                    randomCounter = 2;
                    maxNumber = 9;
                    break;
                default:
                    randomCounter = 7;
                    maxNumber = 40;
                    break;
            }

            for (int i = 0; i < randomCounter; i++)
            {

                int number = rnd.Next(1, maxNumber);

                while (Temp.Contains(number))
                {
                    number = rnd.Next(1, maxNumber);
                }

                Temp.Add(number);
            };

            return Temp;
        }

    }
}
