namespace MilkTea.Server.Models
{
    public class CongThuc
    {
        // Mã công thức (Primary Key, tự tăng)
        public int MaCT { get; set; }

        // Tên công thức
        public string Ten { get; set; } = string.Empty;

        // Mã sản phẩm (FK tới bảng SanPham)
        public int MaSP { get; set; }

        // Mô tả chi tiết công thức
        public string? MoTa { get; set; }
    }
}
