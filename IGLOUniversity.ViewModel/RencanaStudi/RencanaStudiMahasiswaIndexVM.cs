using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class RencanaStudiMahasiswaIndexVM
    {
        public int IdMahasiswa { get; set; }
        public string Nim { get; set; }
        public string Nama { get; set; }
        public string AsalSma { get; set; }
        public int TotalSks { get; set; }
        public int TotalPages { get; set; }
        public List<RencanaStudiMahasiswaGrid> Grids { get; set; }
    }
}
