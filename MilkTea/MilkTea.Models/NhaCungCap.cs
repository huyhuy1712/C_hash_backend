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
    }
}