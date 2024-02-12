using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.User;
using IGLOUniversity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using IGLOUniversity.ViewModel.RencanaStudi;

namespace IGLOUniversity.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RencanaStudiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RencanaStudiAdminGrid> Get()
        {
            var model = RencanaStudiProvider.GetProvider().GetDataIndex();
            return model;
        }

        [HttpGet("{id}")]
        public IEnumerable<RencanaStudiMahasiswaGrid> Get(int id)
        {
            var result = RencanaStudiProvider.GetProvider().GetDataDetail(id);
            return result;
        }

        [HttpPost]
        public string Post([FromBody] RencanaStudiAddVM model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = RencanaStudiProvider.GetProvider().Insert(model);
                if (isSuccess)
                {
                    return "berhasil insert";
                }
            }
            return "gagal insert";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var isDelete = RencanaStudiProvider.GetProvider().Delete(id);
            if (isDelete)
            {
                return "sukses delete";
            }
            return "gagal delete";
        }
    }
}
