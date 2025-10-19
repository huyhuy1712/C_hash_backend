using MilkTea.Server.Data;
using MilkTea.Server.Models;
using MySql.Data.MySqlClient;

namespace MilkTea.Server.Repositories
{
    public class CTKhuyenMaiRepository
    {
        private readonly DbConnection _db;

        public CTKhuyenMaiRepository(DbConnection db)
        {
            _db = db;
        }

        // Lấy toàn bộ
        public async Task<List<CTKhuyenMai>> GetAllAsync()
        {
            var list = new List<CTKhuyenMai>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM ctkhuyenmai", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new CTKhuyenMai
                {
                    MaCTKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaCTKhuyenMai")),
                    TenCTKhuyenMai = reader.GetString(reader.GetOrdinal("TenCTKhuyenMai")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa")),
                    NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    PhanTramKhuyenMai = reader.GetInt32(reader.GetOrdinal("PhanTramKhuyenMai")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
                });
            }

            return list;
        }

        // Thêm
        public async Task<bool> AddAsync(CTKhuyenMai km)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand(
                @"INSERT INTO ctkhuyenmai (TenCTKhuyenMai, MoTa, NgayBatDau, NgayKetThuc, PhanTramKhuyenMai, TrangThai)
                  VALUES (@Ten, @MoTa, @NgayBatDau, @NgayKetThuc, @PhanTram, @TrangThai)", conn);
            cmd.Parameters.AddWithValue("@Ten", km.TenCTKhuyenMai);
            cmd.Parameters.AddWithValue("@MoTa", km.MoTa ?? "");
            cmd.Parameters.AddWithValue("@NgayBatDau", km.NgayBatDau);
            cmd.Parameters.AddWithValue("@NgayKetThuc", km.NgayKetThuc);
            cmd.Parameters.AddWithValue("@PhanTram", km.PhanTramKhuyenMai);
            cmd.Parameters.AddWithValue("@TrangThai", km.TrangThai);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // Sửa
        public async Task<bool> UpdateAsync(CTKhuyenMai km)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand(
                @"UPDATE ctkhuyenmai SET TenCTKhuyenMai=@Ten, MoTa=@MoTa, NgayBatDau=@NgayBatDau,
                  NgayKetThuc=@NgayKetThuc, PhanTramKhuyenMai=@PhanTram, TrangThai=@TrangThai
                  WHERE MaCTKhuyenMai=@Ma", conn);
            cmd.Parameters.AddWithValue("@Ten", km.TenCTKhuyenMai);
            cmd.Parameters.AddWithValue("@MoTa", km.MoTa ?? "");
            cmd.Parameters.AddWithValue("@NgayBatDau", km.NgayBatDau);
            cmd.Parameters.AddWithValue("@NgayKetThuc", km.NgayKetThuc);
            cmd.Parameters.AddWithValue("@PhanTram", km.PhanTramKhuyenMai);
            cmd.Parameters.AddWithValue("@TrangThai", km.TrangThai);
            cmd.Parameters.AddWithValue("@Ma", km.MaCTKhuyenMai);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // Xóa
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("DELETE FROM ctkhuyenmai WHERE MaCTKhuyenMai=@Ma", conn);
            cmd.Parameters.AddWithValue("@Ma", id);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // Tìm kiếm linh hoạt theo cột và giá trị
        public async Task<List<CTKhuyenMai>> SearchAsync(string column, string value)
        {
            var validColumns = new HashSet<string>
            {
                "TenCTKhuyenMai", "MoTa", "PhanTramKhuyenMai", "TrangThai"
            };
            if (!validColumns.Contains(column))
                throw new ArgumentException("Cột tìm kiếm không hợp lệ.");

            var list = new List<CTKhuyenMai>();
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand($"SELECT * FROM ctkhuyenmai WHERE {column} LIKE @Value", conn);
            cmd.Parameters.AddWithValue("@Value", $"%{value}%");
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new CTKhuyenMai
                {
                    MaCTKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaCTKhuyenMai")),
                    TenCTKhuyenMai = reader.GetString(reader.GetOrdinal("TenCTKhuyenMai")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa")),
                    NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    PhanTramKhuyenMai = reader.GetInt32(reader.GetOrdinal("PhanTramKhuyenMai")),
                    TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai"))
                });
            }

            return list;
        }


        public async Task<CTKhuyenMai?> GetByIdAsync(int id)
        {
            using var conn = await _db.GetConnectionAsync();
            var cmd = new MySqlCommand("SELECT * FROM ctkhuyenmai WHERE MaCTKhuyenMai = @Ma", conn);
            cmd.Parameters.AddWithValue("@Ma", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CTKhuyenMai
                {
                    MaCTKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaCTKhuyenMai")),
                    TenCTKhuyenMai = reader.GetString(reader.GetOrdinal("TenCTKhuyenMai")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? null : reader.GetString(reader.GetOrdinal("MoTa")),
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
