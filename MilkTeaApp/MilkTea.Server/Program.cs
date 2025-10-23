using MilkTea.Server.Data; // namespace DbConnection
using MilkTea.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Đọc connection string từ appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbConnection vào DI container
builder.Services.AddSingleton(new DbConnection(connectionString));

// Thêm dịch vụ MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Nếu có Repository thì đăng ký ở đây (ví dụ LoaiRepository)
builder.Services.AddScoped<LoaiRepository>();
builder.Services.AddScoped<SanPhamRepository>();
builder.Services.AddScoped<NhaCungCapRepository>();
builder.Services.AddScoped<BuzzerRepository>();
builder.Services.AddScoped<ChiPhiRepository>();
builder.Services.AddScoped<ChiTietCongThucRepository>();
builder.Services.AddScoped<ChiTietDonHangRepository>();
builder.Services.AddScoped<ChiTietPhieuNhapRepository>();
builder.Services.AddScoped<ChucNangRepository>();
builder.Services.AddScoped<CongThucRepository>();
builder.Services.AddScoped<CTKhuyenMaiRepository>();
builder.Services.AddScoped<DoanhThuRepository>();
builder.Services.AddScoped<DonHangRepository>();
builder.Services.AddScoped<NguyenLieuRepository>();
builder.Services.AddScoped<NhanVienRepository>();
builder.Services.AddScoped<PhieuNhapRepository>();
builder.Services.AddScoped<QuyenChucNangRepository>();
builder.Services.AddScoped<QuyenRepository>();
builder.Services.AddScoped<SanPhamKhuyenMaiRepository>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<TaiKhoanRepository>();





var app = builder.Build();

// Middleware xử lý lỗi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// Map route mặc định cho MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
