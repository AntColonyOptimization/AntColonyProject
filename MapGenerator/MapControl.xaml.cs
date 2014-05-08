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

        //public MapBase _mapBase = new MapBase();

        public MapControl()
        {
            InitializeComponent();
            LoadMapView();
            //CellWidth = CalculateCellWidth();
            //CreateMazeGrid();
            //RefreshMapView();
        }

        //private void RefreshMapView()
        //{
        //    for (var i = 0; i < _mapBase.MapWidth; i++)
        //    {
        //        for (var j = 0; j < _mapBase.MapHeight; j++)
        //        {
        //            // przeszkody
        //            if (_mapBase.MapDescription[i, j] == '1')
        //            {
        //                var glass = new Glass(i, j);
        //                glass.SetValue(Grid.ColumnProperty, i);
        //                glass.SetValue(Grid.RowProperty, j);
        //                MapControlGrid.Children.Add(glass);
        //            }

        //            ////start (mrowisko)
        //            //else if (mazeValues[c, l] == 'A')
        //            //{
        //            //    //snail.OriginalCellPoint = new Point(c, l);
        //            //}

        //            ////end (jedzenie)
        //            //else if (mazeValues[c, l] == 'B')
        //            //{
        //            //    //snail.OriginalCellPoint = new Point(c, l);
        //            //}
        //        }
        //    }
        //}

        private void LoadMapView()
        {
            CreateMazeGrid();
            int i = 0, j = 0;
            foreach (var row in _mapBase.Map)
            {
                foreach (var cell in row)
                {
                    if (cell == '1')
                    {
                        var glass = new Glass(i, j);
                        glass.SetValue(Grid.ColumnProperty, i);
                        glass.SetValue(Grid.RowProperty, j);
                        MapControlGrid.Children.Add(glass);
                    }
                    i++;
                }
                i = 0;
                j++;
            }
        }

        private void CreateMazeGrid()
        {
            //MapControlGrid = new Grid();
            CellWidth = CalculateCellWidth();

            for (var c = 0; c < _mapBase.MapWidth; c++)
            {
                MapControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CellWidth) });
            }

            for (var l = 0; l < _mapBase.MapHeight; l++)
            {
                MapControlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellWidth) });
            }
        }

        private double CalculateCellWidth()
        {
            var height = Height / _mapBase.MapHeight;
            var width = Width / _mapBase.MapWidth;
            return height <= width ? height : width;
        }
    }
}
