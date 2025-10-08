using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class NguyenLieuRepository
    {
        private readonly DbConnection _db;

        public NguyenLieuRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ nguyên liệu
        public async Task<List<NguyenLieu>> GetAllAsync()
        {
            var list = new List<NguyenLieu>();

            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM nguyenlieu", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaNL = reader.GetOrdinal("MaNL");
            int idxTen = reader.GetOrdinal("Ten");
            int idxSoLuong = reader.GetOrdinal("SoLuong");
            int idxGiaBan = reader.GetOrdinal("GiaBan");

            while (await reader.ReadAsync())
            {
                list.Add(new NguyenLieu
                {
                    MaNL = reader.GetInt32(idxMaNL),
                    Ten = reader.GetString(idxTen),
                    SoLuong = reader.GetInt32(idxSoLuong),
                    GiaBan = reader.GetDecimal(idxGiaBan)
                });
            }

            return list;
        }

        // 2. Thêm nguyên liệu
        public async Task<bool> AddAsync(NguyenLieu nl)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO nguyenlieu (Ten, SoLuong, GiaBan)
                          VALUES (@Ten, @SoLuong, @GiaBan)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ten", nl.Ten);
            cmd.Parameters.AddWithValue("@SoLuong", nl.SoLuong);
            cmd.Parameters.AddWithValue("@GiaBan", nl.GiaBan);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Cập nhật nguyên liệu
        public async Task<bool> UpdateAsync(NguyenLieu nl)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE nguyenlieu 
                          SET Ten = @Ten, SoLuong = @SoLuong, GiaBan = @GiaBan 
                          WHERE MaNL = @MaNL";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Ten", nl.Ten);
            cmd.Parameters.AddWithValue("@SoLuong", nl.SoLuong);
            cmd.Parameters.AddWithValue("@GiaBan", nl.GiaBan);
            cmd.Parameters.AddWithValue("@MaNL", nl.MaNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa nguyên liệu
        public async Task<bool> DeleteAsync(int maNL)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM nguyenlieu WHERE MaNL = @MaNL";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaNL", maNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Tìm kiếm theo tên
        public async Task<List<NguyenLieu>> SearchByNameAsync(string keyword)
        {
            var list = new List<NguyenLieu>();

            using var conn = await _db.GetConnectionAsync();
            var query = "SELECT * FROM nguyenlieu WHERE Ten LIKE @Keyword";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new NguyenLieu
                {
                    MaNL = reader.GetInt32(reader.GetOrdinal("MaNL")),
                    Ten = reader.GetString(reader.GetOrdinal("Ten")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    GiaBan = reader.GetDecimal(reader.GetOrdinal("GiaBan"))
                });
            }

            return list;
        }

        // 6. hàm trừ nguyên liệu
        public async Task<bool> TruSoLuongAsync(int maNL, int soLuongCanTru)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE nguyenlieu
                  SET SoLuong = SoLuong - @SL
                  WHERE MaNL = @MaNL AND SoLuong >= @SL";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SL", soLuongCanTru);
            cmd.Parameters.AddWithValue("@MaNL", maNL);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

    }
}
