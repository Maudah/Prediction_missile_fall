using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL.Models
{
    public class ManageFallPredictionModel
    {
        readonly IBL ibl = FactoryBL.GetBL();

        public ManageFallPredictionModel()
        {
        }
        public void RemoveFallPrediction(int idToDelete)
        {
            ibl.DeleteFallPrediction(idToDelete);
        }

        public void Add(FallPrediction fallPrediction)
        {
            ibl.AddFallPrediction(fallPrediction);
        }
        public ObservableCollection<FallPrediction> AllFallPrediction()
        {
            List<FallPrediction> temp = ibl.GetAllFallPredictions().ToList();
            ObservableCollection<FallPrediction> theCollection = new ObservableCollection<FallPrediction>();
            foreach (FallPrediction item in temp)
                theCollection.Add(item);
            return theCollection;


        }

    }
}


