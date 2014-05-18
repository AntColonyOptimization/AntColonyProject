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

        public MapControl()
        {
            InitializeComponent();
        }

        private void CreateMazeGrid(Map map)
        {
            MapControlGrid.Children.Clear();
            MapControlGrid.ColumnDefinitions.Clear();
            MapControlGrid.RowDefinitions.Clear();

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

        public void LoadMapView(Map map)
        {
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
                case MapSymbols.SymbolObstacle:
                    field = new ObstacleControl(i, j);
                    break;

                case MapSymbols.SymbolFreeField:
                    field = new FreeFieldControl(i, j);
                    break;

                case MapSymbols.SymbolStart:
                    field = new StartControl(i, j);
                    break;

                case MapSymbols.SymbolDestination:
                    field = new DestinationControl(i, j);
                    break;

                default:
                    return;
            }

            field.SetValue(Grid.ColumnProperty, i);
            field.SetValue(Grid.RowProperty, j);
            MapControlGrid.Children.Add(field);
        }


        public void UpdatePheromones(IEnumerable<List<double>> pheromones)
        {
            int i = 0, j = 0;
            foreach (var pheromonRow in pheromones)
            {
                foreach (var pheromon in pheromonRow)
                {
                    if (pheromon > 0)
                    {
                        var rec = new Rectangle();
                        //var brush = new SolidColorBrush { Color = Color.FromScRgb(1,255 * (float)pheromon, 0, 0) };
                        var brush = new SolidColorBrush { Color = Color.FromScRgb((float)pheromon, 255, 0, 0) };
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

        public void UpdateCurrentPaths(IEnumerable<List<Coordinates>> currentPaths)
        {
            int i = 0, j = 0;
            foreach (var pathsRow in currentPaths)
            {
                foreach (var path in pathsRow)
                {
                    var ellipse = new Ellipse {Width = CellWidth/3, Height = CellWidth/3};
                    var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 255) };
                    ellipse.Fill = brush;
                    ellipse.SetValue(Grid.ColumnProperty, path.Width);
                    ellipse.SetValue(Grid.RowProperty, path.Height);
                    MapControlGrid.Children.Add(ellipse);
                    i++;
                }
                i = 0;
                j++;
            }
        }

        public void UpdateBestPath(IEnumerable<Coordinates> bestPath)
        {
            //double x1 = 0;
            //double y1 = 0;
            foreach (var path in bestPath)
            {
                var rec = new Rectangle();
                var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 0) };
                rec.Fill = brush;
                rec.SetValue(Grid.ColumnProperty, path.Width);
                rec.SetValue(Grid.RowProperty, path.Height);
                MapControlGrid.Children.Add(rec);

                //var newLine = new Line();

                //newLine.X1 = x1;
                //newLine.Y1 = y1;

                //newLine.SetValue(Grid.ColumnProperty, path.Width);
                //newLine.SetValue(Grid.RowProperty, path.Height);
                //newLine.X2 = path.Width*CellWidth;
                //newLine.Y2 = path.Height*CellWidth;
                //var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 0) };
                //newLine.StrokeThickness = 2;
                //newLine.Stroke = brush;
                //MapControlGrid.Children.Add(newLine);

                //x1 = newLine.X2;
                //y1 = newLine.Y2;
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
