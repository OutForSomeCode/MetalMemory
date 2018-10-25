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
        GameLogic GameLogic;

        private int TimeRemaining = 30;             //geeft aan hoeveel tijd er nog over is
        private int GridColumn;
        private int GridRows;
        private Grid MemoryGrid;

        public bool TurnOfPlayer1 = true;           //geeft aan wie er aan de beurt is
        public bool TurnOfPlayer2 = false;

        public UserInterface(string Player1, string Player2, Grid GetMemoryGrid, int GetGridColumn, int GetGridRows)
        {
            InitializeComponent();
            WindowPlayer1.Text = Player1;
            WindowPlayer2.Text = Player2;
            GridColumn = GetGridColumn;
            GridRows = GetGridRows;
            MemoryGrid = GetMemoryGrid;
            Game_Loaded();
        }

        private void Game_Loaded()
        {
            PlaySounds SoundPlayer = new PlaySounds("MemoryMusic.wav", "PlayLoop");
            Timer CountDown = new Timer(1000);      //nieuwe timer van 1 seconde         
            CountDown.Elapsed += Timer_Elapsed;     //trigger (elke seconde) 
            CountDown.AutoReset = true;             //timer blijft loopen
            CountDown.Start();                      //start de timer
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            TimeRemaining--;        //-1 elke seconde
            Dispatcher.Invoke(new Action(() => CountDownTimer.Text = string.Format("{0}:{1}", TimeRemaining / 60, TimeRemaining % 60)));    //toont timer op het scherm             

            if (TimeRemaining <= 5)
            {
                if (TimeRemaining % 2 == 1)
                    Dispatcher.Invoke(new Action(() => CountDownTimer.Foreground = new SolidColorBrush(Colors.Red)));

                else
                    Dispatcher.Invoke(new Action(() => CountDownTimer.Foreground = new SolidColorBrush(Colors.White)));
            }
            

            if (TimeRemaining == 0)     //als timer 0 bereikt
            {
                TurnOfPlayer1 = !TurnOfPlayer1;     //wissel van beurt
                //Dispatcher.Invoke(new Action(() => WindowPlayer1.Text = TurnOfPlayer1.ToString())); //test, aanpassen
                TurnOfPlayer2 = !TurnOfPlayer2;     //wissel van beurt
                //Dispatcher.Invoke(new Action(() => WindowPlayer2.Text = TurnOfPlayer2.ToString())); //test, aanpassen
                TimeRemaining = 31;     //set timer weer op 30 seconden
            }
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            GetCards = new InitializeCards(GridColumn, GridRows);
            GameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);
            TimeRemaining = 31;
            CountDownTimer.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
