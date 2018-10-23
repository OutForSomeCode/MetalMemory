using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MetalMemory
{
    class InitializeCards
    {
        private Grid Localgrid;                                         //lege variabele
        private static Random RandomNumberGenerator = new Random();     //lege variabele
        private int TotalCards;                                         //lege variabele
        private int UniqueCards;                                        //lege variabele

        private List<int> IntList = new List<int>(Enumerable.Range(1, 32));     //maakt een lijst met de nummers 1 t/m 32
        private static List<ImageSource> Images = new List<ImageSource>();             //maakt een lege afbeeldingen lijst genaamd Images

        public InitializeCards(Grid Publicgrid, int column, int row)    //geeft de grid naam, aantal kolommen & rijen mee
        {
            Localgrid = Publicgrid;         //vult Localgrid met Publicgrid
            TotalCards = column * row;      //Berekend het aantal kaarten
            UniqueCards = TotalCards / 2;   //geeft aan hoeveel unieke kaarten er zijn
            FillImages();                   //start de method(FillImages) en geeft                   
        }

        private void FillImages()
        { 
            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards);   //Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList)   //voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                for (int i = 0; i < 2; i++)         //zorgt ervoor dat van elke afbeelding 2 worden toegevoegd
                {
                    ImageSource source = new BitmapImage(new Uri("Images/Cards/Card" + CardNumber + ".png", UriKind.Relative));     //voegt de afbeeldingen aan de lijst Images toe
                    Images.Add(source);
                }
            }
        }

        static public List<ImageSource> GetImageList()    //vult deze lijst met afbeeldingen die op de kaarten verschijnen
        {
            return Images.OrderBy(y => RandomNumberGenerator.Next()).ToList();  //Randomized de volgorde van de lijst met afbeeldingen en zet deze in GetImageList
        } 
    }
}

