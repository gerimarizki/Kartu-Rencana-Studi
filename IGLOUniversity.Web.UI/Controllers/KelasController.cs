using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IGLOUniversity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KelasController : BaseController
    {
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var model = KelasProvider.GetProvider().GetIndex(page);
            SetUsernameRole(User.Claims);
            return View(model);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var model = KelasProvider.GetProvider().GetInsertVM();
            //SetUsernameRole(User.Claims);
            return Json(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = KelasProvider.GetProvider().GetUpdateVM(id);
            //SetUsernameRole(User.Claims);
            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([FromBody] KelasUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                if (!KelasProvider.GetProvider().Save(model))
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

        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(int id)
        {
            if (!KelasProvider.GetProvider().Delete(id))
            {
                return Json(new { success = false, message = "gagal delete" });
            }
            else
            {
                return Json(new { success = true, message = "berhasil delete" });
            }
        }

        [HttpGet]
        public IActionResult getKelas()
        {
            var dropDown = KelasProvider.GetProvider().GetDropdownMataKuliah();
            return Json(dropDown);
        }
    }
}
