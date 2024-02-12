using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class RencanaStudiAdminIndexVM
    {
        public int TotalPages { get; set; }
        public List<RencanaStudiAdminGrid> Grids { get; set; }
    }
}
