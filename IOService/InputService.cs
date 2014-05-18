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

        private bool _checkBox1;

        public bool CheckBox1
        {
            get
            {
                return _checkBox1;
            }
            set
            {
                _checkBox1 = value;
                NotifyPropertyChanged("CheckBox1");
            }
        }

        private bool _checkBox2;

        public bool CheckBox2
        {
            get
            {
                return _checkBox2;
            }
            set
            {
                _checkBox2 = value;
                NotifyPropertyChanged("CheckBox2");
            }
        }

        private bool _checkBox3;

        public bool CheckBox3
        {
            get
            {
                return _checkBox3;
            }
            set
            {
                _checkBox3 = value;
                NotifyPropertyChanged("CheckBox3");
            }
        }

        private bool _checkBox4;

        public bool CheckBox4
        {
            get
            {
                return _checkBox4;
            }
            set
            {
                _checkBox4 = value;
                NotifyPropertyChanged("CheckBox4");
            }
        }

        private bool _checkBox5;

        public bool CheckBox5
        {
            get
            {
                return _checkBox5;
            }
            set
            {
                _checkBox5 = value;
                NotifyPropertyChanged("CheckBox5");
            }
        }

        private bool _checkBox6;

        public bool CheckBox6
        {
            get
            {
                return _checkBox6;
            }
            set
            {
                _checkBox6 = value;
                NotifyPropertyChanged("CheckBox6");
            }
        }


        public InputService()
        {
            Alpha = 3.0;
            Beta = 3.0;
            Rho = 0.01;
            Q = 5.0;
            NumberOfIterations = 100;
            NumberOfAnts = 3;

            CheckBox1 = true;
            CheckBox2 = true;
            CheckBox3 = true;
            CheckBox4 = true;
            CheckBox5 = true;
            CheckBox6 = true;
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
