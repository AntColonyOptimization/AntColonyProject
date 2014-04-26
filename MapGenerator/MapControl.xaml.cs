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
using MapGenerator.Controls;

namespace MapGenerator
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public double CellWidth { get; private set; }

        private MapBase _map = new MapBase();

        public MapControl()
        {
            InitializeComponent();
            //CellWidth = CalculateCellWidth();
            CreateMazeGrid();
            RefreshMapView();
        }

        private void RefreshMapView()
        {
            for (var i = 0; i < _map.MapWidth; i++)
            {
                for (var j = 0; j < _map.MapHeight; j++)
                {
                    // przeszkody
                    if (_map.MapDescription[i, j] == '1')
                    {
                        var glass = new Glass(i, j);
                        glass.SetValue(Grid.ColumnProperty, i);
                        glass.SetValue(Grid.RowProperty, j);
                        MapControlGrid.Children.Add(glass);
                    }

                    ////start (mrowisko)
                    //else if (mazeValues[c, l] == 'A')
                    //{
                    //    //snail.OriginalCellPoint = new Point(c, l);
                    //}

                    ////end (jedzenie)
                    //else if (mazeValues[c, l] == 'B')
                    //{
                    //    //snail.OriginalCellPoint = new Point(c, l);
                    //}
                }
            }
        }

        private void CreateMazeGrid()
        {
            //MapControlGrid = new Grid();
            CellWidth = CalculateCellWidth();

            for (var c = 0; c < _map.MapWidth; c++)
            {
                MapControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CellWidth) });
            }

            for (var l = 0; l < _map.MapHeight; l++)
            {
                MapControlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellWidth) });
            }
        }

        private double CalculateCellWidth()
        {
            var height = Height / _map.MapHeight;
            var width = Width / _map.MapWidth;
            return height <= width ? height : width;
        }
    }
}
