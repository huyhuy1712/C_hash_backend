
using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class NhanVienRepository
    {
        private readonly DbConnection _db;

        public NhanVienRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ nhân viên
        public async Task<List<NhanVien>> GetAllAsync()
        {
            var list = new List<NhanVien>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM nhanvien", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaNV = reader.GetOrdinal("MaNV");
            int idxTenNV = reader.GetOrdinal("TenNV");
            int idxSDT = reader.GetOrdinal("SDT");
            int idxNgayLam = reader.GetOrdinal("NgayLam");
            int idxMaTK = reader.GetOrdinal("MaTK");

            while (await reader.ReadAsync())
            {
                list.Add(new NhanVien
                {
                    MaNV = reader.GetInt32(idxMaNV),
                    TenNV = reader.GetString(idxTenNV),
                    SDT = reader.GetString(idxSDT),
                    NgayLam = reader.GetDateTime(idxNgayLam),
                    MaTK = reader.IsDBNull(idxMaTK) ? null : reader.GetInt32(idxMaTK)
                });
            }

            return list;
        }

        // 2. Thêm nhân viên
        public async Task<bool> AddAsync(NhanVien nv)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO nhanvien (TenNV, SDT, NgayLam, MaTK)
                          VALUES (@TenNV, @SDT, @NgayLam, @MaTK)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
            cmd.Parameters.AddWithValue("@SDT", nv.SDT);
            cmd.Parameters.AddWithValue("@NgayLam", nv.NgayLam);
            cmd.Parameters.AddWithValue("@MaTK", (object?)nv.MaTK ?? DBNull.Value);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  3. Cập nhật thông tin nhân viên
        public async Task<bool> UpdateAsync(NhanVien nv)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE nhanvien 
                          SET TenNV = @TenNV, SDT = @SDT, NgayLam = @NgayLam, MaTK = @MaTK
                          WHERE MaNV = @MaNV";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
            cmd.Parameters.AddWithValue("@SDT", nv.SDT);
            cmd.Parameters.AddWithValue("@NgayLam", nv.NgayLam);
            cmd.Parameters.AddWithValue("@MaTK", (object?)nv.MaTK ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa nhân viên theo mã
        public async Task<bool> DeleteAsync(int maNV)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM nhanvien WHERE MaNV = @MaNV";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm kiếm linh hoạt theo cột và giá trị
        public async Task<List<NhanVien>> SearchAsync(string column, string value)
        {
            var allowedColumns = new List<string> { "TenNV", "SDT", "NgayLam", "MaTK" };
            if (!allowedColumns.Contains(column))
                throw new ArgumentException($"Không thể tìm theo cột '{column}'.");

            var list = new List<NhanVien>();

            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM nhanvien WHERE {column} LIKE @Value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", $"%{value}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new NhanVien
                {
                    MaNV = reader.GetInt32(reader.GetOrdinal("MaNV")),
                    TenNV = reader.GetString(reader.GetOrdinal("TenNV")),
                    SDT = reader.GetString(reader.GetOrdinal("SDT")),
                    NgayLam = reader.GetDateTime(reader.GetOrdinal("NgayLam")),
                    MaTK = reader.IsDBNull(reader.GetOrdinal("MaTK")) ? null : reader.GetInt32(reader.GetOrdinal("MaTK"))
                });
            }

            return list;
        }

        // 6. Tìm kiếm nhân viên theo MaNV
        public async Task<NhanVien?> GetByMaNVAsync(int maNV)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT * FROM nhanvien WHERE MaNV = @MaNV";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NhanVien
                {
                    MaNV = reader.GetInt32(reader.GetOrdinal("MaNV")),
                    TenNV = reader.GetString(reader.GetOrdinal("TenNV")),
                    SDT = reader.GetString(reader.GetOrdinal("SDT")),
                    NgayLam = reader.GetDateTime(reader.GetOrdinal("NgayLam")),
                    MaTK = reader.IsDBNull(reader.GetOrdinal("MaTK")) ? null : reader.GetInt32(reader.GetOrdinal("MaTK"))
                };
            }
            return null;
        }

        // 7. Lấy MaNV theo tên nhân viên
        public async Task<int?> GetMaNVByTenAsync(string tenNV)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT MaNV FROM nhanvien WHERE TRIM(LOWER(TenNV)) = TRIM(LOWER(@TenNV)) LIMIT 1";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenNV", tenNV);

            var result = await cmd.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return null; // không tìm thấy
        }

    }
}
