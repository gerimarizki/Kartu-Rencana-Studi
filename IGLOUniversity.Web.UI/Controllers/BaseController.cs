using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IGLOUniversity.Web.UI.Controllers
{
    public class BaseController : Controller
    {
        protected void SetUsernameRole(IEnumerable<Claim> claims)
        {
            ViewBag.Username = GetUsername(claims);
            ViewBag.Role = GetRole(claims);
        }
        protected string GetUsername(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
        protected string GetRole(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value;
        }
        protected IEnumerable<ValidationMessageVM> GetValidationMessageVM(ModelStateDictionary modelState)
        {
            var validationMessages = new List<ValidationMessageVM>();
            foreach (KeyValuePair<string, ModelStateEntry> error in modelState)
            {
                if (error.Value.Errors.Count < 1)
                {
                    continue;
                }
                else
                {
                    string firstErrorMessage = error.Value.Errors[0].ErrorMessage;
                    validationMessages.Add(new ValidationMessageVM(error.Key, firstErrorMessage));
                }
            }
            return validationMessages;
        }
    }
}
