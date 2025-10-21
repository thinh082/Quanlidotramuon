using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class PhieuMuon
{
    public long Id { get; set; }

    public long VatDungId { get; set; }

    public long NguoiMuonId { get; set; }

    public long ChuSoHuuId { get; set; }

    public int SoLuong { get; set; }

    public DateTime NgayMuon { get; set; }

    public DateTime NgayTraDuKien { get; set; }

    public DateTime? NgayTraThucTe { get; set; }

    public string? GhiChu { get; set; }

    public int? TrangThaiId { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<BaoCaoSuCo> BaoCaoSuCos { get; set; } = new List<BaoCaoSuCo>();

    public virtual TaiKhoan ChuSoHuu { get; set; } = null!;

    public virtual ICollection<GiaoDichMuonTra> GiaoDichMuonTras { get; set; } = new List<GiaoDichMuonTra>();

    public virtual TaiKhoan NguoiMuon { get; set; } = null!;

    public virtual TrangThaiPhieuMuon? TrangThai { get; set; }

    public virtual VatDung VatDung { get; set; } = null!;
}
