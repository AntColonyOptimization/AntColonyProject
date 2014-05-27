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
            double max = 0;
            foreach (var pheromonRow in pheromones)
            {
                max = (pheromonRow.Max() > max) ? pheromonRow.Max() : max;
            }
            foreach (var pheromonRow in pheromones)
            {
                foreach (var pheromon in pheromonRow)
                {
                    if (pheromon > 0)
                    {
                        var rec = new Rectangle();
                        //var brush = new SolidColorBrush { Color = Color.FromScRgb(1,255 * (float)pheromon, 0, 0) };
                        var brush = new SolidColorBrush { Color = Color.FromScRgb((float)(pheromon/max), 255, 0, 0) };
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

        public void UpdateCurrentPaths(IEnumerable<List<Coordinates>> currentPaths,int pathNumber = 0)
        {
            if (pathNumber != 0)
            {
                if(pathNumber!=1)
                {
                    DrawLine(currentPaths.ElementAt(pathNumber-2), Color.FromScRgb(1, 0, 255, 255));
                }
                else 
                {
                    foreach (var pathsRow in currentPaths)
                    {
                        DrawLine(pathsRow, Color.FromScRgb(1, 0, 255, 255));
                        //c++;
                        //foreach (var path in pathsRow)
                        //{
                        //    var ellipse = new Ellipse {Width = CellWidth/3, Height = CellWidth/3};
                        //    var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 255) };
                        //    ellipse.Fill = brush;
                        //    ellipse.SetValue(Grid.ColumnProperty, path.Width);
                        //    ellipse.SetValue(Grid.RowProperty, path.Height);
                        //    MapControlGrid.Children.Add(ellipse);
                        //    i++;
                        //}
                        //i = 0;
                        //j++;
                    }
                }
            }
        }

        public void UpdateBestPath(IEnumerable<Coordinates> bestPath)
        {
            DrawLine(bestPath, Color.FromScRgb(1, 0, 255, 0));
            ////double x1 = 0;
            ////double y1 = 0;
            //var lastCoordinates = new Coordinates(0,0);
            //foreach (var path in bestPath)
            //{
            //    var rec = new Rectangle();
            //    var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 0) };
            //    rec.Fill = brush;
            //    rec.SetValue(Grid.ColumnProperty, path.Width);
            //    rec.SetValue(Grid.RowProperty, path.Height);
            //    MapControlGrid.Children.Add(rec);

            //    //var newLine = new Line();

            //    //newLine.X1 = x1;
            //    //newLine.Y1 = y1;

            //    //newLine.SetValue(Grid.ColumnProperty, path.Width);
            //    //newLine.SetValue(Grid.RowProperty, path.Height);
            //    //newLine.X2 = path.Width*CellWidth;
            //    //newLine.Y2 = path.Height*CellWidth;
            //    //var brush = new SolidColorBrush { Color = Color.FromScRgb(1, 0, 255, 0) };
            //    //newLine.StrokeThickness = 2;
            //    //newLine.Stroke = brush;
            //    //MapControlGrid.Children.Add(newLine);

            //    //x1 = newLine.X2;
            //    //y1 = newLine.Y2;
            //}
        }

        private void DrawLine(IEnumerable<Coordinates> coordinateses, Color color)
        {
            var path = coordinateses as Coordinates[] ?? coordinateses.ToArray();
            for (int i = 0; i < path.Count(); i++)
            {
                var line = new Line();
                if (i == 0)
                {
                    line.X1 = CellWidth/2;
                    line.Y1 = CellWidth/2;
                }
                else
                {
                    if (path[i - 1].Height < path[i].Height)
                        line.Y1 = 0;
                    else if (path[i - 1].Height == path[i].Height)
                        line.Y1 = CellWidth/2;
                    else if (path[i - 1].Height > path[i].Height)
                        line.Y1 = CellWidth;

                    if (path[i - 1].Width < path[i].Width)
                        line.X1 = 0;
                    else if (path[i - 1].Width == path[i].Width)
                        line.X1 = CellWidth / 2;
                    else if (path[i - 1].Width > path[i].Width)
                        line.X1 = CellWidth;

                }
                if (i < path.Count()-1)
                {
                    if (path[i + 1].Height < path[i].Height)
                        line.Y2 = 0;
                    else if (path[i + 1].Height == path[i].Height)
                        line.Y2 = CellWidth / 2;
                    else if (path[i + 1].Height > path[i].Height)
                        line.Y2 = CellWidth;

                    if (path[i + 1].Width < path[i].Width)
                        line.X2 = 0;
                    else if (path[i + 1].Width == path[i].Width)
                        line.X2 = CellWidth / 2;
                    else if (path[i + 1].Width > path[i].Width)
                        line.X2 = CellWidth;
                }
                else
                {
                    line.X2 = CellWidth / 2;
                    line.Y2 = CellWidth / 2;
                }
                line.SetValue(Grid.ColumnProperty, path[i].Width);
                line.SetValue(Grid.RowProperty, path[i].Height);
                var brush = new SolidColorBrush { Color = color };
                line.StrokeThickness = 2;
                line.Stroke = brush;
                MapControlGrid.Children.Add(line);
            }
        }

        //private void Calculat

        private double CalculateCellWidth(Map map)
        {
            var height = Height / map.Height;
            var width = Width / map.Width;
            return height <= width ? height : width;
        }
    }
}
