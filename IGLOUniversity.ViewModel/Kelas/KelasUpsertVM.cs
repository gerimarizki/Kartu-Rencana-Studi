using IGLOUniversity.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class KelasUpsertVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(50, ErrorMessage = "Tidak boleh lebih dari 50 karakter.")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        public int IdMatakuliah { get; set; }

        [StringLength(30, ErrorMessage = "Tidak boleh lebih dari 30 karakter.")]
        public string Semester { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [KapasitasKelas]
        public int Kapasitas { get; set; }
    }
}
