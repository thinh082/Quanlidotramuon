using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class TaiKhoan
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string MatKhau { get; set; } = null!;

    public int? LoaiTaiKhoanId { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? DiaChi { get; set; }

    public string? HoTen { get; set; }

    public virtual ICollection<BaoCaoSuCo> BaoCaoSuCos { get; set; } = new List<BaoCaoSuCo>();

    public virtual ICollection<GiaoDichMuonTra> GiaoDichMuonTras { get; set; } = new List<GiaoDichMuonTra>();

    public virtual LoaiTaiKhoan? LoaiTaiKhoan { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuonChuSoHuus { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<PhieuMuon> PhieuMuonNguoiMuons { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();

    public virtual ICollection<VatDung> VatDungs { get; set; } = new List<VatDung>();
}
