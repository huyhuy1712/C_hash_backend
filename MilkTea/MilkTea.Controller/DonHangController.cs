using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonHangController : ControllerBase
    {
        private readonly DonHangRepository _repo;

        public DonHangController(DonHangRepository repo)
        {
            _repo = repo;
        }

        // GET: api/donhang
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
                return StatusCode(500, $"Lỗi khi lấy danh sách đơn hàng: {ex.Message}");
            }
        }

        //  POST: api/donhang
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DonHang dh)
        {
            try
            {
                bool added = await _repo.AddAsync(dh);
                return added ? Ok(new { Message = "Thêm đơn hàng thành công!" })
                             : StatusCode(500, "Không thể thêm đơn hàng.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm đơn hàng: {ex.Message}");
            }
        }

        // PUT: api/donhang
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DonHang dh)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(dh);
                return updated ? Ok(new { Message = "Cập nhật đơn hàng thành công!" })
                               : NotFound($"Không tìm thấy đơn hàng có mã {dh.MaDH}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật đơn hàng: {ex.Message}");
            }
        }

        // DELETE: api/donhang/{maDH}
        [HttpDelete("{maDH}")]
        public async Task<IActionResult> Delete(int maDH)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maDH);
                return deleted ? Ok(new { Message = "Xóa đơn hàng thành công!" })
                               : NotFound($"Không tìm thấy đơn hàng có mã {maDH}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa đơn hàng: {ex.Message}");
            }
        }

        // GET: api/donhang/search?column=TrangThai&value=1
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string column, [FromQuery] string value)
        {
            try
            {
                var list = await _repo.SearchAsync(column, value);
                return Ok(list);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tìm kiếm đơn hàng: {ex.Message}");
            }
        }
    }
}
