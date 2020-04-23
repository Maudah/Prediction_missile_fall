
using BE;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class KMeans
    {
        List<String> tempGPS = new List<string>();
        List<FallPrediction> fallPredictions;
        List<FallReport> reports;
        readonly IBL _ibl = FactoryBL.GetBL();
        DateTime dateTime;
        private void GetKMeans(DateTime t)
        {

            int counter = 0;
            K = 0;
            DateTime dt = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute - t.Minute % 10, 0);
            dateTime = dt;
            fallPredictions = _ibl.GetAllFallPredictionsInCurrent10Minutes(dt);
            for (int i = 0; i < fallPredictions.Count(); i++)
            {
                List<FallReport> frl = _ibl.GetAllFallReports(f => f.FallPredictionId == fallPredictions[i].FallPredictionId).ToList();
                for (int j = 0; j < frl.Count(); j++)
                {
                    frl[j].FallPrediction = null;
                    frl[j].FallPredictionId = null;
                    _ibl.UpdateFallReport(frl[j]);
                }
                _ibl.DeleteFallPrediction(fallPredictions[i].FallPredictionId);
                fallPredictions.Remove(fallPredictions[i]);
            }

            reports = _ibl.GetAllFallReports(r => r.ReportTime >= dt && r.ReportTime <= dt.AddMinutes(10)).ToList();

            for (int i = 0; i < reports.Count(); i++)
            {

                if (reports[i]._originReportId == -1)
                {
                    K += reports[i].NumOfExplosions;
                    counter++;
                    for (int j = 0; j < reports[i].NumOfExplosions; j++)
                    {
                        for (int k = 1; k <= reports[i].ReportIntensity; k++)
                        {
                            FallReport r = new FallReport();

                            r.FallReportId = reports[i].FallReportId;
                            r.ReportLocation = reports[i].ReportLocation;
                            r.PersonName = reports[i].PersonName;
                            r.ReportIntensity = reports[i].ReportIntensity;
                            r.ReportAddress = reports[i].ReportAddress;
                            r.NumOfExplosions = reports[i].NumOfExplosions;
                            r.ReportTime = reports[i].ReportTime;
                            r._originReportId = reports[i].FallReportId;
                            reports.Add(r);
                        }
                    }
                }
            }
            if (K % counter == 0)
                K = K / counter;
            else K = K / counter + 1;
            for (int i = 0; i < K; i++)
            {
                FallPrediction e = new FallPrediction();
                e.FallPredictionLocation = new GPSCoordinate();
                e.FallPredictionTime = dt;
                e.FallPredictionId = -1;
                _ibl.AddFallPrediction(e);
                fallPredictions.Add(e);
            }
        }
        public KMeans(DateTime dateTime)
        {
            K = 1;
            GetKMeans(dateTime);
            if (reports.Count == 0)
                return;
        }
        public int K { get; set; }

        public void Calculate_K_Means()
        {

            if (reports.Count == 0)
                return;
            //list of cluse
            List<FallPrediction> clustersIdList = ClustersGenerator();


            bool isClustersChanged;
            var counter = 0;
            do
            {
                isClustersChanged = false;
                //for each report looking for the closest cluster 
                for (int i = 0; i < reports.Count; i++)
                {

                    if (clustersIdList.ElementAt(0).FallPredictionLocation.Latitude != 0 && clustersIdList.ElementAt(0).FallPredictionLocation.Longitude != 0)
                    {
                        double min = reports[i].GetCoordinate().GetDistanceTo(new GeoCoordinate(clustersIdList.ElementAt(0).FallPredictionLocation.Latitude, clustersIdList.ElementAt(0).FallPredictionLocation.Longitude));
                        reports[i].FallPredictionId = clustersIdList.ElementAt(0).FallPredictionId;
                        for (int j = 1; j < clustersIdList.Count; j++)
                        {
                            double temp = reports[i].GetCoordinate().GetDistanceTo(new GeoCoordinate(clustersIdList.ElementAt(j).FallPredictionLocation.Latitude, clustersIdList.ElementAt(j).FallPredictionLocation.Longitude));
                            if (temp < min)
                            {
                                min = temp;
                                isClustersChanged = true;
                                reports[i].FallPredictionId = clustersIdList.ElementAt(j).FallPredictionId;
                            }
                        }
                    }

                }
                counter++;
                clustersIdList = RecenterClusters(clustersIdList);
                /** if (counter == 10000)
               {
                     break;
                }*/
            } while (isClustersChanged);

            for (int i = 0; i < clustersIdList.Count(); i++)
            {
                _ibl.UpdateFallPrediction(clustersIdList[i]);
            }
            for (int i = 0; i < reports.Count(); i++)
            {
                // Console.WriteLine(ReportsList[i]._originReportId + " " + ReportsList[i].ReportLatLong.Latitude + " " +ReportsList[i].ReportLatLong.Longitude +" "+ ReportsList[i].ReportIntensity);
                if (reports[i]._originReportId == -1)
                    _ibl.UpdateFallReport(reports[i]);
            }
        }

        private List<FallPrediction> RecenterClusters(List<FallPrediction> clustersIdList)
        {
            //Recenter the clusters
            reports = reports.OrderBy(c => c.FallPredictionId).ToList();
            int id = 0;
            double clustersLongitudeSum = 0;
            double clustersLatitudeSum = 0;
            double counter = 0;
            for (int i = 0; i < reports.Count; i++)
            {
                if (reports[i].FallPredictionId == clustersIdList.ElementAt(id).FallPredictionId)
                {
                    clustersLatitudeSum += reports[i].GetCoordinate().Latitude;
                    clustersLongitudeSum += reports[i].GetCoordinate().Longitude;
                    counter++;
                }
                else if (reports[i].FallPredictionId != clustersIdList.ElementAt(id).FallPredictionId)
                {
                    if (counter == 0)
                        Console.WriteLine(i + "," + clustersLongitudeSum + "," + clustersLatitudeSum);
                    clustersIdList[id].FallPredictionLocation.Latitude = clustersLatitudeSum / counter;
                    clustersIdList[id].FallPredictionLocation.Longitude = clustersLongitudeSum / counter;
                    counter = 0;
                    clustersLongitudeSum = 0;
                    clustersLatitudeSum = 0;
                    i--;
                    id++;
                }
            }
            clustersIdList[id].FallPredictionLocation.Latitude = clustersLatitudeSum / counter;
            clustersIdList[id].FallPredictionLocation.Longitude = clustersLongitudeSum / counter;
            return clustersIdList;

        }

        private List<FallPrediction> ClustersGenerator()
        {
            List<FallReport> fallReportList = _ibl.GetAllFallReports(r => r.ReportTime >= dateTime && r.ReportTime <= dateTime.AddMinutes(10)).ToList();


            List<FallPrediction> clustersIdList = new List<FallPrediction>();


            double minLatitude = reports.Min(r => r.ReportLocation.Latitude);
            double maxLatitude = reports.Max(r => r.ReportLocation.Latitude);
            double minLongitude = reports.Min(r => r.ReportLocation.Longitude);
            double maxLongitude = reports.Max(r => r.ReportLocation.Longitude);
            double temp = (maxLatitude - minLatitude) / (double)K;

            for (int i = 0; i < K; i++)
            {
                Random rand = new Random(i);
                // double latitude = minLatitude + rand.NextDouble() * (maxLatitude - minLatitude);
                //double longitude = minLongitude + rand.NextDouble() * (maxLongitude - minLongitude);
                // GPSCoordinate gpsTemp =  _ibl.Geocode(tempGPS[i]);
                //  double latitude = minLatitude + temp*(i+1);
                //double longitude = minLongitude + temp*(i+1);
                //fallPredictions.ElementAt(i).FallPredictionLocation.Latitude = latitude;
                //fallPredictions.ElementAt(i).FallPredictionLocation.Longitude = longitude;
                fallPredictions.ElementAt(i).FallPredictionLocation.Latitude = fallReportList[i % fallReportList.Count].ReportLocation.Latitude;
                fallPredictions.ElementAt(i).FallPredictionLocation.Longitude = fallReportList[i % fallReportList.Count].ReportLocation.Longitude;
                // GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                clustersIdList.Add(fallPredictions.ElementAt(i));
                // Console.WriteLine(clustersIdList[i].FallPredictionLocation.Latitude + ", " + clustersIdList[i].FallPredictionLocation.Longitude);
            }

            return clustersIdList;
        }

    }
}