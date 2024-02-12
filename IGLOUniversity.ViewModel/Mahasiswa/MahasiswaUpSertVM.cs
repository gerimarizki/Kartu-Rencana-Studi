using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class MahasiswaUpSertVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(50, ErrorMessage = "Tidak boleh lebih dari 50 karakter.")]
        public string Nim { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(200, ErrorMessage = "Tidak boleh lebih dari 200 karakter.")]
        public string NamaDepan { get; set; }

        [StringLength(200, ErrorMessage = "Tidak boleh lebih dari 200 karakter.")]
        public string? NamaTengah { get; set; }

        [StringLength(200, ErrorMessage = "Tidak boleh lebih dari 200 karakter.")]
        public string? NamaBelakang { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(100, ErrorMessage = "Tidak boleh lebih dari 100 karakter.")]
        public string AsalSma { get; set; }

        [MaxLength(13, ErrorMessage = "Tidak boleh lebih dari 13 angka.")]
        public string? NomorHp { get; set; }

        [StringLength(100, ErrorMessage = "Tidak boleh lebih dari 200 karakter.")]
        public string? Alamat { get; set; }
    }
}
