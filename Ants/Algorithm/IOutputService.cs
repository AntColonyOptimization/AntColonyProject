namespace Ants
{
    using System.Collections.Generic;

    public interface IOutputService
    {
        List<List<double>> Pheromones { get; set; }
        List<List<Coordinates>> CurrentPaths { get; set; }
        List<Coordinates> BestPath { get; set; }
        int CurrentIteration { get; set; }
    }
}
