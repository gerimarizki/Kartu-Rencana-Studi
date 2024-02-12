using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.RencanaStudi;
using IGLOUniversity.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace IGLOUniversity.Web.UI.Controllers
{
    public class RencanaStudiController : BaseController
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var model = RencanaStudiProvider.GetProvider().GetIndex(page);
            SetUsernameRole(User.Claims);
            return View(model);
        }

        [Authorize(Roles = "Administrator, Aktif")]
        [HttpGet]
        public IActionResult Detail(int id, int page = 1)
        {
            var model = RencanaStudiProvider.GetProvider().GetDetails(id, page);
            SetUsernameRole(User.Claims);
            return View(model);
        }

        [Authorize(Roles = "Administrator, Aktif")]
        [HttpGet]
        public IActionResult IndexMahasiswa() 
        {
            SetUsernameRole(User.Claims);
            var userName = ViewBag.Username;
            var id = RencanaStudiProvider.GetProvider().GetId(userName);
            return RedirectToAction("Detail", new { id = id });
        }

        [Authorize(Roles = "Administrator, Aktif")]
        [HttpGet]
        public IActionResult GetDropdown()
        {
            var dropDown = RencanaStudiProvider.GetProvider().GetDropdown();
            return Json(dropDown);
        }

        [Authorize(Roles = "Administrator, Aktif")]
        [HttpGet]
        public IActionResult GetId(int id)
        {
            //SetUsernameRole(User.Claims);
            return Json(new {Id = id});
        }

        [Authorize(Roles = "Administrator, Aktif")]
        [HttpPost]
        public IActionResult Insert([FromBody] RencanaStudiAddVM model)
        {
            if (ModelState.IsValid)
            {
                if (!RencanaStudiProvider.GetProvider().Insert(model))
                {
                    SetUsernameRole(User.Claims);
                    return View("FailedUpsert");
                }

                return Json(new { success = true });
            }
            else
            {
                var validationMessage = GetValidationMessageVM(ModelState);
                return Json(new { success = false, message = "gagal", validations = validationMessage });
            }
        }

        [Authorize(Roles = "Administrator")]
        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(int id)
        {
            if (!RencanaStudiProvider.GetProvider().Delete(id))
            {
                return Json(new { success = false, message = "gagal delete" });
            }
            else
            {
                return Json(new { success = true, message = "berhasil delete" });
            }
        }
    }
}
