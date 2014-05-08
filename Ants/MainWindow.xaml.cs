using System.Windows.Controls;

namespace Ants
{
    using System;
    using System.Windows;
    using IOService;
    using IOService.Core;
    using System.Threading.Tasks;
    using Ants.Map;

    public partial class MainWindow : Window
    {
        private const int _delay = 10;

        private IAlgorithm _algorithm;
        private IInputService _input;

        private MapControl _mapControl;// = new MapControl();
        private MapInput _mapInput = new MapInput();
        private InputView _inputView = new InputView();

        public MainWindow()
        {
            InitializeComponent();

            _inputView.RunAlgorithm += RunAlgorithm;
            _inputView.StepAlgorithm += StepAlgorithm;


            //Algorithm a = new Algorithm();
            //a.Execute();
            //var map = this.FindResource("MapViewGrid") as Grid;
            //map.
            //sbLevel.Begin();
            MapConfigGrid.Children.Add(_mapInput);

            _mapControl = new MapControl(_mapInput);
            MapViewGrid.Children.Add(_mapControl);
            MapViewGrid.Width = _mapControl.Width;
            
            ConfigGrid.Children.Add(_inputView);
            ConfigGrid.Width = _inputView.Width;
        }

        private async void RunAlgorithm(IInputService input)
        {
            if (_input == null || _algorithm == null || _input != input)
            {
                _input = input;
                _algorithm = new Algorithm(input);
            }

            IOutputService output;
            while (!_algorithm.IsFinished())
            {
                output = _algorithm.Execute();
                UpdateState(output);
                await Task.Delay(_delay);
            }
        }

        private void StepAlgorithm(IInputService input)
        {
            if (input == null || _algorithm == null || _input != input)
            {
                _input = input;
                _algorithm = new Algorithm(input);
            }

            IOutputService output;
            if(!_algorithm.IsFinished())
            {
                output = _algorithm.Execute();
                UpdateState(output);
            }
        }

        private void UpdateState(IOutputService output)
        {
            _mapControl = new MapControl(_mapInput);
            _mapControl.UpdateMapControl(output);
            //_mapControl.UpdatePheromones(output.Pheromones);
            //_mapControl.LoadMapView();
            MapViewGrid.Children.Add(_mapControl);
        }
    }
}
