namespace IOService
{
    using IOService.Core;
    using System.Windows.Controls;

    public partial class InputView : UserControl
    {
        private IInputService _inputService;

        public InputView()
        {
            InitializeComponent();

            _inputService = new InputService();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
