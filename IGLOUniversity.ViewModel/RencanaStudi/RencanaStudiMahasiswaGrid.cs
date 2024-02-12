using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class RencanaStudiMahasiswaGrid
    {
        public int Id { get; set; }
        public string Kelas { get; set; }
        public string MataKuliah { get; set; }
        public int Sks { get; set; }
        public string Semester { get; set; }
        public int Kapasitas { get; set; }
        public string Status { get; set; }
    }
}
