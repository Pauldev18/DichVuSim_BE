using System;
using System.Collections.Generic;

namespace DichVuSim_BE.Models
{
    public partial class GoiCuoc
    {
        public GoiCuoc()
        {
            GiaoDiches = new HashSet<GiaoDich>();
        }

        public int MaGoiCuoc { get; set; }
        public string TenGoiCuoc { get; set; } = null!;
        public string? MoTa { get; set; }
        public decimal Gia { get; set; }
        public int ThoiHanSuDung { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual ICollection<GiaoDich> GiaoDiches { get; set; }
    }
}
