using System;
using System.Collections.Generic;

namespace DichVuSim_BE.Models
{
    public partial class GiaoDich
    {
        public int MaGiaoDich { get; set; }
        public int MaNguoiDung { get; set; }
        public string SoDienThoai { get; set; } = null!;
        public decimal SoTien { get; set; }
        public int? MaGoiCuoc { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public string? TrangThai { get; set; }
        public string? MaXacThuc { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual GoiCuoc? MaGoiCuocNavigation { get; set; }
        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
    }
}
