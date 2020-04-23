using BE;
using LiveCharts;
using LiveCharts.Wpf;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Interaction logic for GraphUC.xaml
    /// </summary>
    public partial class GraphUC : UserControl
    {
        public FallPredictionVM CurrentFallPredictionVM { get; set; }
        public FallsVM CurrentFallVM { get; set; }

        List<FallPrediction> FallPredictionList;

        public GraphUC()
        {
            InitializeComponent();
            DataContext = this;
            CurrentFallPredictionVM = new FallPredictionVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<FallPrediction>)(CurrentFallPredictionVM.FallPredictions)).ToList<FallPrediction>();
            InitializeGraph();

        }
        public void Load()
        {
            CurrentFallPredictionVM = new FallPredictionVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<FallPrediction>)(CurrentFallPredictionVM.FallPredictions)).ToList<FallPrediction>();
            InitializeGraph();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        private double GetDinstance(GPSCoordinate gPSCoordinate1, GPSCoordinate gPSCoordinate2)
        {

            return (new GeoCoordinate(gPSCoordinate1.Latitude, gPSCoordinate1.Longitude)).GetDistanceTo(new GeoCoordinate(gPSCoordinate2.Latitude, gPSCoordinate2.Longitude));
        }
        private double SumErrors(List<FallPrediction> predictionList)
        {
            Fall currentFall = new Fall();
            double currentDinstance = 0;

            if (FallPredictionList == null)
                return 1;
            foreach (FallPrediction item in predictionList)
            {
                currentFall = CurrentFallVM.GetFallByPrediction(item.FallPredictionFallKey);
                if (currentFall == null)
                {
                    currentDinstance += 10;
                }
                else
                    currentDinstance += GetDinstance(item.FallPredictionLocation, currentFall.FallLocation);
            }
            return currentDinstance;
        }
        public void InitializeGraph()
        {
            Labels = new string[24];
            string currentValue = "";
            for (int i = 0; i < 24; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString() + ":00";
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<FallPrediction> predictionList = new List<FallPrediction>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();



            for (int i = 0; i < 24; i++)
            {
                predictionList.Clear();
                foreach (FallPrediction item in FallPredictionList)
                {
                    if ((item.FallPredictionTime.Day == DateTime.Today.Day) && (item.FallPredictionTime.Hour == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
        }
        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            Labels = new string[24];
            string currentValue = "";
            for (int i = 0; i < 24; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString() + ":00";
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<FallPrediction> predictionList = new List<FallPrediction>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();



            for (int i = 0; i < 24; i++)
            {
                predictionList.Clear();
                foreach (FallPrediction item in FallPredictionList)
                {
                    if ((item.FallPredictionTime.Day == DateTime.Today.Day) && (item.FallPredictionTime.Hour == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void ThisMounthButton_Click(object sender, RoutedEventArgs e)
        {
            int days = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            Labels = new string[days];
            string currentValue = "";
            for (int i = 0; i < days; i++)
            {
                currentValue = "";
                if (i < 10)
                    currentValue = "0";
                currentValue += i.ToString();
                Labels[i] = currentValue;
            }
            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<FallPrediction> predictionList = new List<FallPrediction>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = 0; i < days; i++)
            {
                predictionList.Clear();
                foreach (FallPrediction item in FallPredictionList)
                {
                    if ((item.FallPredictionTime.Month == DateTime.Today.Month) && (item.FallPredictionTime.Day == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void THisYearButton_Click(object sender, RoutedEventArgs e)
        {
            Labels = DateTimeFormatInfo.CurrentInfo.MonthNames;

            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<FallPrediction> predictionList = new List<FallPrediction>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = 0; i < 12; i++)
            {
                predictionList.Clear();
                foreach (FallPrediction item in FallPredictionList)
                {
                    if ((item.FallPredictionTime.Year == DateTime.Today.Year) && (item.FallPredictionTime.Month == i))
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }

        private void AllTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int minYear = DateTime.Now.Year;
            foreach (FallPrediction item in FallPredictionList)
                if (item.FallPredictionTime.Year <= minYear)
                    minYear = item.FallPredictionTime.Year;
            int maxYear = DateTime.Now.Year;
            if (maxYear - minYear == 0)
            {
                THisYearButton_Click(sender, e);
                return;
            }
            Labels = new string[maxYear - minYear];
            for (int i = 0; i < maxYear - minYear; i++)
                Labels[i] = (i + minYear).ToString();

            SeriesCollection = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            double currentVal;
            List<FallPrediction> predictionList = new List<FallPrediction>();
            lineSeries.Title = "Errors";
            lineSeries.Values = new ChartValues<double>();

            for (int i = minYear; i <= maxYear; i++)
            {
                predictionList.Clear();
                foreach (FallPrediction item in FallPredictionList)
                {
                    if (item.FallPredictionTime.Year == i)
                        predictionList.Add(item);
                }
                currentVal = SumErrors(predictionList);
                lineSeries.Values.Add(currentVal);
            }
            SeriesCollection.Add(lineSeries);
            Graph.Series = SeriesCollection;
            XLabels.Labels = Labels;
        }
    }
}