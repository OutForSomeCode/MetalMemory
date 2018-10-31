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
    /// Interaction logic for HighScore.xaml
    /// </summary>
    public partial class HighScore : Page
    {
        // dit zijn de aankoppelpunten voor de benodigde data
        //GameLogic.ScoreOfPlayer1;         score speler 1
        //GameLogic.ScoreOfPlayer2;         score speler 2
        //InitializeGame.Player1;           naam speler 1
        //InitializeGame.Player2;           naam speler 2

        public HighScore()
        {
            InitializeComponent();
        }
    }
}
