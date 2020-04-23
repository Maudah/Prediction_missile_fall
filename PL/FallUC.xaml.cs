using BE;
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

namespace PL
{
    
    public partial class FallUC : UserControl
    {
        public string FallAddress { get; set; }
        public string FallTime { get; set; }

        public FallUC()
        {
            InitializeComponent();
            this.DataContext = this;
            FallAddress = "";
            FallTime = "";
        }
        public FallUC(Fall currentFall)
        {
            InitializeComponent();
            this.DataContext = this;
            DateTime dt = currentFall.FallTime;
            FallAddress = currentFall.FallAddress;
            FallTime = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString();
        }
        public void UpdateUC(Fall currentFall)
        {
            DateTime dt = currentFall.FallTime;
            FallAddress = currentFall.FallAddress;
            FallTime = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString();

        }
    }
}
