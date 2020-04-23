using BE;
using PL.ViewModels;
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
    /// <summary>
    /// Interaction logic for ExtraFieldsFall.xaml
    /// </summary>
    public partial class ExtraFieldsFall : UserControl
    {
        public string RealLocation { get; set; }
        public string CalculatedLocation { get; set; }
        public int ReportsNumber { get; set; }
        public FallReportVM CurrentFallReportVM { get; set; }
        public FallPredictionVM CurrentFallPredictionVM { get; set; }
        public ExtraFieldsFall()
        {
            InitializeComponent();
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new FallPredictionVM();
            DataContext = this;
            RealLocation = "";
            CalculatedLocation = "";
            ReportsNumber = 0;
        }
        public ExtraFieldsFall(Fall CurrentFall )
        {
            InitializeComponent();
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new FallPredictionVM();
            GPSCoordinate fallPredictionLocation = Prediction(CurrentFall).FallPredictionLocation;
            DataContext = this;
            RealLocation = CurrentFall.FallLocation.Latitude.ToString() + " " + "," + " " + CurrentFall.FallLocation.Longitude.ToString();
            CalculatedLocation = fallPredictionLocation.Latitude.ToString() + " " + "," + " " + fallPredictionLocation.Longitude.ToString();
            ReportsNumber = CalculateReportNumber(CurrentFall);
        }
        private int CalculateReportNumber(Fall CurrentFall)
        {
            int counter = 0;
            List<FallPrediction> fallPredictions = CurrentFallPredictionVM.FallPredictions.ToList();
            List<FallReport> fallReports = CurrentFallReportVM.FallReports.ToList();
            FallPrediction currentFallPredictions = new FallPrediction();

            foreach (FallPrediction item in fallPredictions)
            {
                if (item.FallPredictionFallKey == CurrentFall.FallId)
                {
                    currentFallPredictions = item;
                }
            }

            foreach (FallReport fallReport in fallReports)
                if (currentFallPredictions.FallPredictionId == fallReport.FallPredictionId)
                    counter++;

            return counter;
        }
        private FallPrediction Prediction(Fall CurrentFall)
        {
            List<FallPrediction> fallPredictions = CurrentFallPredictionVM.FallPredictions.ToList();
            List<FallReport> fallReports = CurrentFallReportVM.FallReports.ToList();
            FallPrediction currentFallPredictions = new FallPrediction();

            foreach (FallPrediction item in fallPredictions)
            {
                if (item.FallPredictionFallKey == CurrentFall.FallId)
                {
                    currentFallPredictions = item;
                }
            }
            return currentFallPredictions;
        }
        public void Update(Fall CurrentFall)
        {
            CurrentFallReportVM = new FallReportVM();
            CurrentFallPredictionVM = new FallPredictionVM();
            GPSCoordinate fallPredictionLocation = Prediction(CurrentFall).FallPredictionLocation;
            DataContext = this;
            if (RealLocation != null)
                RealLocation = CurrentFall.FallLocation.Latitude.ToString() + " " + "," + " " + CurrentFall.FallLocation.Longitude.ToString();
            if (CalculatedLocation != "")
                CalculatedLocation = fallPredictionLocation.Latitude.ToString() + " " + "," + " " + fallPredictionLocation.Longitude.ToString();
            ReportsNumber = CalculateReportNumber(CurrentFall);

        }

    }
}
