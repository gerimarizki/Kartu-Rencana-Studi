using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class KelasRepository : IRepository<Kela>
    {
        private static IRepository<Kela> _instance = new KelasRepository();
        public static IRepository<Kela> GetRepository()
        {
            return _instance;
        }

        public IQueryable<Kela> GetAll()
        {
            var context = new IGLOUniversityContext();
            return context.Kelas;
        }

        public Kela GetSingle(int id)
        {
            using (var context = new IGLOUniversityContext())
            {
                return context.Kelas.SingleOrDefault(k => k.Id == id);
            }
        }

        public bool Insert(Kela model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    context.Kelas.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Kela model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var kelas = context.Kelas.SingleOrDefault(k => k.Id == model.Id);
                    if (kelas == null) { return false; }

                    HelperFunction.MappingModel(kelas, model);
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
                    var kelas = context.Kelas.SingleOrDefault(k => k.Id == id);
                    if (kelas == null) { return false; }

                    var isAnyRelation = context.DistribusiMataKuliahs.Any(d => d.IdKelas == id);
                    if (isAnyRelation) { return false; }

                    context.Kelas.Remove(kelas);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateCapacity(int id, int kapasitas)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var mahasiswa = context.Kelas.SingleOrDefault(k => k.Id == id);
                    mahasiswa.Kapasitas = kapasitas;
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
