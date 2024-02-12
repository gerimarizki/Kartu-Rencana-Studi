using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class DistribusiMataKuliahRepository
    {
        private static DistribusiMataKuliahRepository _instance = new DistribusiMataKuliahRepository();
        public static DistribusiMataKuliahRepository GetRepository()
        {
            return _instance;
        }

        public IQueryable<DistribusiMataKuliah> GetAll()
        {
            var context = new IGLOUniversityContext();
            return context.DistribusiMataKuliahs;
        }

        public DistribusiMataKuliah GetSingle(int id)
        {
            using (var context = new IGLOUniversityContext())
            {
                return context.DistribusiMataKuliahs.SingleOrDefault(d => d.Id == id);
            }
        }

        public bool Insert(DistribusiMataKuliah model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    context.DistribusiMataKuliahs.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(DistribusiMataKuliah model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var rencanaStudi = context.DistribusiMataKuliahs.SingleOrDefault(d => d.Id == model.Id);
                    if (rencanaStudi == null) { return false; }

                    HelperFunction.MappingModel(rencanaStudi, model);
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
                    var rencanaStudi = context.DistribusiMataKuliahs.SingleOrDefault(d => d.Id == id);
                    if (rencanaStudi == null) { return false; }

                    context.DistribusiMataKuliahs.Remove(rencanaStudi);
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
