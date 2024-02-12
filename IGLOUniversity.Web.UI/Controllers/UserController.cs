using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace IGLOUniversity.Web.UI.Controllers
{
    public class UserController : BaseController
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var model = UserProvider.GetProvider().GetIndex(page);
            SetUsernameRole(User.Claims);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Insert()
        {
            var model = UserProvider.GetProvider().GetInsertVM();
            SetUsernameRole(User.Claims);
            return Json(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = UserProvider.GetProvider().GetUpdateVM(id);
            //SetUsernameRole(User.Claims);
            return Json(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save([FromBody] UserUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                if (!UserProvider.GetProvider().Save(model))
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
            if (!UserProvider.GetProvider().Delete(id))
            {
                return Json(new { success = false, message = "gagal delete" });
            }
            else
            {
                return Json(new { success = true, message = "berhasil delete" });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult GetDropdown()
        {
            var dropdown = UserProvider.GetProvider().GetDropdown();
            return Json(dropdown);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var viewModel = new LoginVM();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (UserProvider.GetProvider().IsAuthentication(viewModel))
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, viewModel.Username),
                        new Claim(ClaimTypes.Role, UserProvider.GetProvider().GetRoleName(viewModel.Username))
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    return View("LoginFailed");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
