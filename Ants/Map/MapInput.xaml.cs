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
using System.IO;


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
        private ObservableCollection<string> _mapPaths;



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


        public ObservableCollection<ComboBoxItem> CurrentPaths { get; set; }
        /// <summary>
        /// selected current path
        /// </summary>
        private ComboBoxItem _selectedCurrentPath;
        public ComboBoxItem SelectedCurrentPath 
        { 
            get
            {
                return _selectedCurrentPath;
            }
            set
            {
                _selectedCurrentPath = value;
                OnPropertyChanged("SelectedCurrentPath");
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
                OnPropertyChanged("CurrentState");
            }
        }

        #endregion

        public MapInput(MapControl mapControl)
        {
            InitializeComponent();
            MapInputGrid.DataContext = this;

            CurrentPaths = new ObservableCollection<ComboBoxItem>();
            CurrentPaths.Add(new ComboBoxItem { Content = "Brak" });
            CurrentPaths.Add(new ComboBoxItem { Content = "Wszystkie" });
            SelectedCurrentPath = CurrentPaths.ElementAt(0);
            PropertyChanged += SelectedCurrentPathPropertyChanged;


            MapPaths = new ObservableCollection<string>();
            string[] fileEntries = Directory.GetFiles(@"Map\Source");
            foreach (string fileName in fileEntries)
            {
                MapPaths.Add(fileName);
            }
            SelectedMapPath = MapPaths.ElementAt(0);
            _mapControl = mapControl;

        }


        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void SelectedCurrentPathPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedCurrentPath")
            {
                //Console.WriteLine("A property has changed: " + CurrentPaths.IndexOf(SelectedCurrentPath));
                UpdateMap(CurrentState, CurrentPaths.IndexOf(SelectedCurrentPath));
            }
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


        public void UpdateMap(IOutputService output, int selectedCurrentPathIndex=-1)
        {
            if (output != null)
            {
                CurrentState = output;
                if (output.CurrentIteration == 1 && CurrentPaths.Count==2)
                {
                    GenerateTemporaryPathsNamesToComboBox(output.CurrentPaths.Count);
                }
                _mapControl.LoadMapView(Map);
                if (ShowPheromones)
                    _mapControl.UpdatePheromones(output.Pheromones);
                if (ShowBestPath)
                    _mapControl.UpdateBestPath(output.BestPath);
                CurrentIteration = output.CurrentIteration;
                if (selectedCurrentPathIndex == -1)
                {
                    selectedCurrentPathIndex = CurrentPaths.IndexOf(SelectedCurrentPath);
                }
                    _mapControl.UpdateCurrentPaths(output.CurrentPaths, selectedCurrentPathIndex);
            }
        }

        private void GenerateTemporaryPathsNamesToComboBox(int numberOfPaths)
        {
            for(int i = 0 ; i < numberOfPaths ; i++)
            {
                CurrentPaths.Add(new ComboBoxItem { Content = 1+i+". Mrówka "});
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
