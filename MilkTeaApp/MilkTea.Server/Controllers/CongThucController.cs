using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongThucController : ControllerBase
    {
        private readonly CongThucRepository _repo;

        public CongThucController(CongThucRepository repo)
        {
            _repo = repo;
        }

        // GET: api/congthuc
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
                return StatusCode(500, $"Lỗi khi lấy danh sách công thức: {ex.Message}");
            }
        }
        

        //  POST: api/congthuc
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CongThuc ct)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ct.Ten))
                    return BadRequest("Tên công thức không được để trống.");

                bool added = await _repo.AddAsync(ct);
                return added
                    ? Ok(new { Message = "Thêm công thức thành công!" })
                    : StatusCode(500, "Không thể thêm công thức.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm công thức: {ex.Message}");
            }
        }

        //  PUT: api/congthuc
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CongThuc ct)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(ct);

                if (!updated)
                    return NotFound($"Không tìm thấy công thức có mã {ct.MaCT}.");

                return Ok(new { Message = "Cập nhật công thức thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật công thức: {ex.Message}");
            }
        }

        //  DELETE: api/congthuc/{maCT}
        [HttpDelete("{maCT}")]
        public async Task<IActionResult> Delete(int maCT)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maCT);

                if (!deleted)
                    return NotFound($"Không tìm thấy công thức có mã {maCT}.");

                return Ok(new { Message = $"Đã xóa công thức có mã {maCT} thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa công thức: {ex.Message}");
            }
        }

        //  GET: api/congthuc/search?keyword=trà sữa
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return BadRequest("Vui lòng nhập từ khóa tìm kiếm.");

                var result = await _repo.SearchByNameAsync(keyword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tìm kiếm công thức: {ex.Message}");
            }
        }
    }
}
