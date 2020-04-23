using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using PL.Commands;
using PL.Models;

namespace PL.ViewModels
{
    public class FallsVM : IFallsVM
    {
        public OpenImageCommand openImageCommand { get; set; }
        public ObservableCollection<Fall> Falls { get; set; }
        public ManageFallsModel CurrentModel { get; set; }
        public PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }
        public AddFallCommand Add { get; set; }

        public FallsVM()
        {
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
            openImageCommand = new OpenImageCommand();
            CurrentModel = new ManageFallsModel();
            Add = new AddFallCommand(this);
            Falls = new ObservableCollection<Fall>(CurrentModel.AllFalls());
            Falls.CollectionChanged += Falls_CollectionChanged;
        }

        private void Falls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //הוספה
                CurrentModel.AddFall(e.NewItems[0] as Fall);
                PredictionAndRealFalls.AddFall(e.NewItems[0] as Fall);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //הסרה
                var oldId = (e.OldItems[0] as Fall).FallId;
                CurrentModel.Remove(oldId);

            }

        }
        public Fall GetFallByPrediction(int id)
        {
            return CurrentModel.Search(id);
        }
        public void AddFall(Fall fall)
        {
            if (fall.FallId == -1 ||
                fall.FallLocation == null ||
                fall.FallTime.AddMinutes(10) > DateTime.Now)
                return;

            Falls.Add(fall);
            //  CurrentModel.AddFall(fall);
        }

    }
}
