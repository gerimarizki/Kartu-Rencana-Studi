using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel
{
    public class JsonResultViewModel
    {
        public int Code { get; set; }
        public bool Succes { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }
    }
}
