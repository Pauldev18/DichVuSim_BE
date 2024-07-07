using System;
namespace DichVuSim_BE.DTO
{
    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string SoDienThoai { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? HoTen { get; set; }
        public decimal? SoDu { get; set; }
        public string? VaiTro { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}

