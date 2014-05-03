using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MapGenerator;
using IOService;

namespace Ants
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapControl _mapControl = new MapControl();
        private InputView _inputView = new InputView();

        public MainWindow()
        {
            InitializeComponent();
<<<<<<< HEAD

            this.Width = _mapControl.Width + _inputView.Width;
            this.Height = Math.Max(_mapControl.Height, _inputView.Height);

=======
            //Algorithm a = new Algorithm();
            //a.Execute();
>>>>>>> 7042bb26f97e8b01e6a47a29123be6139e6d19a9
            //var map = this.FindResource("MapViewGrid") as Grid;
            //map.
            //sbLevel.Begin();
            MapViewGrid.Children.Add(_mapControl);
            MapViewGrid.Width = _mapControl.Width;
            ConfigGrid.Children.Add(_inputView);
            ConfigGrid.Width = _inputView.Width;
        }
    }
}
