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
    /// venster waar de game in draait
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        /// <summary>
        /// fullscreen modes door op F11 te drukken
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SaveLoad.LoadHighscores();                                      // laad de highscores

            NavigationCommands.BrowseBack.InputGestures.Clear();            // ontkoppel terugfunctie
            NavigationCommands.BrowseForward.InputGestures.Clear();         // ontkopper vooruitfunctie

            // kijkt of er toetsen worden ingedrukt
            PreviewKeyDown +=
                (s, e) =>
                {
                    if (e.Key == Key.F11)
                    {
                        if (WindowStyle != WindowStyle.SingleBorderWindow)      // checked of de game fullscreen staat
                        {
                            ResizeMode = ResizeMode.CanResize;                  // haalt de game uit fullscreen
                            WindowStyle = WindowStyle.SingleBorderWindow;
                            WindowState = WindowState.Normal;
                            Topmost = false;
                        }
                        else
                        {
                            ResizeMode = ResizeMode.NoResize;                   //zet de game in fullscreen
                            WindowStyle = WindowStyle.None;
                            WindowState = WindowState.Maximized;
                            Topmost = true;
                        }
                    }
                };
        }
    }
}
