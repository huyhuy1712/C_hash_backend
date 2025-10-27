using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhaCungCapController : ControllerBase
    {
        private readonly NhaCungCapRepository _repo;

        public NhaCungCapController(NhaCungCapRepository repo)
        {
            _repo = repo;
        }

        // GET: api/nhacungcap
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
                return StatusCode(500, $"Lỗi khi lấy danh sách nhà cung cấp: {ex.Message}");
            }
        }

        // POST: api/nhacungcap
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NhaCungCap ncc)
        {
            try
            {
                bool added = await _repo.AddAsync(ncc);
                return added
                    ? Ok(new { Message = "Thêm nhà cung cấp thành công!" })
                    : StatusCode(500, "Không thể thêm nhà cung cấp.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm nhà cung cấp: {ex.Message}");
            }
        }

        // PUT: api/nhacungcap
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NhaCungCap ncc)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(ncc);
                return updated
                    ? Ok(new { Message = "Cập nhật nhà cung cấp thành công!" })
                    : NotFound($"Không tìm thấy nhà cung cấp có mã {ncc.MaNCC}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật nhà cung cấp: {ex.Message}");
            }
        }

        // DELETE: api/nhacungcap/{maNCC}
        [HttpDelete("{maNCC}")]
        public async Task<IActionResult> Delete(int maNCC)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maNCC);
                return deleted
                    ? Ok(new { Message = "Xóa nhà cung cấp thành công!" })
                    : NotFound($"Không tìm thấy nhà cung cấp có mã {maNCC}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa nhà cung cấp: {ex.Message}");
            }
        }

        // GET: api/nhacungcap/search?column=TenNCC&value=Trà
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
                return StatusCode(500, $"Lỗi khi tìm kiếm nhà cung cấp: {ex.Message}");
            }
        }

        // GET: api/nhacungcap/searchID/{maNCC}
        [HttpGet("searchID/{maNCC}")]
        public async Task<IActionResult> GetByMaNCC(int maNCC)
        {
            try
            {
                var ncc = await _repo.GetByMaNCCAsync(maNCC);
                return ncc != null
                    ? Ok(ncc)
                    : NotFound($"Không tìm thấy nhà cung cấp có mã {maNCC}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy nhà cung cấp: {ex.Message}");
            }
        }

        // GET: api/nhacungcap/mancc-by-ten?tenNCC=Công ty TNHH Trà Sữa A
        [HttpGet("mancc-by-ten")]
        public async Task<IActionResult> GetMaNCCByTen([FromQuery] string tenNCC)
        {
            if (string.IsNullOrWhiteSpace(tenNCC))
                return BadRequest("Tên nhà cung cấp không được để trống.");

            try
            {
                var maNCC = await _repo.GetMaNCCByTenAsync(tenNCC);
                if (maNCC == null)
                    return NotFound($"Không tìm thấy nhà cung cấp có tên '{tenNCC}'.");

                return Ok(new { MaNCC = maNCC });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy mã nhà cung cấp: {ex.Message}");
            }
        }
    }
}
