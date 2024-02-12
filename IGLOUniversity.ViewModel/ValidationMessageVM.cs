using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel
{
    public class ValidationMessageVM
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public ValidationMessageVM(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }
    }
}
