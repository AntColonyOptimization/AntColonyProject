using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants.Map
{
    public class Map
    {
        public List<List<char>> MapDescription = new List<List<char>>();
        public int Width { get; set; }
        public int Height { get; set; }

        public Coordinates Start;

        public Coordinates Destination;

    }
}
