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
        private Grid Localgrid;     //lege variabele

        public InitializeMemoryGrid(Grid Publicgrid, int column, int row)   //geeft de grid naam, aantal kolommen & rijen mee
        {
            Localgrid = Publicgrid;
            CreateGrid(column, row);    //start de method(CreateGrid) en geeft de int column & row mee(deze worden uit InitializeMemoryGrid gehaald)
        }

        private void CreateGrid(int column, int row)    //method met 2 variabelen
        {
            for (int i = 0; i < column; i++)            //loopt door de kolommen (links naar rechts)
            {
                Localgrid.ColumnDefinitions.Add(new ColumnDefinition()); //voegt per kolom een ColumnDefinition(xaml)
            }
            for (int i = 0; i < row; i++)               //loopt door de rijen (boven naar onderen)
            {
                Localgrid.RowDefinitions.Add(new RowDefinition());  //voegt per rij een RowDefinition(xaml)
            }
        }
    }
}
