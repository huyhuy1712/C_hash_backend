using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class NhaCungCapRepository
    {
        private readonly DbConnection _db;

        public NhaCungCapRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ nhà cung cấp
        public async Task<List<NhaCungCap>> GetAllAsync()
        {
            var list = new List<NhaCungCap>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM nhacungcap", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaNCC = reader.GetOrdinal("MaNCC");
            int idxTenNCC = reader.GetOrdinal("TenNCC");
            int idxSDT = reader.GetOrdinal("SDT");
            int idxDiaChi = reader.GetOrdinal("DiaChi");
            int idxTrangThai = reader.GetOrdinal("TrangThai");

            while (await reader.ReadAsync())
            {
                list.Add(new NhaCungCap
                {
                    MaNCC = reader.GetInt32(idxMaNCC),
                    TenNCC = reader.GetString(idxTenNCC),
                    SDT = reader.GetString(idxSDT),
                    DiaChi = reader.GetString(idxDiaChi),
                    TrangThai = reader.GetInt32(idxTrangThai)
                });
            }

            return list;
        }

        // 2. Thêm nhà cung cấp
        public async Task<NhaCungCap?> AddAsync(NhaCungCap ncc)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"
                INSERT INTO nhacungcap (TenNCC, SDT, DiaChi, TrangThai)
                VALUES (@TenNCC, @SDT, @DiaChi, @TrangThai);
                SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
            cmd.Parameters.AddWithValue("@SDT", ncc.SDT);
            cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
            cmd.Parameters.AddWithValue("@TrangThai", ncc.TrangThai);

            var newId = await cmd.ExecuteScalarAsync();
            if (newId != null && int.TryParse(newId.ToString(), out int maNCC))
            {
                ncc.MaNCC = maNCC;
                return ncc;
            }

            return null;
        }

        // 3. Cập nhật thông tin nhà cung cấp
        public async Task<bool> UpdateAsync(NhaCungCap ncc)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE nhacungcap 
                          SET TenNCC = @TenNCC, SDT = @SDT, DiaChi = @DiaChi, TrangThai = @TrangThai
                          WHERE MaNCC = @MaNCC";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
            cmd.Parameters.AddWithValue("@SDT", ncc.SDT);
            cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
            cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
            cmd.Parameters.AddWithValue("@TrangThai", ncc.TrangThai);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa nhà cung cấp theo mã
        public async Task<bool> DeleteAsync(int maNCC)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM nhacungcap WHERE MaNCC = @MaNCC";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNCC", maNCC);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm kiếm linh hoạt theo cột và giá trị
        public async Task<List<NhaCungCap>> SearchAsync(string column, string value)
        {
            var allowedColumns = new List<string> { "TenNCC", "SDT", "DiaChi", "TrangThai" };
            if (!allowedColumns.Contains(column))
                throw new ArgumentException($"Không thể tìm theo cột '{column}'.");

            var list = new List<NhaCungCap>();

            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM nhacungcap WHERE {column} LIKE @Value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", $"%{value}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new NhaCungCap
                {
                    MaNCC = reader.GetInt32(reader.GetOrdinal("MaNCC")),
                    TenNCC = reader.GetString(reader.GetOrdinal("TenNCC")),
                    SDT = reader.GetString(reader.GetOrdinal("SDT")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
                });
            }

            return list;
        }

        // 6. Tìm kiếm nhà cung cấp theo MaNCC
        public async Task<NhaCungCap?> GetByMaNCCAsync(int maNCC)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT * FROM nhacungcap WHERE MaNCC = @MaNCC";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNCC", maNCC);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NhaCungCap
                {
                    MaNCC = reader.GetInt32(reader.GetOrdinal("MaNCC")),
                    TenNCC = reader.GetString(reader.GetOrdinal("TenNCC")),
                    SDT = reader.GetString(reader.GetOrdinal("SDT")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
                };
            }
            return null;
        }

        // 7. Lấy MaNCC theo tên nhà cung cấp
        public async Task<int?> GetMaNCCByTenAsync(string tenNCC)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaNCC FROM nhacungcap WHERE TRIM(LOWER(TenNCC)) = TRIM(LOWER(@TenNCC)) LIMIT 1";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNCC", tenNCC);

            var result = await cmd.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return null; // không tìm thấy
        }

        // 8.soft delete nhà cung cấp
        public async Task<bool> SoftDeleteAsync(int maNCC)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "UPDATE nhacungcap SET TrangThai = 0 WHERE MaNCC = @MaNCC AND TrangThai = 1";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNCC", maNCC);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
