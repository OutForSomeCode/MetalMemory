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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationCommands.BrowseBack.InputGestures.Clear();            //ontkoppel terugfunctie
            NavigationCommands.BrowseForward.InputGestures.Clear();         //ontkopper vooruitfunctie

            Topmost = false;     //game start op de voorgrond
            PreviewKeyDown +=
                (s, e) =>
                {
                    if (e.Key == Key.F11)
                    {
                        if (WindowStyle != WindowStyle.SingleBorderWindow)      //checked of de game fullscreen staat
                        {
                            ResizeMode = ResizeMode.CanResize;                  //haalt de game uit fullscreen
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
