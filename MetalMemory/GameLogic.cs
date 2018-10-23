using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace MetalMemory
{
    class GameLogic
    {
        private Grid Localgrid;

        public GameLogic(Grid Publicgrid, int column, int row)
        {      
            Localgrid = Publicgrid;                         //vult Localgrid met Publicgrid
            AddCardToGrid(column, row);                     //start de method(AddCardToGrid) en geeft de int column & row mee(deze worden uit InitializeCards gehaald)
        }

        private void AddCardToGrid(int columns, int rows)   //method met 2 variabelen 
        {
            List<ImageSource> CardFaces = InitializeCards.GetImageList();       //maakt een image lijst genaamt CardFaces en vult deze met afbeeldingen uit GetImageList
            for (int i = 0; i < columns; i++)                                   //loopt door de kolommen (links naar rechts)
            {
                for (int j = 0; j < rows; j++)                                  //per kolom, loopt door de rijen (boven naar onderen) en voert de code hieronder uit
                {
                    Image CardBack = new Image();                               //maakt een nieuwe image tag aan in xaml
                    CardBack.Source = new BitmapImage(new Uri("Images/Cards/CardBack.png", UriKind.Relative));  //geeft aan welke afbeelding te gebruiken als achterkant)
                    CardBack.Tag = CardFaces.First();                           //voegt een afbeelding to aan de voorkant van de kaart)
                    CardFaces.RemoveAt(0);                                      //verbergt de voorkant van de kaart
                    Grid.SetColumn(CardBack, i);                                //positie van de kaart in het grid (kolom)
                    Grid.SetRow(CardBack, j);                                   //positie van de kaart in het grid (rij)
                    Localgrid.Children.Add(CardBack);                           //voegt de achterkant toe aan alle kaarten

                    CardBack.MouseLeftButtonUp += new MouseButtonEventHandler(Flip_Card);   //voegt een trigger toe aan de achterkant van de kaarten
                    CardBack.Cursor = Cursors.Hand;                                         //veranderd de cursor in een hand als je over de kaarten heen gaat
                }
            }
        }

        private void Flip_Card(object sender, MouseButtonEventArgs e)   //klik trigger
        {
            Image Card = (Image)sender;                                 //welke kaart geklikt wordt
            ImageSource Face = (ImageSource)Card.Tag;                   //"draait" de kaart om
            Card.Source = Face;
        }
    }
}
