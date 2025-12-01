namespace MilkTea.Server.Models
{
    public class SanPhamKhuyenMai
    {
        // Mã sản phẩm (FK tới SanPham)
        public int MaSP { get; set; }

        // Mã chương trình khuyến mãi (FK tới CTKhuyenMai)
        public int MaCTKhuyenMai { get; set; }

        public DateTime? NgayBatDau { get; set; }

        // Ngày kết thúc
        public DateTime? NgayKetThuc { get; set; } 
    }
}
