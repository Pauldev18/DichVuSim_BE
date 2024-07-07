using System;
namespace DichVuSim_BE.DTO
{
    public class PhanHoiDTO
    {
        public int MaPhanHoi { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderName { get; set; } // Thêm tên người gửi
        public string ReceiverName { get; set; } // Thêm tên người nhận
        public string NoiDungPhanHoi { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}

