using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Quanlidotramuon.Models.Entities;

public partial class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoCaoSuCo> BaoCaoSuCos { get; set; }

    public virtual DbSet<DanhMucVatDung> DanhMucVatDungs { get; set; }

    public virtual DbSet<GiaoDichMuonTra> GiaoDichMuonTras { get; set; }

    public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<TrangThaiPhieuMuon> TrangThaiPhieuMuons { get; set; }

    public virtual DbSet<VatDung> VatDungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoCaoSuCo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BaoCaoSu__3214EC07E74096E7");

            entity.ToTable("BaoCaoSuCo");

            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.LoaiSuCo).HasMaxLength(50);
            entity.Property(e => e.NgayBaoCao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.NguoiBaoCao).WithMany(p => p.BaoCaoSuCos)
                .HasForeignKey(d => d.NguoiBaoCaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaoCaoSuC__Nguoi__49C3F6B7");

            entity.HasOne(d => d.PhieuMuon).WithMany(p => p.BaoCaoSuCos)
                .HasForeignKey(d => d.PhieuMuonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaoCaoSuC__Phieu__48CFD27E");
        });

        modelBuilder.Entity<DanhMucVatDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DanhMuc___3214EC07696845C3");

            entity.ToTable("DanhMuc_VatDung");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(255);
        });

        modelBuilder.Entity<GiaoDichMuonTra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GiaoDich__3214EC073D0E4015");

            entity.ToTable("GiaoDichMuonTra");

            entity.Property(e => e.HanhDong).HasMaxLength(50);
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.NguoiThucHien).WithMany(p => p.GiaoDichMuonTras)
                .HasForeignKey(d => d.NguoiThucHienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichM__Nguoi__403A8C7D");

            entity.HasOne(d => d.PhieuMuon).WithMany(p => p.GiaoDichMuonTras)
                .HasForeignKey(d => d.PhieuMuonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichM__Phieu__3F466844");
        });

        modelBuilder.Entity<LoaiTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoaiTaiK__3214EC071E8A4341");

            entity.ToTable("LoaiTaiKhoan");

            entity.Property(e => e.TenLoaiTaiKhoan).HasMaxLength(100);
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuMuo__3214EC07A8234665");

            entity.ToTable("PhieuMuon", tb => tb.HasTrigger("trg_CapNhatSoLuongMuon"));

            entity.Property(e => e.NgayMuon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayTraDuKien).HasColumnType("datetime");
            entity.Property(e => e.NgayTraThucTe).HasColumnType("datetime");

            entity.HasOne(d => d.ChuSoHuu).WithMany(p => p.PhieuMuonChuSoHuus)
                .HasForeignKey(d => d.ChuSoHuuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__ChuSo__3A81B327");

            entity.HasOne(d => d.NguoiMuon).WithMany(p => p.PhieuMuonNguoiMuons)
                .HasForeignKey(d => d.NguoiMuonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__Nguoi__398D8EEE");

            entity.HasOne(d => d.TrangThai).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.TrangThaiId)
                .HasConstraintName("FK__PhieuMuon__Trang__3B75D760");

            entity.HasOne(d => d.VatDung).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.VatDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__VatDu__38996AB5");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC07BB1368F2");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.Email, "UQ__TaiKhoan__A9D10534619D44E5").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.MatKhau).IsUnicode(false);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);

            entity.HasOne(d => d.LoaiTaiKhoan).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.LoaiTaiKhoanId)
                .HasConstraintName("FK__TaiKhoan__LoaiTa__286302EC");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ThongBao__3214EC07F22E5D17");

            entity.ToTable("ThongBao");

            entity.Property(e => e.DaDoc).HasDefaultValue(false);
            entity.Property(e => e.Loai).HasMaxLength(50);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(255);

            entity.HasOne(d => d.NguoiNhan).WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.NguoiNhanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThongBao__NguoiN__44FF419A");
        });

        modelBuilder.Entity<TrangThaiPhieuMuon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TrangTha__3214EC070C7F42D1");

            entity.ToTable("TrangThai_PhieuMuon");

            entity.Property(e => e.TrangThai).HasMaxLength(100);
        });

        modelBuilder.Entity<VatDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VatDung__3214EC075B9BADC8");

            entity.ToTable("VatDung");

            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoLuongCon).HasDefaultValue(1);
            entity.Property(e => e.SoLuongTong).HasDefaultValue(1);
            entity.Property(e => e.TenVatDung).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);

            entity.HasOne(d => d.ChuSoHuu).WithMany(p => p.VatDungs)
                .HasForeignKey(d => d.ChuSoHuuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VatDung__ChuSoHu__30F848ED");

            entity.HasOne(d => d.DanhMuc).WithMany(p => p.VatDungs)
                .HasForeignKey(d => d.DanhMucId)
                .HasConstraintName("FK__VatDung__DanhMuc__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
