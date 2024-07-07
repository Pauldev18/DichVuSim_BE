using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DichVuSim_BE.Models
{
    public partial class VMchargerDBContext : DbContext
    {
        public VMchargerDBContext()
        {
        }

        public VMchargerDBContext(DbContextOptions<VMchargerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DichVuNguoiDung> DichVuNguoiDungs { get; set; } = null!;
        public virtual DbSet<GiaoDich> GiaoDiches { get; set; } = null!;
        public virtual DbSet<GoiCuoc> GoiCuocs { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhanHoi> PhanHois { get; set; } = null!;
        public virtual DbSet<ThanhToanHoaDon> ThanhToanHoaDons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=VMchargerDB;User ID=sa;Password=Pauldev182@;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DichVuNguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaDichVu)
                    .HasName("PK__DichVuNg__C0E6DE8F2837435C");

                entity.ToTable("DichVuNguoiDung");

                entity.Property(e => e.LoaiDichVu).HasMaxLength(50);

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThaiDichVu)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('khong_kich_hoat')");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.DichVuNguoiDungs)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DichVuNgu__MaNgu__5DCAEF64");
            });

            modelBuilder.Entity<GiaoDich>(entity =>
            {
                entity.HasKey(e => e.MaGiaoDich)
                    .HasName("PK__GiaoDich__0A2A24EB35FFEC25");

                entity.ToTable("GiaoDich");

                entity.Property(e => e.MaXacThuc)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhuongThucThanhToan)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('PayPal')");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoTien).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('cho_xu_ly')");

                entity.HasOne(d => d.MaGoiCuocNavigation)
                    .WithMany(p => p.GiaoDiches)
                    .HasForeignKey(d => d.MaGoiCuoc)
                    .HasConstraintName("FK__GiaoDich__MaGoiC__4AB81AF0");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.GiaoDiches)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiaoDich__MaNguo__49C3F6B7");
            });

            modelBuilder.Entity<GoiCuoc>(entity =>
            {
                entity.HasKey(e => e.MaGoiCuoc)
                    .HasName("PK__GoiCuoc__164E5F52965714C7");

                entity.ToTable("GoiCuoc");

                entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenGoiCuoc).HasMaxLength(50);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung)
                    .HasName("PK__NguoiDun__C539D7625977FBD3");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BDD798C447")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534C3CE01E0")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(255);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoDu)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.VaiTro)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('nguoi_dung')");
            });

            modelBuilder.Entity<PhanHoi>(entity =>
            {
                entity.HasKey(e => e.MaPhanHoi)
                    .HasName("PK__PhanHoi__3458D20FADBAE4FF");

                entity.ToTable("PhanHoi");

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.PhanHoiReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhanHoi__Receive__72C60C4A");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.PhanHoiSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhanHoi__SenderI__71D1E811");
            });

            modelBuilder.Entity<ThanhToanHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaThanhToan)
                    .HasName("PK__ThanhToa__D4B2584438FB4DE0");

                entity.ToTable("ThanhToanHoaDon");

                entity.Property(e => e.NgayCapNhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoTien).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('cho_xu_ly')");

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.ThanhToanHoaDons)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThanhToan__MaNgu__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
