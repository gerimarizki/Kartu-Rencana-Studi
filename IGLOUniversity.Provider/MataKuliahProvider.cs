using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.MataKuliah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class MataKuliahProvider : BaseProvider
    {
        private static MataKuliahProvider _instance = new MataKuliahProvider();
        public static MataKuliahProvider GetProvider()
        {
            return _instance;
        }
        public IEnumerable<MataKuliahGrid> GetDataIndex()
        {
            var context = MataKuliahRepository.GetRepository().GetAll().ToList();
            var listMataKuliah = from m in context
                                select new MataKuliahGrid
                                {
                                    Id = m.Id,
                                    Nama = m.Nama,
                                    Sks = m.Sks,
                                    Deskripsi = m.Deskripsi
                                };

            return listMataKuliah;
        }
        public MataKuliahIndexVM GetIndex(int page)
        {
            var data = GetDataIndex();
            var model = new MataKuliahIndexVM
            {
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()

            };

            return model;
        }
        public MataKuliahUpsertVM GetInsertVM()
        {
            var insertViewModel = new MataKuliahUpsertVM();
            return insertViewModel;
        }
        public MataKuliahUpsertVM GetUpdateVM(int id)
        {
            var mataKuliah = MataKuliahRepository.GetRepository().GetSingle(id);
            var updateViewModel = new MataKuliahUpsertVM();
            HelperFunction.MappingModel(updateViewModel, mataKuliah);
            return updateViewModel;
        }
        public bool Save(MataKuliahUpsertVM model)
        {
            if (model.Id == 0)
            {
                var mataKuliah = new Matakuliah();
                HelperFunction.MappingModel(mataKuliah, model);
                return MataKuliahRepository.GetRepository().Insert(mataKuliah);
            }
            else
            {
                var mataKuliah = MataKuliahRepository.GetRepository().GetSingle(model.Id);
                HelperFunction.MappingModel(mataKuliah, model);
                return MataKuliahRepository.GetRepository().Update(mataKuliah);
            }
        }
        public bool Insert(MataKuliahUpsertVM model)
        {
            var mataKuliah = new Matakuliah();
            HelperFunction.MappingModel(mataKuliah, model);
            return MataKuliahRepository.GetRepository().Insert(mataKuliah);
        }
        public bool Update(MataKuliahUpsertVM model)
        {
            var mataKuliah = MataKuliahRepository.GetRepository().GetSingle(model.Id);
            HelperFunction.MappingModel(mataKuliah, model);
            return MataKuliahRepository.GetRepository().Update(mataKuliah);
        }
        public bool Delete(int id)
        {
            return MataKuliahRepository.GetRepository().Delete(id);
        }
        public JsonResultViewModel GetById(int id)
        {
            var data = MataKuliahRepository.GetRepository().GetSingle(id);
            var returnData = new MataKuliahUpsertVM();
            var result = new JsonResultViewModel();
            if (data != null)
            {
                HelperFunction.MappingModel(returnData, data);
                result.Succes = true;
                result.ReturnObject = returnData;
                return result;
            }
            else
            {
                result.Succes = false;
                result.Message = "tidak tersedia";
                result.ReturnObject = new MataKuliahUpsertVM
                {

                };
                return result;
            }
        }
        public bool UpdateDeskripsi(int id, string deskripsi)
        {
            var repo = (MataKuliahRepository)MataKuliahRepository.GetRepository();
            return repo.UpdateDescription(id, deskripsi);
        }
    }
}
