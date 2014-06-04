namespace IOService
{
    using IOService.Core;
    using System.Windows.Controls;

    public delegate void InputEventHandler(IInputService input);

    public partial class InputView : UserControl
    {
        private bool _algorithmEnabled = false;

        private IInputService Input { get; set; }

        public event InputEventHandler RunAlgorithm;
        public event InputEventHandler PauseAlgorithm;
        public event InputEventHandler StepAlgorithm;
        public event InputEventHandler ResetAlgorithm;

        public InputView()
        {
            InitializeComponent();

            Input = new InputService();
            DataGrid.DataContext = Input;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_algorithmEnabled)
            {
                if (RunAlgorithm != null)
                {
                    RunAlgorithm(Input);
                    _algorithmEnabled = true;
                    run_pauseButton.Content = "Pauza";
                }
            }
            else
            {
                if(PauseAlgorithm != null)
                {
                    PauseAlgorithm(Input);
                    _algorithmEnabled = false;
                    run_pauseButton.Content = "Uruchom algorytm";
                }
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
