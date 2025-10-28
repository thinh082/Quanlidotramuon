using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlidotramuon.Models.Entities;
using System.Threading.Tasks;

namespace Quanlidotramuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatDungController : ControllerBase
    {
        private readonly MyContextDb _context;
        public VatDungController(MyContextDb myContextDb)
        {
            _context = myContextDb;
        }
        [HttpGet("DanhSachVatDung")]
        public async Task<IActionResult> DanhSachVatDung()
        {
            var vatDungs = await _context.VatDungs.Where(r=>r.CoTheMuon==true&& r.SoLuongCon>0).Select(r => new
            {
                r.ChuSoHuuId,
                r.TenVatDung,
                r.MoTa,
                r.DanhMucId,
                r.SoLuongCon,
                r.CoTheMuon,
                r.TinhTrang,
                r.HinhAnh,
                r.Id    

            }).OrderByDescending(r=>r.Id).ToListAsync();
            return Ok(vatDungs);
        }
        [HttpPost("DanhSachVatDungTheoChuSoHuu")]
        public async Task<IActionResult> DanhSachVatDungTheoChuSoHuu(long chuSoHuuId)
        {
            if(chuSoHuuId == 0)
            {
                return BadRequest(new { message = "Chủ sở hữu Id không được để trống" });
            }
            var chuSoHuu = _context.TaiKhoans.FirstOrDefault(c => c.Id == chuSoHuuId);
            if (chuSoHuu == null)
            {
                return NotFound(new { message = "Chủ sở hữu không tồn tại" });
            }
            if(chuSoHuu.LoaiTaiKhoanId != 2)
            {
                return BadRequest(new { message = "Bạn không quyền này" });
            }
            var vatDungs = await _context.VatDungs.Where(v => v.ChuSoHuuId == chuSoHuuId).Select(r => new
            {
                r.ChuSoHuuId,
                r.TenVatDung,
                r.MoTa,
                r.DanhMucId,
                r.SoLuongCon,
                r.SoLuongTong,
                r.CoTheMuon,
                r.TinhTrang,
                r.HinhAnh,
                r.Id
            }).ToListAsync();
            return Ok(vatDungs);
        }   
        [HttpPost("ThemVatDung")]
        public IActionResult ThemVatDung([FromBody] VatDungModel vatDung)
        {
            if (vatDung == null)
            {
                return BadRequest(new { message = "Dữ liệu vật dụng không hợp lệ" });
            }
            var vatDung_new = new VatDung
            {
                ChuSoHuuId = vatDung.ChuSoHuuId,
                TenVatDung = vatDung.TenVatDung,
                MoTa = vatDung.MoTa,
                DanhMucId = vatDung.DanhMucId,
                SoLuongTong = vatDung.SoLuongTong,
                SoLuongCon = vatDung.SoLuongCon,
                CoTheMuon = vatDung.CoTheMuon,
                TinhTrang = vatDung.TinhTrang,
                HinhAnh = vatDung.HinhAnh,
                TrangThai = true
            };
            _context.VatDungs.Add(vatDung_new);
            _context.SaveChanges();
            return Ok(new { message = "Thêm vật dụng thành công", success = true });
        }
        [HttpPost("CapNhatVatDung")]
        public IActionResult CapNhatVatDung([FromBody] VatDungModel vatDung )
        {
            var existingVatDung = _context.VatDungs.FirstOrDefault(v => v.Id == vatDung.Id);
            if (existingVatDung == null)
            {
                return NotFound(new { message = "Vật dụng không tồn tại" });
            }
            existingVatDung.TenVatDung = vatDung.TenVatDung;
            existingVatDung.MoTa = vatDung.MoTa;
            existingVatDung.SoLuongCon = vatDung.SoLuongCon;
            existingVatDung.SoLuongTong = vatDung.SoLuongTong;
            existingVatDung.CoTheMuon = vatDung.CoTheMuon;
            existingVatDung.TinhTrang = vatDung.TinhTrang;
            existingVatDung.HinhAnh = vatDung.HinhAnh;
            existingVatDung.DanhMucId = vatDung.DanhMucId;
            _context.VatDungs.Update(existingVatDung);
            _context.SaveChanges();//
            return Ok(new { message = "Cập nhật vật dụng thành công", success = true });
        }
        [HttpPost("XoaVatDung")]
        public IActionResult XoaVatDung(long id)
        {
            var existingVatDung = _context.VatDungs.FirstOrDefault(v => v.Id == id);
            if (existingVatDung == null)
            {
                return NotFound(new { message = "Vật dụng không tồn tại" });
            }
            _context.VatDungs.Remove(existingVatDung);
            _context.SaveChanges();
            return Ok(new { message = "Xóa vật dụng thành công", success = true });
        }
        [HttpPost("ChiTietVatDung")]
        public async Task<IActionResult> ChiTietVatDung(int idVatDung)
        {
            if(idVatDung == 0)
            {
                return BadRequest(new { message = "Vật dụng Id không được để trống" });
            }
            var vatDung = await _context.VatDungs.Where(v => v.Id == idVatDung).Select(r => new
            {
                r.ChuSoHuuId,
                r.TenVatDung,
                r.MoTa,
                r.DanhMucId,
                r.SoLuongTong,
                r.SoLuongCon,
                r.CoTheMuon,
                r.TinhTrang,
                r.HinhAnh,
                r.Id
            }).FirstOrDefaultAsync();
            if (vatDung == null)
            {
                return NotFound(new { message = "Vật dụng không tồn tại" });
            }
            return Ok(vatDung);
        }
        
    }
    public class VatDungModel
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

        public bool? TrangThai { get; set; }
    }
} 
