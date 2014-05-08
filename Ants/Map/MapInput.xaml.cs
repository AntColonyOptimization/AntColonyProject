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

        private string _selectedMap;
        public string SelectedMap
        {
            get { return _selectedMap; }
            set
            {
                MapGenerator.Instance.MapPath = value;
                _selectedMap = MapGenerator.Instance.MapPath; 
                OnPropertyChanged("SelectedMap");
            }
        }


        #endregion

        public MapInput()
        {
            InitializeComponent();
            MapInputGrid.DataContext = this;
            SelectedMap = MapPaths.ElementAt(0);
            //MapPaths = new
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadMapClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
