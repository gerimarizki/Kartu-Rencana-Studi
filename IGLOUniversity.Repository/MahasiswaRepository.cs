using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class MahasiswaRepository : IRepository<Mahasiswa>
    {
        private static IRepository<Mahasiswa> _instance = new MahasiswaRepository();
        public static IRepository<Mahasiswa> GetRepository()
        {
            return _instance;
        }

        public IQueryable<Mahasiswa> GetAll()
        {
            var context = new IGLOUniversityContext();
            return context.Mahasiswas;
        }

        public Mahasiswa GetSingle(int id)
        {
            using (var context = new IGLOUniversityContext())
            {
                return context.Mahasiswas.SingleOrDefault(m => m.Id == id);
            }
        }

        public bool Insert(Mahasiswa model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    context.Mahasiswas.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Mahasiswa model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mahasiswa = context.Mahasiswas.SingleOrDefault(m => m.Id == model.Id);
                    if (mahasiswa == null) { return false; }

                    HelperFunction.MappingModel(mahasiswa, model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mahasiswa = context.Mahasiswas.SingleOrDefault(m => m.Id == id);
                    if (mahasiswa == null) { return false; }

                    var isAnyRelation = context.DistribusiMataKuliahs.Any(d => d.IdMahasiswa == id);
                    if (isAnyRelation) { return false; }

                    var isAnyUser = context.Users.Any(d => d.MahasiswaId == id);
                    if (isAnyUser) { return false; }

                    context.Mahasiswas.Remove(mahasiswa);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateNomorHp(int id, string nomorHp)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mahasiswa = context.Mahasiswas.SingleOrDefault(m => m.Id == id);
                    mahasiswa.NomorHp = nomorHp;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}
