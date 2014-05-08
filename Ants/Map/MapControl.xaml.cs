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
using Ants.Map.Controls;


namespace Ants.Map
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public double CellWidth { get; private set; }

        private MapInput _input;

        public MapControl(MapInput input)
        {
            InitializeComponent();
            _input = input; 
            LoadMapView();
        }

        public void UpdateMapControl(IOutputService output)// : this()
        {
            if (_input.ShowPheromones)
                UpdatePheromones(output.Pheromones);
            if (_input.ShowBestPath)
                UpdateBestPath(output.BestPath);
            if (_input.ShowCurrentPaths)
                UpdateCurrentPaths(output.CurrentPaths);
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
            var map = MapGenerator.Instance.GetMap();

            CreateMazeGrid(map);
            int i = 0, j = 0;
            foreach (var row in map.MapDescription)
            {
                foreach (var cell in row)
                {
                    CrateOneField(i, j, cell);
                    i++;
                }
                i = 0;
                j++;
            }
        }

        private void CrateOneField(int i, int j, char symbol)
        {
            UserControl field;
            switch (symbol)
            {
                case MapGenerator.SymbolObstacle:
                    field = new ObstacleControl(i, j);
                    break;

                case MapGenerator.SymbolStart:
                    field = new StartControl(i, j);
                    break;

                case MapGenerator.SymbolDestination:
                    field = new DestinationControl(i, j);
                    break;

                default:
                    return;
            }

            field.SetValue(Grid.ColumnProperty, i);
            field.SetValue(Grid.RowProperty, j);
            MapControlGrid.Children.Add(field);
        }

        private void UpdatePheromones(List<List<double>> pheromones)
        {
            int i = 0, j = 0;
            foreach (var pheromonRow in pheromones)
            {
                foreach (var pheromon in pheromonRow)
                {
                    if (pheromon > 0)
                    {
                        var rec = new Rectangle();
                        var brush = new SolidColorBrush {Color = Color.FromScRgb((float) pheromon, 255, 0, 0)};
                        rec.Fill = brush;
                        rec.SetValue(Grid.ColumnProperty, i);
                        rec.SetValue(Grid.RowProperty, j);
                        MapControlGrid.Children.Add(rec);
                    }
                    i++;
                }
                i = 0;
                j++;
            }
        }

        private void UpdateCurrentPaths(List<List<Coordinates>> currentPaths)
        {
            int i = 0, j = 0;
            foreach (var pathsRow in currentPaths)
            {
                foreach (var path in pathsRow)
                {
                    var rec = new Rectangle();// {Width = path.Width, Height = path.Height};
                    var brush = new SolidColorBrush { Color = Color.FromScRgb((float)0.7,0, 255, 255) };
                    rec.Fill = brush;
                    rec.SetValue(Grid.ColumnProperty, path.Width);
                    rec.SetValue(Grid.RowProperty, path.Height);
                    MapControlGrid.Children.Add(rec);
                    i++;
                }
                i = 0;
                j++;
            }
        }

        private void UpdateBestPath(List<Coordinates> bestPath)
        {
            foreach (var path in bestPath)
            {
                var rec = new Rectangle();// {Width = path.Width, Height = path.Height};
                var brush = new SolidColorBrush { Color = Color.FromScRgb((float)0.7,0, 255, 0) };
                rec.Fill = brush;
                rec.SetValue(Grid.ColumnProperty, path.Width);
                rec.SetValue(Grid.RowProperty, path.Height);
                MapControlGrid.Children.Add(rec);
            }
        }

        private void CreateMazeGrid(Map map)
        {
            //MapControlGrid = new Grid();
            CellWidth = CalculateCellWidth(map);

            for (var c = 0; c < map.Width; c++)
            {
                MapControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CellWidth) });
            }

            for (var l = 0; l < map.Height; l++)
            {
                MapControlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellWidth) });
            }
        }

        private double CalculateCellWidth(Map map)
        {
            var height = Height / map.Height;
            var width = Width / map.Width;
            return height <= width ? height : width;
        }
    }
}
