using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ClassLibrary;

namespace CSharp_UI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData Vd = new ViewData();
        public static RoutedCommand ApplyDataCommand = new RoutedCommand("ApplyData", typeof(CSharp_UI2.MainWindow));
        public static RoutedCommand SplineCommand = new RoutedCommand("Spline", typeof(CSharp_UI2.MainWindow));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FuncTypeBox.ItemsSource = Enum.GetValues(typeof(SPf));
            NodeTypeBox.ItemsSource = Enum.GetValues(typeof(Uniform));
            FuncTypeBox.DataContext = Vd;
            NodeTypeBox.DataContext = Vd;
            NodeNumBox.DataContext = Vd;
            SegstartBox.DataContext = Vd;
            SegendBox.DataContext = Vd;
            SplineNodeNumBox.DataContext = Vd;
            DerivSegstartBox.DataContext = Vd;
            DerivSegendBox.DataContext = Vd;
            IntegSegstartBox.DataContext = Vd;
            IntegSegendBox.DataContext = Vd;
            MDListBox.ItemsSource = Vd.mdList;
            SPListBox.ItemsSource = Vd.splnList;
        }

        private void CanApplyDataCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (MData != null)
            {
                foreach (FrameworkElement child in MData.Children)
                {
                    if (Validation.GetHasError(child) == true)
                    {
                        e.CanExecute = false;
                        return;
                    }
                    e.CanExecute = true;
                }
            }
            else e.CanExecute = false;
        }

        private void ApplyDataCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Vd.mdList.Clear();
            Vd.ApplyMeasureData();
        }

        private void CanSplineCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SpParam != null)
            {
                foreach (FrameworkElement child in SpParam.Children)
                {
                    if (Validation.GetHasError(child) == true || Vd.md == null)
                    {
                        e.CanExecute = false;
                        return;
                    }
                    e.CanExecute = true;
                }
            }
            else e.CanExecute = false;
        }

        private void SplineCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Vd.splnList.Clear();
            Vd.ApplySplineData();
        }
    }
}
