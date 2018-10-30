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
        public static bool TurnOfPlayer1 = true;
        public static int ScoreOfPlayer1 = 0;
        public static int ScoreOfPlayer2 = 0;
        private int ScoreMultiplier = 1;
        private static int Columns;
        private static int Rows;

        private static Grid Localgrid;
        public static Timer EndOfTurnTimer;
        private List<InitializeCards.CardTagData> TagDataList = new List<InitializeCards.CardTagData>();
        private List<Button> ChosenCardsList = new List<Button>();
        private List<int> CardCompareList = new List<int>();

        public GameLogic(Grid Publicgrid, int column, int row)
        {
            Localgrid = Publicgrid;
            Columns = column;
            Rows = row;
            AddCardToGrid();

            EndOfTurnTimer = new Timer(2000);                               // nieuwe timer van 2 seconden         
            EndOfTurnTimer.AutoReset = false;                               // timer loopt maar 1 keer
            EndOfTurnTimer.Elapsed += EndOfTurnTimer_Elapsed;               // trigger als de timer afloopt
        }

        private void AddCardToGrid()   
        {
            Style CardStyle = new Style(typeof(Button));
            CardStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Transparent));
            CardStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Transparent));

            TagDataList = InitializeCards.GetTagDataList;                   // maakt een lijst genaamt CardFaces en vult deze met buttons(afbeeldingen) uit GetCardList  
            for (int i = 0; i < Columns; i++)                               // loopt door de kolommen (links naar rechts)
            {
                for (int j = 0; j < Rows; j++)                              // per kolom, loopt door de rijen (boven naar onderen)
                {
                    Button Card = new Button();
                    Card.Style = CardStyle;
                    Card.Tag = TagDataList.First();                         // vult het grid met buttons. aan deze buttons hangt een tag met de info voor kaartnummer, voorkant, achterkant & false/true
                    TagDataList.RemoveAt(0);                                // verwijdert de button na het plaatsen zodat hij niet nog een keer gebruikt kan worden

                    // haalt de info voor de achterkant uit de tag 
                    Image CardBack = new Image();
                    CardBack.Source = ((InitializeCards.CardTagData)Card.Tag).SourceCardBack;
                    Card.Content = CardBack;

                    // plaatst de kaart op het speelveld
                    Grid.SetColumn(Card, i);                                // positie van de kaart in het grid (kolom)
                    Grid.SetRow(Card, j);                                   // positie van de kaart in het grid (rij)
                    Localgrid.Children.Add(Card);

                    // maak de kaarten klikbaar
                    Card.Click += new RoutedEventHandler(Button_Click);
                }
            }
        }

        // als er op een kaart geklikt is doe het volgende:
        private void Button_Click(object sender, RoutedEventArgs e)        
        {
            // de kaart(button) die geklikt is
            Button ThisButton = sender as Button;

            // als einde van de beurt al gestart is kun je geen kaarten meer omdraaien
            if (EndOfTurnTimer.Enabled) return;

            // geluid voor het omdraaien van de kaart
            PlaySounds SoundPlayer = new PlaySounds("CardSound.wav", "Play");

            // draai de kaart om
            Image CardFace = new Image();
            CardFace.Source = ((InitializeCards.CardTagData)ThisButton.Tag).SourceCardFace;
            ThisButton.Content = CardFace;
  
            ChosenCards(ThisButton);
            StartEndOfTurn();
        }
 
        private void ChosenCards(Button ThisButton)
        {
            // haalt het kaart nummer uit de "Tag"(informatie die aan elke kaart hangt) en zet deze in de lijst vergelijk
            int CardIndex = ((InitializeCards.CardTagData)ThisButton.Tag).IndexNumber;
            CardCompareList.Add(CardIndex);

            // voegt de geklikte kaart toe aan de lijst gekozen kaarten
            ChosenCardsList.Add(ThisButton);
        }

        private void StartEndOfTurn()
        {
            // doe miks als er nog geen 2 kaarten zijn geselecteerd
            if (CardCompareList.Count < 2) return;

            // start einde van de beurt
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

        // voegt score toe na de beurt(2 kaarten omgedraait)
        private void PlayerScore()
        {
            // als speler 1 aan de beurt is
            if (TurnOfPlayer1 == true)
            {
                if (ScoreMultiplier > 1)
                {
                    ScoreOfPlayer1 *= ScoreMultiplier;
                }

                ScoreOfPlayer1 += 50;
            }
            
            // anders speler 2
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

        // einde beurt
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

        public static void SaveDataTags()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    //Button Card = Localgrid.Children();
                    int CardNumber = ((InitializeCards.CardTagData)Card.Tag).IndexNumber;
                    ImageSource CardFace = ((InitializeCards.CardTagData)Card.Tag).SourceCardFace;
                    ImageSource CardBack = ((InitializeCards.CardTagData)Card.Tag).SourceCardBack;
                    bool HideCard = ((InitializeCards.CardTagData)Card.Tag).CardHidden;

                    var CardTag = new InitializeCards.CardTagData(CardNumber, CardFace, CardBack, HideCard);
                    InitializeCards.GetTagDataList.Add(CardTag);
                }
            }
        }
    }
}
