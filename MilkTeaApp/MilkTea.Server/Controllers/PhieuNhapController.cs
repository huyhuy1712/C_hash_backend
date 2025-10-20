using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhieuNhapController : ControllerBase
    {
        private readonly PhieuNhapRepository _repo;

        public PhieuNhapController(PhieuNhapRepository repo)
        {
            _repo = repo;
        }

        // GET: api/phieunhap
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
                return StatusCode(500, $"Lỗi khi lấy danh sách phiếu nhập: {ex.Message}");
            }
        }

        // POST: api/phieunhap
        [HttpPost]
        public async Task<IActionResult> Create(PhieuNhap pn)
        {
            var id = await _repo.AddAsync(pn);
            return Ok(new { id });
        }

        // PUT: api/phieunhap
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PhieuNhap pn)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(pn);
                return updated ? Ok(new { Message = "Cập nhật phiếu nhập thành công!" })
                               : NotFound($"Không tìm thấy phiếu nhập có mã {pn.MaPN}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật phiếu nhập: {ex.Message}");
            }
        }

        // DELETE: api/phieunhap/{maPN}
        [HttpDelete("{maPN}")]
        public async Task<IActionResult> Delete(int maPN)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maPN);
                return deleted ? Ok(new { Message = "Xóa phiếu nhập thành công!" })
                               : NotFound($"Không tìm thấy phiếu nhập có mã {maPN}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa phiếu nhập: {ex.Message}");
            }
        }

        // GET: api/phieunhap/search?column=NgayNhap&value=2025-10-04
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
                return StatusCode(500, $"Lỗi khi tìm kiếm phiếu nhập: {ex.Message}");
            }
        }
    }
}
