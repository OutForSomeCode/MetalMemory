using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace MetalMemory
{
    class GameLogic
    {
        public GameLogic(int column, int row)
        {
            //hier kun je var koppelen aan de column/row
        }

        private void Flip_Card(object sender, MouseButtonEventArgs e)
        {
            Image Card = (Image)sender;
            ImageSource Face = (ImageSource)Card.Tag;
            Card.Source = Face;
        }

    }
}
