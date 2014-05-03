namespace IOService
{
    using IOService.Core;
    using System.Windows.Controls;

    public partial class InputView : UserControl
    {
        private IInputService Input { get; set; }

        public InputView()
        {
            InitializeComponent();

            Input = new InputService();
            DataGrid.DataContext = Input;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
