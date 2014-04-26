using System.Windows;
using System.Windows.Controls;

namespace AntColonyOptimization.Controls
{
    /// <summary>
    /// Interaction logic for Glass.xaml
    /// </summary>
    public partial class Glass : UserControl
    {
        #region DPs
        private static DependencyProperty LeftValueProperty = DependencyProperty.Register("Left", typeof(char), typeof(Glass), new PropertyMetadata(LeftValueChanged));

        public char LeftValue
        {
            get { return (char)this.GetValue(LeftValueProperty); }
            set { this.SetValue(LeftValueProperty, value); }
        }

        static void LeftValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var glass = (Glass)d;
            glass.rct01.Visibility = (char)e.NewValue == '1' ? Visibility.Visible : Visibility.Hidden;
        }

        private static DependencyProperty RightValueProperty = DependencyProperty.Register("Right", typeof(char), typeof(Glass), new PropertyMetadata(RightValueChanged));

        public char RightValue
        {
            get { return (char)this.GetValue(RightValueProperty); }
            set { this.SetValue(RightValueProperty, value); }
        }

        static void RightValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var glass = (Glass)d;
            glass.rct21.Visibility = (char)e.NewValue == '1' ? Visibility.Visible : Visibility.Hidden;
        }

        private static DependencyProperty TopValueProperty = DependencyProperty.Register("Top", typeof(char), typeof(Glass), new PropertyMetadata(TopValueChanged));

        public char TopValue
        {
            get { return (char)this.GetValue(TopValueProperty); }
            set { this.SetValue(TopValueProperty, value); }
        }

        static void TopValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var glass = (Glass)d;
            glass.rct10.Visibility = (char)e.NewValue == '1' ? Visibility.Visible : Visibility.Hidden;
        }

        private static DependencyProperty BottomValueProperty = DependencyProperty.Register("Bottom", typeof(char), typeof(Glass), new PropertyMetadata(BottomValueChanged));

        public char BottomValue
        {
            get { return (char)this.GetValue(BottomValueProperty); }
            set { this.SetValue(BottomValueProperty, value); }
        }

        static void BottomValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var glass = (Glass)d;
            glass.rct12.Visibility = (char)e.NewValue == '1' ? Visibility.Visible : Visibility.Hidden;
        }

        #endregion DPs

        #region ctor
        public Glass()
        {
            InitializeComponent();
        }
        #endregion ctor
    }
}
