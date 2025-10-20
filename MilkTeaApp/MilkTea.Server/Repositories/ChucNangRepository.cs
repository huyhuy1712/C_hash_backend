using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class ChucNangRepository
    {
        private readonly DbConnection _db;

        public ChucNangRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy tất cả chức năng
        public async Task<List<ChucNang>> GetAllAsync()
        {
            var list = new List<ChucNang>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaChucNang, TenChucNang, MoTa FROM chucnang", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMa = reader.GetOrdinal("MaChucNang");
            int idxTen = reader.GetOrdinal("TenChucNang");
            int idxMoTa = reader.GetOrdinal("MoTa");

            while (await reader.ReadAsync())
            {
                list.Add(new ChucNang
                {
                    MaChucNang = reader.GetInt32(idxMa),
                    TenChucNang = reader.GetString(idxTen),
                    MoTa = reader.IsDBNull(idxMoTa) ? "" : reader.GetString(idxMoTa)
                });
            }

            return list;
        }

        // 2. Thêm chức năng mới
        public async Task<bool> AddAsync(ChucNang cn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "INSERT INTO chucnang (TenChucNang, MoTa) VALUES (@TenChucNang, @MoTa)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenChucNang", cn.TenChucNang);
            cmd.Parameters.AddWithValue("@MoTa", cn.MoTa ?? "");

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Sửa chức năng
        public async Task<bool> UpdateAsync(ChucNang cn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE chucnang 
                          SET TenChucNang = @TenChucNang, MoTa = @MoTa 
                          WHERE MaChucNang = @MaChucNang";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenChucNang", cn.TenChucNang);
            cmd.Parameters.AddWithValue("@MoTa", cn.MoTa ?? "");
            cmd.Parameters.AddWithValue("@MaChucNang", cn.MaChucNang);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa chức năng
        public async Task<bool> DeleteAsync(int maChucNang)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM chucnang WHERE MaChucNang = @MaChucNang";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm chức năng theo tên (tìm gần đúng)
        public async Task<List<ChucNang>> SearchByNameAsync(string keyword)
        {
            var list = new List<ChucNang>();
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaChucNang, TenChucNang, MoTa FROM chucnang WHERE TenChucNang LIKE @keyword";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

            using var reader = await cmd.ExecuteReaderAsync();
            int idxMa = reader.GetOrdinal("MaChucNang");
            int idxTen = reader.GetOrdinal("TenChucNang");
            int idxMoTa = reader.GetOrdinal("MoTa");

            while (await reader.ReadAsync())
            {
                list.Add(new ChucNang
                {
                    MaChucNang = reader.GetInt32(idxMa),
                    TenChucNang = reader.GetString(idxTen),
                    MoTa = reader.IsDBNull(idxMoTa) ? "" : reader.GetString(idxMoTa)
                });
            }

            return list;
        }
        // 6. Lấy danh sách chức năng theo quyền
        public async Task<List<ChucNang>> GetByQuyenAsync(int maQuyen)
        {
            var list = new List<ChucNang>();
            using var conn = await _db.GetConnectionAsync();
            var query = @"
        SELECT c.MaChucNang, c.TenChucNang, c.MoTa
        FROM chucnang c
        INNER JOIN quyen_chucnang qc ON c.MaChucNang = qc.MaChucNang
        WHERE qc.MaQuyen = @MaQuyen";

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);

            using var reader = await cmd.ExecuteReaderAsync();

            int idxMa = reader.GetOrdinal("MaChucNang");
            int idxTen = reader.GetOrdinal("TenChucNang");
            int idxMoTa = reader.GetOrdinal("MoTa");

            while (await reader.ReadAsync())
            {
                list.Add(new ChucNang
                {
                    MaChucNang = reader.GetInt32(idxMa),
                    TenChucNang = reader.GetString(idxTen),
                    MoTa = reader.IsDBNull(idxMoTa) ? "" : reader.GetString(idxMoTa)
                });
            }

            return list;
        }
    }
}
