using System.Windows.Controls;

namespace Ants
{
    using System;
    using System.Windows;
    using IOService;
    using IOService.Core;
    using System.Threading.Tasks;
    using Ants.Map;
    using Ants.Algorithm;


    public partial class MainWindow : Window
    {
        private bool _pause = false;
        private const int _delay = 10;

        private IAlgorithm _algorithm;
        private IInputService _input;

        private InputView _inputView = new InputView();

        
        private MapControl _mapControl;

        private readonly MapInput _mapInput;

        private readonly TheoreticalData _theory;

        private readonly Results _results;

        public MainWindow()
        {
            _results = new Results();
            _theory = new TheoreticalData();
            _mapControl = new MapControl();
            _mapInput = new MapInput(_mapControl);
            InitializeComponent();

            _inputView.RunAlgorithm += RunAlgorithm;
            _inputView.StepAlgorithm += StepAlgorithm;
            _inputView.ResetAlgorithm += ResetAlgorithm;
            _inputView.PauseAlgorithm += PauseAlgorithm;

            //Algorithm a = new Algorithm();
            //a.Execute();
            //var map = this.FindResource("MapViewGrid") as Grid;
            //map.
            //sbLevel.Begin();
            MapConfigGrid.Children.Add(_mapInput);

            MapViewGrid.Children.Add(_mapControl);
            MapViewGrid.Width = _mapControl.Width;
            
            ConfigGrid.Children.Add(_inputView);
            ConfigGrid.Width = _inputView.Width;

            TheoryGrid.Children.Add(_theory);
            TheoryGrid.Width = _theory.Width;

            ResultsGrid.Children.Add(_results);
        }

        void PauseAlgorithm(IInputService input)
        {
            _pause = true;
        }

        private async void RunAlgorithm(IInputService input)
        {
            _pause = false;

            if (_mapInput.Map == null)
            {
                MessageBox.Show("Nie wczytano mapy.");
                return;
            }
            if (_input == null || _algorithm == null || _input != input)
            {
                _input = input;
                _algorithm = new AlgorithmLogic(input, _mapInput.Map);
            }

            IOutputService output;
            while (!_algorithm.IsFinished() && !_pause)
            {
                output = _algorithm.Execute();
                UpdateState(output);
                await Task.Delay(_delay);
            }
            //ustawiane na nulla, żeby przy kolejnym uruchomieniu pełnego algorytmu podał nowe dane z interfejsu w konstruktorze
            if (!_pause)
            {
                _algorithm = null;
            }
        }

        private void StepAlgorithm(IInputService input)
        {
            if (_mapInput.Map == null)
            {
                MessageBox.Show("Nie wczytano mapy.");
                return;
            }
            if (input == null || _algorithm == null || _input != input)
            {
                _input = input;
                _algorithm = new AlgorithmLogic(input, _mapInput.Map);
            }
            int stepsCounter = 0;
            IOutputService output = new OutputService();
            while (!_algorithm.IsFinished() && stepsCounter < _mapInput.NumOfSteps)
            {
                output = _algorithm.Execute();
                stepsCounter++;
            }

            UpdateState(output);

            if (_algorithm.IsFinished())
            {
                //ustawiane na nulla po pełnym przejściu algorytmu, żeby przy kolejnym uruchomieniu algorytmu podał nowe dane z interfejsu w konstruktorze
                _algorithm = null;
            }
        }


        private void ResetAlgorithm(IInputService input)
        {
            if (_mapInput.Map == null)
            {
                MessageBox.Show("Nie wczytano mapy.");
                return;
            }
            _input = input;
            _algorithm = new AlgorithmLogic(input, _mapInput.Map);
            _mapInput.Reset();
        }

        private void UpdateState(IOutputService output)
        {
            _mapInput.UpdateMap(output);
            _results.UpdateResults(output);
        }
    }
}
