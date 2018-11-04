using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace MetalMemory
{
    /// <summary>
    /// userinterface tijdens de game, knoppen, spelernamen en scores
    /// </summary>
    public partial class UserInterface : Page
    {
        InitializeCards GetCards;
        GameLogic StartGameLogic;

        public static int TimeRemaining = 30;       //geeft aan hoeveel tijd er nog over is
        private int GridColumn;
        private int GridRows;
        private Grid MemoryGrid;
        private Timer CountDown;

        /// <summary>
        /// maakt variablen die zijn megegeven bekent binnen de class, start de method Game_Loaded
        /// </summary>
        /// <param name="Player1">naam van speler 1</param>
        /// <param name="Player2">naam van speler 2</param>
        /// <param name="GetMemoryGrid">het gamegrid, megegeven ivm reset game knop</param>
        /// <param name="GetGridColumn">aantal kolommen, megegeven ivm reset game knop</param>
        /// <param name="GetGridRows">aantal rijen, megegeven ivm reset game knop</param>
        public UserInterface(string Player1, string Player2, Grid GetMemoryGrid, int GetGridColumn, int GetGridRows)
        {
            InitializeComponent();
            ScreenNamePlayer1.Text = Player1;
            ScreenNamePlayer2.Text = Player2;
            GridColumn = GetGridColumn;
            GridRows = GetGridRows;
            MemoryGrid = GetMemoryGrid;
            Game_Loaded();
        }

        /// <summary>
        /// start een timer die elke seconde een update uitvoert
        /// </summary>
        private void Game_Loaded()
        {
            //PlaySounds SoundPlayer = new PlaySounds("MemoryMusic.wav", "PlayLoop");

            CountDown = new Timer(1000);            // nieuwe timer van 1 seconde         
            CountDown.AutoReset = true;             // timer blijft loopen
            CountDown.Start();                      // start de timer
            CountDown.Elapsed += Timer_Elapsed;     // trigger (elke seconde)

            ScreenScorePlayer1.Text = "Score: " + GameLogic.ScoreOfPlayer1.ToString();      // laat de score op het scherm zien
            ScreenScorePlayer2.Text = "Score: " + GameLogic.ScoreOfPlayer2.ToString();
        }

        /// <summary>
        /// update de tijd(aftellend), scores en eidigd de beurt als de tijd afloopt
        /// </summary>
        /// <param name="sender">word niks mee gedaan</param>
        /// <param name="e">word niks mee gedaan</param>
        private void Timer_Elapsed(object sender, EventArgs e)
        {
            // highlight speler die aan de beurt is
            if (GameLogic.TurnOfPlayer1 == true)
            {
                Dispatcher.Invoke(new Action(() => ScreenNamePlayer1.Foreground = new SolidColorBrush(Colors.Green)));
                Dispatcher.Invoke(new Action(() => ScreenNamePlayer2.Foreground = new SolidColorBrush(Colors.Red)));
            }
            else
            {
                Dispatcher.Invoke(new Action(() => ScreenNamePlayer2.Foreground = new SolidColorBrush(Colors.Green)));
                Dispatcher.Invoke(new Action(() => ScreenNamePlayer1.Foreground = new SolidColorBrush(Colors.Red)));
            }

            // update speler scores
            Dispatcher.Invoke(new Action(() => ScreenScorePlayer1.Text = "Score: " + GameLogic.ScoreOfPlayer1.ToString()));
            Dispatcher.Invoke(new Action(() => ScreenScorePlayer2.Text = "Score: " + GameLogic.ScoreOfPlayer2.ToString()));

            // toont timer op het scherm
            TimeRemaining--;
            Dispatcher.Invoke(new Action(() => CountDownTimer.Text = string.Format("{0}:{1}", TimeRemaining / 60, TimeRemaining % 60)));             

            // timer knippert rood laatste 5 seconden
            if (TimeRemaining <= 5)
            {
                if (TimeRemaining % 2 == 1)
                    Dispatcher.Invoke(new Action(() => CountDownTimer.Foreground = new SolidColorBrush(Colors.Red)));

                else
                    Dispatcher.Invoke(new Action(() => CountDownTimer.Foreground = new SolidColorBrush(Colors.White)));
            }

            // als timer 2 bereikt, start method die ook de kaartcheck doet
            if (TimeRemaining == 2)
            {
                GameLogic.EndOfTurnTimer.Enabled = true;
            }

            // als timer 0 bereikt, zet de timer weer op 30 seconden (+1 ivm updaten)
            if (TimeRemaining == 0)     
            {
                Dispatcher.Invoke(new Action(() => CountDownTimer.Foreground = new SolidColorBrush(Colors.White)));
                TimeRemaining = 31;
            }
        }

        /// <summary>
        /// reset de game en haalt een nieuwe set kaarten op
        /// </summary>
        /// <param name="sender">word niks mee gedaan</param>
        /// <param name="e">word niks mee gedaan</param>
        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");
            GetCards = new InitializeCards(GridColumn, GridRows);                       // haalt een nieuwe set kaarten op
            StartGameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);           // start de game logic opnieuw
            CountDownTimer.Foreground = new SolidColorBrush(Colors.White);              // set de timer kleur weer op wit
            TimeRemaining = 31;                                                         // reset de tijd op het scherm
            GameLogic.ScoreOfPlayer1 = 0;                                               // reset de scores
            GameLogic.ScoreOfPlayer2 = 0;
        }

        /// <summary>
        /// opslaan van de game
        /// </summary>
        /// <param name="sender">word niks mee gedaan</param>
        /// <param name="e">word niks mee gedaan</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            GameLogic.SaveDataTags();
            SaveLoad.SaveSomething();
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// terug naar het hoofd menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");
            CountDown.Stop();               // stopt de timer als de game gestopt word
            TimeRemaining = 31;             // reset de tijd voor de volgende game
            // HighScore.UpdateHighscore();
        }
    }
}
