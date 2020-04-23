using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
   public interface IPredictionAndRealFallsVM
    {
        ObservableCollection<Locations> Locations { get; set; }
    }
}
