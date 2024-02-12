using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class KelasIndexVM
    {
        public int TotalPages { get; set; }
        public List<KelasGrid> Grids { get; set; }
    }
}
