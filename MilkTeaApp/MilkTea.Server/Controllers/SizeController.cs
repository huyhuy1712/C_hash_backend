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
    }
}
