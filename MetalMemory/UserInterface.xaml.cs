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
        private int TimeRemaining = 30;
        public UserInterface()
        {
            InitializeComponent();
            Game_Loaded();
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
            Dispatcher.Invoke(new Action(() => CountDownTimer.Text = string.Format("Remaining time: {0}:{1}", TimeRemaining / 60, TimeRemaining % 60)));            

            if (TimeRemaining == 0)
            {
                //Switch player code here
                TimeRemaining = 30;
            }
        }
    }
}
