using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    public class OutputAlgorithm
    {
        private List<List<double>> pheromones;
        private List<List<Coordinates>> currentPaths;
        private List<Coordinates> bestPath;

        public List<List<double>> Pheromones
        {
            get
            {
                return this.pheromones;
            }
            set 
            {
                pheromones = value;
            }
        }
        public List<List<Coordinates>> CurrentPaths
        {
            get
            {
                return this.currentPaths;
            }
            set 
            { 
                currentPaths = value; 
            }
        }
        public List<Coordinates> BestPath
        {
            get
            {
                return this.bestPath;
            }
            set 
            {
                bestPath = value; 
            }
        }

        public OutputAlgorithm(List<List<double>> _pheromones, List<List<Coordinates>> _currentPaths, List<Coordinates> _bestPath)
        {
            this.Pheromones = _pheromones;
            this.CurrentPaths = _currentPaths;
            this.BestPath = _bestPath;
        }


    }
}
