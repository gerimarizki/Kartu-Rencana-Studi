using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class KelasProvider : BaseProvider
    {
        private static KelasProvider _instance = new KelasProvider();
        public static KelasProvider GetProvider()
        {
            return _instance;
        }
        public IEnumerable<KelasGrid> GetDataIndex()
        {
            var contextKelas = KelasRepository.GetRepository().GetAll().ToList();
            var contextMataKuliah = MataKuliahRepository.GetRepository().GetAll().ToList();
            var listKelas = from k in contextKelas
                            join m in contextMataKuliah on k.IdMatakuliah equals m.Id into tbl
                            from t in tbl.DefaultIfEmpty()
                            select new KelasGrid
                            {
                                Id = k.Id,
                                Nama = k.Nama,
                                Matakuliah = t.Nama,
                                Sks = t.Sks,
                                Semester = k.Semester,
                                Kapasitas = k.Kapasitas
                            };

            return listKelas;
        }
        public KelasIndexVM GetIndex(int page)
        {
            var data = GetDataIndex();
            var model = new KelasIndexVM
            {
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()

            };
            return model;
        }
        public List<DropdownListVM> GetDropdownMataKuliah()
        {
            var context = MataKuliahRepository.GetRepository().GetAll();
            var listMataKuliah = context.Select(a => new DropdownListVM()
            {
                IntValue = a.Id,
                Text = a.Nama
            }).ToList();

            return listMataKuliah;
        }
        public KelasUpsertVM GetInsertVM()
        {
            var insertViewModel = new KelasUpsertVM();
            return insertViewModel;
        }
        public KelasUpsertVM GetUpdateVM(int id)
        {
            var mahasiswa = KelasRepository.GetRepository().GetSingle(id);
            var updateViewModel = new KelasUpsertVM();
            HelperFunction.MappingModel(updateViewModel, mahasiswa);
            return updateViewModel;
        }
        public bool Save(KelasUpsertVM model)
        {
            if (model.Id == 0)
            {
                var kelas = new Kela();
                HelperFunction.MappingModel(kelas, model);
                return KelasRepository.GetRepository().Insert(kelas);
            }
            else
            {
                var kelas = KelasRepository.GetRepository().GetSingle(model.Id);
                HelperFunction.MappingModel(kelas, model);
                return KelasRepository.GetRepository().Update(kelas);
            }
        }
        public bool Insert(KelasUpsertVM model)
        {
            var kelas = new Kela();
            HelperFunction.MappingModel(kelas, model);
            return KelasRepository.GetRepository().Insert(kelas);
        }
        public bool Update(KelasUpsertVM model)
        {
            var kelas = KelasRepository.GetRepository().GetSingle(model.Id);
            HelperFunction.MappingModel(kelas, model);
            return KelasRepository.GetRepository().Update(kelas);
        }
        public bool Delete(int id)
        {
            return KelasRepository.GetRepository().Delete(id);
        }
        public JsonResultViewModel GetById(int id)
        {
            var data = KelasRepository.GetRepository().GetSingle(id);
            var returnData = new KelasUpsertVM();
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
                result.ReturnObject = new KelasUpsertVM
                {

                };
                return result;
            }
        }
        public bool UpdateKapasitas(int id, int kapasitas)
        {
            var repo = (KelasRepository)KelasRepository.GetRepository();
            return repo.UpdateCapacity(id, kapasitas);
        }

    }
}
