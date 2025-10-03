using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MilkTea.Server.Models;
using MilkTea.Server.Data;

namespace MilkTea.Server.Repositories
{
    public class SanPhamRepository
    {
        private readonly DbConnection _db;

        public SanPhamRepository(DbConnection db)
        {
            _db = db;
        }

        // Lấy tất cả sản phẩm
            public async Task<List<SanPham>> GetAllAsync()
            {
                var list = new List<SanPham>();

                using var conn = await _db.GetConnectionAsync();
                using var cmd  = new MySqlCommand(
                    "SELECT MaSP, TenSP, Gia, Anh, SLDuKien, TrangThai, MaLoai FROM sanpham",
                    conn);
                using var reader = await cmd.ExecuteReaderAsync();

                // Lấy ordinal 1 lần, dùng lại trong vòng lặp
                int ordMaSP      = reader.GetOrdinal("MaSP");
                int ordTenSP     = reader.GetOrdinal("TenSP");
                int ordGia       = reader.GetOrdinal("Gia");
                int ordAnh       = reader.GetOrdinal("Anh");
                int ordSLDuKien  = reader.GetOrdinal("SLDuKien");
                int ordTrangThai = reader.GetOrdinal("TrangThai");
                int ordMaLoai    = reader.GetOrdinal("MaLoai");

                while (await reader.ReadAsync())
                {
                    var sp = new SanPham
                    {
                        MaSP      = reader.GetInt32(ordMaSP),
                        TenSP     = reader.GetString(ordTenSP),
                        Gia       = reader.IsDBNull(ordGia) ? 0m : reader.GetDecimal(ordGia),
                        Anh       = reader.IsDBNull(ordAnh) ? "" : reader.GetString(ordAnh),
                        SLDuKien  = reader.IsDBNull(ordSLDuKien) ? 0 : reader.GetInt32(ordSLDuKien),
                        TrangThai = reader.GetInt32(ordTrangThai),
                        MaLoai    = reader.GetInt32(ordMaLoai)
                    };
                    list.Add(sp);
                }

                return list;
            }


        // Lấy sản phẩm theo ID
        public async Task<SanPham?> GetByIdAsync(int id)
        {
            using var conn = await _db.GetConnectionAsync();
            using var cmd  = new MySqlCommand(
                "SELECT MaSP, TenSP, Gia, Anh, SLDuKien, TrangThai, MaLoai FROM sanpham WHERE MaSP=@id",
                conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            int ordMaSP      = reader.GetOrdinal("MaSP");
            int ordTenSP     = reader.GetOrdinal("TenSP");
            int ordGia       = reader.GetOrdinal("Gia");
            int ordAnh       = reader.GetOrdinal("Anh");
            int ordSLDuKien  = reader.GetOrdinal("SLDuKien");
            int ordTrangThai = reader.GetOrdinal("TrangThai");
            int ordMaLoai    = reader.GetOrdinal("MaLoai");

            return new SanPham
            {
                MaSP      = reader.GetInt32(ordMaSP),
                TenSP     = reader.GetString(ordTenSP),
                Gia       = reader.IsDBNull(ordGia) ? 0m : reader.GetDecimal(ordGia),
                Anh       = reader.IsDBNull(ordAnh) ? "" : reader.GetString(ordAnh),
                SLDuKien  = reader.IsDBNull(ordSLDuKien) ? 0 : reader.GetInt32(ordSLDuKien),
                TrangThai = reader.GetInt32(ordTrangThai),
                MaLoai    = reader.GetInt32(ordMaLoai)
            };
        }


        // Thêm sản phẩm
        public async Task<int> AddAsync(SanPham sp)
        {
            using var conn = await _db.GetConnectionAsync();
            using var cmd = new MySqlCommand(
                @"INSERT INTO sanpham (TenSP, Gia, Anh, SLDuKien, TrangThai, MaLoai) 
                  VALUES (@TenSP, @Gia, @Anh, @SLDuKien, @TrangThai, @MaLoai)", conn);

            cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
            cmd.Parameters.AddWithValue("@Gia", sp.Gia);
            cmd.Parameters.AddWithValue("@Anh", sp.Anh ?? "");
            cmd.Parameters.AddWithValue("@SLDuKien", sp.SLDuKien);
            cmd.Parameters.AddWithValue("@TrangThai", sp.TrangThai);
            cmd.Parameters.AddWithValue("@MaLoai", sp.MaLoai);

            return await cmd.ExecuteNonQueryAsync();
        }

        // Cập nhật sản phẩm
        public async Task<int> UpdateAsync(SanPham sp)
        {
            using var conn = await _db.GetConnectionAsync();
            using var cmd = new MySqlCommand(
                @"UPDATE sanpham SET TenSP=@TenSP, Gia=@Gia, Anh=@Anh, 
                                     SLDuKien=@SLDuKien, TrangThai=@TrangThai, MaLoai=@MaLoai 
                  WHERE MaSP=@MaSP", conn);

            cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
            cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
            cmd.Parameters.AddWithValue("@Gia", sp.Gia);
            cmd.Parameters.AddWithValue("@Anh", sp.Anh ?? "");
            cmd.Parameters.AddWithValue("@SLDuKien", sp.SLDuKien);
            cmd.Parameters.AddWithValue("@TrangThai", sp.TrangThai);
            cmd.Parameters.AddWithValue("@MaLoai", sp.MaLoai);

            return await cmd.ExecuteNonQueryAsync();
        }

        // Xóa sản phẩm
        public async Task<int> DeleteAsync(int id)
        {
            using var conn = await _db.GetConnectionAsync();
            using var cmd = new MySqlCommand("DELETE FROM sanpham WHERE MaSP=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
