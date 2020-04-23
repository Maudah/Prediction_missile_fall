using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Locations : INotifyPropertyChanged
    {
        public enum FallStatus { REAL, PREDICTION, REPORT };
        List<iLocationClass> _theObject;
        FallStatus _status;
        public Locations(FallStatus mystatus, List<iLocationClass> code)
        {
            _status = mystatus;

            _theObject = code;
        }
        public List<iLocationClass> TheObject
        {
            get
            {
                return _theObject;
            }
            set
            {
                if (_theObject != value)
                {
                    _theObject = value;
                    OnPropertyChanged("TheObject");
                }
            }
        }
        public FallStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}