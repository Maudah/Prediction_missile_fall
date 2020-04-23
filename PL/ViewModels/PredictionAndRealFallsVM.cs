using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using PL.Models;

namespace PL.ViewModels
{
    public sealed class PredictionAndRealFallsVM : IPredictionAndRealFallsVM
    {
        public ObservableCollection<Locations> Locations { get; set; }

        public ManagePredictionReportAndRealFallsModel currentModel;
        private static PredictionAndRealFallsVM instance = null;
        public static PredictionAndRealFallsVM Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PredictionAndRealFallsVM();
                }
                return new PredictionAndRealFallsVM();
            }
        }

        private PredictionAndRealFallsVM()
        {
            currentModel = ManagePredictionReportAndRealFallsModel.Instance;
            Locations = new ObservableCollection<Locations>(currentModel.LocationsList);
            Locations.CollectionChanged += Locations_CollectionChanged;
        }
        private void Locations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //הוספה
                currentModel.LocationsList.Add(e.NewItems[0] as Locations);
            }


        }
        public void AddReport(iLocationClass location)
        {
            Locations temp = new Locations(BE.Locations.FallStatus.REPORT, new List<iLocationClass> { location });
            currentModel.LocationsList.Add(temp);
        }
        public void AddPediction(iLocationClass location)
        {
            Locations temp = new Locations(BE.Locations.FallStatus.PREDICTION, new List<iLocationClass> { location });
            currentModel.LocationsList.Add(temp);
        }
        public void AddFall(iLocationClass location)
        {
            Locations temp = new Locations(BE.Locations.FallStatus.REAL, new List<iLocationClass> { location });
            currentModel.LocationsList.Add(temp);
        }
    }
}
