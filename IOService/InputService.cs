namespace IOService
{
    using IOService.Core;
    using System.ComponentModel;

    public class InputService : IInputService, INotifyPropertyChanged
    {
        private double _alpha;

        public double Alpha 
        { 
            get
            {
                return _alpha;
            }
            set 
            {
                _alpha = value;
                NotifyPropertyChanged("Alpha");
            }
        }

        private double _beta;

        public double Beta
        {
            get
            {
                return _beta;
            }
            set
            {
                _beta = value;
                NotifyPropertyChanged("Beta");
            }
        }

        private double _rho;

        public double Rho
        {
            get
            {
                return _rho;
            }
            set
            {
                _rho = value;
                NotifyPropertyChanged("Rho");
            }
        }

        private double _q;

        public double Q
        {
            get
            {
                return _q;
            }
            set
            {
                _q = value;
                NotifyPropertyChanged("Q");
            }
        }

        private int _numberOfIterations;

        public int NumberOfIterations
        {
            get
            {
                return _numberOfIterations;
            }
            set
            {
                _numberOfIterations = value;
                NotifyPropertyChanged("NumberOfIterations");
            }
        }

        private int _numberOfAnts;

        public int NumberOfAnts
        {
            get
            {
                return _numberOfAnts;
            }
            set
            {
                _numberOfAnts = value;
                NotifyPropertyChanged("NumberOfAnts");
            }
        }

        private bool _ACS;

        public bool ACS
        {
            get
            {
                return _ACS;
            }
            set
            {
                _ACS = value;
                NotifyPropertyChanged("CheckBox4");
            }
        }

        private double _q0 = 0.6;

        public double Q0
        {
            get
            {
                return _q0;
            }
            set
            {
                _q0 = value;
                NotifyPropertyChanged("q0");
            }
        }

        private bool _asRank;

        public bool AsRank
        {
            get
            {
                return _asRank;
            }
            set
            {
                _asRank = value;
                NotifyPropertyChanged("CheckBox5");
            }
        }

        private double _sigma = 2;

        public double Sigma
        {
            get
            {
                return _sigma;
            }
            set
            {
                _sigma = value;
                NotifyPropertyChanged("Sigma");
            }
        }

        private bool _deleteLoops;

        public bool DeleteLoops
        {
            get
            {
                return _deleteLoops;
            }
            set
            {
                _deleteLoops = value;
                NotifyPropertyChanged("DeleteLoops");
            }
        }


        public InputService()
        {
            Alpha = 2;
            Beta = 2;
            Rho = 0.1;
            Q = 3.0;
            NumberOfIterations = 100;
            NumberOfAnts = 3;

            ACS = false;
            AsRank = false;
            DeleteLoops = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
