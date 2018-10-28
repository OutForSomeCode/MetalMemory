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
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Page
    {
        InitializeCards GetCards;
        GameLogic StartGameLogic;

        public static int TimeRemaining = 30;       //geeft aan hoeveel tijd er nog over is
        private int GridColumn;
        private int GridRows;
        private Grid MemoryGrid;

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

        private void Game_Loaded()
        {
            //PlaySounds SoundPlayer = new PlaySounds("MemoryMusic.wav", "PlayLoop");

            Timer CountDown = new Timer(1000);      // nieuwe timer van 1 seconde         
            CountDown.AutoReset = true;             // timer blijft loopen
            CountDown.Start();                      // start de timer
            CountDown.Elapsed += Timer_Elapsed;     // trigger (elke seconde)
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            // update speler scores
            Dispatcher.Invoke(new Action(() => ScreenScorePlayer1.Text = GameLogic.ScoreOfPlayer1.ToString()));
            Dispatcher.Invoke(new Action(() => ScreenScorePlayer2.Text = GameLogic.ScoreOfPlayer2.ToString()));

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
                TimeRemaining = 31;
            }
        }

        // reset de game en haalt een nieuwe set kaarten op
        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            GetCards = new InitializeCards(GridColumn, GridRows);
            StartGameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);
            TimeRemaining = 31;
            CountDownTimer.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
