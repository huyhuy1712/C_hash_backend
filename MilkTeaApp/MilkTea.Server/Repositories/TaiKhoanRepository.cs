using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class TaiKhoanRepository
    {
        private readonly DbConnection _db;

        public TaiKhoanRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy tất cả tài khoản
        public async Task<List<TaiKhoan>> GetAllAsync()
        {
            var list = new List<TaiKhoan>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM taikhoan", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaTK = reader.GetOrdinal("MaTK");
            int idxTenTaiKhoan = reader.GetOrdinal("TenTaiKhoan");
            int idxAnh = reader.GetOrdinal("Anh");
            int idxMatKhau = reader.GetOrdinal("MatKhau");
            int idxTrangThai = reader.GetOrdinal("TrangThai");
            int idxMaQuyen = reader.GetOrdinal("MaQuyen");

            while (await reader.ReadAsync())
            {
                list.Add(new TaiKhoan
                {
                    MaTK = reader.GetInt32(idxMaTK),
                    TenTaiKhoan = reader.GetString(idxTenTaiKhoan),
                    anh = reader.IsDBNull(idxAnh) ? string.Empty : reader.GetString(idxAnh),
                    MatKhau = reader.GetString(idxMatKhau),
                    TrangThai = reader.GetInt32(idxTrangThai),
                    MaQuyen = reader.GetInt32(idxMaQuyen)
                });
            }

            return list;
        }

        // 2. Thêm tài khoản
        public async Task<bool> AddAsync(TaiKhoan tk)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO taikhoan (TenTaiKhoan, Anh, MatKhau, TrangThai, MaQuyen)
                          VALUES (@TenTaiKhoan, @Anh, @MatKhau, @TrangThai, @MaQuyen)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TenTaiKhoan", tk.TenTaiKhoan);
            cmd.Parameters.AddWithValue("@Anh", tk.anh ?? "");
            cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
            cmd.Parameters.AddWithValue("@MaQuyen", tk.MaQuyen);
            cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Cập nhật tài khoản
        public async Task<bool> UpdateAsync(TaiKhoan tk)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE taikhoan 
                          SET TenTaiKhoan=@TenTaiKhoan, Anh=@Anh, MatKhau=@MatKhau, TrangThai=@TrangThai, MaQuyen=@MaQuyen
                          WHERE MaTK=@MaTK";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaTK", tk.MaTK);
            cmd.Parameters.AddWithValue("@TenTaiKhoan", tk.TenTaiKhoan);
            cmd.Parameters.AddWithValue("@Anh", tk.anh ?? "");
            cmd.Parameters.AddWithValue("@MatKhau", tk.MatKhau);
            cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
            cmd.Parameters.AddWithValue("@MaQuyen", tk.MaQuyen);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa tài khoản
        public async Task<bool> DeleteAsync(int maTK)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("DELETE FROM taikhoan WHERE MaTK = @MaTK", conn);
            cmd.Parameters.AddWithValue("@MaTK", maTK);
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //5. Tìm kiếm theo cột và giá trị
        public async Task<List<TaiKhoan>> SearchAsync(string column, string value)
        {
            var allowedColumns = new List<string> { "TenTaiKhoan", "TrangThai", "MaQuyen" };
            if (!allowedColumns.Contains(column))
                throw new ArgumentException($"Không thể tìm kiếm theo cột '{column}'.");

            var list = new List<TaiKhoan>();
            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM taikhoan WHERE {column} LIKE @Value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", $"%{value}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new TaiKhoan
                {
                    MaTK = reader.GetInt32(reader.GetOrdinal("MaTK")),
                    TenTaiKhoan = reader.GetString(reader.GetOrdinal("TenTaiKhoan")),
                    anh = reader.IsDBNull(reader.GetOrdinal("Anh")) ? string.Empty : reader.GetString(reader.GetOrdinal("Anh")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai")),
                    MaQuyen = reader.GetInt32(reader.GetOrdinal("MaQuyen"))
                });
            }

            return list;
        }

        // 6. Lấy tài khoản theo MaTK
        public async Task<TaiKhoan?> GetByIdAsync(int maTK)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT * FROM taikhoan WHERE MaTK = @MaTK";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaTK", maTK);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new TaiKhoan
                {
                    MaTK = reader.GetInt32(reader.GetOrdinal("MaTK")),
                    TenTaiKhoan = reader.GetString(reader.GetOrdinal("TenTaiKhoan")),
                    anh = reader.IsDBNull(reader.GetOrdinal("Anh")) ? string.Empty : reader.GetString(reader.GetOrdinal("Anh")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai")),
                    MaQuyen = reader.GetInt32(reader.GetOrdinal("MaQuyen"))
                };
            }

            return null;
        }
    }
}
