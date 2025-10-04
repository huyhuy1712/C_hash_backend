using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class ChiTietCongThucRepository
    {
        private readonly DbConnection _db;

        public ChiTietCongThucRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Đọc toàn bộ chi tiết công thức
        public async Task<List<ChiTietCongThuc>> GetAllAsync()
        {
            var list = new List<ChiTietCongThuc>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaCT, MaNL, SL FROM chitietcongthuc", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaCT = reader.GetOrdinal("MaCT");
            int idxMaNL = reader.GetOrdinal("MaNL");
            int idxSL = reader.GetOrdinal("SL");

            while (await reader.ReadAsync())
            {
                list.Add(new ChiTietCongThuc
                {
                    MaCT = reader.GetInt32(idxMaCT),
                    MaNL = reader.GetInt32(idxMaNL),
                    SL = reader.GetInt32(idxSL)
                });
            }

            return list;
        }

        // 2. Thêm mới chi tiết công thức
        public async Task<bool> AddAsync(ChiTietCongThuc ct)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO chitietcongthuc (MaCT, MaNL, SL)
                          VALUES (@MaCT, @MaNL, @SL)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCT", ct.MaCT);
            cmd.Parameters.AddWithValue("@MaNL", ct.MaNL);
            cmd.Parameters.AddWithValue("@SL", ct.SL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Sửa số lượng nguyên liệu trong công thức
        public async Task<bool> UpdateAsync(int maCT, int maNL, int soLuongMoi)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE chitietcongthuc 
                          SET SL = @SL 
                          WHERE MaCT = @MaCT AND MaNL = @MaNL";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SL", soLuongMoi);
            cmd.Parameters.AddWithValue("@MaCT", maCT);
            cmd.Parameters.AddWithValue("@MaNL", maNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa nguyên liệu khỏi công thức
        public async Task<bool> DeleteAsync(int maCT, int maNL)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM chitietcongthuc WHERE MaCT = @MaCT AND MaNL = @MaNL";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCT", maCT);
            cmd.Parameters.AddWithValue("@MaNL", maNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Xóa toàn bộ chi tiết công thức theo mã công thức
            public async Task<bool> DeleteByMaCTAsync(int maCT)
            {
                using var conn = await _db.GetConnectionAsync();
                var query = "DELETE FROM chitietcongthuc WHERE MaCT = @MaCT";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCT", maCT);

                var rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0; // Trả về true nếu có ít nhất một dòng bị xóa
            }
    }
}
