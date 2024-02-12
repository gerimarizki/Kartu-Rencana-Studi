using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Validation
{
    public class JumlahSksAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var sks = (int)value;
            if (sks < 1)
            {
                return new ValidationResult(ErrorMessage = "Tidak boleh kurang dari 1");
            }
            else if(sks > 5)
            {
                return new ValidationResult(ErrorMessage = "Tidak boleh lebih dari 5");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
