using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NguyenLieuController : ControllerBase
    {
        private readonly NguyenLieuRepository _repo;

        public NguyenLieuController(NguyenLieuRepository repo)
        {
            _repo = repo;
        }

        // GET: api/nguyenlieu
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
                return StatusCode(500, $"Lỗi khi lấy danh sách nguyên liệu: {ex.Message}");
            }
        }

        // POST: api/nguyenlieu
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NguyenLieu nl)
        {
            try
            {
                bool added = await _repo.AddAsync(nl);
                return added ? Ok(new { Message = "Thêm nguyên liệu thành công!" })
                             : StatusCode(500, "Không thể thêm nguyên liệu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm nguyên liệu: {ex.Message}");
            }
        }

        // PUT: api/nguyenlieu
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NguyenLieu nl)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(nl);
                return updated ? Ok(new { Message = "Cập nhật nguyên liệu thành công!" })
                               : NotFound($"Không tìm thấy nguyên liệu có mã {nl.MaNL}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật nguyên liệu: {ex.Message}");
            }
        }

        // DELETE: api/nguyenlieu/{maNL}
        [HttpDelete("{maNL}")]
        public async Task<IActionResult> Delete(int maNL)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maNL);
                return deleted ? Ok(new { Message = "Xóa nguyên liệu thành công!" })
                               : NotFound($"Không tìm thấy nguyên liệu có mã {maNL}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa nguyên liệu: {ex.Message}");
            }
        }

        // GET: api/nguyenlieu/search?ten=Trà
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
                return StatusCode(500, $"Lỗi khi tìm kiếm nguyên liệu: {ex.Message}");
            }
        }
    }
}
