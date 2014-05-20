namespace IOService
{
    using IOService.Core;
    using System.Windows.Controls;

    public delegate void RunAlgorithmEventHandler(IInputService input);

    public partial class InputView : UserControl
    {
        private IInputService Input { get; set; }

        public event RunAlgorithmEventHandler RunAlgorithm;
        public event RunAlgorithmEventHandler StepAlgorithm;
        public event RunAlgorithmEventHandler ResetAlgorithm;

        public InputView()
        {
            InitializeComponent();

            Input = new InputService();
            DataGrid.DataContext = Input;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(RunAlgorithm != null)
            {
                RunAlgorithm(Input);
            }
        }


        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if(StepAlgorithm != null)
            {
                StepAlgorithm(Input);
            }
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ResetAlgorithm != null)
            {
                ResetAlgorithm(Input);
            }
        }
    }
}
