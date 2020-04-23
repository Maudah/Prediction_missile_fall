using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Text;

namespace BE
{
    public class FallReport : INotifyPropertyChanged,iLocationClass
    {
        public int _originReportId = -1;


        #region Private Fields
        private int _reportId;
        private string _personName;
        private DateTime _reportTime;
        private int _reportIntensity;
        private GPSCoordinate _reportLocation;
        private int _numOfExplosions;
        private string _reportAddress;
        private int? _reportPredictionKey;
        #endregion

        #region Constructors
        public FallReport()
        {
            _reportId = 0;
            _personName = "";
            _reportTime = DateTime.Now;
            _reportIntensity = 0;
            _reportLocation = null;
            _numOfExplosions = 0;
            _reportAddress = "";
            _reportPredictionKey = 0;

        }
        public FallReport(int reportCode, string personName, DateTime reportTime, int reportIntensity, GPSCoordinate reportLocation, int numOfExplosions, string reportAddress)
        {
            _reportId = reportCode;
            _personName = personName;
            _reportTime = reportTime;
            _reportIntensity = reportIntensity;
            _reportLocation = reportLocation;
            _numOfExplosions = numOfExplosions;
            _reportAddress = reportAddress;
        }
        public FallReport(string personName, DateTime reportTime, int reportIntensity, int numOfExplosions, string reportAddress)
        {
            _reportId = 0;
            _personName = personName;
            _reportTime = reportTime;
            _reportIntensity = reportIntensity;
            _numOfExplosions = numOfExplosions;
            _reportAddress = reportAddress;
            // _reportPredictionKey = -1;
            _reportLocation = new GPSCoordinate();
        }
        #endregion

        #region Public Properties
        public int FallReportId
        {
            get
            {
                return _reportId;
            }
            set
            {
                if (_reportId != value)
                {
                    _reportId = value;
                    OnPropertyChanged("ReportId");
                }
            }
        }
        public FallPrediction FallPrediction { get; set; }
        public int? FallPredictionId
        {
            get
            {
                return _reportPredictionKey;
            }
            set
            {
                if (_reportPredictionKey != value)
                {
                    _reportPredictionKey = value;
                    OnPropertyChanged("ReportPredictionKey");
                }
            }
        }
        public string ReportAddress
        {
            get
            {
                return _reportAddress;
            }
            set
            {
                if (_reportAddress != value)
                {
                    _reportAddress = value;
                    OnPropertyChanged("ReportAddress");
                }
            }
        }
        public int NumOfExplosions
        {
            get
            {
                return _numOfExplosions;
            }
            set
            {
                if (_numOfExplosions != value)
                {
                    _numOfExplosions = value;
                    OnPropertyChanged("NumOfExplosions");
                }
            }
        }

        public GPSCoordinate ReportLocation
        {
            get
            {
                return _reportLocation;
            }
            set
            {
                if (_reportLocation != value)
                {
                    _reportLocation = value;
                    OnPropertyChanged("ReportLocation");
                }
            }
        }

        public string PersonName
        {
            get
            {
                return _personName;
            }
            set
            {
                if (_personName != value)
                {
                    _personName = value;
                    OnPropertyChanged("PersonName");
                }
            }
        }

        public DateTime ReportTime
        {
            get
            {
                return _reportTime;
            }
            set
            {
                if (_reportTime != value)
                {
                    _reportTime = value;
                    OnPropertyChanged("ReportTime");
                }
            }
        }

        public int ReportIntensity
        {
            get
            {
                return _reportIntensity;
            }
            set
            {
                if (_reportIntensity != value)
                {
                    _reportIntensity = value;
                    OnPropertyChanged("ReportIntensity");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Other Functions
        public GeoCoordinate GetCoordinate()
        {
            return new GeoCoordinate(_reportLocation.Latitude, _reportLocation.Longitude);
        }
        #endregion

    }
}
