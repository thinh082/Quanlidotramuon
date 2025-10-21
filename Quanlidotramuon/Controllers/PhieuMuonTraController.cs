using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quanlidotramuon.Models.Entities;

namespace Quanlidotramuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonTraController : ControllerBase
    {
        private readonly MyContextDb _context;
        public PhieuMuonTraController(MyContextDb myContextDb)
        {
            _context = myContextDb;
        }
        [HttpGet("DanhSachPhieuMuonTra")]
        public IActionResult DanhSachPhieuMuonTra()
        {
            var phieuMuonTras = _context.PhieuMuons.Select(r => new
            {
                r.Id,
                r.SoLuong,
                r.NgayMuon,
                r.NgayTraDuKien,
                r.NgayTraThucTe,
                r.GhiChu,
                r.TrangThaiId,
                r.NgayTao,
                VatDung = new
                {
                    r.VatDung.Id,
                    r.VatDung.TenVatDung,
                    r.VatDung.MoTa,
                    r.VatDung.TinhTrang
                },
            }).ToList();
            return Ok(phieuMuonTras);
        }
        [HttpPost("DanhSachPhieuMuonTraTheoNguoiMuon")]
        public IActionResult DanhSachPhieuMuonTraTheoNguoiMuon(long nguoiMuonId)
        {
            if (nguoiMuonId == 0)
            {
                return BadRequest(new { message = "Người mượn Id không được để trống" });
            }
            var phieuMuonTras = _context.PhieuMuons.Where(p => p.NguoiMuonId == nguoiMuonId).Select(r => new
            {
                r.Id,
                r.SoLuong,
                r.NgayMuon,
                r.NgayTraDuKien,
                r.NgayTraThucTe,
                r.GhiChu,
                r.TrangThaiId,
                r.NgayTao,
                VatDung = new
                {
                    r.VatDung.Id,
                    r.VatDung.TenVatDung,
                    r.VatDung.MoTa,
                    r.VatDung.TinhTrang
                },
            }).ToList();
            return Ok(phieuMuonTras);
        }
        [HttpPost("chitiet")]
        public async Task<IActionResult> ChiTietPhieuMuon(int id)
        {
            if(id <= 0)
            {
                return BadRequest(new { message = "ID phiếu mượn trả không hợp lệ" });
            }

            var phieuMuonTras = _context.PhieuMuons.Select(r => new
            {
                r.Id,
                r.SoLuong,
                r.NgayMuon,
                r.NgayTraDuKien,
                r.NgayTraThucTe,
                r.GhiChu,
                r.TrangThaiId,
                r.NgayTao,
                VatDung = new
                {
                    r.VatDung.Id,
                    r.VatDung.TenVatDung,
                    r.VatDung.MoTa,
                    r.VatDung.TinhTrang
                },
            }).FirstOrDefault();
            if (phieuMuonTras == null)
            {
                return NotFound(new { message = "Phiếu mượn trả không tồn tại" });
            }
            return Ok(phieuMuonTras);
        }
        [HttpPost("ThemPhieuMuonTra")]
        public IActionResult ThemPhieuMuonTra([FromBody] ThemPhieuMuon phieuMuon)
        {
            var newPhieuMuon = new PhieuMuon
            {
                VatDungId = phieuMuon.VatDungId,
                NguoiMuonId = phieuMuon.NguoiMuonId,
                ChuSoHuuId = phieuMuon.ChuSoHuuId,
                SoLuong = phieuMuon.SoLuong,
                NgayMuon = phieuMuon.NgayMuon,
                NgayTraDuKien = phieuMuon.NgayTraDuKien,
                GhiChu = phieuMuon.GhiChu,
                TrangThaiId = 1, 
                NgayTao = DateTime.Now
            };
            _context.PhieuMuons.Add(newPhieuMuon);
            _context.SaveChanges();
            return Ok(new { message = "Thêm phiếu mượn trả thành công", success = true });
        }
        [HttpPost("CapNhatPhieuMuonTra")]
        public IActionResult CapNhatPhieuMuonTra([FromBody] SuaPhieuMuon phieuMuon)
        {
            var existingPhieuMuon = _context.PhieuMuons.FirstOrDefault(p => p.Id == phieuMuon.Id);
            if (existingPhieuMuon == null)
            {
                return NotFound(new { message = "Phiếu mượn trả không tồn tại" });
            }
            existingPhieuMuon.NgayTraThucTe = phieuMuon.NgayTraThucTe;
            existingPhieuMuon.GhiChu = phieuMuon.GhiChu;
            existingPhieuMuon.TrangThaiId = phieuMuon.TrangThaiId;
            _context.SaveChanges();
            return Ok(new { message = "Cập nhật phiếu mượn trả thành công", success = true });
        }
        [HttpPost("XoaPhieuMuonTra")]
        public IActionResult XoaPhieuMuonTra(long id)
        {
            var existingPhieuMuon = _context.PhieuMuons.FirstOrDefault(p => p.Id == id);
            if (existingPhieuMuon == null)
            {
                return NotFound(new { message = "Phiếu mượn trả không tồn tại" });
            }
            _context.PhieuMuons.Remove(existingPhieuMuon);
            _context.SaveChanges();
            return Ok(new { message = "Xóa phiếu mượn trả thành công", success = true });
        }

    }
    public class ThemPhieuMuon
    {

        public long VatDungId { get; set; }

        public long NguoiMuonId { get; set; }

        public long ChuSoHuuId { get; set; }

        public int SoLuong { get; set; }

        public DateTime NgayMuon { get; set; }

        public DateTime NgayTraDuKien { get; set; }


        public string? GhiChu { get; set; }


    }
    public class SuaPhieuMuon
    {
        public long Id { get; set; }

        public DateTime? NgayTraThucTe { get; set; }

        public string? GhiChu { get; set; }

        public int? TrangThaiId { get; set; }

    }
}
