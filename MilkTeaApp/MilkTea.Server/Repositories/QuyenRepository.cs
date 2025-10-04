using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class QuyenRepository
    {
        private readonly DbConnection _db;

        public QuyenRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ quyền
        public async Task<List<Quyen>> GetAllAsync()
        {
            var list = new List<Quyen>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM quyen", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaQuyen = reader.GetOrdinal("MaQuyen");
            int idxTenQuyen = reader.GetOrdinal("TenQuyen");
            int idxMota = reader.GetOrdinal("Mota");

            while (await reader.ReadAsync())
            {
                list.Add(new Quyen
                {
                    MaQuyen = reader.GetInt32(idxMaQuyen),
                    TenQuyen = reader.GetString(idxTenQuyen),
                    Mota = reader.IsDBNull(idxMota) ? "" : reader.GetString(idxMota)
                });
            }

            return list;
        }

        //  2. Thêm quyền
        public async Task<bool> AddAsync(Quyen q)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO quyen (TenQuyen, Mota) VALUES (@TenQuyen, @Mota)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenQuyen", q.TenQuyen);
            cmd.Parameters.AddWithValue("@Mota", (object?)q.Mota ?? DBNull.Value);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Cập nhật quyền
        public async Task<bool> UpdateAsync(Quyen q)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE quyen SET TenQuyen = @TenQuyen, Mota = @Mota WHERE MaQuyen = @MaQuyen";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenQuyen", q.TenQuyen);
            cmd.Parameters.AddWithValue("@Mota", (object?)q.Mota ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaQuyen", q.MaQuyen);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa quyền
        public async Task<bool> DeleteAsync(int maQuyen)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM quyen WHERE MaQuyen = @MaQuyen";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm kiếm quyền theo tên
        public async Task<List<Quyen>> SearchByNameAsync(string keyword)
        {
            var list = new List<Quyen>();
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT * FROM quyen WHERE TenQuyen LIKE @Keyword";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Quyen
                {
                    MaQuyen = reader.GetInt32(reader.GetOrdinal("MaQuyen")),
                    TenQuyen = reader.GetString(reader.GetOrdinal("TenQuyen")),
                    Mota = reader.IsDBNull(reader.GetOrdinal("Mota")) ? "" : reader.GetString(reader.GetOrdinal("Mota"))
                });
            }

            return list;
        }
    }
}
