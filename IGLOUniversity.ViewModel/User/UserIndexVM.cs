using IGLOUniversity.ViewModel.Kelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class UserIndexVM
    {
        public int TotalPages { get; set; }
        public List<UserGrid> Grids { get; set; }
    }
}
