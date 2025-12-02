using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class DonViTinhRepository
    {
        private readonly DbConnection _db;

        public DonViTinhRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ đơn vị tính
        public async Task<List<DonViTinh>> GetAllAsync()
        {
            var list = new List<DonViTinh>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM donvitinh ORDER BY MaDVT", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaDVT = reader.GetOrdinal("MaDVT");
            int idxTenDVT = reader.GetOrdinal("TenDVT");
            while (await reader.ReadAsync())
            {
                list.Add(new DonViTinh
                {
                    MaDVT = reader.GetInt32(idxMaDVT),
                    TenDVT = reader.GetString(idxTenDVT)
                });
            }
            return list;
        }

        // 2. Thêm mới đơn vị tính
        public async Task<DonViTinh?> AddAsync(DonViTinh dvt)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"
                INSERT INTO donvitinh (TenDVT) 
                VALUES (@TenDVT);
                SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenDVT", dvt.TenDVT.Trim());

            var newId = await cmd.ExecuteScalarAsync();
            if (newId != null && int.TryParse(newId.ToString(), out int maDVT))
            {
                dvt.MaDVT = maDVT;
                return dvt;
            }
            return null;
        }

        // 3. Cập nhật đơn vị tính
        public async Task<bool> UpdateAsync(DonViTinh dvt)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"
                UPDATE donvitinh 
                SET TenDVT = @TenDVT 
                WHERE MaDVT = @MaDVT";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenDVT", dvt.TenDVT.Trim());
            cmd.Parameters.AddWithValue("@MaDVT", dvt.MaDVT);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa đơn vị tính (có thể đổi thành Soft Delete nếu cần)
        public async Task<bool> DeleteAsync(int maDVT)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM donvitinh WHERE MaDVT = @MaDVT";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaDVT", maDVT);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Soft Delete đơn vị tính
        public async Task<bool> SoftDeleteAsync(int maDVT)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "UPDATE donvitinh SET TrangThai = 0 WHERE MaDVT = @MaDVT";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaDVT", maDVT);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}