using MilkTea.Server.Data; // namespace DbConnection
using MilkTea.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new DbConnection(connectionString));

builder.Services.AddControllersWithViews();

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
builder.Services.AddScoped<DonViTinhRepository>();





var app = builder.Build();

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
