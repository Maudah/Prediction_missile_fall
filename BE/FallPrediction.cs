using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;

namespace BE
{
    public class FallPrediction : INotifyPropertyChanged,iLocationClass
    {
        #region Private Fields
        private int _fallPredictionId;
        private DateTime _fallPredictionTime;
        private int _fallPredictionFallKey = -1;
        private GPSCoordinate _fallPredictionLocation;
        #endregion

        #region Public Properties
        public int FallPredictionId
        {
            get
            {
                return _fallPredictionId;
            }
            set
            {
                if (_fallPredictionId != value)
                {
                    _fallPredictionId = value;
                    OnPropertyChanged("FallPredictionId");
                }
            }
        }
        public virtual Fall Fall { get; set; }

        public DateTime FallPredictionTime
        {
            get
            {
                return _fallPredictionTime;
            }
            set
            {
                if (_fallPredictionTime != value)
                {
                    _fallPredictionTime = value;
                    OnPropertyChanged("FallPredictionTime");
                }
            }
        }
        public int FallPredictionFallKey
        {
            get
            {
                return _fallPredictionFallKey;
            }
            set
            {
                if (_fallPredictionFallKey != value)
                {
                    _fallPredictionFallKey = value;
                    OnPropertyChanged("FallPredictionFallKey");
                }
            }
        }
        public GPSCoordinate FallPredictionLocation
        {
            get
            {
                return _fallPredictionLocation;
            }
            set
            {
                if (_fallPredictionLocation != value)
                {
                    _fallPredictionLocation = value;
                    OnPropertyChanged("FallPredictionLocation");
                }
            }

        }
        public ICollection<FallReport> Students { get; set; }



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
            return new GeoCoordinate(_fallPredictionLocation.Latitude, _fallPredictionLocation.Longitude);
        }
        #endregion
    }
}

