using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuzzerController : ControllerBase
    {
        private readonly BuzzerRepository _buzzerRepo;

        public BuzzerController(BuzzerRepository buzzerRepo)
        {
            _buzzerRepo = buzzerRepo;
        }

        //GET: api/buzzer/trangthai/{trangThai}
        [HttpGet("trangthai/{trangThai}")]
        public async Task<IActionResult> GetBuzzerByTrangThai(int trangThai)
        {
            try
            {
                if (trangThai != 0 && trangThai != 1)
                    return BadRequest("Trạng thái chỉ có thể là 0 hoặc 1.");

                var list = await _buzzerRepo.GetBuzzersByTrangThaiAsync(trangThai);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách buzzer: {ex.Message}");
            }
        }


        // PUT: api/buzzer/update/trangthai/{sohieu}/{trangthai}
      // PUT: api/buzzer/update/trangthai/{soHieu}/{trangThai}
[HttpPut("update/trangthai/{soHieu}/{trangThai}")]
public async Task<IActionResult> UpdateTrangThai(string soHieu, int trangThai)
{
    try
    {
        if (trangThai != 0 && trangThai != 1)
            return BadRequest("Trạng thái chỉ có thể là 0 hoặc 1.");

        bool updated = await _buzzerRepo.UpdateTrangThaiAsync(soHieu.ToString(), trangThai);

        if (!updated)
            return NotFound($"Không tìm thấy buzzer có số hiệu: {soHieu}");

        return Ok(new
        {
            Message = $"Đã cập nhật trạng thái buzzer '{soHieu}' thành {trangThai} thành công."
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Lỗi khi cập nhật trạng thái buzzer: {ex.Message}");
    }
}
        
                 //  Lấy MaMay theo số hiệu
        [HttpGet("mamay-by-sohieu")]
        public async Task<IActionResult> GetMaMayBySoHieuAsync(string sohieu)
        {
            if (string.IsNullOrWhiteSpace(sohieu))
                return BadRequest("Tên maáy không được để trống.");

            var mamay = await _buzzerRepo.GetMaMayBySoHieuAsync(sohieu);
            if (mamay == null)
                return NotFound($"Không tìm thấy máy có tên '{sohieu}'.");

            return Ok(new { MaBuzzer = mamay });
        }
    }
}
