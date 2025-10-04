using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class ChiTietPhieuNhapRepository
    {
        private readonly DbConnection _db;

        public ChiTietPhieuNhapRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ chi tiết phiếu nhập
        public async Task<List<ChiTietPhieuNhap>> GetAllAsync()
        {
            var list = new List<ChiTietPhieuNhap>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM chitietphieunhap", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaCTPN = reader.GetOrdinal("MaChiTietPhieuNhap");
            int idxMaPN = reader.GetOrdinal("MaPN");
            int idxMaNL = reader.GetOrdinal("MaNguyenLieu");
            int idxSoLuong = reader.GetOrdinal("SoLuong");
            int idxDonGia = reader.GetOrdinal("DonGia");
            int idxTongGia = reader.GetOrdinal("TongGia");

            while (await reader.ReadAsync())
            {
                list.Add(new ChiTietPhieuNhap
                {
                    MaChiTietPhieuNhap = reader.GetInt32(idxMaCTPN),
                    MaPN = reader.GetInt32(idxMaPN),
                    MaNguyenLieu = reader.GetInt32(idxMaNL),
                    SoLuong = reader.GetInt32(idxSoLuong),
                    DonGiaNhap = reader.GetDecimal(idxDonGia),
                    TongGia = reader.GetDecimal(idxTongGia)
                });
            }

            return list;
        }

        // 2. Thêm chi tiết phiếu nhập
        public async Task<bool> AddAsync(ChiTietPhieuNhap ctpn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO chitietphieunhap (MaPN, MaNguyenLieu, SoLuong, DonGiaNhap, TongGia)
                          VALUES (@MaPN, @MaNguyenLieu, @SoLuong, @DonGia, @TongGia)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaPN", ctpn.MaPN);
            cmd.Parameters.AddWithValue("@MaNguyenLieu", ctpn.MaNguyenLieu);
            cmd.Parameters.AddWithValue("@SoLuong", ctpn.SoLuong);
            cmd.Parameters.AddWithValue("@DonGia", ctpn.DonGiaNhap);
            cmd.Parameters.AddWithValue("@TongGia", ctpn.TongGia);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 3. Sửa thông tin chi tiết phiếu nhập
        public async Task<bool> UpdateAsync(ChiTietPhieuNhap ctpn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE chitietphieunhap 
                          SET SoLuong = @SoLuong, DonGiaNhap = @DonGia, TongGia = @TongGia 
                          WHERE MaChiTietPhieuNhap = @MaChiTietPhieuNhap";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SoLuong", ctpn.SoLuong);
            cmd.Parameters.AddWithValue("@DonGia", ctpn.DonGiaNhap);
            cmd.Parameters.AddWithValue("@TongGia", ctpn.TongGia);
            cmd.Parameters.AddWithValue("@MaChiTietPhieuNhap", ctpn.MaChiTietPhieuNhap);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 4. Xóa 1 chi tiết phiếu nhập (theo mã chi tiết)
        public async Task<bool> DeleteAsync(int maChiTietPhieuNhap)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM chitietphieunhap WHERE MaChiTietPhieuNhap = @MaChiTietPhieuNhap";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaChiTietPhieuNhap", maChiTietPhieuNhap);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  5. Xóa toàn bộ chi tiết theo mã phiếu nhập (MaPN)
        public async Task<bool> DeleteByMaPNAsync(int maPN)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM chitietphieunhap WHERE MaPN = @MaPN";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaPN", maPN);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
