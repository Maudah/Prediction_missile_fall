using BE;
using Microsoft.Win32;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for NewFallUC.xaml
    /// </summary>
    public partial class NewFallUC : UserControl
    {
        private FallsVM CurrentVM { get; set; }
        private PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }
        private System.Windows.Media.ImageSource defaultPath;
        public NewFallUC()
        {
            InitializeComponent();
            CurrentVM = new FallsVM();
            this.DataContext = CurrentVM;
            SaveButton.IsEnabled = true;
            TimeDatePicker.Text = DateTime.Now.ToString();
            defaultPath = Image.Source;
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
        }
        private void SaveEnableCheck(object sender, RoutedEventArgs routedEventArgs)
        {

            SaveButton.IsEnabled = true;
        }

        private void CancelButton_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void CancelButton_ButtonClick(object sender, EventArgs e)
        {
            // Image.Source = defaultPath;
            //   TimeDatePicker.Text = "";

        }

        private void SaveButton_ButtonClick(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(TimeDatePicker.Text) > DateTime.Now)

                TimeDatePicker.Background = Brushes.Red;
            else
            {
                TimeDatePicker.Background = Brushes.White;
                SaveButton.Command = CurrentVM.Add;
                Fall Currentfall;
                if (Image.Source == null || TimeDatePicker.Text == "") return;
                Currentfall = new Fall(Image.Source.ToString(), Convert.ToDateTime(TimeDatePicker.Text));
                SaveButton.CommandParameter = Currentfall;

                /*   List<iLocationClass> locationClasses = new List<iLocationClass>();
                   locationClasses.Add(Currentfall);
                   Locations locations = new Locations(Locations.FallStatus.REAL, locationClasses);
                   PredictionAndRealFalls.Locations.Add(locations);*/
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Image.Source = new BitmapImage(new Uri(op.FileName));
                Image.Source = new BitmapImage(new Uri(Copy(op.FileName.ToString())));
                MessageBox.Show("Image cocied");
            }
        }
        private static string Copy(string path)
        {
            string sourceDir = path;
            string backupDir = @"YOUR_URL_TO_PL/images";
            string fName = System.IO.Path.GetFileName(sourceDir);
            sourceDir = System.IO.Path.GetDirectoryName(sourceDir);
            try
            {
                File.Copy(System.IO.Path.Combine(sourceDir, fName), System.IO.Path.Combine(backupDir, fName), true);

            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
            string imagePath = System.IO.Path.Combine(backupDir, fName);
            return imagePath;

        }

    }
}
