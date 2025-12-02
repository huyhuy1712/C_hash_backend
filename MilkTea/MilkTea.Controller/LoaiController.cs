using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoaiController : ControllerBase
    {
        private readonly LoaiRepository _repo;

        public LoaiController(LoaiRepository repo)
        {
            _repo = repo;
        }

        // 1. Lấy toàn bộ loại
        [HttpGet]
        public async Task<ActionResult<List<Loai>>> GetAllLoai()
        {
            var result = await _repo.GetAllLoaiAsync();
            return Ok(result);
        }

        // 2. Lấy tên loại theo mã
        [HttpGet("tenloai/{ma}")]
        public async Task<ActionResult<string>> GetTenLoaiByMa(int ma)
        {
            var ten = await _repo.GetTenLoaiByIdAsync(ma);
            if (ten == null)
                return NotFound($"Không tìm thấy loại có mã {ma}");
            return Ok(ten);
        }

        // 3. Lấy mã loại theo tên
        [HttpGet("maloai/{ten}")]
        public async Task<ActionResult<int?>> GetMaLoaiByTen(string ten)
        {
            var ma = await _repo.GetMaLoaiByTenAsync(ten);
            if (ma == null)
                return NotFound($"Không tìm thấy loại có tên '{ten}'");
            return Ok(ma);
        }
    }
}
