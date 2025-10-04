using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly ChiTietDonHangRepository _repo;

        public ChiTietDonHangController(ChiTietDonHangRepository repo)
        {
            _repo = repo;
        }

        //  GET: api/chitietdonhang
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

        // POST: api/chitietdonhang
        [HttpPost]
        public async Task<IActionResult> AddChiTiet([FromBody] ChiTietDonHang ct)
        {
            try
            {
                if (ct == null)
                    return BadRequest("Dữ liệu không hợp lệ.");

                bool added = await _repo.AddAsync(ct);
                if (added)
                    return Ok(new { Message = "Thêm chi tiết đơn hàng thành công!" });
                else
                    return StatusCode(500, "Không thể thêm chi tiết đơn hàng.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chi tiết đơn hàng: {ex.Message}");
            }
        }

        // PUT: api/chitietdonhang
        [HttpPut]
        public async Task<IActionResult> UpdateChiTiet([FromBody] ChiTietDonHang ct)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(ct);

                if (!updated)
                    return NotFound($"Không tìm thấy chi tiết đơn hàng có mã {ct.MaCTDH}.");

                return Ok(new { Message = $"Cập nhật chi tiết đơn hàng {ct.MaCTDH} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật chi tiết đơn hàng: {ex.Message}");
            }
        }

        // DELETE: api/chitietdonhang/5
        [HttpDelete("{maCTDH}")]
        public async Task<IActionResult> DeleteChiTiet(int maCTDH)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maCTDH);

                if (!deleted)
                    return NotFound($"Không tìm thấy chi tiết đơn hàng có mã {maCTDH}.");

                return Ok(new { Message = $"Đã xóa chi tiết đơn hàng có mã {maCTDH} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chi tiết đơn hàng: {ex.Message}");
            }
        }

        // DELETE: api/chitietdonhang/delete-by-madh/5
            [HttpDelete("delete-by-madh/{maDH}")]
            public async Task<IActionResult> DeleteByMaDH(int maDH)
            {
                try
                {
                    bool deleted = await _repo.DeleteByMaDHAsync(maDH);

                    if (!deleted)
                        return NotFound($"Không tìm thấy chi tiết nào thuộc đơn hàng có mã {maDH}.");

                    return Ok(new { Message = $"Đã xóa tất cả chi tiết thuộc đơn hàng có mã {maDH} thành công." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Lỗi khi xóa chi tiết đơn hàng theo mã đơn hàng: {ex.Message}");
                }
            }

                }
}
