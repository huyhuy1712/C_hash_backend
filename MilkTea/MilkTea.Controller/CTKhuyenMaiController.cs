using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CTKhuyenMaiController : ControllerBase
    {
        private readonly CTKhuyenMaiRepository _repo;

        public CTKhuyenMaiController(CTKhuyenMaiRepository repo)
        {
            _repo = repo;
        }

        // GET: api/ctkhuyenmai
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
                return StatusCode(500, $"Lỗi khi lấy danh sách khuyến mãi: {ex.Message}");
            }
        }

        // POST: api/ctkhuyenmai
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CTKhuyenMai km)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(km.TenCTKhuyenMai))
                    return BadRequest("Tên chương trình không được để trống.");

                bool added = await _repo.AddAsync(km);
                return added ? Ok(new { Message = "Thêm chương trình khuyến mãi thành công!" })
                             : StatusCode(500, "Không thể thêm chương trình khuyến mãi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chương trình khuyến mãi: {ex.Message}");
            }
        }

        // PUT: api/ctkhuyenmai
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CTKhuyenMai km)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(km);

                if (!updated)
                    return NotFound($"Không tìm thấy chương trình khuyến mãi có mã {km.MaCTKhuyenMai}.");

                return Ok(new { Message = "Cập nhật chương trình khuyến mãi thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật chương trình khuyến mãi: {ex.Message}");
            }
        }

        // DELETE: api/ctkhuyenmai/{id}
        [HttpDelete("{maCTKhuyenMai}")]
        public async Task<IActionResult> Delete(int maCTKhuyenMai)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maCTKhuyenMai);

                if (!deleted)
                    return NotFound($"Không tìm thấy chương trình khuyến mãi có mã {maCTKhuyenMai}.");

                return Ok(new { Message = "Đã xóa chương trình khuyến mãi thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chương trình khuyến mãi: {ex.Message}");
            }
        }

        // GET: api/ctkhuyenmai/search?column=TenCTKhuyenMai&value=Giảm
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string column, [FromQuery] string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(column) || string.IsNullOrWhiteSpace(value))
                    return BadRequest("Vui lòng cung cấp cột và giá trị tìm kiếm.");

                var result = await _repo.SearchAsync(column, value);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        // GET: api/ctkhuyenmai/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var km = await _repo.GetByIdAsync(id);
                if (km == null)
                    return NotFound($"Không tìm thấy chương trình khuyến mãi có mã {id}.");

                return Ok(km);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy chi tiết chương trình khuyến mãi: {ex.Message}");
            }
        }

    }
}
