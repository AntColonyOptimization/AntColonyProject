namespace IOService.Core
{
    public interface IInputService
    {
        double Alpha { get; set; }
        double Beta { get; set; }
        double Rho { get; set; }
        double Q { get; set; }
        int NumberOfIterations { get; set; }
        int NumberOfAnts { get; set; }

        bool ACS { get; set; }
        bool AsRank { get; set; }
        bool DeleteLoops{ get; set; }
    }
}
