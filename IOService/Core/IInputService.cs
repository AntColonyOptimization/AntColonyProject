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

        bool CheckBox1 { get; set; }
        bool CheckBox2 { get; set; }
        bool CheckBox3 { get; set; }
        bool CheckBox4 { get; set; }
        bool CheckBox5 { get; set; }
        bool DeleteLoops{ get; set; }
    }
}
