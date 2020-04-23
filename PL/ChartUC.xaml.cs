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
using System.Device.Location;
using LiveCharts;

using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace PL
{
    /// <summary>
    /// Interaction logic for ChartUC.xaml
    /// </summary>
    public partial class ChartUC : UserControl
    {
        public FallPredictionVM CurrentFallPredictionVM { get; set; }
        public FallsVM CurrentFallVM { get; set; }
        List<FallPrediction> FallPredictionList;
        private SeriesCollection _seriesCol;
        public virtual SeriesCollection seriesCol
        {
            get { return _seriesCol; }
            set
            {
                _seriesCol = value;
            }
        }
        public ChartUC()
        {
            InitializeComponent();
            DataContext = this;
            CurrentFallPredictionVM = new FallPredictionVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<FallPrediction>)(CurrentFallPredictionVM.FallPredictions)).ToList<FallPrediction>();
            InitData();

        }
        public void Load()
        {
            CurrentFallPredictionVM = new FallPredictionVM();
            CurrentFallVM = new FallsVM();
            FallPredictionList = ((IEnumerable<FallPrediction>)(CurrentFallPredictionVM.FallPredictions)).ToList<FallPrediction>();
            InitData();
        }
        private void MakeData()
        {
            MyPieChart.Series.Clear();
            List<string> allValues = new List<string>();
            var NameLables = new List<string>();
            double precent = Precent();
            

            int precent1 = (int)(precent * 100);

            int temp = 100 - precent1;

            allValues.Add((temp).ToString());
            NameLables.Add((temp).ToString() + "%");

            allValues.Add((precent1).ToString());
            NameLables.Add(precent1.ToString() + "%");


            seriesCol = new SeriesCollection();

            MyPieChart.Series.AddRange(Enumerable.Range(0, allValues.Count).Select(x => new PieSeries { Title = NameLables[x], Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(allValues[x])) } }));
        }
        private void InitData()
        {

            List<string> allValues = new List<string>();
            var NameLables = new List<string>();
            double precent = Precent();
            int precent1 = (int)(precent * 100);


            int temp = 100 - precent1;

            allValues.Add((temp).ToString());
            NameLables.Add((temp).ToString() + "%");
            allValues.Add((precent1).ToString());
            NameLables.Add(precent1.ToString() + "%");

            seriesCol = new SeriesCollection();

            seriesCol.AddRange(Enumerable.Range(0, allValues.Count).Select(x => new PieSeries { Title = NameLables[x], Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(allValues[x])) } }));
        }

        private double Precent()
        {
            Fall currentFall = new Fall();
            double currentDinstance = 0;
            int errorConter = 0;
            if (FallPredictionList == null)
                return 1;
            foreach (FallPrediction item in FallPredictionList)
            {
                currentFall = CurrentFallVM.GetFallByPrediction(item.FallPredictionFallKey);
                if (currentFall == null)
                {
                    errorConter++;
                }
                else
                {
                    currentDinstance = GetDinstance(item.FallPredictionLocation, currentFall.FallLocation);
                    double temp = Double.Parse(RangeLable.Content.ToString().Substring(0, RangeLable.Content.ToString().Length - 1));
                    if (currentDinstance > temp)
                        errorConter++;
                }

            }
            if (FallPredictionList.Count() == 0) return 0;
            double retValue = (double)errorConter / ((double)FallPredictionList.Count());
            return retValue;
        }
        private double GetDinstance(GPSCoordinate gPSCoordinate1, GPSCoordinate gPSCoordinate2)
        {

            return (new GeoCoordinate(gPSCoordinate1.Latitude, gPSCoordinate1.Longitude)).GetDistanceTo(new GeoCoordinate(gPSCoordinate2.Latitude, gPSCoordinate2.Longitude));
        }

        private void ErrorRangeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MakeData();
        }
    }
}
