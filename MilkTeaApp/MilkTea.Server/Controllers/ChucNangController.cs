using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucNangController : ControllerBase
    {
        private readonly ChucNangRepository _repo;

        public ChucNangController(ChucNangRepository repo)
        {
            _repo = repo;
        }

        // GET: api/chucnang
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
                return StatusCode(500, $"Lỗi khi lấy dữ liệu: {ex.Message}");
            }
        }

        //  POST: api/chucnang
        [HttpPost]
        public async Task<IActionResult> AddChucNang([FromBody] ChucNang cn)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cn.TenChucNang))
                    return BadRequest("Tên chức năng không được để trống.");

                bool added = await _repo.AddAsync(cn);
                return added
                    ? Ok(new { Message = "Thêm chức năng thành công!" })
                    : StatusCode(500, "Không thể thêm chức năng.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm chức năng: {ex.Message}");
            }
        }

        //  PUT: api/chucnang
        [HttpPut]
        public async Task<IActionResult> UpdateChucNang([FromBody] ChucNang cn)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(cn);

                if (!updated)
                    return NotFound($"Không tìm thấy chức năng có mã {cn.MaChucNang}.");

                return Ok(new { Message = "Cập nhật chức năng thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật chức năng: {ex.Message}");
            }
        }

        //  DELETE: api/chucnang/{maChucNang}
        [HttpDelete("{maChucNang}")]
        public async Task<IActionResult> DeleteChucNang(int maChucNang)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maChucNang);

                if (!deleted)
                    return NotFound($"Không tìm thấy chức năng có mã {maChucNang}.");

                return Ok(new { Message = "Xóa chức năng thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa chức năng: {ex.Message}");
            }
        }

        // GET: api/chucnang/search?keyword=quản lý
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
                return StatusCode(500, $"Lỗi khi tìm kiếm chức năng: {ex.Message}");
            }
        }

        // GET: api/chucnang/{maQuyen}
        [HttpGet("{maQuyen}")]
        public async Task<IActionResult> GetByQuyen(int maQuyen)
        {
            try
            {
                var list = await _repo.GetByQuyenAsync(maQuyen);

                if (list == null || list.Count == 0)
                    return NotFound($"Không tìm thấy chức năng nào cho quyền có mã {maQuyen}.");

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy chức năng theo quyền: {ex.Message}");
            }
        }
    }
}
