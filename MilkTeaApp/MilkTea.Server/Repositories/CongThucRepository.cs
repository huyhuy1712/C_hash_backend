using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class CongThucRepository
    {
        private readonly DbConnection _db;

        public CongThucRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ công thức
        public async Task<List<CongThuc>> GetAllAsync()
        {
            var list = new List<CongThuc>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaCT, Ten, MaSP, MoTa FROM congthuc", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaCT = reader.GetOrdinal("MaCT");
            int idxTen = reader.GetOrdinal("Ten");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMoTa = reader.GetOrdinal("MoTa");

            while (await reader.ReadAsync())
            {
                list.Add(new CongThuc
                {
                    MaCT = reader.GetInt32(idxMaCT),
                    Ten = reader.GetString(idxTen),
                    MaSP = reader.GetInt32(idxMaSP),
                    MoTa = reader.IsDBNull(idxMoTa) ? null : reader.GetString(idxMoTa)
                });
            }

            return list;
        }

        // 2. Thêm mới công thức
        public async Task<bool> AddAsync(CongThuc ct)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO congthuc (Ten, MaSP, MoTa)
                          VALUES (@Ten, @MaSP, @MoTa)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ten", ct.Ten);
            cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
            cmd.Parameters.AddWithValue("@MoTa", ct.MoTa ?? "");

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Sửa công thức
        public async Task<bool> UpdateAsync(CongThuc ct)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE congthuc 
                          SET Ten = @Ten, MaSP = @MaSP, MoTa = @MoTa 
                          WHERE MaCT = @MaCT";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ten", ct.Ten);
            cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
            cmd.Parameters.AddWithValue("@MoTa", ct.MoTa ?? "");
            cmd.Parameters.AddWithValue("@MaCT", ct.MaCT);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa công thức
        public async Task<bool> DeleteAsync(int maCT)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM congthuc WHERE MaCT = @MaCT";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCT", maCT);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm kiếm công thức theo tên
        public async Task<List<CongThuc>> SearchByNameAsync(string keyword)
        {
            var list = new List<CongThuc>();
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaCT, Ten, MaSP, MoTa FROM congthuc WHERE Ten LIKE @keyword";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

            using var reader = await cmd.ExecuteReaderAsync();
            int idxMaCT = reader.GetOrdinal("MaCT");
            int idxTen = reader.GetOrdinal("Ten");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMoTa = reader.GetOrdinal("MoTa");

            while (await reader.ReadAsync())
            {
                list.Add(new CongThuc
                {
                    MaCT = reader.GetInt32(idxMaCT),
                    Ten = reader.GetString(idxTen),
                    MaSP = reader.GetInt32(idxMaSP),
                    MoTa = reader.IsDBNull(idxMoTa) ? null : reader.GetString(idxMoTa)
                });
            }

            return list;
        }
        public async Task<CongThuc?> GetByIdSpAsync(int maSP)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaCT, Ten, MaSP, MoTa FROM congthuc WHERE MaSP = @MaSP";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", maSP);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CongThuc
                {
                    MaCT = reader.GetInt32(reader.GetOrdinal("MaCT")),
                    Ten = reader.GetString(reader.GetOrdinal("Ten")),
                    MaSP = reader.GetInt32(reader.GetOrdinal("MaSP")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa"))
                };
            }

            return null;
        }
    }
}
