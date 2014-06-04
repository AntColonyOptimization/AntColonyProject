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
        
        //public string MapPath
        //{
        //    get { return _mapPath; }
        //    set 
        //    {
        //        if (ValidateMapPath(value))
        //        {
        //            _mapPath = value;
        //            _map = new Map();
        //            ReadMapFromFile();
        //        }
        //    }
        //}

        //private Map _map;
        //private char [,] MapDescription;

        //public List<List<char>> Map = new List<List<char>>();

        private static MapGenerator _instance;

        public MapGenerator() { }

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

        public Map ReadMapFromFile(string path)
        {
            string[] linesArray = File.ReadAllLines(path);
            var map = new Map();
            var lines = linesArray.Where(line => !line.StartsWith("#")).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                map.MapDescription.Add(new List<char>());
                //zamiana kilkukrotnych spacji na pojedyńcze (jak się komuś wpisze za dużo przypadkiem)
                lines[i] = System.Text.RegularExpressions.Regex.Replace(lines[i], @"\s+", " ");
                string[] line = lines[i].Split(' ');
                map.Height = lines.Count;
                map.Width = line.Count();
                
                for (int j = 0; j < line.Length; j++)
                {
                    map.MapDescription[i].Add(char.Parse(line[j]));
                    if (map.MapDescription[i][j] == MapSymbols.SymbolDestination)
                    {
                        map.Destination = new Coordinates(i, j);
                    }
                    if (map.MapDescription[i][j] == MapSymbols.SymbolStart)
                    {
                        map.Start = new Coordinates(i, j);
                    }
                }
            }
            return map;
        }

        public bool ValidateMapPath(string newPath)
        {
            return File.Exists(newPath);
        }
    }
}
