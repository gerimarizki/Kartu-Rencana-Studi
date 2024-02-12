using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class UserUpsertVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(50, ErrorMessage = "Tidak boleh lebih dari 50 karakter.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        public bool IsAdmin { get; set; }

        public int? MahasiswaId { get; set; }

        [Required(ErrorMessage = "Wajib Diisi")]
        [StringLength(50, ErrorMessage = "Tidak boleh lebih dari 50 karakter.")]
        public string Password { get; set; }
    }
}
