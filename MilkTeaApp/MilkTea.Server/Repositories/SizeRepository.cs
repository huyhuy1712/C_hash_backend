using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class SizeRepository
    {
        private readonly DbConnection _db;

        public SizeRepository(DbConnection db)
        {
            _db = db;
        }

        //Hàm đọc toàn bộ size trong bảng "size"
        public async Task<List<Size>> GetAllSizeAsync()
        {
            var list = new List<Size>();

            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT MaSize, TenSize, PhuThu FROM size", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaSize = reader.GetOrdinal("MaSize");
            int idxTenSize = reader.GetOrdinal("TenSize");
            int idxPhuThu = reader.GetOrdinal("PhuThu");

            while (await reader.ReadAsync())
            {
                list.Add(new Size
                {
                    MaSize = reader.GetInt32(idxMaSize),
                    TenSize = reader.GetString(idxTenSize),
                    PhuThu = reader.GetInt32(idxPhuThu)
                });
            }

            return list;
        }

        //Hàm lấy thông tin size theo tên
        public async Task<Size?> GetSizeByTenAsync(string tenSize)
        {
            using var conn = await _db.GetConnectionAsync();

            var cmd = new MySqlCommand("SELECT MaSize, TenSize, PhuThu FROM size WHERE TenSize = @TenSize", conn);
            cmd.Parameters.AddWithValue("@TenSize", tenSize);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Size
                {
                    MaSize = reader.GetInt32(reader.GetOrdinal("MaSize")),
                    TenSize = reader.GetString(reader.GetOrdinal("TenSize")),
                    PhuThu = reader.GetInt32(reader.GetOrdinal("PhuThu"))
                };
            }

            return null; // Không tìm thấy size
        }


    }
}
