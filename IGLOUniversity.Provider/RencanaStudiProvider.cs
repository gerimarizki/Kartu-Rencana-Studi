using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.RencanaStudi;
using IGLOUniversity.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class RencanaStudiProvider : BaseProvider
    {
        private static RencanaStudiProvider _instance = new RencanaStudiProvider();
        public static RencanaStudiProvider GetProvider()
        {
            return _instance;
        }

        public IEnumerable<RencanaStudiAdminGrid> GetDataIndex()
        {
            var contextDistribusi = DistribusiMataKuliahRepository.GetRepository().GetAll().ToList();
            var contextMahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var contextKelas = KelasRepository.GetRepository().GetAll().ToList();
            var contextMataKuliah = MataKuliahRepository.GetRepository().GetAll().ToList();
            var listRencanaStudi = from d in contextDistribusi
                                   join k in contextKelas on d.IdKelas equals k.Id
                                   join m in contextMahasiswa on d.IdMahasiswa equals m.Id
                                   join mk in contextMataKuliah on k.IdMatakuliah equals mk.Id into newTbl
                                   from tbl in newTbl
                                   group tbl by new
                                   {
                                       m.Id,
                                       m.Nim,
                                       m.NamaDepan,
                                       m.NamaTengah,
                                       m.NamaBelakang,
                                       m.AsalSma,
                                       m.NomorHp,
                                       m.Alamat
                                   } into result
                                   select new RencanaStudiAdminGrid
                                   {
                                       Id = result.Key.Id,
                                       Nim = result.Key.Nim,
                                       Nama = HelperFunction.GetFullName(result.Key.NamaDepan, result.Key.NamaTengah, result.Key.NamaBelakang),
                                       AsalSma = result.Key.AsalSma,
                                       NomorHp = result.Key.NomorHp,
                                       Alamat = result.Key.Alamat,
                                       TotalSks = result.Sum(t => t == null ? 0 : t.Sks)
                                   };

            return listRencanaStudi;
        }
        public RencanaStudiAdminIndexVM GetIndex(int page)
        {
            var data = GetDataIndex();
            var model = new RencanaStudiAdminIndexVM
            {
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()
            };
            return model;
        }
        public IEnumerable<RencanaStudiMahasiswaGrid> GetDataDetail(int id)
        {
            var contextDistribusi = DistribusiMataKuliahRepository.GetRepository().GetAll().ToList();
            var contextMahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var contextKelas = KelasRepository.GetRepository().GetAll().ToList();
            var contextMataKuliah = MataKuliahRepository.GetRepository().GetAll().ToList();
            var listRencanaStudi = from d in contextDistribusi
                                   join k in contextKelas on d.IdKelas equals k.Id
                                   join m in contextMahasiswa on d.IdMahasiswa equals m.Id
                                   join mk in contextMataKuliah on k.IdMatakuliah equals mk.Id
                                   where m.Id == id
                                   select new RencanaStudiMahasiswaGrid
                                   {
                                       Id = d.Id,
                                       Kelas = k.Nama,
                                       MataKuliah = mk.Nama,
                                       Sks = mk.Sks,
                                       Semester = k.Semester,
                                       Kapasitas = k.Kapasitas,
                                       Status = d.Status
                                   };

            return listRencanaStudi;
        }
        private int GetTotalSks(int id)
        {
            var contextDistribusi = DistribusiMataKuliahRepository.GetRepository().GetAll().ToList();
            var contextMahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var contextKelas = KelasRepository.GetRepository().GetAll().ToList();
            var contextMataKuliah = MataKuliahRepository.GetRepository().GetAll().ToList();
            var sks = from m in contextMahasiswa
                      join d in contextDistribusi on m.Id equals d.IdMahasiswa into newTbl
                      from tbl in newTbl
                      join k in contextKelas on tbl.IdKelas equals k.Id
                      join mk in contextMataKuliah on k.IdMatakuliah equals mk.Id into tblBaru
                      where m.Id == id
                      from t in tblBaru
                      select t.Sks;

            var totalSks = sks == null? 0 : sks.Sum();
            return totalSks;
        }
        public RencanaStudiMahasiswaIndexVM GetDetails(int id,int page)
        {
            var data = GetDataDetail(id).ToList();
            var totalSks = GetTotalSks(id);
            var mahasiswa = MahasiswaRepository.GetRepository().GetSingle(id);

            var model = new RencanaStudiMahasiswaIndexVM
            {
                IdMahasiswa = mahasiswa.Id,
                Nim = mahasiswa.Nim,
                Nama = HelperFunction.GetFullName(mahasiswa.NamaDepan, mahasiswa.NamaTengah, mahasiswa.NamaBelakang),
                AsalSma = mahasiswa.AsalSma,
                TotalSks = totalSks,
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()
            };
            return model;
        }
        private List<DropdownListVM> GetDropdownKelas()
        {
            var context = KelasRepository.GetRepository().GetAll();
            var listKelas = context.Select(a => new DropdownListVM()
            {
                IntValue = a.Id,
                Text = a.Nama
            }).ToList();

            return listKelas;
        }
        private List<string> GetListStatus()
        {
            var list = new List<string>()
            {
                "Aktif", "Lulus", "Tidak Lulus"
            };
            return list;
        }
        private List<DropdownListVM> GetDropdownStatus()
        {
            var dropdown = GetListStatus().Select(a => new DropdownListVM()
            {
                StringValue = a,
                Text = a
            }).ToList();

            return dropdown;
        }

        private List<DropdownListVM> GetListKapasitas()
        {
            var context = KelasRepository.GetRepository().GetAll();
            var listKelas = context.Select(a => new DropdownListVM()
            {
                IntValue = a.Kapasitas,
                Text = a.Nama
            }).ToList();

            return listKelas;
        }
        public DropdownDataVM GetDropdown()
        {
            var model = new DropdownDataVM()
            {
                DropdownKelas = GetDropdownKelas(),
                DropdownKapasitas = GetListKapasitas(),
                DropdownStatus = GetDropdownStatus()
            };
            return model;
        }
        public MahasiswaUpSertVM GetUpdateVM(int id)
        {
            var mahasiswa = MahasiswaRepository.GetRepository().GetSingle(id);
            var updateViewModel = new MahasiswaUpSertVM();
            HelperFunction.MappingModel(updateViewModel, mahasiswa);
            return updateViewModel;
        }
        public bool Insert(RencanaStudiAddVM model)
        {

            var transaksi = new DistribusiMataKuliah();
            HelperFunction.MappingModel(transaksi, model);
            return DistribusiMataKuliahRepository.GetRepository().Insert(transaksi);
        }
        public bool Delete(int id)
        {
            return DistribusiMataKuliahRepository.GetRepository().Delete(id);
        }
        public int GetId(string userName)
        {
            return UserRepository.GetRepository().GetId(userName);
        }
    }
}
