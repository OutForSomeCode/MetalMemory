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
        public static Dictionary<string, int> highscores = new Dictionary<string, int>();

        public HighScore()
        {
            InitializeComponent();
        }
        
        public void UpdateHighscore()
        {
            var listorder = highscores.OrderByDescending(x => x.Value).ToList();

            //var listlist = from pair in highscores orderby pair.Value descending select pair;
            // var strlist = highscores.Keys.ToList();
            // var intlist = highscores.Values.ToList();
            //Score_0.Text = Convert.ToString(listlist.First());

            int listsize = listorder.Count();
            for (int i = 0; i < listsize && i < 7; i++)
            {
                TextBlock tb = (FindName(string.Format("Score_{0}", i)) as TextBlock);
                tb.Text = Convert.ToString(listorder.First());
                listorder.RemoveAt(0);
            }
        }

        public static void HighscoreList()
        {
            if (GameLogic.ScoreOfPlayer1 == GameLogic.ScoreOfPlayer2)
            {
                highscores.Add(InitializeGame.Player1, GameLogic.ScoreOfPlayer1);
                highscores.Add(InitializeGame.Player2, GameLogic.ScoreOfPlayer2);
            }
            else if (GameLogic.ScoreOfPlayer1 > GameLogic.ScoreOfPlayer2)
            {
                highscores.Add(InitializeGame.Player1, GameLogic.ScoreOfPlayer1);
            }
            else
            {
                highscores.Add(InitializeGame.Player2, GameLogic.ScoreOfPlayer2);
            }
        }
    }
}
