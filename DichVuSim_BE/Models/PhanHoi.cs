using System;
using System.Collections.Generic;

namespace DichVuSim_BE.Models
{
    public partial class PhanHoi
    {
        public int MaPhanHoi { get; set; }
        public int? MaNguoiDung { get; set; }
        public string NoiDungPhanHoi { get; set; } = null!;
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public virtual NguoiDung? MaNguoiDungNavigation { get; set; }
    }
}
