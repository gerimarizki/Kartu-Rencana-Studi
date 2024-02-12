using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using IGLOUniversity.ViewModel.Mahasiswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class MahasiswaProvider : BaseProvider
    {
        private static MahasiswaProvider _instance = new MahasiswaProvider();
        public static MahasiswaProvider GetProvider()
        {
            return _instance;
        }
        public IEnumerable<MahasiswaGrid> GetDataIndex()
        {
            var context = MahasiswaRepository.GetRepository().GetAll().ToList();
            var listMahasiswa = from m in context
                                select new MahasiswaGrid
                                {
                                    Id = m.Id,
                                    Nim = m.Nim,
                                    NamaLengkap = HelperFunction.GetFullName(m.NamaDepan, m.NamaTengah, m.NamaBelakang),
                                    AsalSma = m.AsalSma,
                                    NomorHp = m.NomorHp == null? "N/A": m.NomorHp,
                                    Alamat = m.Alamat == null? "N/A" : m.Alamat
                                };
                             

            return listMahasiswa;
        }
        public MahasiswaIndexVM GetIndex(int page)
        {
            var data = GetDataIndex();
            var model = new MahasiswaIndexVM
            {
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()

            };
            return model;
        }
        public MahasiswaUpSertVM GetInsertVM()
        {
            var insertViewModel = new MahasiswaUpSertVM();
            return insertViewModel;
        }
        public MahasiswaUpSertVM GetUpdateVM(int id)
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetSingle(id);
            var updateViewModel = new MahasiswaUpSertVM();
            HelperFunction.MappingModel(updateViewModel, mahasiswa);
            return updateViewModel;
        }
        public bool Save(MahasiswaUpSertVM model)
        {
            if (model.Id == 0)
            {
                var mahasiswa = new Mahasiswa();
                HelperFunction.MappingModel(mahasiswa, model);
                return MahasiswaRepository.GetRepository().Insert(mahasiswa);
            }
            else
            {
                var mahasiswa = MahasiswaRepository.GetRepository().GetSingle(model.Id);
                HelperFunction.MappingModel(mahasiswa, model);
                return MahasiswaRepository.GetRepository().Update(mahasiswa);
            }
        }
        public bool Insert(MahasiswaUpSertVM model)
        {
            var mahasiswa = new Mahasiswa();
            HelperFunction.MappingModel(mahasiswa, model);
            return MahasiswaRepository.GetRepository().Insert(mahasiswa);
        }
        public bool Update(MahasiswaUpSertVM model)
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetSingle(model.Id);
            HelperFunction.MappingModel(mahasiswa, model);
            return MahasiswaRepository.GetRepository().Update(mahasiswa);
        }
        public bool Delete(int id)
        {
            return MahasiswaRepository.GetRepository().Delete(id);
        }
        public JsonResultViewModel GetById(int id)
        {
            var data = MahasiswaRepository.GetRepository().GetSingle(id);
            var returnData = new MahasiswaUpSertVM();
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
                result.ReturnObject = new MahasiswaUpSertVM
                {

                };
                return result;
            }
        }
        public bool UpdateNomorHp(int id, string nomorHp)
        {
            var repo = (MahasiswaRepository)MahasiswaRepository.GetRepository();
            return repo.UpdateNomorHp(id, nomorHp);
        }
    }
}
