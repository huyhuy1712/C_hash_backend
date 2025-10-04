using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuyenController : ControllerBase
    {
        private readonly QuyenRepository _repo;

        public QuyenController(QuyenRepository repo)
        {
            _repo = repo;
        }

        //  GET: api/quyen
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
                return StatusCode(500, $"Lỗi khi lấy danh sách quyền: {ex.Message}");
            }
        }

        //  POST: api/quyen
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Quyen q)
        {
            try
            {
                bool added = await _repo.AddAsync(q);
                return added ? Ok(new { Message = "Thêm quyền thành công!" })
                             : StatusCode(500, "Không thể thêm quyền.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm quyền: {ex.Message}");
            }
        }

        //  PUT: api/quyen
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Quyen q)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(q);
                return updated ? Ok(new { Message = "Cập nhật quyền thành công!" })
                               : NotFound($"Không tìm thấy quyền có mã {q.MaQuyen}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật quyền: {ex.Message}");
            }
        }

        //  DELETE: api/quyen/{maQuyen}
        [HttpDelete("{maQuyen}")]
        public async Task<IActionResult> Delete(int maQuyen)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maQuyen);
                return deleted ? Ok(new { Message = "Xóa quyền thành công!" })
                               : NotFound($"Không tìm thấy quyền có mã {maQuyen}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa quyền: {ex.Message}");
            }
        }

        //  GET: api/quyen/search?ten=admin
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string ten)
        {
            try
            {
                var list = await _repo.SearchByNameAsync(ten);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tìm kiếm quyền: {ex.Message}");
            }
        }
    }
}
