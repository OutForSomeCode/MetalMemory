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
        private static int score1 = GameLogic.ScoreOfPlayer1;
        private static int score2 = GameLogic.ScoreOfPlayer2;
        private static string naam1 = InitializeGame.Player1;
        private static string naam2 = InitializeGame.Player2;
        private static Dictionary<string, int> highscores = new Dictionary<string, int>();

        public HighScore()
        {
            InitializeComponent();
        }

        public static void HighscoreList()
        {

            if (score1 == score2)
            {
                highscores.Add(naam1, score1);
                highscores.Add(naam2, score2);
            }
            else if (score1 > score2)
            {
                highscores.Add(naam1, score1);
            }
            else
            {
                highscores.Add(naam2, score2);
            }

        }
    }
}
