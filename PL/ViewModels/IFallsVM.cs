using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BE;

namespace PL.ViewModels
{
    public interface IFallsVM
    {
        void AddFall(Fall fall);
        ObservableCollection<Fall> Falls { get; set; }
    }
}
