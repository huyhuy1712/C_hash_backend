using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class ChiTietDonHangRepository
    {
        private readonly DbConnection _db;

        public ChiTietDonHangRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ chi tiết đơn hàng
        public async Task<List<ChiTietDonHang>> GetAllAsync()
        {
            var list = new List<ChiTietDonHang>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM chitietdonhang", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaCTDH = reader.GetOrdinal("MaCTDH");
            int idxMaDH = reader.GetOrdinal("MaDH");
            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMaSize = reader.GetOrdinal("MaSize");
            int idxSoLuong = reader.GetOrdinal("SoLuong");
            int idxGiaVon = reader.GetOrdinal("GiaVon");
            int idxTongGia = reader.GetOrdinal("TongGia");

            while (await reader.ReadAsync())
            {
                list.Add(new ChiTietDonHang
                {
                    MaCTDH = reader.GetInt32(idxMaCTDH),
                    MaDH = reader.GetInt32(idxMaDH),
                    MaSP = reader.GetInt32(idxMaSP),
                    MaSize = reader.GetInt32(idxMaSize),
                    SoLuong = reader.GetInt32(idxSoLuong),
                    GiaVon = reader.GetDecimal(idxGiaVon),
                    TongGia = reader.GetDecimal(idxTongGia)
                });
            }

            return list;
        }

        // 2. Thêm mới chi tiết đơn hàng
        public async Task<bool> AddAsync(ChiTietDonHang ct)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO chitietdonhang (MaDH, MaSP, MaSize, SoLuong, GiaVon, TongGia)
                          VALUES (@MaDH, @MaSP, @MaSize, @SoLuong, @GiaVon, @TongGia)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaDH", ct.MaDH);
            cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
            cmd.Parameters.AddWithValue("@MaSize", ct.MaSize);
            cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
            cmd.Parameters.AddWithValue("@GiaVon", ct.GiaVon);
            cmd.Parameters.AddWithValue("@TongGia", ct.TongGia);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Sửa chi tiết đơn hàng
        public async Task<bool> UpdateAsync(ChiTietDonHang ct)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE chitietdonhang 
                          SET MaSP = @MaSP, MaSize = @MaSize, SoLuong = @SoLuong, GiaVon = @GiaVon, TongGia = @TongGia
                          WHERE MaCTDH = @MaCTDH";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
            cmd.Parameters.AddWithValue("@MaSize", ct.MaSize);
            cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
            cmd.Parameters.AddWithValue("@GiaVon", ct.GiaVon);
            cmd.Parameters.AddWithValue("@TongGia", ct.TongGia);
            cmd.Parameters.AddWithValue("@MaCTDH", ct.MaCTDH);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa chi tiết đơn hàng
        public async Task<bool> DeleteAsync(int maCTDH)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM chitietdonhang WHERE MaCTDH = @MaCTDH";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCTDH", maCTDH);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  5. Xóa toàn bộ chi tiết đơn hàng theo mã đơn hàng (MaDH)
            public async Task<bool> DeleteByMaDHAsync(int maDH)
            {
                using var conn = await _db.GetConnectionAsync();
                var query = "DELETE FROM chitietdonhang WHERE MaDH = @MaDH";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", maDH);

                var rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0; // true nếu có ít nhất 1 dòng bị xóa
            }

    }
}
