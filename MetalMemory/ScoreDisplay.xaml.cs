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
    /// Interaction logic for ScoreDisplay.xaml
    /// </summary>
    public partial class ScoreDisplay : Page
    {
        public ScoreDisplay()
        {
            InitializeComponent();
        }

        private string CountDownTimer()
        {
            int SecondsRemaining = 30;
            Timer Timer = new Timer(1000);
            Timer.Start();

            while (SecondsRemaining != 0)
                SecondsRemaining--;

            return Timer.ToString(); 
        } 
    }
}
