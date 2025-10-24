using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {
        private readonly SizeRepository _repo;

        public SizeController(SizeRepository repo)
        {
            _repo = repo;
        }

        // GET: api/size
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _repo.GetAllSizeAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy dữ liệu size: {ex.Message}");
            }
        }

        // GET: api/size
        [HttpGet("ten/{tenSize}")]
        public async Task<IActionResult> GetSizeByTenAsync(string tenSize)
        {
            try
            {
                var list = await _repo.GetSizeByTenAsync(tenSize);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy dữ liệu size: {ex.Message}");
            }
        }
        //GET api/size/{maSize}
        [HttpGet("{maSize}")]
        public async Task<IActionResult> GetSizeByMaAsync(int maSize)
        {
            try
            {
                var size = await _repo.GetSizeByIdAsync(maSize);
                if (size == null)
                    return NotFound($"Không tìm thấy size với mã: {maSize}");

                return Ok(size);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi lấy dữ liệu size: {ex.Message}");
            }
        }
    }
}
