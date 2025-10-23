using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class PhieuNhapRepository
    {
        private readonly DbConnection _db;

        public PhieuNhapRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ phiếu nhập
        public async Task<List<PhieuNhap>> GetAllAsync()
        {
            var list = new List<PhieuNhap>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM phieunhap", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaPN = reader.GetOrdinal("MaPN");
            int idxNgayNhap = reader.GetOrdinal("NgayNhap");
            int idxSoLuong = reader.GetOrdinal("SoLuong");
            int idxMaNCC = reader.GetOrdinal("MaNCC");
            int idxMaNV = reader.GetOrdinal("MaNV");
            int idxTongTien = reader.GetOrdinal("TongTien");
            int idxTrangThai = reader.GetOrdinal("TrangThai");

            while (await reader.ReadAsync())
            {
                list.Add(new PhieuNhap
                {
                    MaPN = reader.GetInt32(idxMaPN),
                    NgayNhap = reader.IsDBNull(idxNgayNhap) ? null : reader.GetDateTime(idxNgayNhap),
                    SoLuong = reader.GetInt32(idxSoLuong),
                    MaNCC = reader.IsDBNull(idxMaNCC) ? null : reader.GetInt32(idxMaNCC),
                    MaNV = reader.IsDBNull(idxMaNV) ? null : reader.GetInt32(idxMaNV),
                    TongTien = reader.GetDecimal(idxTongTien),
                    TrangThai = reader.GetInt32(idxTrangThai)
                });
            }

            return list;
        }

        //  2. Thêm phiếu nhập
        public async Task<int> AddAsync(PhieuNhap pn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO phieunhap (NgayNhap, SoLuong, TrangThai, MaNCC, MaNV, TongTien)
                  VALUES (@NgayNhap, @SoLuong, @TrangThai, @MaNCC, @MaNV, @TongTien);

                  SELECT LAST_INSERT_ID();";

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);
            cmd.Parameters.AddWithValue("@SoLuong", pn.SoLuong);
            cmd.Parameters.AddWithValue("@TrangThai", pn.TrangThai);
            cmd.Parameters.AddWithValue("@MaNCC", (object?)pn.MaNCC ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV", (object?)pn.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TongTien", pn.TongTien);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        //  3. Cập nhật phiếu nhập
        public async Task<bool> UpdateAsync(PhieuNhap pn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE phieunhap 
                          SET NgayNhap = @NgayNhap, SoLuong = @SoLuong, MaNCC = @MaNCC, MaNV = @MaNV, TongTien = @TongTien, TrangThai = @TrangThai

                          WHERE MaPN = @MaPN";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);
            cmd.Parameters.AddWithValue("@SoLuong", pn.SoLuong);
            cmd.Parameters.AddWithValue("@MaNCC", (object?)pn.MaNCC ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV", (object?)pn.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TongTien", pn.TongTien);
            cmd.Parameters.AddWithValue("@MaPN", pn.MaPN);
            cmd.Parameters.AddWithValue("@TrangThai", pn.TrangThai);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  4. Xóa phiếu nhập
        public async Task<bool> DeleteAsync(int maPN)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM phieunhap WHERE MaPN = @MaPN";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaPN", maPN);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  5. Tìm kiếm theo cột & giá trị
        public async Task<List<PhieuNhap>> SearchAsync(string column, string value)
        {
            var allowedColumns = new List<string> { "NgayNhap", "SoLuong", "MaNCC", "MaNV", "TongTien" };
            if (!allowedColumns.Contains(column))
                throw new ArgumentException($"Không thể tìm kiếm theo cột '{column}'.");

            var list = new List<PhieuNhap>();
            using var conn = await _db.GetConnectionAsync();
            var query = $"SELECT * FROM phieunhap WHERE {column} LIKE @Value";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", $"%{value}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new PhieuNhap
                {
                    MaPN = reader.GetInt32(reader.GetOrdinal("MaPN")),
                    NgayNhap = reader.IsDBNull(reader.GetOrdinal("NgayNhap")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayNhap")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    MaNCC = reader.IsDBNull(reader.GetOrdinal("MaNCC")) ? null : reader.GetInt32(reader.GetOrdinal("MaNCC")),
                    MaNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? null : reader.GetInt32(reader.GetOrdinal("MaNV")),
                    TongTien = reader.GetDecimal(reader.GetOrdinal("TongTien")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))

                });
            }

            return list;
        }
    }
}
