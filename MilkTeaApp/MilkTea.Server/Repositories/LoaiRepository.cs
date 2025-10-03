using MilkTea.Server.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkTea.Server.Repositories
{
    public class LoaiRepository
    {
        private readonly DbConnection _db;

        public LoaiRepository(DbConnection db)
        {
            _db = db;
        }

        public async Task<List<string>> GetLoaiAsync()
        {
            var list = new List<string>();

            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT TenLoai FROM loai", conn); // giả sử bảng loai có cột 'ten_loai'
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(reader.GetString(reader.GetOrdinal("TenLoai")));        
            }

            return list;
        }
    }
}
