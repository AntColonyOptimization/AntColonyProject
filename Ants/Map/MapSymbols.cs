using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants.Map
{
    public static class MapSymbols
    {
        public const char SymbolStart = 'S';
        public const char SymbolDestination = 'F';
        public const char SymbolObstacle = 'x';
        public const char SymbolFreeField = '0';
        
        public const char SymbolIgnore = ' ';
        //private static char _symbolFreeToGo = '0';
        //private static string mapCatalogPath = @"../../../MapGenerator/MapSource/map.txt";

        //public string _mapPath = @"Map\Source\map.txt";
    }
}
