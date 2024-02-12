using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class DropdownDataVM
    {
        public List<DropdownListVM> DropdownKelas { get; set; }
        public List<DropdownListVM> DropdownKapasitas { get; set; }
        public List<DropdownListVM> DropdownStatus { get; set; }
    }
}
