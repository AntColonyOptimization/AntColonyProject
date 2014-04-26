﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MapGenerator.Controls
{
    /// <summary>
    /// Interaction logic for Glass.xaml
    /// </summary>
    public partial class Glass : UserControl
    {
        public Point Position { get; set; }

        public Glass(int x, int y)
        {
            Position = new Point(x, y);
            InitializeComponent();
        }

    }
}
