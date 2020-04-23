using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        #region Fall Functins
        void AddFall(Fall fall);
        void UpdateFall(Fall fall);
        void DeleteFall(int id);
        Fall GetFall(int id);
        List<Fall> GetAllFalls();
        #endregion
        #region Report Functions
        void AddReport(FallReport report);
        void UpdateReport(FallReport report);
        void DeleteReport(int id);
        FallReport GetReport(int id);
        List<FallReport> GetAllReports();
        #endregion
        #region Event Functions
        void AddFallPrediction(FallPrediction ev);
        void UpdateFallPrediction(FallPrediction ev);
        void DeleteFallPrediction(int id);
        FallPrediction GetFallPrediction(int id);
        List<FallPrediction> GetAllFallPredictions();
        #endregion
    }
}
