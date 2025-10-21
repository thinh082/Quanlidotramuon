using System;
using System.Collections.Generic;

namespace Quanlidotramuon.Models.Entities;

public partial class TrangThaiPhieuMuon
{
    public int Id { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();
}
