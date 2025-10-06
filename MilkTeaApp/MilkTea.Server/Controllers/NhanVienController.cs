using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienRepository _repo;

        public NhanVienController(NhanVienRepository repo)
        {
            _repo = repo;
        }

        // GET: api/nhanvien
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
                return StatusCode(500, $"Lỗi khi lấy danh sách nhân viên: {ex.Message}");
            }
        }

        // POST: api/nhanvien
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NhanVien nv)
        {
            try
            {
                bool added = await _repo.AddAsync(nv);
                return added ? Ok(new { Message = "Thêm nhân viên thành công!" })
                             : StatusCode(500, "Không thể thêm nhân viên.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        // PUT: api/nhanvien
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NhanVien nv)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(nv);
                return updated ? Ok(new { Message = "Cập nhật nhân viên thành công!" })
                               : NotFound($"Không tìm thấy nhân viên có mã {nv.MaNV}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật nhân viên: {ex.Message}");
            }
        }

        // DELETE: api/nhanvien/{maNV}
        [HttpDelete("{maNV}")]
        public async Task<IActionResult> Delete(int maNV)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maNV);
                return deleted ? Ok(new { Message = "Xóa nhân viên thành công!" })
                               : NotFound($"Không tìm thấy nhân viên có mã {maNV}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa nhân viên: {ex.Message}");
            }
        }

        //  GET: api/nhanvien/search?column=TenNV&value=An
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
                return StatusCode(500, $"Lỗi khi tìm kiếm nhân viên: {ex.Message}");
            }
        }

        // GET: api/nhanvien/searchID/{maNV}
        [HttpGet("searchID/{maNV}")]
        public async Task<IActionResult> GetByMaNV(int maNV)
        {
            try
            {
                var nv = await _repo.GetByMaNVAsync(maNV);
                return nv != null ? Ok(nv) : NotFound($"Không tìm thấy nhân viên có mã {maNV}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy nhân viên: {ex.Message}");
            }
        }
    }
}
