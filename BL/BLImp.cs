using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Geocoding;
using Geocoding.Microsoft.Json;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Device.Location;
using Location = Geocoding.Microsoft.Json.Location;
using ExifLib;
using System.Windows;
using System.Windows.Forms;

namespace BL
{
    public class BLImp : IBL
    {
        readonly DAL.IDAL dal;
        #region Constructor
        public BLImp()
        {
            dal = DAL.FactoryDal.GetDAL();
            /* Fall f = new Fall();
             f.FallTime = DateTime.Now;
             f.FallId = 0;
             f.FallImagePath = "";
             f.FallLocation = "";
             AddFall(f);*/
        }
        #endregion
        public static void Main(string[] args)
        {
            BLImp b = new BLImp();
        }
        #region Fall functions
        /// <summary>
        /// First Get GeoTagging from image path
        /// Adds Fall to the database
        /// </summary>
        /// <param name="fall">The new Fall to be add to the database</param>
        public void AddFall(Fall fall)
        {
            fall.FallLocation = GetGeoTaggingFromImage(fall.FallImage.ToString().Substring(8));
            try
            {
                fall.FallAddress = ReverseGeocode(fall.FallLocation.Latitude, fall.FallLocation.Longitude);


                //dal.AddFall(fall);
                DateTime dt = new DateTime(fall.FallTime.Year, fall.FallTime.Month, fall.FallTime.Day, fall.FallTime.Hour, fall.FallTime.Minute - fall.FallTime.Minute % 10, 0);
                List<FallPrediction> _fallPredictions = GetAllFallPredictions(fp => fp.FallPredictionFallKey == -1 && fp.FallPredictionTime == dt).ToList();
                if (_fallPredictions.Count == 0)
                    throw new Exception("invalid fall time");
                double minDist = Double.MaxValue;
                DateTime minDT = _fallPredictions[0].FallPredictionTime;
                double currDist;
                int _fallPredictionId = -1;
                for (int i = 0; i < _fallPredictions.Count(); i++)
                {
                    currDist = fall.GetCoordinate().GetDistanceTo(new GeoCoordinate(_fallPredictions[i].FallPredictionLocation.Latitude, _fallPredictions[i].FallPredictionLocation.Longitude));
                    if (currDist < minDist &&
                        fall.FallTime >= _fallPredictions[i].FallPredictionTime && fall.FallTime <= _fallPredictions[i].FallPredictionTime.AddMinutes(10))
                    {
                        _fallPredictionId = _fallPredictions[i].FallPredictionId;
                        minDist = currDist;
                    }
                }
                FallPrediction temp = GetFallPrediction(_fallPredictionId);
                fall.FallPrediction = temp;
                dal.AddFall(fall);
                temp.FallPredictionFallKey = fall.FallId;
                UpdateFallPrediction(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            int st = fall.FallId;
        }
        public void UpdateFall(Fall fall)
        {
            Fall f = dal.GetFall(fall.FallId);
            if (f == null)
                throw new Exception("not found");
            else
                dal.UpdateFall(fall);
        }
        public void DeleteFall(int id)
        {
            dal.DeleteFall(id);
        }
        public Fall GetFall(int id)
        {
            return dal.GetFall(id);
        }
        public List<Fall> GetAllFalls(Func<Fall, bool> filter = null)
        {
            List<Fall> result;
            if (filter == null)
            {
                result = dal.GetAllFalls();
            }
            else
            {
                result = (from m in dal.GetAllFalls()
                          where filter(m)
                          select m).ToList();
            }
            return result;
        }
        #endregion
        #region Report Functions
        public void AddFallReport(FallReport report)
        {
            try
            {

                report.ReportLocation = Geocode(report.ReportAddress);
                dal.AddReport(report);
                KMeans kMeans = new KMeans(report.ReportTime);
                kMeans.Calculate_K_Means();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateFallReport(FallReport report)
        {
            FallReport r = dal.GetReport(report.FallReportId);
            if (r == null)
                throw new Exception("not found");
            else
                dal.UpdateReport(report);
        }

        public void DeleteFallReport(int id)
        {
            dal.DeleteReport(id);
        }

        public FallReport GetFallReport(int id)
        {
            return dal.GetReport(id);
        }

        public IEnumerable<FallReport> GetAllFallReports(Func<FallReport, bool> filter = null)
        {
            List<FallReport> result;
            if (filter == null)
            {
                result = dal.GetAllReports();
            }
            else
            {
                result = (from m in dal.GetAllReports()
                          where filter(m)
                          select m).ToList();
            }
            return result;
        }
        #endregion
        #region Event Function
        public void AddFallPrediction(FallPrediction ev)
        {
            dal.AddFallPrediction(ev);
        }

        public void UpdateFallPrediction(FallPrediction ev)
        {
            FallPrediction e = dal.GetFallPrediction(ev.FallPredictionId);
            if (e == null)
                throw new Exception("not found");
            else
                dal.UpdateFallPrediction(ev);
        }

        public void DeleteFallPrediction(int id)
        {
            dal.DeleteFallPrediction(id);
        }

        public FallPrediction GetFallPrediction(int id)
        {
            return dal.GetFallPrediction(id);
        }

        public IEnumerable<FallPrediction> GetAllFallPredictions(Func<FallPrediction, bool> filter = null)
        {
            List<FallPrediction> result;
            if (filter == null)
            {
                result = dal.GetAllFallPredictions();
            }
            else
            {
                result = (from m in dal.GetAllFallPredictions()
                          where filter(m)
                          select m).ToList();
            }
            return result;
        }
        #endregion
        #region GeoCoordinate Functions
        /// <summary>
        /// gets address and finds the goeCoordinate of it
        /// </summary>
        /// <param name="address"></param>
        /// <returns>latitude,longitude</returns>
        public GPSCoordinate Geocode(string address)
        {
            try
            {

                string url = "http://dev.virtualearth.net/REST/v1/Locations?query=" + address + "&key=YOUR_BING_MAP_KEY";

                using (var client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                    using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                    {
                        var mapResponse = (ser.ReadObject(es) as Response); //Response is one of the Bing Maps DataContracts
                        Location location = (Location)mapResponse.ResourceSets.First().Resources.First();
                        return new GPSCoordinate()
                        {
                            Latitude = location.Point.Coordinates[0],
                            Longitude = location.Point.Coordinates[1]
                        };
                    }
                }

            }
            catch (Exception)
            {

                throw new Exception("Can't find this address");
            }
        }
        /// <summary>
        /// the function gets latitude and longitude and finds the address
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Street, City, Country</returns>
        public string ReverseGeocode(double latitude, double longitude)
        {
            try
            {


                using (var client = new WebClient())
                {
                    var queryString = "http://dev.virtualearth.net/REST/v1/Locations/" + latitude.ToString() + "," + longitude.ToString() + "?key=YOUR_BING_MAP_KEY";

                    string response = client.DownloadString(queryString);
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                    using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                    {
                        var mapResponse = (ser.ReadObject(es) as Response);
                        Location location = (Location)mapResponse.ResourceSets.First().Resources.First();
                        return location.Address.AddressLine + ", " + location.Address.Locality + ", " + location.Address.CountryRegion;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("invalid image");

            }
        }
        #endregion
        #region others
        public List<FallPrediction> GetAllFallPredictionWithoutFall()
        {
            return GetAllFallPredictions(e => e.FallPredictionId == -1).ToList();
        }
        public List<FallPrediction> GetAllFallPredictionsInCurrent10Minutes(DateTime dateTime)
        {
            return GetAllFallPredictions(e => e.FallPredictionTime >= dateTime).ToList();
        }
        #endregion
        #region Get GeoTagging From Image
        public GPSCoordinate GetGeoTaggingFromImage(string imagePath)
        {
            imagePath.Replace(@"\", "/");
            ExifReader reader = null;
            try
            {
                reader = new ExifReader(imagePath);
            }
            catch (Exception)
            {

                reader = new ExifReader(@"C:\Users\litalush\Documents\ConsoleApp1 (2)\ConsoleApp1\PL\images\2.jpg");
            }


            // EXIF lat/long tags stored as [Degree, Minute, Second]
            double[] latitudeComponents;
            double[] longitudeComponents;

            string latitudeRef; // "N" or "S" ("S" will be negative latitude)
            string longitudeRef; // "E" or "W" ("W" will be a negative longitude)

            if (reader.GetTagValue(ExifTags.GPSLatitude, out latitudeComponents)
                && reader.GetTagValue(ExifTags.GPSLongitude, out longitudeComponents)
                && reader.GetTagValue(ExifTags.GPSLatitudeRef, out latitudeRef)
                && reader.GetTagValue(ExifTags.GPSLongitudeRef, out longitudeRef))
            {
                var temp = new GPSCoordinate();
                var latitude = ConvertDegreeAngleToDouble(latitudeComponents[0], latitudeComponents[1], latitudeComponents[2], latitudeRef);
                var longitude = ConvertDegreeAngleToDouble(longitudeComponents[0], longitudeComponents[1], longitudeComponents[2], longitudeRef);
                temp.Latitude = latitude;
                temp.Longitude = longitude;
                return temp;
            }

            return null;
        }
        public double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds, string latLongRef)
        {
            double result = ConvertDegreeAngleToDouble(degrees, minutes, seconds);
            if (latLongRef == "S" || latLongRef == "W")
            {
                result *= -1;
            }
            return result;
        }

        public double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
        }
        #endregion
    }
}
