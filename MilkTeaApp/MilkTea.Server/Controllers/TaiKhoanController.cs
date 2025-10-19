using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanRepository _repo;

        public TaiKhoanController(TaiKhoanRepository repo)
        {
            _repo = repo;
        }

        // GET: api/taikhoan
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
                return StatusCode(500, $"Lỗi khi lấy danh sách tài khoản: {ex.Message}");
            }
        }

        // POST: api/taikhoan
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaiKhoan tk)
        {
            try
            {
                bool added = await _repo.AddAsync(tk);
                return added
                    ? Ok(new { Message = "Thêm tài khoản thành công!" })
                    : StatusCode(500, "Không thể thêm tài khoản.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm tài khoản: {ex.Message}");
            }
        }

        // PUT: api/taikhoan
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaiKhoan tk)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(tk);
                return updated
                    ? Ok(new { Message = "Cập nhật tài khoản thành công!" })
                    : NotFound("Không tìm thấy tài khoản cần cập nhật.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật tài khoản: {ex.Message}");
            }
        }

        // DELETE: api/taikhoan/{maTK}
        [HttpDelete("{maTK}")]
        public async Task<IActionResult> Delete(int maTK)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maTK);
                return deleted
                    ? Ok(new { Message = "Xóa tài khoản thành công!" })
                    : NotFound("Không tìm thấy tài khoản cần xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa tài khoản: {ex.Message}");
            }
        }

        // GET: api/taikhoan/search?column=TenTaiKhoan&value=anh
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
                return StatusCode(500, $"Lỗi khi tìm kiếm tài khoản: {ex.Message}");
            }
        }
        // GET: api/taikhoan/{maTK}
        [HttpGet("{maTK}")]
        public async Task<IActionResult> GetById(int maTK)
        {
            try
            {
                var taiKhoan = await _repo.GetByIdAsync(maTK);
                if (taiKhoan == null)
                    return NotFound($"Không tìm thấy tài khoản với MaTK = {maTK}");

                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy tài khoản theo ID: {ex.Message}");
            }
        }
    }
}
