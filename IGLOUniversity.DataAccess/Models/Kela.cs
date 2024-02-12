using System;
using System.Collections.Generic;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class Kela
    {
        public Kela()
        {
            DistribusiMataKuliahs = new HashSet<DistribusiMataKuliah>();
        }

        public int Id { get; set; }
        public string Nama { get; set; }
        public int IdMatakuliah { get; set; }
        public string Semester { get; set; }
        public int Kapasitas { get; set; }

        public virtual Matakuliah IdMatakuliahNavigation { get; set; }
        public virtual ICollection<DistribusiMataKuliah> DistribusiMataKuliahs { get; set; }
    }
}
