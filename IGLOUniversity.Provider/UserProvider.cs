using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.Utility;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGLOUniversity.ViewModel.User;
using IGLOUniversity.ViewModel.Mahasiswa;

namespace IGLOUniversity.Provider
{
    public class UserProvider : BaseProvider
    {
        private static UserProvider _instance = new UserProvider();
        public static UserProvider GetProvider()
        {
            return _instance;
        }
        public bool IsAuthentication(LoginVM model)
        {
            return UserRepository.GetRepository().IsAuthentication(model.Username, model.Password);
        }
        public string GetRoleName(string userName)
        {
            return UserRepository.GetRepository().GetRole(userName);
        }
        public IEnumerable<UserGrid> GetDataIndex()
        {
            var contextUser = UserRepository.GetRepository().GetAll().ToList();
            var contextMahasiswa = MahasiswaRepository.GetRepository().GetAll().ToList();
            var listUser = from u in contextUser
                           join m in contextMahasiswa on u.MahasiswaId equals m.Id into tbl
                           from t in tbl.DefaultIfEmpty()
                           select new UserGrid
                           {
                               Id = u.Id,
                               UserName = u.UserName,
                               Status = u.Status,
                               IsAdmin = u.IsAdmin ? "Ya" : "Tidak",
                               Mahasiswa = u.IsAdmin ? "N/A" : HelperFunction.GetFullName(t.NamaDepan, t.NamaTengah, t.NamaBelakang)
                           };

            return listUser;
        }
        public UserIndexVM GetIndex(int page)
        {
            var data = GetDataIndex();
            var model = new UserIndexVM
            {
                TotalPages = GetTotalPage(data.Count()),
                Grids = data.Skip(GetSkipValue(page)).Take(dataPerPage).ToList()

            };
            return model;
        }
        private List<string> GetListStatus()
        {
            var list = new List<string>()
            {
                "Aktif", "Tidak Aktif", "Terkunci"
            };
            return list;
        }
        private List<DropdownListVM> GetDropdownStatus()
        {
            var context = GetListStatus();
            var listStatus = context.Select(a => new DropdownListVM()
            {
                StringValue = a,
                Text = a
            }).ToList();

            return listStatus;
        }
        private List<DropdownListVM> GetDropdownMahasiswa()
        {
            var context = MahasiswaRepository.GetRepository().GetAll();
            var listStatus = context.Select(a => new DropdownListVM()
            {
                IntValue = a.Id,
                Text = HelperFunction.GetFullName(a.NamaDepan,a.NamaTengah, a.NamaBelakang)
            }).ToList();

            return listStatus;
        }
        public DropdownVM GetDropdown()
        {
            var model = new DropdownVM
            {
                DropdownStatus = GetDropdownStatus(),
                DropdownMahasiswa = GetDropdownMahasiswa()
            };
            return model;
        }
        public UserUpsertVM GetInsertVM()
        {
            var insertViewModel = new UserUpsertVM();
            return insertViewModel;
        }
        public UserUpsertVM GetUpdateVM(int id)
        {
            var user = UserRepository.GetRepository().GetSingle(id);
            var updateViewModel = new UserUpsertVM();
            HelperFunction.MappingModel(updateViewModel, user);
            return updateViewModel;
        }
        public bool Save(UserUpsertVM model)
        {
            if (model.IsAdmin)
            {
                model.MahasiswaId = null;
                model.Status = "Aktif";
            }
            if (model.Id == 0)
            {
                var user = new User();
                HelperFunction.MappingModel(user, model);
                user.Password = "indocyber";
                return UserRepository.GetRepository().Insert(user);
            }
            else
            {
                var user = UserRepository.GetRepository().GetSingle(model.Id);
                HelperFunction.MappingModel(user, model);
                user.Password = "indocyber";
                return UserRepository.GetRepository().Update(user);
            }
        }
        public bool Insert(UserUpsertVM model)
        {
            if (model.IsAdmin)
            {
                model.MahasiswaId = null;
                model.Status = "Aktif";
            }
            var user = new User();
            HelperFunction.MappingModel(user, model);
            user.Password = "indocyber";
            return UserRepository.GetRepository().Insert(user);
        }
        public bool Update(UserUpsertVM model)
        {
            if (model.IsAdmin)
            {
                model.MahasiswaId = null;
                model.Status = "Aktif";
            }
            var user = UserRepository.GetRepository().GetSingle(model.Id);
            HelperFunction.MappingModel(user, model);
            user.Password = "indocyber";
            return UserRepository.GetRepository().Update(user);
        }
        public bool Delete(int id)
        {
            return UserRepository.GetRepository().Delete(id);
        }
        public JsonResultViewModel GetById(int id)
        {
            var data = UserRepository.GetRepository().GetSingle(id);
            var returnData = new UserUpsertVM();
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
                result.ReturnObject = new UserUpsertVM
                {

                };
                return result;
            }
        }
        public bool UpdatePassword(int id, string password)
        {
            return UserRepository.GetRepository().UpdatePassword(id, password);
        }
    }
}
