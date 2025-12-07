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
        [HttpPut("{maPN:int}")]
        public async Task<IActionResult> Update(int maPN, [FromBody] PhieuNhap pn)
        {
            try
            {
                pn.MaPN = maPN;
                bool updated = await _repo.UpdateAsync(pn);
                return updated 
                    ? Ok(new { Message = "Cập nhật thành công!" })
                    : NotFound("Không tìm thấy phiếu nhập (có thể đã bị xóa)");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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

                [HttpDelete("{maPN}/soft")]
        public async Task<IActionResult> SoftDelete(int maPN)
        {
            try
            {
                bool deleted = await _repo.SoftDeleteAsync(maPN);
                return deleted 
                    ? Ok(new { Message = "Xóa phiếu nhập thành công (trạng thái = 0)!" })
                    : NotFound($"Không tìm thấy phiếu nhập có mã {maPN} hoặc đã bị xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa phiếu nhập: {ex.Message}");
            }
        }

        [HttpGet("{maPN}")]
        public async Task<IActionResult> GetById(int maPN)
        {
            try
            {
                var list = await _repo.GetAllAsync();
                var phieuNhap = list.FirstOrDefault(p => p.MaPN == maPN && p.TrangThai == 2);
                
                if (phieuNhap == null)
                    return NotFound($"Không tìm thấy phiếu nhập có mã {maPN}");

                return Ok(phieuNhap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy phiếu nhập: {ex.Message}");
            }
        }

        [HttpPut("{maPN}/trangthai")]
        public async Task<IActionResult> CapNhatTrangThai(int maPN, [FromBody] CapNhatTrangThaiRequest request)
        {
            try
            {
                var list = await _repo.GetAllAsync();
                var pn = list.FirstOrDefault(p => p.MaPN == maPN && (p.TrangThai == 1 || p.TrangThai == 2));

                if (pn == null)
                    return NotFound("Không tìm thấy phiếu nhập hoặc đã bị xóa.");

                // Chỉ cho phép chuyển từ 2 → 1 (chưa nhập → đã nhập), hoặc ngược lại
                if (request.TrangThai != 1 && request.TrangThai != 2)
                    return BadRequest("Trạng thái chỉ có thể là 1 (đã nhập) hoặc 2 (chưa nhập).");

                pn.TrangThai = request.TrangThai;
                bool success = await _repo.UpdateAsync(pn);

                return success 
                    ? Ok(new { Message = "Cập nhật trạng thái thành công!" })
                    : StatusCode(500, "Cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public class CapNhatTrangThaiRequest
        {
            public int TrangThai { get; set; }
        }
    }
}
