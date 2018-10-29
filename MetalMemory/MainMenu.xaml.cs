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
            //ScoreBoardFrame.NavigationService.Navigate(new HighScore());
        }

        //start knop
        private void Start_Click(object sender, EventArgs e)
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "Play");        //speelt het geluid af


            NavigationService.Navigate(new InitializeGame());                               //navigatie
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)         
        {
            PlaySounds SoundPlayer = new PlaySounds("ButtonClickSound.wav", "PlaySync");    //speelt het geluid af, en pauseert de code tot het geluit klaar is met afspelen

            Application.Current.Shutdown();                                                 //exit game
        }
    }
}
