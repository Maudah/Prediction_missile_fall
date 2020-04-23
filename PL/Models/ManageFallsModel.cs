using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using BL;

namespace PL.Models
{
    public class ManageFallsModel
    {
        readonly IBL ibl = FactoryBL.GetBL();

        public ManageFallsModel()
        {

        }

        public void Remove(int idToDelete)
        {
            ibl.DeleteFall(idToDelete);
        }
        public void AddFall(Fall fall)
        {
            ibl.AddFall(fall);
        }
        public Fall Search(int id)
        {
            return ibl.GetFall(id);
        }
        public ObservableCollection<Fall> AllFalls1()
        {
            List<Fall> temp = ibl.GetAllFalls();
            ObservableCollection<Fall> theCollection = new ObservableCollection<Fall>();
            foreach (Fall item in temp)
                theCollection.Add(item);
            return theCollection;
        }
        public ObservableCollection<Fall> AllFalls()
        {
            List<Fall> temp = ibl.GetAllFalls();
            ObservableCollection<Fall> theCollection = new ObservableCollection<Fall>();
            foreach (Fall item in temp)
                theCollection.Add(item);
            return theCollection;

            /*ObservableCollection<Fall> theCollection = new ObservableCollection<Fall>();
          
                theCollection.Add(new Fall(100, new GPSCoordinate(31.7, 35.1), "/images/rocket1.jpg", DateTime.Now, "some address"));

            return theCollection;*/
        }

    }
}

