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
using System.Windows.Threading;
using System.Timers;

namespace MetalMemory
{
    /// <summary>
    /// Interaction logic for ScoreDisplay.xaml
    /// </summary>
    public partial class ScoreDisplay : Page
    {
        private int TimeRemaining = 30;

        public ScoreDisplay()
        {
            InitializeComponent();
        }

        private void Game_Loaded()
        {
            Timer CountDown = new Timer(1000);
            CountDown.Elapsed += Timer_Elapsed;
            CountDown.AutoReset = true;
            CountDown.Start();
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            TimeRemaining--;
            //CountDownTimer.Text = "Remaining time: " + TimeRemaining.ToString();

            if (TimeRemaining == 0)
            {
                //Switch player code here
                TimeRemaining = 30;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CountDownTimer.Text = "Remaining time: " + TimeRemaining.ToString();             
        }
    }
}
