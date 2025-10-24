using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class QuyenChucNangRepository
    {
        private readonly DbConnection _db;

        public QuyenChucNangRepository(DbConnection db)
        {
            _db = db;
        }

        //  1. Lấy toàn bộ quyền - chức năng
        public async Task<List<Quyen_ChucNang>> GetAllAsync()
        {
            var list = new List<Quyen_ChucNang>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM quyen_chucnang", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxMaQuyen = reader.GetOrdinal("MaQuyen");
            int idxMaChucNang = reader.GetOrdinal("MaChucNang");

            while (await reader.ReadAsync())
            {
                list.Add(new Quyen_ChucNang
                {
                    MaQuyen = reader.GetInt32(idxMaQuyen),
                    MaChucNang = reader.GetInt32(idxMaChucNang)
                });
            }

            return list;
        }

        // 2. Thêm mới
        public async Task<bool> AddAsync(Quyen_ChucNang qcn)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"INSERT INTO quyen_chucnang (MaQuyen, MaChucNang)
                          VALUES (@MaQuyen, @MaChucNang)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaQuyen", qcn.MaQuyen);
            cmd.Parameters.AddWithValue("@MaChucNang", qcn.MaChucNang);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  3. Cập nhật (sửa mã chức năng của 1 quyền)
        public async Task<bool> UpdateAsync(int maQuyen, int oldMaChucNang, int newMaChucNang)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = @"UPDATE quyen_chucnang 
                          SET MaChucNang = @NewMaChucNang
                          WHERE MaQuyen = @MaQuyen AND MaChucNang = @OldMaChucNang";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);
            cmd.Parameters.AddWithValue("@OldMaChucNang", oldMaChucNang);
            cmd.Parameters.AddWithValue("@NewMaChucNang", newMaChucNang);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        //  4. Xóa
        public async Task<bool> DeleteAsync(int maQuyen, int maChucNang)
        {
            using var conn = await _db.GetConnectionAsync();
            var query = "DELETE FROM quyen_chucnang WHERE MaQuyen = @MaQuyen AND MaChucNang = @MaChucNang";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);
            cmd.Parameters.AddWithValue("@MaChucNang", maChucNang);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
