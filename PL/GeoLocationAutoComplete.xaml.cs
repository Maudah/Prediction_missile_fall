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
using NSubstitute.Core;
using PL.Models;
using PL.Types;
using PL.ViewModels;

namespace PL
{
    /// <summary>
    /// Interaction logic for GeoLocationAutoComplete.xaml
    /// </summary>
    public partial class GeoLocationAutoComplete : UserControl
    {
        

        public GeoLocationAutoCompleteVM CompleteVM { get; set; }
        public GeoLocationAutoComplete()
        {
            InitializeComponent();
            CompleteVM = new GeoLocationAutoCompleteVM();
            DataContext = CompleteVM;
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
           

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            //resultStack.Children.Add(block);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}
