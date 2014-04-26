using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class MapBase
    {
        #region Fields
        
        /// <summary>
        /// Szerokość siatki.
        /// </summary>
        private int _mapWidth = 10;
        public int MapWidth
        {
            get { return _mapWidth; }
            private set { _mapWidth = value; }
        }

        /// <summary>
        /// Wysokość siatki.
        /// </summary>
        private int _mapHeight = 10;
        public int MapHeight
        {
            get { return _mapHeight; }
            private set { _mapHeight = value; }
        }

        /// <summary>
        /// Szerokość komórki.
        /// </summary>
        private int _cellWidth = 60;
        public int CellWidth
        {
            get { return _cellWidth; }
            private set { _cellWidth = value; }
        }

        //private double _mapDensity;

        //List<Glass> glassList = new List<Glass>();
        //private List<Glass> _glasses;

        /// <summary>
        /// Tablica reprezentująca strukturę mapy;
        /// </summary>
        private char [,] _mazeDescription;

        //char[,] mazeValues = new char[_mazeWidth, _mazeHeight];
        //Glass[,] mazeGlasses = new Glass[_mazeWidth, _mazeHeight];

        #endregion

        #region Constructors
        public MapBase()
        {
            //CreateMazeGrid();

            //LoadMaze();
            
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

        //private void LoadMaze(string fileName = "def1")
        //{
        //    //_glasses = new List<Glass>();

        //    _mazeDescription = new char[MapWidth, MapHeight];

        //    //for (var i = 0; i < mazeGlasses.GetLength(0); i++)
        //    //{
        //    //    for (var j = 0; j < mazeGlasses.GetLength(1); j++)
        //    //    {
        //    //        mazeGlasses[i, j] = null;
        //    //    }
        //    //}

        //    for (var i = 0; i < _mazeDescription.GetLength(0); i++)
        //    {
        //        for (var j = 0; j < _mazeDescription.GetLength(1); j++)
        //        {
        //            _mazeDescription[i, j] = '0';
        //        }
        //    }


        //    var filePath = string.Format(@"{0}.txt", fileName);

        //    using (var sr = new StreamReader(filePath))
        //    {
        //        var l = 0;
        //        while (!sr.EndOfStream)
        //        {
        //            string line = sr.ReadLine();
                    
        //            //jeżeli linijka zaczyna się od # (komentarz) to przejdź do następnej
        //            if (line.StartsWith("#"))
        //                continue;

        //            //jeżeli zadeklarowana gęstość siatki jest inna niż zadeklarowana 
        //            if (line.Length != MapWidth)
        //                throw new NotImplementedException();

        //            for (var c = 0; c < line.Length; c++)
        //            {
        //                mazeValues[c, l] = line[c];

        //                // przeszkody
        //                if (mazeValues[c, l] == '1')
        //                {
        //                    var glass = new Glass();
        //                    glass.SetValue(Grid.ColumnProperty, c);
        //                    glass.SetValue(Grid.RowProperty, l);
        //                    grdMaze.Children.Add(glass);
        //                    mazeGlasses[c, l] = glass;
        //                }

        //                //start (mrowisko)
        //                else if (mazeValues[c, l] == 'A')
        //                {
        //                    //snail.OriginalCellPoint = new Point(c, l);
        //                }

        //                //end (jedzenie)
        //                else if (mazeValues[c, l] == 'B')
        //                {
        //                    //snail.OriginalCellPoint = new Point(c, l);
        //                }
        //            }
        //            l++;
        //        }
        //    }

        //    for (var c = 0; c < mazeWidth; c++)
        //    {
        //        for (var l = 0; l < mazeHeight; l++)
        //        {
        //            var topValue = ' ';
        //            var bottomValue = ' ';
        //            var leftValue = ' ';
        //            var rightValue = ' ';

        //            if (l > 0)
        //                topValue = mazeValues[c, l - 1];
        //            if (l < mazeHeight - 1)
        //                bottomValue = mazeValues[c, l + 1];
        //            if (c > 0)
        //                leftValue = mazeValues[c - 1, l];
        //            if (c < mazeWidth - 1)
        //                rightValue = mazeValues[c + 1, l];

        //            var glass = mazeGlasses[c, l];
        //            if (glass != null)
        //            {
        //                glass.LeftValue = leftValue;
        //                glass.RightValue = rightValue;
        //                glass.TopValue = topValue;
        //                glass.BottomValue = bottomValue;
        //            }
        //        }
        //    }
        //}

        //private void InitMaze()
        //{
        //    //Storyboard sbStart = this.FindResource("sbStart") as Storyboard;
        //    //sbStart.Begin();

        //    var sbLevel = this.FindResource("sbStart") as Storyboard;
        //    sbLevel.Begin();
        //}
    }
}
