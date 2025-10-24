using Microsoft.EntityFrameworkCore;
using MilkTea.Server.Models;

namespace MilkTea.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Mỗi DbSet tương ứng với 1 bảng trong DB
        public DbSet<Loai> Loais { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
    }
}
