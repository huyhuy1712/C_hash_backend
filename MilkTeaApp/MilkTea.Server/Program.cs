using MilkTea.Server.Data; // namespace DbConnection
var builder = WebApplication.CreateBuilder(args);

// Đọc connection string từ appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbConnection vào DI container
builder.Services.AddSingleton(new DbConnection(connectionString));

// Thêm dịch vụ MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Nếu có Repository thì đăng ký ở đây (ví dụ LoaiRepository)
// builder.Services.AddScoped<LoaiRepository>();

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
