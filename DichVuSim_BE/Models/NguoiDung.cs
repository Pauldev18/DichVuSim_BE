using System;
using System.Collections.Generic;

namespace DichVuSim_BE.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            DichVuNguoiDungs = new HashSet<DichVuNguoiDung>();
            GiaoDiches = new HashSet<GiaoDich>();
            PhanHoiReceivers = new HashSet<PhanHoi>();
            PhanHoiSenders = new HashSet<PhanHoi>();
            ThanhToanHoaDons = new HashSet<ThanhToanHoaDon>();
        }

        public int MaNguoiDung { get; set; }
        public string SoDienThoai { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? HoTen { get; set; }
        public decimal? SoDu { get; set; }
        public string? VaiTro { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual ICollection<DichVuNguoiDung> DichVuNguoiDungs { get; set; }
        public virtual ICollection<GiaoDich> GiaoDiches { get; set; }
        public virtual ICollection<PhanHoi> PhanHoiReceivers { get; set; }
        public virtual ICollection<PhanHoi> PhanHoiSenders { get; set; }
        public virtual ICollection<ThanhToanHoaDon> ThanhToanHoaDons { get; set; }
    }
}
