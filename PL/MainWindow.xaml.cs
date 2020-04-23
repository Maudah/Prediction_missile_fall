using System;
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
using PL.ViewModels;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public FallsVM CurrentVM { get; set; }
        private static MainWindow instance = null;
        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            CurrentVM = new FallsVM();
            this.DataContext = CurrentVM;
            fallImageElement.Load();
            fallImageElement.UpdateAll("0", secondMapElement);
            //  MainMap.TodayOnly();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            MainMap.TodayOnly();
        }

        private void GraphUC_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            fallImageElement.Load();
            fallImageElement.UpdateAll("0", secondMapElement);
        }

        private void Ellipse_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainMap.TodayOnly();
        }

        private void TabItem_GotFocus_1(object sender, RoutedEventArgs e)
        {
            PieChartElement.Load();
            StatisticsElement.Load();
            GraphElement.Load();
        }
    }
}
