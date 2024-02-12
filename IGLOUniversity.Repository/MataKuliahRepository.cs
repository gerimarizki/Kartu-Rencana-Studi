using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class MataKuliahRepository : IRepository<Matakuliah>
    {
        private static IRepository<Matakuliah> _instance = new MataKuliahRepository();
        public static IRepository<Matakuliah> GetRepository()
        {
            return _instance;
        }

        public IQueryable<Matakuliah> GetAll()
        {
            var context = new IGLOUniversityContext();
            return context.Matakuliahs;
        }

        public Matakuliah GetSingle(int id)
        {
            using (var context = new IGLOUniversityContext())
            {
                return context.Matakuliahs.SingleOrDefault(m => m.Id == id);
            }
        }

        public bool Insert(Matakuliah model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    context.Matakuliahs.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Matakuliah model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mataKuliah = context.Matakuliahs.SingleOrDefault(m => m.Id == model.Id);
                    if (mataKuliah == null) { return false; }

                    HelperFunction.MappingModel(mataKuliah, model);
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
                    var mataKuliah = context.Matakuliahs.SingleOrDefault(m => m.Id == id);
                    if (mataKuliah == null) { return false; }

                    var isAnyRelation = context.Kelas.Any(d => d.IdMatakuliah == id);
                    if (isAnyRelation) { return false; }

                    context.Matakuliahs.Remove(mataKuliah);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDescription(int id, string deskripsi)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mataKuliah = context.Matakuliahs.SingleOrDefault(m => m.Id == id);
                    mataKuliah.Deskripsi = deskripsi;
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
