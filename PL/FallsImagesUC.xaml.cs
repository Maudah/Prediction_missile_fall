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
using PL.Models;
using System.Diagnostics;
using BE;

namespace PL
{
    /// <summary>
    /// Interaction logic for FallsImagesUC.xaml
    /// </summary>

    public partial class FallsImagesUC : UserControl
    {
        public FallsVM CurrentVM { get; set; }

        public FallsImagesUC()
        {
            InitializeComponent();
            CurrentVM = new FallsVM();
            this.DataContext = CurrentVM;

        }
        public void Load()
        {
            InitializeComponent();
            CurrentVM = new FallsVM();
            this.DataContext = CurrentVM;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (Image)sender;
            image.Width = 160;
            Thickness margin = image.Margin;
            margin.Top = 0;
            margin.Bottom = 0;
            image.Margin = margin;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = (Image)sender;
            image.Width = 150;
            Thickness margin = image.Margin;
            margin.Top = 5;
            margin.Bottom = 5;
            image.Margin = margin;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {

            Image image = (Image)sender;
            System.Windows.Media.ImageSource path = image.Source;
            ImageWpf imageWpf = new ImageWpf(path);
            MainWindow mainWindow = MainWindow.Instance;
            UpdateAll(image.ToolTip.ToString(), mainWindow.secondMapElement);

            imageWpf.Show();
            

        }
        public void UpdateAll(string fallId, GoogleMapsUC googleMapsUC)
        {
            List<Fall> falls = CurrentVM.Falls.ToList();
            Fall fall = null;
            foreach (Fall item in falls)
                if (item.FallId.ToString() == fallId)
                    fall = item;
            if (fall != null)
            {

                googleMapsUC.Update(Convert.ToInt32(fallId));
            }
        }

        private void Listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            MainWindow mainWindow = MainWindow.Instance;
            UpdateAll("100", mainWindow.secondMapElement);

        }

        private void ItemFallId_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            MainWindow mainWindow = MainWindow.Instance;
            UpdateAll(label.Content.ToString(), mainWindow.secondMapElement);

        }
    }
}
