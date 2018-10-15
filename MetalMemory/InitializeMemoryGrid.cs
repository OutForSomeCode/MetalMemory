using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MetalMemory
{
    class InitializeMemoryGrid
    {
        private Grid Localgrid;

        public InitializeMemoryGrid(Grid Publicgrid, int column, int row)
        {
            Localgrid = Publicgrid;
            CreateGrid(column, row);
        }

        private void CreateGrid(int column, int row)
        {
            for (int i = 0; i < column; i++)
            {
                Localgrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < row; i++)
            {
                Localgrid.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
