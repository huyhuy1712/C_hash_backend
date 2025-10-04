namespace MilkTea.Server.Models
{
    public class NguyenLieu
    {
        // Mã nguyên liệu (Primary Key, tự tăng)
        public int MaNL { get; set; }

        // Số lượng tồn kho của nguyên liệu
        public int SoLuong { get; set; }

        // Tên nguyên liệu
        public string Ten { get; set; } = string.Empty;

        // Giá bán nguyên liệu (có thể dùng khi xuất bán hoặc định giá sản phẩm)
        public decimal GiaBan { get; set; }
    }
}
