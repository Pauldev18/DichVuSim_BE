using System;
namespace DichVuSim_BE.DTO
{
    public class GoiCuocDTO
    {
        public int MaGoiCuoc { get; set; }
        public string TenGoiCuoc { get; set; } = null!;
        public string? MoTa { get; set; }
        public decimal Gia { get; set; }
        public int ThoiHanSuDung { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}

