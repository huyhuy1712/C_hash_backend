namespace MilkTea.Server.Models
{
    public class NhanVien
    {
        // Mã nhân viên (Primary Key, tự tăng)
        public int MaNV { get; set; }

        // Tên nhân viên
        public string TenNV { get; set; } = string.Empty;

        // Số điện thoại
        public string SDT { get; set; } = string.Empty;

        // Ngày làm việc (mặc định current_date trong DB)
        public DateTime NgayLam { get; set; }

        // Mã tài khoản (Foreign Key trỏ sang bảng TaiKhoan)
        public int? MaTK { get; set; }
    }
}
