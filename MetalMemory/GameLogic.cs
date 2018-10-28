using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Input;

namespace MetalMemory
{
    class GameLogic
    {
        private bool TurnOfPlayer1 = true;
        private int ScoreMultiplier = 1;
        public static int ScoreOfPlayer1 = 0;
        public static int ScoreOfPlayer2 = 0;
                
        private Grid Localgrid;
        public static Timer EndOfTurnTimer;
        private List<Button> CardsList = new List<Button>();
        private List<Button> ChosenCardsList = new List<Button>();
        private List<int> CardCompareList = new List<int>();

        public GameLogic(Grid Publicgrid, int column, int row)
        {
            Localgrid = Publicgrid;                                         // vult Localgrid met Publicgrid
            AddCardToGrid(column, row);                                     // start de method(AddCardToGrid) en geeft de int column & row mee(deze worden uit InitializeCards gehaald)

            EndOfTurnTimer = new Timer(2000);                               // nieuwe timer van 2 seconden         
            EndOfTurnTimer.AutoReset = false;                               // timer loopt maar 1 keer
            EndOfTurnTimer.Elapsed += EndOfTurnTimer_Elapsed;               // trigger als de timer afloopt
        }

        private void AddCardToGrid(int columns, int rows)   
        {
            CardsList = InitializeCards.GetCardList;                        // maakt een image lijst genaamt CardFaces en vult deze met afbeeldingen uit GetImageList  
            for (int i = 0; i < columns; i++)                               // loopt door de kolommen (links naar rechts)
            {
                for (int j = 0; j < rows; j++)                              // per kolom, loopt door de rijen (boven naar onderen) en voert de code hieronder uit
                {
                    // vult het grid met buttons. aan deze buttons hangt een tag met de info voor kaartnummer, voorkant, achterkant & false/true 
                    Button Card = new Button();                    
                    Card = CardsList.First();
                    CardsList.RemoveAt(0);                                  // verwijdert de button na het plaatsen zodat hij niet nog een keer gebruikt kan worden

                    // haalt de info voor de achterkant uit de tag 
                    Image CardBack = new Image();
                    CardBack.Source = ((InitializeCards.CardTagData)Card.Tag).SourceCardBack;
                    Card.Content = CardBack;

                    // plaatst de kaart op het speelveld
                    Grid.SetColumn(Card, i);                                // positie van de kaart in het grid (kolom)
                    Grid.SetRow(Card, j);                                   // positie van de kaart in het grid (rij)
                    Localgrid.Children.Add(Card);

                    Card.Click += new RoutedEventHandler(Button_Click);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)         // klik trigger
        {
            Button ThisButton = sender as Button;

            if (EndOfTurnTimer.Enabled) return;

            PlaySounds SoundPlayer = new PlaySounds("CardSound.wav", "Play");

            Image CardFace = new Image();
            CardFace.Source = ((InitializeCards.CardTagData)ThisButton.Tag).SourceCardFace;
            ThisButton.Content = CardFace;

            ChosenCards(ThisButton);
            CompareCards();
        }

        private void ChosenCards(Button ThisButton)
        {
            int CardIndex = ((InitializeCards.CardTagData)ThisButton.Tag).IndexNumber;
            CardCompareList.Add(CardIndex);

            ChosenCardsList.Add(ThisButton);
        }

        private void CompareCards()
        {
            // doe miks als er nog geen 2 kaarten zijn geselecteerd
            if (CardCompareList.Count < 2) return;

            else
                EndOfTurnTimer.Enabled = true;
        }

        private void EndOfTurnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // als de beurt afloopt en er is maar 1 kaart omgedraaid
            if (CardCompareList.Count < 2)
            {
                EndOfTurn();
            }

            // als de kaarten gelijk zijn
            else if (CardCompareList[0] == CardCompareList[1])
            {
                foreach (Button Card in ChosenCardsList)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        ((InitializeCards.CardTagData)Card.Tag).CardHidden = true;
                        Card.Visibility = Visibility.Hidden;
                    });
                }
                PlayerScore();
            }

            // als de kaarten NIET gelijk zijn
            else
            {
                EndOfTurn();
            }

            // leeg de vergelijk & gekozen kaarten lijsten
            CardCompareList.Clear();
            ChosenCardsList.Clear();

            // schermtimer resetten
            UserInterface.TimeRemaining = 31;
        }

        private void EndOfTurn()
        {
            // draai de kaarten terug
            foreach (Button Card in ChosenCardsList)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Image CardBack = new Image();
                    CardBack.Source = ((InitializeCards.CardTagData)Card.Tag).SourceCardBack;
                    Card.Content = CardBack;
                });
            }
            // beurt wisselen
            TurnOfPlayer1 = !TurnOfPlayer1;

            // reset score multiplier
            ScoreMultiplier = 1;
        }
        
        private void PlayerScore()
        {
            if (TurnOfPlayer1 == true)
            {
                if (ScoreMultiplier > 1)
                {
                    ScoreOfPlayer1 *= ScoreMultiplier;
                }

                ScoreOfPlayer1 += 50;
            }
            else
            {
                if (ScoreMultiplier > 1)
                {
                    ScoreOfPlayer2 *= ScoreMultiplier;
                }

                ScoreOfPlayer2 += 50;
            }

            // set score multyplier
            ScoreMultiplier++;
        }
    }
}
