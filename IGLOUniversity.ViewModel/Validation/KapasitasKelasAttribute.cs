using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Validation
{
    public class KapasitasKelasAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var capacity = (int)value;
            if (capacity < 1)
            {
                return new ValidationResult(ErrorMessage = "Tidak boleh kurang dari 1");
            }
            else if (capacity > 30)
            {
                return new ValidationResult(ErrorMessage = "Tidak boleh lebih dari 30");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
