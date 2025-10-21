using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class DanhMucVatDung
{
    public int Id { get; set; }

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<VatDung> VatDungs { get; set; } = new List<VatDung>();
}
