using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class MahasiswaIndexVM
    {
        public int TotalPages { get; set; }
        public List<MahasiswaGrid> Grids { get; set; }
    }
}
