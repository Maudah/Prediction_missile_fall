using Hangfire.Annotations;
using System.Windows.Controls;
using PL.Models;
using PL.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PL.ViewModels
{
    public class GeoLocationAutoCompleteVM : INotifyPropertyChanged
    {
        private List<Result> _locationList;
        private Result _selectedResult;

        public List<Result> LocatioList
        {
            get { return _locationList; }
            set
            {
                _locationList = value;
                OnPropertyChanged();
            }
        }

        public Result SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
