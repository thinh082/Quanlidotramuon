using Quanlidotramuon.Models.Entities;

namespace Quanlidotramuon.Controllers
{
    public class HamDungChung
    {
        private readonly MyContextDb _context;
        public HamDungChung(MyContextDb context)
        {
            _context = context;
        }
        public async Task GiaoDich(GiaoDichRequest request)
        {
            var Giaodich = new GiaoDichMuonTra
            {
                PhieuMuonId = request.PhieuMuonId,
                HanhDong = request.HanhDong,
                NguoiThucHienId = request.NguoiThucHienId,
                GhiChu = request.GhiChu,
                HinhAnh = request.HinhAnh,
                ThoiGian = DateTime.Now
            };
            _context.GiaoDichMuonTras.Add(Giaodich);
            await _context.SaveChangesAsync();
        }
        
    }
    public class GiaoDichRequest
    {
        public long PhieuMuonId { get; set; }
        public string HanhDong { get; set; }
        public int NguoiThucHienId { get; set; }
        public string GhiChu { get; set; }
        public string HinhAnh { get; set; }
    }
}
