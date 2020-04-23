using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.Models
{
    public class ManageFallReportModel
    {
        readonly IBL ibl = FactoryBL.GetBL();
        public ManageFallReportModel()
        {
        }

        public void RemoveFallReport(int idToDelete)
        {
            ibl.DeleteFallReport(idToDelete);
        }
        public void AddFallReport(FallReport fallReport)
        {
            ibl.AddFallReport(fallReport);
        }
        public ObservableCollection<FallReport> AllFallReports()
        {
            List<FallReport> temp = ibl.GetAllFallReports().ToList();
            ObservableCollection<FallReport> theCollection = new ObservableCollection<FallReport>();
            foreach (FallReport item in temp)
                theCollection.Add(item);
            return theCollection;

        }
    }
}
