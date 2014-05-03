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

            this.Width = _mapControl.Width + _inputView.Width;
            this.Height = Math.Max(_mapControl.Height, _inputView.Height);

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
