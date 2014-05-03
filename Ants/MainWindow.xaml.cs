namespace Ants
{
    using System;
    using System.Windows;
    using MapGenerator;
    using IOService;
    using IOService.Core;

    public partial class MainWindow : Window
    {
        private MapControl _mapControl = new MapControl();
        private InputView _inputView = new InputView();

        public MainWindow()
        {
            InitializeComponent();

            _inputView.RunAlgorithm += RunAlgorithm;

            //Algorithm a = new Algorithm();
            //a.Execute();
            //var map = this.FindResource("MapViewGrid") as Grid;
            //map.
            //sbLevel.Begin();
            MapViewGrid.Children.Add(_mapControl);
            MapViewGrid.Width = _mapControl.Width;
            ConfigGrid.Children.Add(_inputView);
            ConfigGrid.Width = _inputView.Width;
        }

        void RunAlgorithm(IInputService input)
        {
            Algorithm algorithm = new Algorithm(input);

            algorithm.Execute();
        }
    }
}
