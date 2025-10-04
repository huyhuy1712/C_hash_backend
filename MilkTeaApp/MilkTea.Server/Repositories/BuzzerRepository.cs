using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class BuzzerRepository
    {
        private readonly DbConnection _db;

        public BuzzerRepository(DbConnection db)
        {
            _db = db;
        }

       //lấy danh sách theo trạng thái (0 hoặc 1)
        public async Task<List<string>> GetSoHieuByTrangThaiAsync(int trangThai)
        {
            var list = new List<string>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT SoHieu FROM buzzer WHERE TrangThai = @TrangThai", conn);
            cmd.Parameters.AddWithValue("@TrangThai", trangThai);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(reader.GetString(reader.GetOrdinal("SoHieu")));
            }

            return list;
        }

        // Cập nhật trạng thái buzzer (theo số hiệu)
        public async Task<bool> UpdateTrangThaiAsync(string soHieu, int trangThai)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("UPDATE buzzer SET TrangThai = @TrangThai WHERE SoHieu = @SoHieu", conn);
            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
            cmd.Parameters.AddWithValue("@SoHieu", soHieu);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0; // true nếu có ít nhất 1 dòng bị ảnh hưởng
        }
    }
}
