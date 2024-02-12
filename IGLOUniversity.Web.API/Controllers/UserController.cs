using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using IGLOUniversity.ViewModel.User;

namespace IGLOUniversity.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UserGrid> Get()
        {
            var model = UserProvider.GetProvider().GetDataIndex();
            return model;
        }

        [HttpGet("{id}")]
        public JsonResultViewModel Get(int id)
        {
            var result = UserProvider.GetProvider().GetById(id);
            return result;
        }

        [HttpPost]
        public string Post([FromBody] UserUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = UserProvider.GetProvider().Insert(model);
                if(isSuccess)
                {
                    return "berhasil insert";
                }
            }
            return "gagal insert";
        }

        [HttpPut]
        public string Put([FromBody] UserUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = UserProvider.GetProvider().Update(model);
                if(isSuccess)
                {
                    return "berhasil update";
                }
            }
            return "gagal update";
        }

        [HttpPatch("{id}")]
        public string Patch(int id, [FromBody] string password)
        {
            var isSuccess = UserProvider.GetProvider().UpdatePassword(id, password);
            if (isSuccess)
            {
                return "berhasil update password";
            }
            return "gagal update password";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var isDelete = UserProvider.GetProvider().Delete(id);
            if (isDelete)
            {
                return "sukses delete";
            }
            return "gagal delete";
        }
    }
}
