namespace MilkTea.Server.Models
{
    public class NhaCungCap
    {
        // Mã nhân viên (Primary Key, tự tăng)
        public int MaNCC { get; set; }

        // Tên nhà cung cấp
        public string TenNCC { get; set; } = string.Empty;

        // Số điện thoại
        public string SDT { get; set; } = string.Empty;

        // Địa chỉ
        public string DiaChi { get; set; } = string.Empty;

        // Trạng thái (1: Hoạt động, 0: Không hoạt động)
        public int TrangThai { get; set; }
    }
}