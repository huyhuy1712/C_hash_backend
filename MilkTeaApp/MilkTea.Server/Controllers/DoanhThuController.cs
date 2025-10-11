using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Repositories;
using MilkTea.Server.Models;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoanhThuController : ControllerBase
    {
        private readonly DoanhThuRepository _repo;

        public DoanhThuController(DoanhThuRepository repo)
        {
            _repo = repo;
        }

        // GET: api/doanhthu
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
                Console.WriteLine(ex.ToString());
                return StatusCode(500, $"Lỗi khi lấy dữ liệu doanh thu: {ex.Message}");
            }
        }

        // POST: api/doanhthu
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DoanhThu dt)
        {
            try
            {
                bool added = await _repo.AddAsync(dt);
                return added
                    ? Ok(new { Message = "Thêm doanh thu thành công!" })
                    : StatusCode(500, "Không thể thêm doanh thu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm doanh thu: {ex.Message}");
            }
        }

        // GET: api/doanhthu/search?column=Thang&value=5
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
                return StatusCode(500, $"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }
    }
}
