using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class DonHangRepository
    {
        private readonly DbConnection _db;

        public DonHangRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Đọc toàn bộ đơn hàng
        public async Task<List<DonHang>> GetAllAsync()
        {
            var list = new List<DonHang>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM donhang", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaDH = reader.GetOrdinal("MaDH");
            int idxMaNV = reader.GetOrdinal("MaNV");
            int idxNgayLap = reader.GetOrdinal("NgayLap");
            int idxGioLap = reader.GetOrdinal("GioLap");
            int idxTrangThai = reader.GetOrdinal("TrangThai");
            int idxMaBuzzer = reader.GetOrdinal("MaBuzzer");
            int idxPTTT = reader.GetOrdinal("PhuongThucThanhToan");
            int idxTongGia = reader.GetOrdinal("TongGia");

            while (await reader.ReadAsync())
            {
                list.Add(new DonHang
                {
                    MaDH = reader.GetInt32(idxMaDH),
                    MaNV = reader.IsDBNull(idxMaNV) ? null : reader.GetInt32(idxMaNV),
                    NgayLap = reader.GetDateTime(idxNgayLap),
                    //GioLap = reader.IsDBNull(idxGioLap)
                    //    ? TimeSpan.Zero
                    //    : TimeSpan.Parse(reader.GetString(idxGioLap)),
                    GioLap = reader.IsDBNull(idxGioLap)
                    ? (TimeSpan?)null
                    : (TimeSpan?)reader.GetValue(idxGioLap),
                    TrangThai = reader.GetInt32(idxTrangThai),
                    MaBuzzer = reader.IsDBNull(idxMaBuzzer) ? null : reader.GetInt32(idxMaBuzzer),
                    PhuongThucThanhToan = reader.IsDBNull(idxPTTT) ? null : reader.GetInt32(idxPTTT),
                    TongGia = reader.GetDecimal(idxTongGia)
                }); 
            }

            return list;
        }

        // 2. Thêm mới đơn hàng
        public async Task<bool> AddAsync(DonHang dh)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO donhang 
                         (MaNV, NgayLap, GioLap, TrangThai, MaBuzzer, PhuongThucThanhToan, TongGia)
                         VALUES (@MaNV, @NgayLap, @GioLap, @TrangThai, @MaBuzzer, @PhuongThucThanhToan, @TongGia)";
            var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@MaNV", (object?)dh.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayLap", dh.NgayLap);
            cmd.Parameters.AddWithValue("@GioLap", dh.GioLap.ToString());
            cmd.Parameters.AddWithValue("@TrangThai", dh.TrangThai);
            cmd.Parameters.AddWithValue("@MaBuzzer", (object?)dh.MaBuzzer ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PhuongThucThanhToan", (object?)dh.PhuongThucThanhToan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TongGia", dh.TongGia);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Cập nhật đơn hàng
        public async Task<bool> UpdateAsync(DonHang dh)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE donhang SET 
                          MaNV = @MaNV,
                          NgayLap = @NgayLap,
                          GioLap = @GioLap,
                          TrangThai = @TrangThai,
                          MaBuzzer = @MaBuzzer,
                          PhuongThucThanhToan = @PhuongThucThanhToan,
                          TongGia = @TongGia
                          WHERE MaDH = @MaDH";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaDH", dh.MaDH);
            cmd.Parameters.AddWithValue("@MaNV", (object?)dh.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayLap", dh.NgayLap);
            cmd.Parameters.AddWithValue("@GioLap", dh.GioLap.ToString());
            cmd.Parameters.AddWithValue("@TrangThai", dh.TrangThai);
            cmd.Parameters.AddWithValue("@MaBuzzer", (object?)dh.MaBuzzer ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PhuongThucThanhToan", (object?)dh.PhuongThucThanhToan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TongGia", dh.TongGia);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  4. Xóa đơn hàng
        public async Task<bool> DeleteAsync(int maDH)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("DELETE FROM donhang WHERE MaDH = @MaDH", conn);
            cmd.Parameters.AddWithValue("@MaDH", maDH);
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  5. Tìm kiếm theo cột & giá trị
       public async Task<List<DonHang>> SearchAsync(string column, string value)
        {
            // Chỉ cho phép tìm trong các cột an toàn
            var allowedColumns = new HashSet<string>
            {
                "MaDH", "MaNV", "NgayLap", "GioLap", "MaBuzzer", "PhuongThucThanhToan"
            };

            if (!allowedColumns.Contains(column))
                throw new ArgumentException($"Cột '{column}' không hợp lệ để tìm kiếm.");

            var list = new List<DonHang>();
            using var conn = await _db.GetConnectionAsync();

            string query;
            MySqlCommand cmd;

            // Tìm theo ngày
            //if (column == "NgayLap")
            //{
            //    query = $"SELECT * FROM donhang WHERE DATE_FORMAT(NgayLap, '%Y-%m-%d') LIKE @value";
            //    cmd = new MySqlCommand(query, conn);
            //    cmd.Parameters.AddWithValue("@value", $"%{value}%");
            //}
            //// Tìm theo giờ
            //else if (column == "GioLap")
            //{
            //    query = $"SELECT * FROM donhang WHERE TIME_FORMAT(GioLap, '%H:%i:%s') LIKE @value";
            //    cmd = new MySqlCommand(query, conn);
            //    cmd.Parameters.AddWithValue("@value", $"%{value}%");
            //}
            // Tìm theo số nguyên (MaDH, MaNV, MaBuzzer, PhuongThucThanhToan)
            if (column == "PhuongThucThanhToan")
            {
                // Chuyển giá trị từ chữ sang số
                if (value.ToLower() == "tm") value = "0";
                else if (value.ToLower() == "ck") value = "1";

                query = $"SELECT * FROM donhang WHERE {column} = @value";
                cmd = new MySqlCommand(query, conn);
                if (int.TryParse(value, out int intValue))
                    cmd.Parameters.AddWithValue("@value", intValue);
                else
                    cmd.Parameters.AddWithValue("@value", -1); // giá trị sai, không khớp
            }

            else if (column == "MaNV" || column == "MaBuzzer"  || column == "MaDH")
            {
                query = $"SELECT * FROM donhang WHERE {column} = @value";
                cmd = new MySqlCommand(query, conn);

                if (int.TryParse(value, out int intValue))
                    cmd.Parameters.AddWithValue("@value", intValue);
                else
                    cmd.Parameters.AddWithValue("@value", -99999); // nếu nhập sai kiểu dữ liệu
            }
            // Nếu tìm theo chuỗi khác (nếu sau này thêm)
            else
            {
                query = $"SELECT * FROM donhang WHERE {column} LIKE @value";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", $"%{value}%");
            }

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new DonHang
                {
                    MaDH = reader.GetInt32(reader.GetOrdinal("MaDH")),
                    MaNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? null : reader.GetInt32(reader.GetOrdinal("MaNV")),
                    NgayLap = reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    GioLap = reader.IsDBNull(reader.GetOrdinal("GioLap"))
                        ? (TimeSpan?)null
                        : (TimeSpan?)reader.GetValue(reader.GetOrdinal("GioLap")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai")),
                    MaBuzzer = reader.IsDBNull(reader.GetOrdinal("MaBuzzer")) ? null : reader.GetInt32(reader.GetOrdinal("MaBuzzer")),
                    PhuongThucThanhToan = reader.IsDBNull(reader.GetOrdinal("PhuongThucThanhToan")) ? null : reader.GetInt32(reader.GetOrdinal("PhuongThucThanhToan")),
                    TongGia = reader.GetDecimal(reader.GetOrdinal("TongGia"))
                });
            }

            return list;
        }



    }
}
