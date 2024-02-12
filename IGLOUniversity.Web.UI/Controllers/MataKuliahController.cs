using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.MataKuliah;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IGLOUniversity.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MataKuliahController : BaseController
    {
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var model = MataKuliahProvider.GetProvider().GetIndex(page);
            SetUsernameRole(User.Claims);
            return View(model);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var model = MataKuliahProvider.GetProvider().GetInsertVM();
            //SetUsernameRole(User.Claims);
            return Json(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = MataKuliahProvider.GetProvider().GetUpdateVM(id);
            //SetUsernameRole(User.Claims);
            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([FromBody] MataKuliahUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                if (!MataKuliahProvider.GetProvider().Save(model))
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
            if (!MataKuliahProvider.GetProvider().Delete(id))
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
