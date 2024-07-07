using System;
namespace DichVuSim_BE.DTO
{
    public class DichVuNguoiDungDTO
    {
        public int MaDichVu { get; set; }
        public int MaNguoiDung { get; set; }
        public string? LoaiDichVu { get; set; }
        public string? TrangThaiDichVu { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}

