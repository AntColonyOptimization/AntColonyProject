using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MapGenerator.Controls;

namespace MapGenerator
{
    public class MapBase
    {
        #region Fields

        public int MapWidth { get; private set; }

        public int MapHeight { get; private set; }

        private string _mapPath = @"MapSource\defMap.txt";
        public string MapPath
        {
            get { return _mapPath; }
            set 
            {
                if (ValidateMapPath(value))
                {
                    _mapPath = value;
                    LoadMapFromFile();
                }
            }
        }

        private static char _symbolStart = 'S';
        private static char _symbolDestination = 'F';
        private static char _symbolObstacle = 'x';
        private static char _symbolFreeToGo = '0';
        //private static string _mapCatalogPath = @"../../../MapGenerator/MapSource/map.txt";

        /// <summary>
        /// Tablica reprezentująca strukturę mapy;
        /// </summary>
        private char [,] MapDescription;

        public List<List<char>> Map = new List<List<char>>();

        #endregion

        #region Constructors
        public MapBase()
        {
            LoadMapFromFile();
        }

        #endregion

        //private void CreateMazeGrid()
        //{
        //    //for (var c = 0; c < _mazeWidth; c++)
        //    //{
        //    //    grdMaze.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(_cellWidth) });
        //    //}

        //    //for (var l = 0; l < _mazeHeight; l++)
        //    //{
        //    //    grdMaze.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(_cellWidth) });
        //    //}
        //}

        //private void LoadMapFromFile()
        //{
        //    Map.Clear();


        //    //wczytywanie z pliku
        //    using (var sr = new StreamReader(_mapPath))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string line = sr.ReadLine();
                    
        //            //jeżeli linijka zaczyna się od # (komentarz) to przejdź do następnej
        //            if (line == null || line.StartsWith("#"))
        //                continue;

        //            //jeżeli zadeklarowana gęstość siatki jest inna niż zadeklarowana 
        //            //if (line.Length != MapWidth)
        //            //    throw new NotImplementedException();
        //            Map.Add(line.ToList());
        //        }
        //    }

        //    //CellWidth = CalculateCellWidth(tempMap);
        //    MapWidth = Map.First().Count;
        //    MapHeight = Map.Count;

        //    MapDescription = new char[MapWidth, MapHeight];

        //    int i = 0, j = 0;
        //    foreach (var row in Map)
        //    {
        //        foreach (var cell in row)
        //        {
        //            MapDescription[i, j] = cell;
        //            i++;
        //        }
        //        i = 0;
        //        j++;
        //    }
        //}

        private void ReadMapFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(_mapPath);

            for (int i = 0; i < lines.Length; i++)
            {
                Map.Add(new List<char>());
                string[] line = lines[i].Split(' ');
                MapHeight = lines.Length;
                MapWidth = line.Length;
                for (int j = 0; j < line.Length; j++)
                {
                    if ((line[0] != "#")) //to skip comment lines
                    {
                        Map[i].Add(char.Parse(line[j]));
                        if (Map[i][j] == _symbolDestination)
                        {
                            destination = new Coordinates(j, i);
                        }
                        if (map[i][j] == symbolStart)
                        {
                            start = new Coordinates(j, i);
                        }
                    }
                }
            }
        }

        private bool ValidateMapPath(string newPath)
        {
            ///TODO: dopisać walidacje ścieżki mapy
            return true;
        }
    }
}
