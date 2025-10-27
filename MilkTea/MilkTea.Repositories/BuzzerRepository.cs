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

      // Lấy danh sách buzzer theo trạng thái (0 hoặc 1)
public async Task<List<Buzzer>> GetBuzzersByTrangThaiAsync(int trangThai)
{
    var list = new List<Buzzer>();

    using var conn = await _db.GetConnectionAsync();
    var query = "SELECT MaBuzzer, SoHieu, TrangThai FROM buzzer WHERE TrangThai = @TrangThai";
    var cmd = new MySqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

    using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var buzzer = new Buzzer
        {
            MaBuzzer = reader.GetInt32(reader.GetOrdinal("MaBuzzer")),
            SoHieu = reader.GetString(reader.GetOrdinal("SoHieu")),
            TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
        };
        list.Add(buzzer);
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

                // Lấy MaMay theo số hiệu
        public async Task<int?> GetMaMayBySoHieuAsync(string sohieu)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaBuzzer FROM buzzer WHERE SoHieu = @SoHieu LIMIT 1";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SoHieu", sohieu);

            var result = await cmd.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return null; // không tìm thấy
        }
    }
}
