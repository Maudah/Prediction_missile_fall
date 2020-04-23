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
    /// Interaction logic for StatisticsUC.xaml
    /// </summary>
    public partial class StatisticsUC : UserControl
    {
        public int ReportsNumber { get; set; }
        public int PredictionsNumber { get; set; }
        public int FallsNumber { get; set; }
        public FallPredictionVM CurrentFallPredictionVM { get; set; }
        public FallsVM CurrentFallVM { get; set; }
        public FallReportVM CurrentFallReportVM { get; set; }


        List<FallPrediction> FallPredictionList;
        List<Fall> FallList;
        List<FallReport> FallReportList;

        public StatisticsUC()
        {
            InitializeComponent();
            this.DataContext = this;

            CurrentFallPredictionVM = new FallPredictionVM();
            FallPredictionList = CurrentFallPredictionVM.FallPredictions.ToList();

            CurrentFallReportVM = new FallReportVM();
            FallReportList = CurrentFallReportVM.FallReports.ToList();

            CurrentFallVM = new FallsVM();
            FallList = CurrentFallVM.Falls.ToList();

            ReportsNumber = FallReportList.Count();
            PredictionsNumber = FallPredictionList.Count();
            FallsNumber = FallList.Count();


        }
        public void Load()
        {

            CurrentFallPredictionVM = new FallPredictionVM();
            FallPredictionList = CurrentFallPredictionVM.FallPredictions.ToList();

            CurrentFallReportVM = new FallReportVM();
            FallReportList = CurrentFallReportVM.FallReports.ToList();

            CurrentFallVM = new FallsVM();
            FallList = CurrentFallVM.Falls.ToList();

            ReportsNumber = FallReportList.Count();
            PredictionsNumber = FallPredictionList.Count();
            FallsNumber = FallList.Count();

        }
    }
}
