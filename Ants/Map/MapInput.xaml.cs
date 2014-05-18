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
            if (ShowPheromones)
                _mapControl.UpdatePheromones(output.Pheromones);
            if (ShowBestPath)
                _mapControl.UpdateBestPath(output.BestPath);
            if (ShowCurrentPaths)
                _mapControl.UpdateCurrentPaths(output.CurrentPaths);
        }
    }
}
