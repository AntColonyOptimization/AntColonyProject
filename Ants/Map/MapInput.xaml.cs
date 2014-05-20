using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Ants.Annotations;
using Ants.Map.Controls;


namespace Ants.Map
{
    public partial class MapInput : UserControl, INotifyPropertyChanged
    {
        private readonly MapControl _mapControl;
        private readonly MapGenerator _mapGenerator = new MapGenerator();
        public Map Map { get; private set; }

        #region Fields

        private bool _showPheromones = true;
        public bool ShowPheromones
        {
            get { return _showPheromones;  }
            set
            {
                _showPheromones = value;
                OnPropertyChanged("ShowPheromones");
            }
        }

        private bool _showCurrentPaths = true;
        public bool ShowCurrentPaths
        {
            get { return _showCurrentPaths; }
            set
            {
                _showCurrentPaths = value;
                OnPropertyChanged("ShowCurrentPaths");
            }
        }

        private bool _showBestPath = true;
        public bool ShowBestPath
        {
            get { return _showBestPath; }
            set
            {
                _showBestPath = value;
                OnPropertyChanged("ShowBestPath");
            }
        }

        private int _numOfSteps = 1;
        public int NumOfSteps
        {
            get { return _numOfSteps; }
            set
            {
                _numOfSteps = value;
                OnPropertyChanged("NumOfSteps");
            }
        }

        private int _currentIteration = 0;
        public int CurrentIteration
        {
            get { return _currentIteration; }
            set
            {
                _currentIteration = value;
                OnPropertyChanged("CurrentIteration");
            }
        }

        /// <summary>
        /// Map Paths Combobox collection.
        /// </summary>
        private ObservableCollection<string> _mapPaths = new ObservableCollection<string>
            {
                @"Map\Source\map.txt",
                @"Map\Source\map2.txt"
            };
        public ObservableCollection<string> MapPaths
        {
            get { return _mapPaths; }
            set
            {
                _mapPaths = value;
                OnPropertyChanged("MapPaths");
            }
        }

        /// <summary>
        /// Selected map path
        /// </summary>
        private string _selectedMapPath;
        public string SelectedMapPath
        {
            get { return _selectedMapPath; }
            set
            {
                _selectedMapPath = value;
                OnPropertyChanged("SelectedMapPath");
            }
        }

        /// <summary>
        /// holds all the data from current iteration
        /// </summary>
        private IOutputService _currentState;
        public IOutputService CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                OnPropertyChanged("SelectedMapPath");
            }
        }

        #endregion

        public MapInput(MapControl mapControl)
        {
            InitializeComponent();
            MapInputGrid.DataContext = this;
            SelectedMapPath = MapPaths.ElementAt(0);
            _mapControl = mapControl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public void LoadMapClick(object sender, RoutedEventArgs e)
        {
            if (!_mapGenerator.ValidateMapPath(SelectedMapPath))
            {
                MessageBox.Show("Wybrana mapa nie istnieje");
                return;
            }
            LoadMap();
        }


        private void LoadMap()
        {
            Map = _mapGenerator.ReadMapFromFile(SelectedMapPath);
            _mapControl.LoadMapView(Map);
        }


        public void UpdateMap(IOutputService output)
        {
            if (output != null)
            {
                CurrentState = output;

                _mapControl.LoadMapView(Map);
                if (ShowPheromones)
                    _mapControl.UpdatePheromones(output.Pheromones);
                if (ShowCurrentPaths)
                    _mapControl.UpdateCurrentPaths(output.CurrentPaths);
                if (ShowBestPath)
                    _mapControl.UpdateBestPath(output.BestPath);
                CurrentIteration = output.CurrentIteration;
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            UpdateMap(CurrentState);
        }

        internal void Reset()
        {
            _mapControl.LoadMapView(Map);

            NumOfSteps = 1;
            CurrentIteration = 0;

        }

    }
}
