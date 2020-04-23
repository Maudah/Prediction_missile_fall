using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Fall Functins
        void AddFall(Fall fall);
        void UpdateFall(Fall fall);
        void DeleteFall(int id);
        Fall GetFall(int id);
        List<Fall> GetAllFalls(Func<Fall, bool> filter = null);
        #endregion
        #region Report Functions
        void AddFallReport(FallReport report);
        void UpdateFallReport(FallReport report);
        void DeleteFallReport(int id);
        FallReport GetFallReport(int id);
        IEnumerable<FallReport> GetAllFallReports(Func<FallReport, bool> filter = null);
        #endregion
        #region GeoCoordinate Functions
        GPSCoordinate Geocode(string address);
        string ReverseGeocode(double latitude, double longitude);
        #endregion
        #region Event Functions
        void AddFallPrediction(FallPrediction ev);
        void UpdateFallPrediction(FallPrediction ev);
        void DeleteFallPrediction(int id);
        FallPrediction GetFallPrediction(int id);
        IEnumerable<FallPrediction> GetAllFallPredictions(Func<FallPrediction, bool> filter = null);
        #endregion
        #region others
        List<FallPrediction> GetAllFallPredictionWithoutFall();
        GPSCoordinate GetGeoTaggingFromImage(string imagePath);
        List<FallPrediction> GetAllFallPredictionsInCurrent10Minutes(DateTime dateTime);
        #endregion
    }
}
