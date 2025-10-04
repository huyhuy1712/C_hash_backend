using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietCongThucController : ControllerBase
    {
        private readonly ChiTietCongThucRepository _repo;

        public ChiTietCongThucController(ChiTietCongThucRepository repo)
        {
            _repo = repo;
        }

        //GET: api/chitietcongthuc
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

        // POST: api/chitietcongthuc
        [HttpPost]
        public async Task<IActionResult> AddChiTiet([FromBody] ChiTietCongThuc ct)
        {
            try
            {
                if (ct == null)
                    return BadRequest("Dữ liệu không hợp lệ.");

                bool added = await _repo.AddAsync(ct);
                if (added)
                    return Ok(new { Message = "Thêm chi tiết công thức thành công!" });
                else
                    return StatusCode(500, "Không thể thêm chi tiết công thức.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chi tiết công thức: {ex.Message}");
            }
        }

        // PUT: api/chitietcongthuc/update/1/3?soLuongMoi=5
        [HttpPut("update/{maCT}/{maNL}")]
        public async Task<IActionResult> UpdateChiTiet(int maCT, int maNL, [FromQuery] int soLuongMoi)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(maCT, maNL, soLuongMoi);

                if (!updated)
                    return NotFound($"Không tìm thấy chi tiết công thức có MaCT={maCT}, MaNL={maNL}.");

                return Ok(new { Message = $"Đã cập nhật SL={soLuongMoi} cho MaCT={maCT}, MaNL={maNL}." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật chi tiết công thức: {ex.Message}");
            }
        }

        // DELETE: api/chitietcongthuc/delete/1/3
        [HttpDelete("delete/{maCT}/{maNL}")]
        public async Task<IActionResult> DeleteChiTiet(int maCT, int maNL)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maCT, maNL);

                if (!deleted)
                    return NotFound($"Không tìm thấy chi tiết công thức có MaCT={maCT}, MaNL={maNL}.");

                return Ok(new { Message = $"Đã xóa chi tiết công thức có MaCT={maCT}, MaNL={maNL} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chi tiết công thức: {ex.Message}");
            }
        }

        // DELETE: api/chitietcongthuc/delete-by-mact/1
            [HttpDelete("delete-by-mact/{maCT}")]
            public async Task<IActionResult> DeleteByMaCT(int maCT)
            {
                try
                {
                    bool deleted = await _repo.DeleteByMaCTAsync(maCT);

                    if (!deleted)
                        return NotFound($"Không tìm thấy chi tiết nào thuộc công thức có mã {maCT}.");

                    return Ok(new { Message = $"Đã xóa tất cả chi tiết thuộc công thức có mã {maCT} thành công." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Lỗi khi xóa chi tiết công thức theo mã công thức: {ex.Message}");
                }
            }

    }
}
