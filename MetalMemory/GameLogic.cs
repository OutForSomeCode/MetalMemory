using System;
using System.Windows;
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
        private List<Button> Cards = new List<Button>();

        public GameLogic(Grid Publicgrid, int column, int row)
        {
            Localgrid = Publicgrid;                         //vult Localgrid met Publicgrid
            AddCardToGrid(column, row);                     //start de method(AddCardToGrid) en geeft de int column & row mee(deze worden uit InitializeCards gehaald)
        }

        private void AddCardToGrid(int columns, int rows)   //method met 2 variabelen 
        {
            Cards = InitializeCards.GetCardList;                           //maakt een image lijst genaamt CardFaces en vult deze met afbeeldingen uit GetImageList  
            for (int i = 0; i < columns; i++)                                   //loopt door de kolommen (links naar rechts)
            {
                for (int j = 0; j < rows; j++)                                  //per kolom, loopt door de rijen (boven naar onderen) en voert de code hieronder uit
                {
                    //vult het grid met buttons. aan deze buttons hangt een tag met de info voor kaartnummer, voorkant, achterkant & false/true 
                    Button Card = new Button();                    
                    Card = Cards.First();
                    Cards.RemoveAt(0);                                      //verwijdert de button na het plaatsen zodat hij niet nog een keer gebruikt kan worden

                    //haalt de info voor de achterkant uit de tag 
                    Image CardFace = new Image();
                    CardFace.Source = ((InitializeCards.CardTagData)Card.Tag).SourceCardBack;
                    Card.Content = CardFace;

                    //plaatst de kaart op het speelveld
                    Grid.SetColumn(Card, i);                                //positie van de kaart in het grid (kolom)
                    Grid.SetRow(Card, j);                                   //positie van de kaart in het grid (rij)
                    Localgrid.Children.Add(Card);

                    Card.Click += new RoutedEventHandler(Button_Click);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)   //klik trigger
        {
            Button ThisButton = sender as Button;

            Image CardBack = new Image();
            CardBack.Source = ((InitializeCards.CardTagData)ThisButton.Tag).SourceCardFace;
            ThisButton.Content = CardBack;

            PlaySounds SoundPlayer = new PlaySounds("CardSound.wav", "Play");
        }
    }
}
