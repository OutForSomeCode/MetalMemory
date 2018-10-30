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

namespace MetalMemory
{
    /// <summary>
    /// Interaction logic for InitializeGame.xaml
    /// </summary>
    public partial class InitializeGame : Page
    {
        public static string Player1;
        public static string Player2;
        public static int GridColumn;
        public static int GridRows;
        InitializeMemoryGrid StartGame;
        InitializeCards GetCards;
        GameLogic GameLogic;

        public InitializeGame()
        {
            InitializeComponent();            
        }
        
        //new game knop
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");

            //kijkt welke hoeveelheid kaarten gekozen is --> geeft aan hoeveel kolommen en rijen daarvoor nodig zijn
            if (Cards16.IsChecked == true)
            {
                GridColumn = 4;
                GridRows = 4;
            }
            else if (Cards36.IsChecked == true)
            {
                GridColumn = 6;
                GridRows = 6;
            }
            else if (Cards64.IsChecked == true)
            {
                GridColumn = 8;
                GridRows = 8;
            }

            // check als er namen zijn ingevoert          
            if (string.IsNullOrWhiteSpace(PlayerName_1.Text) && string.IsNullOrWhiteSpace(PlayerName_2.Text))
            {
                MessageBox.Show("Please enter a name for both Players");
                PlayerName_1.Clear();
                PlayerName_2.Clear();
            }
            else if (string.IsNullOrWhiteSpace(PlayerName_1.Text))
            {
                MessageBox.Show("Please enter a name for Player 1");
                PlayerName_1.Clear();
            }
            else if (string.IsNullOrWhiteSpace(PlayerName_2.Text))
            {
                MessageBox.Show("Please enter a name for Player 2");
                PlayerName_2.Clear();
            }
            else
            {
                //start de verschillende game onderdelen(geeft variabelen mee om in de class te gebruiken)
                StartGame = new InitializeMemoryGrid(MemoryGrid, GridColumn, GridRows);
                GetCards = new InitializeCards(GridColumn, GridRows);
                GameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);

                //Userinterface --> playernames, gridsize(nodig voor reset)
                Player1 = PlayerName_1.Text;
                Player2 = PlayerName_2.Text;
                UserInterfaceFrame.NavigationService.Navigate(new UserInterface(Player1, Player2, MemoryGrid, GridColumn, GridRows));
            }
        }

        //load game knop
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");
            SaveLoad.LoadSomething();

            // Save functie aanpassen ==> als er gesaved word alle kaart Tags opslaan in GetTagDataList!!!!
            StartGame = new InitializeMemoryGrid(MemoryGrid, GridColumn, GridRows);
            
            // load functie die lijst op de juiste plaats terug zet
            GameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);
            UserInterfaceFrame.NavigationService.Navigate(new UserInterface(Player1, Player2, MemoryGrid, GridColumn, GridRows));
        }
    }
}
