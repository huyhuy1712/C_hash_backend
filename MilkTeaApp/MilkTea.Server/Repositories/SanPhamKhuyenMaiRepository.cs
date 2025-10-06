using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class SanPhamKhuyenMaiRepository
    {
        private readonly DbConnection _db;

        public SanPhamKhuyenMaiRepository(DbConnection db)
        {
            _db = db;
        }

        // 1. Lấy toàn bộ danh sách
        public async Task<List<SanPhamKhuyenMai>> GetAllAsync()
        {
            var list = new List<SanPhamKhuyenMai>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM sanpham_khuyenmai", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaSP = reader.GetOrdinal("MaSP");
            int idxMaCTKM = reader.GetOrdinal("MaCTKhuyenMai");

            while (await reader.ReadAsync())
            {
                list.Add(new SanPhamKhuyenMai
                {
                    MaSP = reader.GetInt32(idxMaSP),
                    MaCTKhuyenMai = reader.GetInt32(idxMaCTKM)
                });
            }

            return list;
        }

        // 2. Thêm mới
        public async Task<bool> AddAsync(SanPhamKhuyenMai spkm)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "INSERT INTO sanpham_khuyenmai (MaSP, MaCTKhuyenMai) VALUES (@MaSP, @MaCTKhuyenMai)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", spkm.MaSP);
            cmd.Parameters.AddWithValue("@MaCTKhuyenMai", spkm.MaCTKhuyenMai);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  3. Cập nhật (nếu cần thay đổi mã khuyến mãi của sản phẩm)
        public async Task<bool> UpdateAsync(int maSP, int maCTKhuyenMaiMoi)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE sanpham_khuyenmai 
                          SET MaCTKhuyenMai = @MaCTKhuyenMaiMoi 
                          WHERE MaSP = @MaSP";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCTKhuyenMaiMoi", maCTKhuyenMaiMoi);
            cmd.Parameters.AddWithValue("@MaSP", maSP);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  4. Xóa theo mã sản phẩm
        public async Task<bool> DeleteAsync(int maSP)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM sanpham_khuyenmai WHERE MaSP = @MaSP";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", maSP);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // 5. Xóa theo mã khuyến mãi
        public async Task<bool> DeleteByCTKMAsync(int maCTKM)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM sanpham_khuyenmai WHERE MaCTKhuyenMai = @MaCTKhuyenMai";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaCTKhuyenMai", maCTKM);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

       // 6. Tìm kiếm chương trình khuyến mãi theo MaSP
        public async Task<CTKhuyenMai?> GetByMaSPAsync(int maSP)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"
        SELECT km.MaCTKhuyenMai, km.TenCTKhuyenMai, km.MoTa,
        km.NgayBatDau, km.NgayKetThuc, km.PhanTramKhuyenMai, km.TrangThai
        FROM sanpham_khuyenmai spkm
        JOIN ctkhuyenmai km ON spkm.MaCTKhuyenMai = km.MaCTKhuyenMai
        WHERE spkm.MaSP = @MaSP
        LIMIT 1;";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", maSP);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CTKhuyenMai
                {
                    MaCTKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaCTKhuyenMai")),
                    TenCTKhuyenMai = reader.GetString(reader.GetOrdinal("TenCTKhuyenMai")),
                    MoTa = reader.GetString(reader.GetOrdinal("MoTa")),
                    NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    PhanTramKhuyenMai = reader.GetInt32(reader.GetOrdinal("PhanTramKhuyenMai")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
                };
            }
            return null;
        }

    }
}
