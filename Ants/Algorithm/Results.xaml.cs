using Ants.Annotations;
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

namespace Ants.Algorithm
{

    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl, INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<AntData> _antsCollection = new ObservableCollection<AntData>();
        public ObservableCollection<AntData> AntsCollection
        {
            get { return _antsCollection; }
        }

        private int _bestPathLength = 0;
        public int BestPathLength
        {
            get { return _bestPathLength; }
            set
            {
                _bestPathLength = value;
                OnPropertyChanged("BestPathLength");
            }
        }

        private int _bestPathIter = 0;
        public int BestPathIter
        {
            get { return _bestPathIter; }
            set
            {
                _bestPathIter = value;
                OnPropertyChanged("BestPathIter");
            }
        }

        #endregion

        public Results()
        {
            InitializeComponent();
            ResultsGr.DataContext = this;
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

        public void UpdateResults(IOutputService output)
        {
            AntsCollection.Clear();
            int i = 1;
            if (BestPathLength != output.BestPath.Count)
            {
                BestPathLength = output.BestPath.Count;
                BestPathIter = output.CurrentIteration;
            }
            foreach(var ant in output.CurrentPaths)
            {
                AntsCollection.Add(new AntData(i++, ant.Count));
            }
        }
    }

    public class AntData 
    {
        public int AntNr { get; set; }

        public int Length { get; set; }

        public AntData(int antNr, int length)
        {
            AntNr = antNr;
            Length = length;
        }

    }

}
