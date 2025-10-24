using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories

{
    public class LoaiRepository
    {
        private readonly DbConnection _db;

        public LoaiRepository(DbConnection db)
        {
            _db = db;
        }

        // Lấy toàn bộ loại
        public async Task<List<Loai>> GetAllLoaiAsync()
        {
            var list = new List<Loai>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaLoai, TenLoai, MoTa FROM loai", conn);
            using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
            {
                list.Add(new Loai
                {
                    MaLoai = reader.GetInt32(reader.GetOrdinal("MaLoai")),
                    TenLoai = reader.GetString(reader.GetOrdinal("TenLoai")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa"))
                        ? ""
                        : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }


            return list;
        }

        //Lấy tên loại theo mã
        public async Task<string?> GetTenLoaiByIdAsync(int maLoai)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT TenLoai FROM loai WHERE MaLoai = @maLoai", conn);
            cmd.Parameters.AddWithValue("@maLoai", maLoai);

            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString();
        }

        // Lấy mã loại theo tên
        public async Task<int?> GetMaLoaiByTenAsync(string tenLoai)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaLoai FROM loai WHERE TenLoai = @tenLoai", conn);
            cmd.Parameters.AddWithValue("@tenLoai", tenLoai);

            var result = await cmd.ExecuteScalarAsync();
            return result == null ? null : Convert.ToInt32(result);
        }
    }
}
