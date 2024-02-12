using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using IGLOUniversity.ViewModel.MataKuliah;

namespace IGLOUniversity.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MataKuliahController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MataKuliahGrid> Get()
        {
            var model = MataKuliahProvider.GetProvider().GetDataIndex();
            return model;
        }

        [HttpGet("{id}")]
        public JsonResultViewModel Get(int id)
        {
            var result = MataKuliahProvider.GetProvider().GetById(id);
            return result;
        }

        [HttpPost]
        public string Post([FromBody] MataKuliahUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = MataKuliahProvider.GetProvider().Insert(model);
                if(isSuccess)
                {
                    return "berhasil insert";
                }
            }
            return "gagal insert";
        }

        [HttpPut]
        public string Put([FromBody] MataKuliahUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = MataKuliahProvider.GetProvider().Update(model);
                if (isSuccess)
                {
                    return "berhasil update";
                }
            }
            return "gagal update";
        }

        [HttpPatch("{id}")]
        public string Patch(int id, [FromBody] string deskripsi)
        {
            var isSuccess = MataKuliahProvider.GetProvider().UpdateDeskripsi(id, deskripsi);
            if (isSuccess)
            {
                return "berhasil update deskripsi";
            }
            return "gagal update deskripsi";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var isDelete = MataKuliahProvider.GetProvider().Delete(id);
            if (isDelete)
            {
                return "delete sukses";
            }
            return "gagal delete";
        }
    }
}
