using IGLOUniversity.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.MataKuliah
{
    public class MataKuliahUpsertVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(50, ErrorMessage = "Tidak boleh lebih dari 50 karakter.")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [JumlahSks]
        public int Sks { get; set; }

        [StringLength(200, ErrorMessage = "Tidak boleh lebih dari 200 karakter.")]
        public string? Deskripsi { get; set; }
    }
}
