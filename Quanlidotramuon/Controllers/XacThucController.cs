using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quanlidotramuon.Models.Entities;

namespace Quanlidotramuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XacThucController : ControllerBase
    {
        private readonly MyContextDb _context;
        public XacThucController(MyContextDb myContextDb)
        {
            _context = myContextDb;
        }

        [HttpPost("DangNhap")]
        public IActionResult DangNhap([FromBody] DangNhapRequest request)
        {
            var existingUser = _context.TaiKhoans
                .FirstOrDefault(u => u.Email == request.Email && u.MatKhau == request.MatKhau);

            if (existingUser != null)
            {
                var chuSoHuu = existingUser.LoaiTaiKhoanId == 2 ? true : false;
                return Ok(new { message = "Đăng nhập thành công", taiKhoanId = existingUser.Id, chuSoHuu = chuSoHuu });
            }
            return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng" });
        }

        [HttpPost("DangKy")]
        public IActionResult DangKy([FromBody] DangKyRequest model)
        {
            var existingUser = _context.TaiKhoans.FirstOrDefault(u => u.Email == model.Email && u.SoDienThoai == model.SoDienThoai);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email hoặc số điện thoại đã được sử dụng" });
            }
            //validate Email 
            if (!model.Email.Contains("@"))
            {
                return BadRequest(new { message = "Email không hợp lệ" });
            }
            if (model.MatKhau.Length < 6)
            {
                return BadRequest(new { message = "Mật khẩu phải có ít nhất 6 ký tự" });
            }

            var newUser = new TaiKhoan
            {
                Email = model.Email,
                MatKhau = model.MatKhau,
                SoDienThoai = model.SoDienThoai,
                DiaChi = model.DiaChi,
                HoTen = model.HoTen,
                NgayTao = DateTime.Now,
                LoaiTaiKhoanId = 1
            };
            _context.TaiKhoans.Add(newUser);
            _context.SaveChanges();
            return Ok(new { message = "Đăng ký thành công", success = true });
        }
        [HttpGet("XemThongTinCaNhan")]
        public IActionResult XemThongTinCaNhan(int idTaiKhoan)
        {
            var existingUser = _context.TaiKhoans.FirstOrDefault(u => u.Id == idTaiKhoan);
            return Ok(new
            {
                existingUser.Id,
                existingUser.Email,
                existingUser.HoTen,
                existingUser.DiaChi,
                existingUser.SoDienThoai,
                existingUser.NgayTao,
                existingUser.HinhAnh
            });
        }
        [HttpPost("CapNhatThongTinCaNhan")]
        public IActionResult CapNhatThongTinCaNhan(int idTaiKhoan, [FromBody] DangKyRequest model)
        {
            var user = _context.TaiKhoans.FirstOrDefault(u => u.Id == idTaiKhoan);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản" });
            }

            // Kiểm tra trùng email với tài khoản khác
            var emailExists = _context.TaiKhoans.Any(u => u.Email == model.Email && u.Id != idTaiKhoan);
            if (emailExists)
            {
                return BadRequest(new { message = "Email đã được sử dụng" });
            }

            // Kiểm tra trùng số điện thoại với tài khoản khác
            var phoneExists = _context.TaiKhoans.Any(u => u.SoDienThoai == model.SoDienThoai && u.Id != idTaiKhoan);
            if (phoneExists)
            {
                return BadRequest(new { message = "Số điện thoại đã được sử dụng" });
            }

            // Validate Email
            if (string.IsNullOrEmpty(model.Email) || !model.Email.Contains("@"))
            {
                return BadRequest(new { message = "Email không hợp lệ" });
            }

            // Validate mật khẩu (chỉ khi người dùng nhập để đổi)
            if (!string.IsNullOrEmpty(model.MatKhau) && model.MatKhau.Length < 6)
            {
                return BadRequest(new { message = "Mật khẩu phải có ít nhất 6 ký tự" });
            }

            // Cập nhật thông tin
            user.HoTen = model.HoTen;
            user.DiaChi = model.DiaChi;
            user.SoDienThoai = model.SoDienThoai;
            user.Email = model.Email;

            if (!string.IsNullOrEmpty(model.MatKhau))
            {
                user.MatKhau = model.MatKhau; // (Khuyên dùng hash mật khẩu ở đây)
            }

            _context.TaiKhoans.Update(user);
            _context.SaveChanges();

            return Ok(new { message = "Cập nhật thông tin thành công", success = true });
        }

        public class DangKyRequest
        {
            public string Email { get; set; }
            public string DiaChi { get; set; }
            public string HoTen { get; set; }
            public string MatKhau { get; set; }
            public string SoDienThoai { get; set; }
        }
        public class DangNhapRequest
        {
            public string Email { get; set; }
            public string MatKhau { get; set; }
        }

    }
}
