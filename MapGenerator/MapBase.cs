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

        //public double CellWidth { get; private set; }

        //private double _mapDensity;

        //List<Glass> glassList = new List<Glass>();
        //private List<Glass> _glasses;

        /// <summary>
        /// Tablica reprezentująca strukturę mapy;
        /// </summary>
        public char [,] MapDescription;

        //char[,] mazeValues = new char[_mazeWidth, _mazeHeight];
        //Glass[,] mazeGlasses = new Glass[_mazeWidth, _mazeHeight];

        #endregion

        #region Constructors
        public MapBase()
        {
            //CreateMazeGrid();

            LoadMaze();

            //InitMaze();
        }

        #endregion

        private void CreateMazeGrid()
        {
            //for (var c = 0; c < _mazeWidth; c++)
            //{
            //    grdMaze.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(_cellWidth) });
            //}

            //for (var l = 0; l < _mazeHeight; l++)
            //{
            //    grdMaze.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(_cellWidth) });
            //}
        }

        public void LoadMaze(string fileName = "defMap.txt")
        {
            //_glasses = new List<Glass>();
            var tempMap = new List<List<char>>();

            //_mazeDescription = new char[MapWidth, MapHeight];

            //for (var i = 0; i < mazeGlasses.GetLength(0); i++)
            //{
            //    for (var j = 0; j < mazeGlasses.GetLength(1); j++)
            //    {
            //        mazeGlasses[i, j] = null;
            //    }
            //}

            //for (var i = 0; i < _mazeDescription.GetLength(0); i++)
            //{
            //    for (var j = 0; j < _mazeDescription.GetLength(1); j++)
            //    {
            //        _mazeDescription[i, j] = '0';
            //    }
            //}

            //wczytywanie z pliku
            var filePath = string.Format(fileName);

            using (var sr = new StreamReader(filePath))
            {
                var l = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    
                    //jeżeli linijka zaczyna się od # (komentarz) to przejdź do następnej
                    if (line == null || line.StartsWith("#"))
                        continue;

                    //jeżeli zadeklarowana gęstość siatki jest inna niż zadeklarowana 
                    //if (line.Length != MapWidth)
                    //    throw new NotImplementedException();
                    tempMap.Add(line.ToList());
                }
            }

            //CellWidth = CalculateCellWidth(tempMap);
            MapWidth = tempMap.First().Count;
            MapHeight = tempMap.Count;

            MapDescription = new char[MapWidth, MapHeight];

            int i = 0, j = 0;
            foreach (var row in tempMap)
            {
                foreach (var cell in row)
                {
                    MapDescription[i, j] = cell;
                    i++;
                }
                i = 0;
                j++;
            }

            //mazeValues[c, l] = line[c];

            

            //for (var c = 0; c < mazeWidth; c++)
            //{
            //    for (var l = 0; l < mazeHeight; l++)
            //    {
            //        var topValue = ' ';
            //        var bottomValue = ' ';
            //        var leftValue = ' ';
            //        var rightValue = ' ';

            //        if (l > 0)
            //            topValue = mazeValues[c, l - 1];
            //        if (l < mazeHeight - 1)
            //            bottomValue = mazeValues[c, l + 1];
            //        if (c > 0)
            //            leftValue = mazeValues[c - 1, l];
            //        if (c < mazeWidth - 1)
            //            rightValue = mazeValues[c + 1, l];

            //        var glass = mazeGlasses[c, l];
            //        if (glass != null)
            //        {
            //            glass.LeftValue = leftValue;
            //            glass.RightValue = rightValue;
            //            glass.TopValue = topValue;
            //            glass.BottomValue = bottomValue;
            //        }
            //    }
            //}
        }

        

        //private void InitMaze()
        //{
        //    //Storyboard sbStart = this.FindResource("sbStart") as Storyboard;
        //    //sbStart.Begin();

        //    var sbLevel = this.FindResource("sbStart") as Storyboard;
        //    sbLevel.Begin();
        //}
    }
}
