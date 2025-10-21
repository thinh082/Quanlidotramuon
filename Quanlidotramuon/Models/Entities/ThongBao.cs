using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class ThongBao
{
    public long Id { get; set; }

    public long NguoiNhanId { get; set; }

    public string Loai { get; set; } = null!;

    public string TieuDe { get; set; } = null!;

    public string? NoiDung { get; set; }

    public bool? DaDoc { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual TaiKhoan NguoiNhan { get; set; } = null!;
}
