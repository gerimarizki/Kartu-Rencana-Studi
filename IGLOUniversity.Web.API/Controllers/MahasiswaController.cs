using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Provider;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MahasiswaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MahasiswaGrid> Get()
        {
            var model = MahasiswaProvider.GetProvider().GetDataIndex();
            return model;
        }

        [HttpGet("{id}")]
        public JsonResultViewModel Get(int id)
        {
            var result = MahasiswaProvider.GetProvider().GetById(id);
            return result;
        }

        [HttpPost]
        public string Post([FromBody] MahasiswaUpSertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = MahasiswaProvider.GetProvider().Insert(model);
                if(isSuccess)
                {
                    return "berhasil insert";
                }
            }
            return "gagal insert";
        }

        [HttpPut]
        public string Put([FromBody] MahasiswaUpSertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = MahasiswaProvider.GetProvider().Update(model);
                if (isSuccess)
                {
                    return "berhasil update";
                }
            }
            return "gagal update";
        }

        [HttpPatch("{id}")]
        public string Patch(int id, [FromBody] string nomorHp)
        {
            var isSuccess = MahasiswaProvider.GetProvider().UpdateNomorHp(id, nomorHp);
            if (isSuccess)
            {
                return "berhasil update nomor hp";
            }
            return "gagal update nomor hp";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var isDelete = MahasiswaProvider.GetProvider().Delete(id);
            if (isDelete)
            {
                return "delete sukses";
            }
            return "gagal delete";
        }
    }
}
