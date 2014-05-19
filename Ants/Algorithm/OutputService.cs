namespace Ants
{
    using System.Collections.Generic;

    public class OutputService : IOutputService
    {
        public List<List<double>> Pheromones { get; set; }
        public List<List<Coordinates>> CurrentPaths { get; set; }
        public List<Coordinates> BestPath { get; set; }
        public int CurrentIteration { get; set; }
    }
}
