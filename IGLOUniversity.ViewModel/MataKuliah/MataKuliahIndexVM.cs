using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.MataKuliah
{
    public class MataKuliahIndexVM
    {
        public int TotalPages { get; set; }
        public List<MataKuliahGrid> Grids { get; set; }
    }
}
