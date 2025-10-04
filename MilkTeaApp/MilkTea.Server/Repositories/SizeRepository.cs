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
    }
}
