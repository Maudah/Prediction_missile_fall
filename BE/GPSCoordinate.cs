using System;



namespace BE
{
   public class GPSCoordinate
    {
        #region Private Fields
        private double _latitude;
        private double _longitude;
        #endregion

        #region Public Properties
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (_latitude != value)
                    _latitude = value;
            }
        }
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (_longitude != value)
                    _longitude = value;
            }
        }
        #endregion

        #region Constructors
        public GPSCoordinate()
        {
           
        }
       
        public GPSCoordinate(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
        #endregion
    }

}
