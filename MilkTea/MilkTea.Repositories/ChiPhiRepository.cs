using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class ChiPhiRepository
    {
        private readonly DbConnection _db;

        public ChiPhiRepository(DbConnection db)
        {
            _db = db;
        }

        //1. Đọc toàn bộ dữ liệu chi phí
        public async Task<List<ChiPhi>> GetAllChiPhiAsync()
        {
            var list = new List<ChiPhi>();

            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM chiphi", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            // Lấy chỉ số cột
            int idxMaCP = reader.GetOrdinal("MaCP");
            int idxNgay = reader.GetOrdinal("Ngay");
            int idxThang = reader.GetOrdinal("Thang");
            int idxNam = reader.GetOrdinal("Nam");
            int idxGio = reader.GetOrdinal("Gio");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMaLoai = reader.GetOrdinal("MaLoai");
            int idxMaKM = reader.GetOrdinal("MaKM");
            int idxTongChiPhiSP = reader.GetOrdinal("TongChiPhiSP");
            int idxTongChiPhiNL = reader.GetOrdinal("TongChiPhiNL");

            // Đọc dữ liệu
            while (await reader.ReadAsync())
            {
                var chiPhi = new ChiPhi
                {
                    MaCP = reader.GetInt32(idxMaCP),
                    Ngay = reader.GetInt32(idxNgay),
                    Thang = reader.GetInt32(idxThang),
                    Nam = reader.GetInt32(idxNam),

                    // Đọc cột Gio (kiểu TIME trong MySQL)
                    Gio = reader.IsDBNull(idxGio)
                        ? TimeSpan.Zero
                        : TimeSpan.Parse(reader.GetString(idxGio)),

                    MaSP = reader.IsDBNull(idxMaSP) ? null : reader.GetInt32(idxMaSP),
                    MaLoai = reader.IsDBNull(idxMaLoai) ? null : reader.GetInt32(idxMaLoai),
                    MaKM = reader.IsDBNull(idxMaKM) ? null : reader.GetInt32(idxMaKM),
                    TongChiPhiSP = reader.GetDecimal(idxTongChiPhiSP),
                    TongChiPhiNL = reader.GetDecimal(idxTongChiPhiNL)
                };

                list.Add(chiPhi);
            }

            return list;
        }

        // 2. Thêm mới chi phí
        public async Task<bool> AddChiPhiAsync(ChiPhi cp)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO chiphi 
                          (Ngay, Thang, Nam, Gio, MaSP, MaLoai, MaKM, TongChiPhiSP, TongChiPhiNL)
                          VALUES (@Ngay, @Thang, @Nam, @Gio, @MaSP, @MaLoai, @MaKM, @TongChiPhiSP, @TongChiPhiNL)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ngay", cp.Ngay);
            cmd.Parameters.AddWithValue("@Thang", cp.Thang);
            cmd.Parameters.AddWithValue("@Nam", cp.Nam);

            // Thêm tham số Gio (TimeSpan → TIME)
            cmd.Parameters.AddWithValue("@Gio", cp.Gio.ToString(@"hh\:mm\:ss"));

            cmd.Parameters.AddWithValue("@MaSP", (object?)cp.MaSP ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaLoai", (object?)cp.MaLoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaKM", (object?)cp.MaKM ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TongChiPhiSP", cp.TongChiPhiSP);
            cmd.Parameters.AddWithValue("@TongChiPhiNL", cp.TongChiPhiNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  3. Xóa chi phí theo mã
        public async Task<bool> DeleteChiPhiAsync(int maCP)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("DELETE FROM chiphi WHERE MaCP = @MaCP", conn);
            cmd.Parameters.AddWithValue("@MaCP", maCP);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  4. Lọc dữ liệu theo cột và giá trị tùy ý
        public async Task<List<ChiPhi>> FilterChiPhiAsync(string columnName, object value)
        {
            var list = new List<ChiPhi>();

            // Bảo mật: chỉ cho phép lọc theo cột hợp lệ
            var allowedColumns = new List<string> { "Ngay", "Thang", "Nam", "Gio", "MaSP", "MaLoai", "MaKM" };
            if (!allowedColumns.Contains(columnName))
                throw new ArgumentException($"Không thể lọc theo cột '{columnName}'.");

            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM chiphi WHERE {columnName} = @Value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", value);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new ChiPhi
                {
                    MaCP = reader.GetInt32(reader.GetOrdinal("MaCP")),
                    Ngay = reader.GetInt32(reader.GetOrdinal("Ngay")),
                    Thang = reader.GetInt32(reader.GetOrdinal("Thang")),
                    Nam = reader.GetInt32(reader.GetOrdinal("Nam")),

                    Gio = reader.IsDBNull(reader.GetOrdinal("Gio"))
                        ? TimeSpan.Zero
                        : TimeSpan.Parse(reader.GetString(reader.GetOrdinal("Gio"))),

                    MaSP = reader.IsDBNull(reader.GetOrdinal("MaSP")) ? null : reader.GetInt32(reader.GetOrdinal("MaSP")),
                    MaLoai = reader.IsDBNull(reader.GetOrdinal("MaLoai")) ? null : reader.GetInt32(reader.GetOrdinal("MaLoai")),
                    MaKM = reader.IsDBNull(reader.GetOrdinal("MaKM")) ? null : reader.GetInt32(reader.GetOrdinal("MaKM")),
                    TongChiPhiSP = reader.GetDecimal(reader.GetOrdinal("TongChiPhiSP")),
                    TongChiPhiNL = reader.GetDecimal(reader.GetOrdinal("TongChiPhiNL"))
                });
            }

            return list;
        }
    }
}
