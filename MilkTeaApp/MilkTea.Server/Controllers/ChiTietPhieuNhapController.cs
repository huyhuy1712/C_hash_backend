using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietPhieuNhapController : ControllerBase
    {
        private readonly ChiTietPhieuNhapRepository _repo;

        public ChiTietPhieuNhapController(ChiTietPhieuNhapRepository repo)
        {
            _repo = repo;
        }

        //GET: api/chitietphieunhap
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _repo.GetAllAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi đọc dữ liệu: {ex.Message}");
            }
        }

        //POST: api/chitietphieunhap
        [HttpPost]
        public async Task<IActionResult> AddChiTiet([FromBody] ChiTietPhieuNhap ctpn)
        {
            try
            {
                if (ctpn == null)
                    return BadRequest("Dữ liệu không hợp lệ.");

                bool added = await _repo.AddAsync(ctpn);
                if (added)
                    return Ok(new { Message = "Thêm chi tiết phiếu nhập thành công!" });
                else
                    return StatusCode(500, "Không thể thêm chi tiết phiếu nhập.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chi tiết phiếu nhập: {ex.Message}");
            }
        }

        //PUT: api/chitietphieunhap
        [HttpPut]
        public async Task<IActionResult> UpdateChiTiet([FromBody] ChiTietPhieuNhap ctpn)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(ctpn);

                if (!updated)
                    return NotFound($"Không tìm thấy chi tiết phiếu nhập có mã {ctpn.MaChiTietPhieuNhap}.");

                return Ok(new { Message = $"Cập nhật chi tiết phiếu nhập {ctpn.MaChiTietPhieuNhap} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật chi tiết phiếu nhập: {ex.Message}");
            }
        }

        //DELETE: api/chitietphieunhap/{maChiTietPhieuNhap}
        [HttpDelete("{maChiTietPhieuNhap}")]
        public async Task<IActionResult> DeleteChiTiet(int maChiTietPhieuNhap)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maChiTietPhieuNhap);

                if (!deleted)
                    return NotFound($"Không tìm thấy chi tiết phiếu nhập có mã {maChiTietPhieuNhap}.");

                return Ok(new { Message = $"Đã xóa chi tiết phiếu nhập có mã {maChiTietPhieuNhap} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chi tiết phiếu nhập: {ex.Message}");
            }
        }

        //DELETE: api/chitietphieunhap/delete-by-mapn/{maPN}
        [HttpDelete("delete-by-mapn/{maPN}")]
        public async Task<IActionResult> DeleteByMaPN(int maPN)
        {
            try
            {
                bool deleted = await _repo.DeleteByMaPNAsync(maPN);

                if (!deleted)
                    return NotFound($"Không tìm thấy chi tiết nào thuộc phiếu nhập có mã {maPN}.");

                return Ok(new { Message = $"Đã xóa tất cả chi tiết thuộc phiếu nhập có mã {maPN} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chi tiết phiếu nhập theo mã phiếu: {ex.Message}");
            }
        }
    }
}
