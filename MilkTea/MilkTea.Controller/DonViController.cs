using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonViTinhController : ControllerBase
    {
        private readonly DonViTinhRepository _repo;

        public DonViTinhController(DonViTinhRepository repo)
        {
            _repo = repo;
        }

        // GET: api/donvitinh
        // Lấy toàn bộ đơn vị tính
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
                return StatusCode(500, $"Lỗi khi lấy danh sách đơn vị tính: {ex.Message}");
            }
        }

        // POST: api/donvitinh
        // Thêm mới đơn vị tính
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DonViTinh dvt)
        {
            if (string.IsNullOrWhiteSpace(dvt.TenDVT))
                return BadRequest("Tên đơn vị tính không được để trống.");

            try
            {
                var added = await _repo.AddAsync(dvt);
                if (added != null)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new { maDVT = added.MaDVT },
                        added);
                }

                return StatusCode(500, "Không thể thêm đơn vị tính.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm đơn vị tính: {ex.Message}");
            }
        }

        // PUT: api/donvitinh
        // Cập nhật đơn vị tính (gửi toàn bộ object)
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DonViTinh dvt)
        {
            if (dvt.MaDVT <= 0)
                return BadRequest("Mã đơn vị tính không hợp lệ.");
            if (string.IsNullOrWhiteSpace(dvt.TenDVT))
                return BadRequest("Tên đơn vị tính không được để trống.");

            try
            {
                bool updated = await _repo.UpdateAsync(dvt);
                return updated
                    ? Ok(new { Message = "Cập nhật đơn vị tính thành công!" })
                    : NotFound($"Không tìm thấy đơn vị tính có mã {dvt.MaDVT}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật đơn vị tính: {ex.Message}");
            }
        }

        // DELETE: api/donvitinh/{maDVT}
        // Xóa cứng đơn vị tính
        [HttpDelete("{maDVT}")]
        public async Task<IActionResult> Delete(int maDVT)
        {
            if (maDVT <= 0)
                return BadRequest("Mã đơn vị tính không hợp lệ.");

            try
            {
                bool deleted = await _repo.DeleteAsync(maDVT);
                return deleted
                    ? Ok(new { Message = "Xóa đơn vị tính thành công!" })
                    : NotFound($"Không tìm thấy đơn vị tính có mã {maDVT}.");
            }
            catch (Exception ex)
            {
                // Nếu có ràng buộc khóa ngoại (ví dụ nguyên liệu đang dùng đơn vị này)
                if (ex.Message.Contains("foreign key") || ex.Message.Contains("1451"))
                    return Conflict("Không thể xóa vì đơn vị tính đang được sử dụng ở nơi khác.");

                return StatusCode(500, $"Lỗi khi xóa đơn vị tính: {ex.Message}");
            }
        }

        // GET: api/donvitinh/{maDVT} (tùy chọn, tiện dùng khi cần lấy 1 cái)
        [HttpGet("{maDVT}")]
        public async Task<IActionResult> GetById(int maDVT)
        {
            if (maDVT <= 0) return BadRequest("Mã không hợp lệ.");

            var list = await _repo.GetAllAsync();
            var item = list.FirstOrDefault(x => x.MaDVT == maDVT);

            return item != null ? Ok(item) : NotFound();
        }
    }
}