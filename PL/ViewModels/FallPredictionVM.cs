using BE;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class FallPredictionVM : IFallPredictionVM
    {
        public ObservableCollection<FallPrediction> FallPredictions { get; set; }
        public ManageFallPredictionModel CurrentModel { get; set; }
        public PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }

        public FallPredictionVM()
        {
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
            CurrentModel = new ManageFallPredictionModel();
            FallPredictions = new ObservableCollection<FallPrediction>(CurrentModel.AllFallPrediction());
            // FallPredictions = CurrentModel.AllFallPrediction();
            FallPredictions.CollectionChanged += Falls_CollectionChanged;
        }

        private void Falls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //הוספה
                CurrentModel.Add(e.NewItems[0] as FallPrediction);
                PredictionAndRealFalls.AddPediction(e.NewItems[0] as FallPrediction);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //הסרה
                var oldId = (e.OldItems[0] as FallPrediction).FallPredictionId;
                CurrentModel.RemoveFallPrediction(oldId);

            }

        }
    }
}
