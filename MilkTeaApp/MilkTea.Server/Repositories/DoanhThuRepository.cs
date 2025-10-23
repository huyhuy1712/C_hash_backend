using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class DoanhThuRepository
    {
        private readonly DbConnection _db;

        public DoanhThuRepository(DbConnection db)
        {
            _db = db;
        }

        // 🟢 1. Lấy toàn bộ doanh thu
        public async Task<List<DoanhThu>> GetAllAsync()
        {
            var list = new List<DoanhThu>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM doanhthu", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaDT = reader.GetOrdinal("MaDT");
            int idxNgay = reader.GetOrdinal("Ngay");
            int idxThang = reader.GetOrdinal("Thang");
            int idxNam = reader.GetOrdinal("Nam");
            int idxGio = reader.GetOrdinal("Gio");
            int idxSLBan = reader.GetOrdinal("SLBan");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMaLoai = reader.GetOrdinal("MaLoai");
            int idxMaKM = reader.GetOrdinal("MaKM");
            int idxMaSize = reader.GetOrdinal("MaSize");
            int idxTongDT = reader.GetOrdinal("TongDoanhThu");

            while (await reader.ReadAsync())
            {
                list.Add(new DoanhThu
                {
                    MaDT = reader.GetInt32(idxMaDT),
                    Ngay = reader.GetInt32(idxNgay),
                    Thang = reader.GetInt32(idxThang),
                    Nam = reader.GetInt32(idxNam),
                    Gio = reader.IsDBNull(idxGio)
    ? TimeSpan.Zero
    : ((TimeSpan?)reader.GetValue(idxGio)) ?? TimeSpan.Zero,

                    SLBan = reader.GetInt32(idxSLBan),
                    MaSP = reader.IsDBNull(idxMaSP) ? null : reader.GetInt32(idxMaSP),
                    MaLoai = reader.IsDBNull(idxMaLoai) ? null : reader.GetInt32(idxMaLoai),
                    MaKM = reader.IsDBNull(idxMaKM) ? null : reader.GetInt32(idxMaKM),
                    MaSize = reader.IsDBNull(idxMaSize) ? null : reader.GetInt32(idxMaSize),
                    TongDoanhThu = reader.GetDecimal(idxTongDT)
                });
            }

            return list;
        }

        // 2. Thêm mới doanh thu
        public async Task<bool> AddAsync(DoanhThu dt)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO doanhthu 
                        (Ngay, Thang, Nam, Gio, SLBan, MaSP, MaLoai, MaKM, MaSize, TongDoanhThu)
                        VALUES (@Ngay, @Thang, @Nam, @Gio, @SLBan, @MaSP, @MaLoai, @MaKM, @MaSize, @TongDoanhThu)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ngay", dt.Ngay);
            cmd.Parameters.AddWithValue("@Thang", dt.Thang);
            cmd.Parameters.AddWithValue("@Nam", dt.Nam);
            cmd.Parameters.AddWithValue("@Gio", dt.Gio);
            cmd.Parameters.AddWithValue("@SLBan", dt.SLBan);
            cmd.Parameters.AddWithValue("@MaSP", dt.MaSP);
            cmd.Parameters.AddWithValue("@MaLoai", dt.MaLoai);
            cmd.Parameters.AddWithValue("@MaKM", dt.MaKM);
            cmd.Parameters.AddWithValue("@MaSize", dt.MaSize);
            cmd.Parameters.AddWithValue("@TongDoanhThu", dt.TongDoanhThu);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  3. Tìm kiếm linh hoạt theo cột và giá trị
        public async Task<List<DoanhThu>> SearchAsync(string column, string value)
        {
            // Giới hạn cột hợp lệ để tránh SQL injection
            var validColumns = new HashSet<string>
            {
                "Ngay", "Thang", "Nam", "MaSP", "MaLoai", "MaKM", "MaSize"
            };
            if (!validColumns.Contains(column))
                throw new ArgumentException("Tên cột không hợp lệ.");

            var list = new List<DoanhThu>();
            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM doanhthu WHERE {column} LIKE @value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@value", $"%{value}%");

            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaDT = reader.GetOrdinal("MaDT");
            int idxNgay = reader.GetOrdinal("Ngay");
            int idxThang = reader.GetOrdinal("Thang");
            int idxNam = reader.GetOrdinal("Nam");
            int idxGio = reader.GetOrdinal("Gio");
            int idxSLBan = reader.GetOrdinal("SLBan");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMaLoai = reader.GetOrdinal("MaLoai");
            int idxMaKM = reader.GetOrdinal("MaKM");
            int idxMaSize = reader.GetOrdinal("MaSize");
            int idxTongDT = reader.GetOrdinal("TongDoanhThu");

            while (await reader.ReadAsync())
            {
                list.Add(new DoanhThu
                {
                    MaDT = reader.GetInt32(idxMaDT),
                    Ngay = reader.GetInt32(idxNgay),
                    Thang = reader.GetInt32(idxThang),
                    Nam = reader.GetInt32(idxNam),
                    Gio = reader.IsDBNull(idxGio)
                        ? TimeSpan.Zero
                        : (TimeSpan)reader.GetValue(idxGio),
                    SLBan = reader.GetInt32(idxSLBan),
                    MaSP = reader.IsDBNull(idxMaSP) ? null : reader.GetInt32(idxMaSP),
                    MaLoai = reader.IsDBNull(idxMaLoai) ? null : reader.GetInt32(idxMaLoai),
                    MaKM = reader.IsDBNull(idxMaKM) ? null : reader.GetInt32(idxMaKM),
                    MaSize = reader.IsDBNull(idxMaSize) ? null : reader.GetInt32(idxMaSize),
                    TongDoanhThu = reader.GetDecimal(idxTongDT)
                });
            }

            return list;
        }
    }
}
