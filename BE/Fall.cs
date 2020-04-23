using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;

namespace BE
{
    public class Fall : INotifyPropertyChanged,iLocationClass
    {
        #region Private Fields
        private int _fallId;
        private DateTime _fallTime;
        private string _fallImage;
        private GPSCoordinate _fallLocation;
        private string _fallAddress;
        #endregion

        #region Public Properties
        [ForeignKey("FallPrediction")]
        public int FallId
        {
            get
            {
                return _fallId;
            }
            set
            {
                if (_fallId != value)
                {
                    _fallId = value;
                    OnPropertyChanged("FallId");
                }
            }
        }
        public virtual FallPrediction FallPrediction { get; set; }

        public string FallAddress
        {
            get
            {
                return _fallAddress;
            }
            set
            {
                if (_fallAddress != value)
                {
                    _fallAddress = value;
                    OnPropertyChanged("FallAddress");
                }
            }
        }
        public GPSCoordinate FallLocation
        {
            get
            {
                return _fallLocation;
            }
            set
            {
                if (_fallLocation != value)
                {
                    _fallLocation = value;
                    OnPropertyChanged("FallLocation");
                }
            }
        }
        public string FallImage
        {
            get
            {
                return _fallImage;
            }
            set
            {
                if (_fallImage != value)
                {
                    _fallImage = value;
                    OnPropertyChanged("FallImage");
                }
            }
        }

        public DateTime FallTime
        {
            get
            {
                return _fallTime;
            }
            set
            {
                if (_fallTime != value)
                {
                    _fallTime = value;
                    OnPropertyChanged("FallTime");
                }
            }
        }
        #endregion

        #region Constructors
        public Fall()
        {
        }
        public Fall(int fallId, GPSCoordinate fallLocation, string fallImage, DateTime fallTime, string fallAddress)
        {
            _fallId = fallId;
            _fallLocation = fallLocation;
            _fallImage = fallImage;
            _fallTime = fallTime;
            _fallAddress = fallAddress;
        }
        public Fall(string fallImage, DateTime fallTime)
        {

            _fallLocation = new GPSCoordinate();
            _fallImage = fallImage;
            _fallTime = fallTime;

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
            return new GeoCoordinate(_fallLocation.Latitude, _fallLocation.Longitude);
        }
        #endregion
    }
}
