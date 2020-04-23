using BE;
using PL.Commands;
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
    public class FallReportVM : IFallReportVM
    {

        public ObservableCollection<FallReport> FallReports { get; set; }
        public ManageFallReportModel CurrentModel { get; set; }
        public AddReportCommand Add { get; set; }
        public PredictionAndRealFallsVM PredictionAndRealFalls { get; set; }

        public FallReportVM()
        {
            PredictionAndRealFalls = PredictionAndRealFallsVM.Instance;
            CurrentModel = new ManageFallReportModel();
            FallReports = new ObservableCollection<FallReport>(CurrentModel.AllFallReports());
            Add = new AddReportCommand(this);
            FallReports.CollectionChanged += FallReports_CollectionChanged;
        }
        private void FallReports_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //הוספה
                CurrentModel.AddFallReport(e.NewItems[0] as FallReport);
                PredictionAndRealFalls.AddReport(e.NewItems[0] as FallReport);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //הסרה
                var oldId = (e.OldItems[0] as FallReport).FallReportId;
                CurrentModel.RemoveFallReport(oldId);

            }

        }
        public void AddReport(FallReport fallReport)
        {
            if (fallReport.PersonName == "" ||
                fallReport.ReportLocation == null ||
                fallReport.ReportIntensity <= 0 || fallReport.ReportIntensity > 10 ||
                fallReport.NumOfExplosions == 0 || fallReport.ReportTime > DateTime.Now)
                return;

            ///בדיקות
            FallReports.Add(fallReport);
            // CurrentModel.AddFallReport(fallReport);
        }

    }
}
