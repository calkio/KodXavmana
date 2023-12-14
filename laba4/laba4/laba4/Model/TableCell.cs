using laba4.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4.Model
{
    internal class TableCell:BaseVM
    {
        private ObservableCollection<double> _rowValues;
        public ObservableCollection<double> RowValues
        {
            get => _rowValues;
            set => Set(ref _rowValues, value);
        }
    }
}
