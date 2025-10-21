using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class VatDung
{
    public long Id { get; set; }

    public long ChuSoHuuId { get; set; }

    public string TenVatDung { get; set; } = null!;

    public string? MoTa { get; set; }

    public int? DanhMucId { get; set; }

    public int? SoLuongTong { get; set; }

    public int? SoLuongCon { get; set; }

    public bool? CoTheMuon { get; set; }

    public string? TinhTrang { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? TrangThai { get; set; }

    public virtual TaiKhoan ChuSoHuu { get; set; } = null!;

    public virtual DanhMucVatDung? DanhMuc { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();
}
