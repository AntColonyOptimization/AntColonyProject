namespace IOService
{
    using IOService.Core;

    public class InputService : IInputService
    {
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Rho { get; set; }
        public double Q { get; set; }
        public int NumberOfIterations { get; set; }
        public int NumberOfAnts { get; set; }

        public bool CheckBox1 { get; set; }
        public bool CheckBox2 { get; set; }
        public bool CheckBox3 { get; set; }
        public bool CheckBox4 { get; set; }
        public bool CheckBox5 { get; set; }
        public bool CheckBox6 { get; set; }

        public InputService()
        {
            Alpha = 3.0;
            Beta = 10.0;
            Rho = 0.01;
            Q = 50.0;
            NumberOfIterations = 100;
            NumberOfAnts = 3;
        }
    }
}
