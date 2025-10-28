using MilkTea.Server.Data; // namespace DbConnection
using MilkTea.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ??c connection string t? appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ??ng ký DbConnection vào DI container
builder.Services.AddSingleton(new DbConnection(connectionString));

// Thêm d?ch v? MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// N?u có Repository thì ??ng ký ? ?ây (ví d? LoaiRepository)
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

// Middleware x? lý l?i
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// Map route m?c ??nh cho MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
