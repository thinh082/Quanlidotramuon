using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class GiaoDichMuonTra
{
    public long Id { get; set; }

    public long PhieuMuonId { get; set; }

    public string HanhDong { get; set; } = null!;

    public long NguoiThucHienId { get; set; }

    public string? GhiChu { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual TaiKhoan NguoiThucHien { get; set; } = null!;

    public virtual PhieuMuon PhieuMuon { get; set; } = null!;
}
