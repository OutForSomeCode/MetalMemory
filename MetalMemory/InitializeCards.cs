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
        private Grid Localgrid;
        private Random RandomNumberGenerator = new Random();
        private int TotalCards;
        private int UniqueCards;

        public InitializeCards(Grid Publicgrid, int column, int row)
        {
            Localgrid = Publicgrid;
            TotalCards = column * row;
            UniqueCards = TotalCards / 2;
            AddCardToGrid(column, row);
        }

        private List<ImageSource> GetImageList()
        {
            List<int> IntList = new List<int>(Enumerable.Range(1, 32)); //maakt een lijst met de nummers 1 t/m 32
            List<ImageSource> Images = new List<ImageSource>(); //maakt een lege lijst genaamd Images

            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards); //Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList) //voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                    for (int i = 0; i < 2; i++) //zorgt ervoor dat van elke afbeelding 2 worden toegevoegd
                    {
                        ImageSource source = new BitmapImage(new Uri("Images/Cards/Card" + CardNumber + ".png", UriKind.Relative)); //voegt de afbeeldingen aan de lijst Images toe
                        Images.Add(source);
                    }
                }
            return Images.OrderBy(y => RandomNumberGenerator.Next()).ToList(); //Randomized de volgorde van de lijst met kaarten
        }

        private void AddCardToGrid(int columns, int rows)
        {
            List<ImageSource> CardFaces = GetImageList();
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Image CardBack = new Image();
                    CardBack.Source = new BitmapImage(new Uri("Images/Cards/CardBack.png", UriKind.Relative));
                    CardBack.Tag = CardFaces.First();
                    CardFaces.RemoveAt(0);
                    Grid.SetColumn(CardBack, i);
                    Grid.SetRow(CardBack, j);
                    Localgrid.Children.Add(CardBack);

                    //Flip card trigger & Cursor
                    CardBack.MouseLeftButtonUp += new MouseButtonEventHandler(Flip_Card);
                    CardBack.Cursor = Cursors.Hand;
                }
            }
        }
        private void Flip_Card(object sender, MouseButtonEventArgs e)
        {
            Image Card = (Image)sender;
            ImageSource Face = (ImageSource)Card.Tag;
            Card.Source = Face;
        }
    }
}

