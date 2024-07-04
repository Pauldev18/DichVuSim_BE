using System;
using System.Collections.Generic;

namespace DichVuSim_BE.Models
{
    public partial class DichVuNguoiDung
    {
        public int MaDichVu { get; set; }
        public int MaNguoiDung { get; set; }
        public string? LoaiDichVu { get; set; }
        public string? TrangThaiDichVu { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
    }
}
