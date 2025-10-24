using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuyenChucNangController : ControllerBase
    {
        private readonly QuyenChucNangRepository _repo;

        public QuyenChucNangController(QuyenChucNangRepository repo)
        {
            _repo = repo;
        }

        //GET: api/quyenchucnang
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

        //POST: api/quyenchucnang
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Quyen_ChucNang qcn)
        {
            try
            {
                bool added = await _repo.AddAsync(qcn);
                return added
                    ? Ok(new { Message = "Thêm quyền - chức năng thành công!" })
                    : StatusCode(500, "Không thể thêm bản ghi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm: {ex.Message}");
            }
        }

        //PUT: api/quyenchucnang/{maQuyen}/{oldMaChucNang}/{newMaChucNang}
        [HttpPut("{maQuyen}/{oldMaChucNang}/{newMaChucNang}")]
        public async Task<IActionResult> Update(int maQuyen, int oldMaChucNang, int newMaChucNang)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(maQuyen, oldMaChucNang, newMaChucNang);
                return updated
                    ? Ok(new { Message = "Cập nhật thành công!" })
                    : NotFound("Không tìm thấy bản ghi cần cập nhật.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        //DELETE: api/quyenchucnang/{maQuyen}/{maChucNang}
        [HttpDelete("{maQuyen}/{maChucNang}")]
        public async Task<IActionResult> Delete(int maQuyen, int maChucNang)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maQuyen, maChucNang);
                return deleted
                    ? Ok(new { Message = "Xóa thành công!" })
                    : NotFound("Không tìm thấy bản ghi cần xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa: {ex.Message}");
            }
        }
    }
}
