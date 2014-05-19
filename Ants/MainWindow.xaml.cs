﻿using System.Windows.Controls;

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

        private InputView _inputView = new InputView();

        
        private MapControl _mapControl;

        private readonly MapInput _mapInput;

        public MainWindow()
        {
            _mapControl = new MapControl();
            _mapInput = new MapInput(_mapControl);
            InitializeComponent();

            _inputView.RunAlgorithm += RunAlgorithm;
            _inputView.StepAlgorithm += StepAlgorithm;


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
        }

        private async void RunAlgorithm(IInputService input)
        {
            if (_mapInput.Map == null)
            {
                MessageBox.Show("Nie wczytano mapy.");
                return;
            }
            if (_input == null || _algorithm == null || _input != input)
            {
                _input = input;
                _algorithm = new Algorithm(input, _mapInput.Map);
            }

            IOutputService output;
            while (!_algorithm.IsFinished())
            {
                output = _algorithm.Execute();
                UpdateState(output);
                await Task.Delay(_delay);
            }
            //ustawiane na nulla, żeby przy kolejnym uruchomieniu pełnego algorytmu podał nowe dane z interfejsu w konstruktorze
            _algorithm = null;
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
                _algorithm = new Algorithm(input, _mapInput.Map);
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

        private void UpdateState(IOutputService output)
        {
            _mapInput.UpdateMap(output);
        }
    }
}
