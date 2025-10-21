using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class BaoCaoSuCo
{
    public long Id { get; set; }

    public long PhieuMuonId { get; set; }

    public long NguoiBaoCaoId { get; set; }

    public string LoaiSuCo { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? NgayBaoCao { get; set; }

    public virtual TaiKhoan NguoiBaoCao { get; set; } = null!;

    public virtual PhieuMuon PhieuMuon { get; set; } = null!;
}
