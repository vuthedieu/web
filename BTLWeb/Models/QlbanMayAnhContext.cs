using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLWeb.Models;

public partial class QlbanMayAnhContext : DbContext
{
    public QlbanMayAnhContext()
    {
    }

    public QlbanMayAnhContext(DbContextOptions<QlbanMayAnhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAnhSp> TAnhSps { get; set; }

    public virtual DbSet<TChatLieu> TChatLieus { get; set; }

    public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; }

    public virtual DbSet<TDanhMucSp> TDanhMucSps { get; set; }

    public virtual DbSet<THangSx> THangSxes { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<TKhachHang> TKhachHangs { get; set; }

    public virtual DbSet<TLoaiDt> TLoaiDts { get; set; }

    public virtual DbSet<TLoaiSp> TLoaiSps { get; set; }

    public virtual DbSet<TQuocGia> TQuocGia { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ANJLJ3K;Initial Catalog=QLBanMayAnh;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAnhSp>(entity =>
        {
            entity.HasKey(e => new { e.MaSp, e.TenFileAnh });

            entity.ToTable("tAnhSP");

            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.TAnhSps)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tAnhSP_tDanhMucSP");
        });

        modelBuilder.Entity<TChatLieu>(entity =>
        {
            entity.HasKey(e => e.MaChatLieu);

            entity.ToTable("tChatLieu");

            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChatLieu).HasMaxLength(150);
        });

        modelBuilder.Entity<TChiTietHdb>(entity =>
        {
            entity.ToTable("tChiTietHDB");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tDanhMucSP");
        });

        modelBuilder.Entity<TDanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("tDanhMucSP");

            entity.Property(e => e.MaSp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GiaLonNhat).HasColumnType("money");
            entity.Property(e => e.GiaNhoNhat).HasColumnType("money");
            entity.Property(e => e.GioiThieuSp)
                .HasColumnType("text")
                .HasColumnName("GioiThieuSP");
            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaDacTinh)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaDt)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDT");
            entity.Property(e => e.MaHangSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHangSX");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNuocSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNuocSX");
            entity.Property(e => e.Model).HasMaxLength(55);
            entity.Property(e => e.TenSp)
                .HasMaxLength(150)
                .HasColumnName("TenSP");
            entity.Property(e => e.Website)
                .HasMaxLength(155)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaChatLieuNavigation).WithMany(p => p.TDanhMucSps)
                .HasForeignKey(d => d.MaChatLieu)
                .HasConstraintName("FK_tDanhMucSP_tChatLieu");

            entity.HasOne(d => d.MaDtNavigation).WithMany(p => p.TDanhMucSps)
                .HasForeignKey(d => d.MaDt)
                .HasConstraintName("FK_tDanhMucSP_tLoaiDT");

            entity.HasOne(d => d.MaHangSxNavigation).WithMany(p => p.TDanhMucSps)
                .HasForeignKey(d => d.MaHangSx)
                .HasConstraintName("FK_tDanhMucSP_tHangSX");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TDanhMucSps)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_tDanhMucSP_tLoaiSP");

            entity.HasOne(d => d.MaNuocSxNavigation).WithMany(p => p.TDanhMucSps)
                .HasForeignKey(d => d.MaNuocSx)
                .HasConstraintName("FK_tDanhMucSP_tQuocGia");
        });

        modelBuilder.Entity<THangSx>(entity =>
        {
            entity.HasKey(e => e.MaHangSx);

            entity.ToTable("tHangSX");

            entity.Property(e => e.MaHangSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHangSX");
            entity.Property(e => e.HangSx)
                .HasMaxLength(100)
                .HasColumnName("HangSX");
            entity.Property(e => e.MaNuocThuongHieu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon);

            entity.ToTable("tHoaDonBan");

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.GiamGiaHd).HasColumnName("GiamGiaHD");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaSoThue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NgayHoaDon)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ThongTinThue).HasMaxLength(250);
            entity.Property(e => e.TongTienHd)
                .HasColumnType("money")
                .HasColumnName("TongTienHD");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_tHoaDonBan_tKhachHang");
        });

        modelBuilder.Entity<TKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhanhHang);

            entity.ToTable("tKhachHang");

            entity.Property(e => e.MaKhanhHang)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.TKhachHangs)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_tKhachHang_tUser");
        });

        modelBuilder.Entity<TLoaiDt>(entity =>
        {
            entity.HasKey(e => e.MaDt);

            entity.ToTable("tLoaiDT");

            entity.Property(e => e.MaDt)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDT");
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<TLoaiSp>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("tLoaiSP");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Loai).HasMaxLength(100);
        });

        modelBuilder.Entity<TQuocGia>(entity =>
        {
            entity.HasKey(e => e.MaNuoc);

            entity.ToTable("tQuocGia");

            entity.Property(e => e.MaNuoc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNuoc).HasMaxLength(100);
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("tUser");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
