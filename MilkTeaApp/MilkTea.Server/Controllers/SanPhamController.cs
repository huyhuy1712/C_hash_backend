using Microsoft.AspNetCore.Mvc;
using MilkTea.Server.Models;
using MilkTea.Server.Repositories;

namespace MilkTea.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamController : ControllerBase
    {
        private readonly SanPhamRepository _repo;

        public SanPhamController(SanPhamRepository repo)
        {
            _repo = repo;
        }

        // GET api/sanpham
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetAll()
        {
            var sanPhams = await _repo.GetAllAsync();
            return Ok(sanPhams);
        }

        // GET api/sanpham/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetById(int id)
        {
            var sp = await _repo.GetByIdAsync(id);
            if (sp == null) return NotFound();
            return Ok(sp);
        }

        // POST api/sanpham
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sp)
        {
            await _repo.AddAsync(sp);
            return Ok(new { message = "Thêm sản phẩm thành công" });
        }

        // PUT api/sanpham/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SanPham sp)
        {
            if (id != sp.MaSP) return BadRequest();

            await _repo.UpdateAsync(sp);
            return Ok(new { message = "Cập nhật sản phẩm thành công" });
        }

        // DELETE api/sanpham/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Xóa sản phẩm thành công" });
        }
    }
}
