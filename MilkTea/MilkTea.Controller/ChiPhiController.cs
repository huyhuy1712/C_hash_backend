using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiPhiController : ControllerBase
    {
        private readonly ChiPhiRepository _chiPhiRepo;

        public ChiPhiController(ChiPhiRepository chiPhiRepo)
        {
            _chiPhiRepo = chiPhiRepo;
        }

        //GET: api/chiphi
        [HttpGet]
        public async Task<IActionResult> GetAllChiPhi()
        {
            try
            {
                var list = await _chiPhiRepo.GetAllChiPhiAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy danh sách chi phí: {ex.Message}");
            }
        }

        //POST: api/chiphi
        [HttpPost]
        public async Task<IActionResult> AddChiPhi([FromBody] ChiPhi cp)
        {
            try
            {
                if (cp == null)
                    return BadRequest("Dữ liệu chi phí không hợp lệ.");

                bool added = await _chiPhiRepo.AddChiPhiAsync(cp);

                if (added)
                    return Ok(new { Message = "Thêm chi phí thành công!" });
                else
                    return StatusCode(500, "Không thể thêm chi phí.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chi phí: {ex.Message}");
            }
        }

        // DELETE: api/chiphi/{maCP}
        [HttpDelete("{maCP}")]
        public async Task<IActionResult> DeleteChiPhi(int maCP)
        {
            try
            {
                bool deleted = await _chiPhiRepo.DeleteChiPhiAsync(maCP);

                if (!deleted)
                    return NotFound($"Không tìm thấy chi phí có mã {maCP}.");

                return Ok(new { Message = $"Đã xóa chi phí có mã {maCP} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chi phí: {ex.Message}");
            }
        }

        //GET: api/chiphi/filter?column=Thang&value=8
        [HttpGet("filter")]
        public async Task<IActionResult> FilterChiPhi([FromQuery] string column, [FromQuery] string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(column) || string.IsNullOrWhiteSpace(value))
                    return BadRequest("Vui lòng cung cấp tên cột và giá trị cần lọc.");

                var result = await _chiPhiRepo.FilterChiPhiAsync(column, value);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // lỗi khi truyền tên cột không hợp lệ
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lọc dữ liệu: {ex.Message}");
            }
        }
    }
}
