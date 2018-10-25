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
        private string Player1;
        private string Player2;
        private int GridColumn;
        private int GridRows;
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

            //start de verschillende game onderdelen(geeft variabelen mee om in de class te gebruiken)
            StartGame = new InitializeMemoryGrid(MemoryGrid, GridColumn, GridRows);
            GetCards = new InitializeCards(GridColumn, GridRows);
            GameLogic = new GameLogic(MemoryGrid, GridColumn, GridRows);

            //Userinterface --> playernames, gridsize(nodig voor reset)
            Player1 = PlayerName_1.Text;
            Player2 = PlayerName_2.Text;
            UserInterfaceFrame.NavigationService.Navigate(new UserInterface(Player1, Player2, MemoryGrid, GridColumn, GridRows));
        }

        //load game knop
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoadGame());
        }
    }
}
