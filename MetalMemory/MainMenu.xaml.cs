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

        private void Start_Click(object sender, EventArgs e)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            path = Path.Combine(path, "Sounds/ButtonClickSound.wav");
            SoundPlayer PlaySounds = new SoundPlayer(path);
            PlaySounds.PlaySync();

            //SoundPlayer PlaySounds = new SoundPlayer();
            //PlaySounds.PlaySync();

            NavigationService.Navigate(new InitializeGame());
        }
    }
}
