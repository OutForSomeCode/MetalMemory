using System;
using System.IO;
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
using System.Media;
using System.Reflection;

namespace MetalMemory
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        //start knop
        private void Start_Click(object sender, EventArgs e)
        {
            string path = Assembly.GetExecutingAssembly().Location;             //kijkt waar de .exe staat
            path = Path.GetDirectoryName(path);                                 //geeft het pad naar de .exe terug als string
            path = Path.Combine(path, "Sounds/ButtonClickSound.wav");           //combineerd het pad naar de .exe met het pad naar het geluids bestand
            SoundPlayer PlaySounds = new SoundPlayer(path);                     //maakt een soundplayer aan met verwijzing naar het geluids bestand
            PlaySounds.PlaySync();                                              //speelt het geluid af, en pauseert de code tot het geluit klaar is met afspelen

            NavigationService.Navigate(new InitializeGame());                   //navigatie
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)         
        {
            Application.Current.Shutdown();                                     //exit game
        }
    }
}
