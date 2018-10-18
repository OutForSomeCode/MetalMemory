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
        private Random RandomNumberGenerator = new Random();            //lege variabele
        private int TotalCards;                                         //lege variabele
        private int UniqueCards;                                        //lege variabele

        public InitializeCards(Grid Publicgrid, int column, int row)    //geeft de grid naam, aantal kolommen & rijen mee
        {
            Localgrid = Publicgrid;         //vult Localgrid met Publicgrid
            TotalCards = column * row;      //Berekend het aantal kaarten
            UniqueCards = TotalCards / 2;   //geeft aan hoeveel unieke kaarten er zijn
            AddCardToGrid(column, row);     //start de method(AddCardToGrid) en geeft de int column & row mee(deze worden uit InitializeCards gehaald)
        }

        private List<ImageSource> GetImageList()    //vult deze lijst met afbeeldingen die op de kaarten verschijnen
        {
            List<int> IntList = new List<int>(Enumerable.Range(1, 32));     //maakt een lijst met de nummers 1 t/m 32
            List<ImageSource> Images = new List<ImageSource>();             //maakt een lege afbeeldingen lijst genaamd Images

            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards);   //Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList)   //voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                    for (int i = 0; i < 2; i++)         //zorgt ervoor dat van elke afbeelding 2 worden toegevoegd
                    {
                        ImageSource source = new BitmapImage(new Uri("Images/Cards/Card" + CardNumber + ".png", UriKind.Relative));     //voegt de afbeeldingen aan de lijst Images toe
                        Images.Add(source);
                    }
                }
            return Images.OrderBy(y => RandomNumberGenerator.Next()).ToList();  //Randomized de volgorde van de lijst met afbeeldingen en zet deze in GetImageList
        }

        private void AddCardToGrid(int columns, int rows)   //method met 2 variabelen 
        {
            List<ImageSource> CardFaces = GetImageList();   //maakt een image lijst CardFaces en vult deze met afbeeldingen uit GetImageList
            for (int i = 0; i < columns; i++)               //loopt door de kolommen (links naar rechts)
            {
                for (int j = 0; j < rows; j++)              //loopt door de rijen (boven naar onderen)
                {
                    Image CardBack = new Image();           //maakt een nieuwe image tag aan in xaml
                    CardBack.Source = new BitmapImage(new Uri("Images/Cards/CardBack.png", UriKind.Relative));  //geeft aan welke afbeelding te gebruiken als achterkant)
                    CardBack.Tag = CardFaces.First();       //voegt een afbeelding to aan de voorkant van de kaart)
                    CardFaces.RemoveAt(0);                  //verbergt de voorkant van de kaart
                    Grid.SetColumn(CardBack, i);            //positie van de kaart in het grid (kolom)
                    Grid.SetRow(CardBack, j);               //positie van de kaart in het grid (rij)
                    Localgrid.Children.Add(CardBack);       //voegt de opgegeven afbeelding toe aan de achterkant van alle kaarten

                    CardBack.MouseLeftButtonUp += new MouseButtonEventHandler(Flip_Card);   //voegt een trigger toe aan de achterkant van de kaarten
                    CardBack.Cursor = Cursors.Hand;                                         //veranderd de cursor in een hand als je over de kaarten heen gaat
                }
            }
        }
        private void Flip_Card(object sender, MouseButtonEventArgs e)   //trigger
        {
            Image Card = (Image)sender;                                 //welke kaart geklikt wordt
            ImageSource Face = (ImageSource)Card.Tag;                   //"draait" de kaart om
            Card.Source = Face;
        }
    }
}

