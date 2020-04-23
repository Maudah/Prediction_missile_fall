using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;



namespace DAL
{
    public class DalSqlServerImp : IDAL
    {
        private LocateMissileProjectContext db;
        #region Constructor
        public DalSqlServerImp()
        {
            // if(db == null)
            db = new LocateMissileProjectContext();
            /*Fall f = new Fall();
            f.FallTime = DateTime.Now;
            f.FallKey = 1;
            f.FallImagePath = "";
            f.FallLocation = "";
            AddFall(f);*/
        }
        #endregion
        #region Fall Functins
        public void AddFall(Fall fall)
        {
            db.Falls.Add(fall);
            db.SaveChanges();
        }
        public void UpdateFall(Fall fall)
        {
            var fallToUpdate = db.Falls.SingleOrDefault(f => f.FallId == fall.FallId);
            if (fallToUpdate != null)
            {
                fallToUpdate = fall;
                db.SaveChanges();
            }
        }

        public void DeleteFall(int id)
        {
            db.Falls.Remove(GetFall(id));
            db.SaveChanges();
        }
        public Fall GetFall(int id)
        {
            return db.Falls.SingleOrDefault(f => f.FallId == id);
        }
        public List<Fall> GetAllFalls()
        {
            return db.Falls.ToList();
        }
        #endregion
        #region Report Functions
        public void AddReport(FallReport report)
        {
            db.Reports.Add(report);
            db.SaveChanges();
        }

        public void UpdateReport(FallReport report)
        {
            var reportToUpdate = db.Reports.SingleOrDefault(r => r.FallReportId == report.FallReportId);
            if (reportToUpdate != null)
            {
                reportToUpdate = report;
                db.SaveChanges();
            }
        }

        public void DeleteReport(int id)
        {
            db.Reports.Remove(GetReport(id));
            db.SaveChanges();
        }

        public FallReport GetReport(int id)
        {
            return db.Reports.SingleOrDefault(r => r.FallReportId == id);
        }

        public List<FallReport> GetAllReports()
        {
            return db.Reports.ToList();
        }
        #endregion
        #region Event Functions
        public void AddFallPrediction(FallPrediction ev)
        {
            db.Predictions.Add(ev);
            db.SaveChanges();
        }

        public void UpdateFallPrediction(FallPrediction ev)
        {
            var eventToUpdate = db.Predictions.SingleOrDefault(e => e.FallPredictionId == ev.FallPredictionId);
            if (eventToUpdate != null)
            {
                eventToUpdate = ev;
                db.SaveChanges();
            }
        }

        public void DeleteFallPrediction(int id)
        {
            db.Predictions.Remove(GetFallPrediction(id));
            db.SaveChanges();
        }

        public FallPrediction GetFallPrediction(int id)
        {
            return db.Predictions.SingleOrDefault(e => e.FallPredictionId == id);
        }

        public List<FallPrediction> GetAllFallPredictions()
        {
            return db.Predictions.ToList();
        }
        #endregion
    }
}
