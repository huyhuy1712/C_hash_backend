using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Models;
using MilkTea.Server.Repositories;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamKhuyenMaiController : ControllerBase
    {
        private readonly SanPhamKhuyenMaiRepository _repo;

        public SanPhamKhuyenMaiController(SanPhamKhuyenMaiRepository repo)
        {
            _repo = repo;
        }

        //  GET: api/sanphamkhuyenmai
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

        // POST: api/sanphamkhuyenmai
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SanPhamKhuyenMai spkm)
        {
            try
            {
                bool added = await _repo.AddAsync(spkm);
                return added
                    ? Ok(new { Message = "Thêm sản phẩm khuyến mãi thành công!" })
                    : StatusCode(500, "Không thể thêm sản phẩm khuyến mãi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm: {ex.Message}");
            }
        }

        // PUT: api/sanphamkhuyenmai/1/5
        [HttpPut("{maSP}/{maCTKhuyenMaiMoi}")]
        public async Task<IActionResult> Update(int maSP, int maCTKhuyenMaiMoi)
        {
            try
            {
                bool updated = await _repo.UpdateAsync(maSP, maCTKhuyenMaiMoi);
                return updated
                    ? Ok(new { Message = "Cập nhật khuyến mãi cho sản phẩm thành công!" })
                    : NotFound("Không tìm thấy sản phẩm để cập nhật.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        // DELETE: api/sanphamkhuyenmai/sanpham/1
        [HttpDelete("sanpham/{maSP}")]
        public async Task<IActionResult> DeleteBySP(int maSP)
        {
            try
            {
                bool deleted = await _repo.DeleteAsync(maSP);
                return deleted
                    ? Ok(new { Message = "Đã xóa sản phẩm khỏi khuyến mãi." })
                    : NotFound("Không tìm thấy sản phẩm cần xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa: {ex.Message}");
            }
        }

        // DELETE: api/sanphamkhuyenmai/khuyenmai/3
        [HttpDelete("khuyenmai/{maCTKM}")]
        public async Task<IActionResult> DeleteByCTKM(int maCTKM)
        {
            try
            {
                bool deleted = await _repo.DeleteByCTKMAsync(maCTKM);
                return deleted
                    ? Ok(new { Message = "Đã xóa tất cả sản phẩm thuộc chương trình khuyến mãi." })
                    : NotFound("Không tìm thấy chương trình cần xóa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa: {ex.Message}");
            }
        }
    }
}
