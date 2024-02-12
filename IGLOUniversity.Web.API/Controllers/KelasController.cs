using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using IGLOUniversity.ViewModel.Kelas;

namespace IGLOUniversity.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KelasController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<KelasGrid> Get()
        {
            var model = KelasProvider.GetProvider().GetDataIndex();
            return model;
        }

        [HttpGet("{id}")]
        public JsonResultViewModel Get(int id)
        {
            var result = KelasProvider.GetProvider().GetById(id);
            return result;
        }

        [HttpPost]
        public string Post([FromBody] KelasUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = KelasProvider.GetProvider().Insert(model);
                if(isSuccess)
                {
                    return "berhasil insert";
                }
            }
            return "gagal insert";
        }

        [HttpPut]
        public string Put([FromBody] KelasUpsertVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = KelasProvider.GetProvider().Update(model);
                if(isSuccess)
                {
                    return "berhasil update";
                }
            }
            return "gagal update";
        }

        [HttpPatch("{id}")]
        public string Patch(int id, [FromBody] int kapasitas)
        {
            var isSuccess = KelasProvider.GetProvider().UpdateKapasitas(id, kapasitas);
            if (isSuccess)
            {
                return "berhasil update kapasitas";
            }
            return "gagal update kapasitas";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var isDelete = KelasProvider.GetProvider().Delete(id);
            if (isDelete)
            {
                return "delete sukses";
            }
            return "gagal delete";
        }
    }
}
