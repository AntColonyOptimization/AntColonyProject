using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants.Map
{
    public class MapGenerator
    {
        public const char SymbolStart = 'S';
        public const char SymbolDestination = 'F';
        public const char SymbolObstacle = 'x';
        //private static char _symbolFreeToGo = '0';
        //private static string mapCatalogPath = @"../../../MapGenerator/MapSource/map.txt";

        private string _mapPath = @"Map\Source\map.txt";
        public string MapPath
        {
            get { return _mapPath; }
            set 
            {
                if (ValidateMapPath(value))
                {
                    _mapPath = value;
                    _map = new Map();
                    ReadMapFromFile();
                }
            }
        }

        private Map _map;
        //private char [,] MapDescription;

        //public List<List<char>> Map = new List<List<char>>();

        private static MapGenerator _instance;

        private MapGenerator() { }

        public static MapGenerator Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new MapGenerator();
                }
                return _instance;
            }
        }

        private void ReadMapFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(MapPath);

            for (int i = 0; i < lines.Length; i++)
            {
                _map.MapDescription.Add(new List<char>());
                string[] line = lines[i].Split(' ');
                _map.Height = lines.Length;
                _map.Width = line.Length;
                for (int j = 0; j < line.Length; j++)
                {
                    if ((line[0] != "#")) //to skip comment lines
                    {
                        _map.MapDescription[i].Add(char.Parse(line[j]));
                        if (_map.MapDescription[i][j] == SymbolDestination)
                        {
                            _map.Destination = new Coordinates(j, i);
                        }
                        if (_map.MapDescription[i][j] == SymbolStart)
                        {
                            _map.Start = new Coordinates(j, i);
                        }
                    }
                }
            }
        }

        public Map GetMap()
        {
            if (_map != null)
                return _map;

            _map = new Map();
            ReadMapFromFile();

            return _map;
        }

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

        private bool ValidateMapPath(string newPath)
        {
            return File.Exists(newPath);
        }
    }
}
