CREATE DATABASE VMchargerDB;
GO

USE VMchargerDB;
GO

-- Bảng quản lý Người dùng
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    SoDienThoai VARCHAR(10) NOT NULL UNIQUE,
    MatKhau VARCHAR(20) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    HoTen NVARCHAR(255),
    SoDu DECIMAL(10, 2) DEFAULT 0.00,
    VaiTro NVARCHAR(50) CHECK (VaiTro IN ('quan_tri', 'nguoi_dung')) DEFAULT 'nguoi_dung',
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE()
);
GO

-- Bảng quản lý thông tin Gói cước
CREATE TABLE GoiCuoc (
    MaGoiCuoc INT IDENTITY(1,1) PRIMARY KEY,
    TenGoiCuoc NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(MAX),
    Gia DECIMAL(10, 2) NOT NULL,
    ThoiHanSuDung INT NOT NULL, -- Thời hạn sử dụng tính bằng ngày
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE()
);
GO

-- Bảng quản lý Giao dịch
CREATE TABLE GiaoDich (
    MaGiaoDich INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    SoDienThoai VARCHAR(10) NOT NULL,
    SoTien DECIMAL(10, 2) NOT NULL,
    MaGoiCuoc INT,
    PhuongThucThanhToan NVARCHAR(50) CHECK (PhuongThucThanhToan IN ('PayPal')) DEFAULT 'PayPal',
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('cho_xu_ly', 'da_hoan_thanh', 'that_bai')) DEFAULT 'cho_xu_ly',
    MaXacThuc VARCHAR(10),
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaGoiCuoc) REFERENCES GoiCuoc(MaGoiCuoc)
);
GO

CREATE TABLE PhanHoi (
    MaPhanHoi INT IDENTITY(1,1) PRIMARY KEY,
    SenderId INT NOT NULL,
    ReceiverId INT NOT NULL,
    NoiDungPhanHoi NVARCHAR(MAX) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SenderId) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (ReceiverId) REFERENCES NguoiDung(MaNguoiDung)
);
GO

-- Bảng quản lý Thanh toán hóa đơn
CREATE TABLE ThanhToanHoaDon (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    SoDienThoai VARCHAR(10) NOT NULL,
    SoTien DECIMAL(10, 2) NOT NULL,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('cho_xu_ly', 'da_hoan_thanh', 'that_bai')) DEFAULT 'cho_xu_ly',
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO

-- Bảng quản lý thông tin Đăng ký nhạc chờ và chế độ làm phiền
CREATE TABLE DichVuNguoiDung (
    MaDichVu INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    LoaiDichVu NVARCHAR(50) CHECK (LoaiDichVu IN ('nhac_cho', 'khong_lam_phien')),
    TrangThaiDichVu NVARCHAR(50) CHECK (TrangThaiDichVu IN ('kich_hoat', 'khong_kich_hoat')) DEFAULT 'khong_kich_hoat',
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO