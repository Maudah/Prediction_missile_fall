using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BE;
using BL;
using static BE.Locations;

namespace PL.Models
{
    public sealed class ManagePredictionReportAndRealFallsModel
    {
        readonly IBL ibl = FactoryBL.GetBL();
        public List<Locations> LocationsList { get; set; }

        private static ManagePredictionReportAndRealFallsModel instance = null;
        public static ManagePredictionReportAndRealFallsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagePredictionReportAndRealFallsModel();
                }
                return new ManagePredictionReportAndRealFallsModel();
            }
        }

        private ManagePredictionReportAndRealFallsModel()
        {
            LocationsList = new List<Locations>();
            List<iLocationClass> realList = ibl.GetAllFalls().ToList<iLocationClass>();
            Locations real = new Locations(Locations.FallStatus.REAL, realList);

            List<iLocationClass> predictionlList = ibl.GetAllFallPredictions().ToList<iLocationClass>();
            Locations prediction = new Locations(Locations.FallStatus.PREDICTION, predictionlList);

            List<iLocationClass> reportList = ibl.GetAllFallReports().ToList<iLocationClass>();
            Locations report = new Locations(Locations.FallStatus.REPORT, reportList);

            LocationsList.Add(real);
            LocationsList.Add(prediction);
            LocationsList.Add(report);
        }
    }
}
