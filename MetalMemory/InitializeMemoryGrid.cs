using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MetalMemory
{
    /// <summary>
    /// maakt het speelbord waar de kaarten in gelagen worden
    /// </summary>
    class InitializeMemoryGrid
    {
        private Grid Localgrid;

        /// <summary>
        /// maakt de megegeven variabelen bekent in deze class, start method CreateGrid
        /// </summary>
        /// <param name="Publicgrid">dit grid</param>
        /// <param name="column">aantal kolommen</param>
        /// <param name="row">aantal rijen</param>
        public InitializeMemoryGrid(Grid Publicgrid, int column, int row)   //geeft de grid naam, aantal kolommen & rijen mee
        {
            Localgrid = Publicgrid;
            CreateGrid(column, row);    //start de method(CreateGrid) en geeft de int column & row mee(deze worden uit InitializeMemoryGrid gehaald)
        }

        /// <summary>
        /// maakt het speelbord waar de kaarten in gelagen worden
        /// </summary>
        /// <param name="column">aantal kolommen</param>
        /// <param name="row">aantal rijen</param>
        private void CreateGrid(int column, int row)
        {
            for (int i = 0; i < column; i++)            //loopt door de kolommen (boven naar onderen)
            {
                Localgrid.ColumnDefinitions.Add(new ColumnDefinition()); //voegt per kolom een ColumnDefinition(xaml)
            }
            for (int i = 0; i < row; i++)               //loopt door de rijen (links naar rechts)
            {
                Localgrid.RowDefinitions.Add(new RowDefinition());  //voegt per rij een RowDefinition(xaml)
            }
        }
    }
}
