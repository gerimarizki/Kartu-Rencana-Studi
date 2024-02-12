using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Form Wajib Diisi")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Form Wajib Diisi")]
        public string Password { get; set; }
    }
}
