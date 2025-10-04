namespace MilkTea.Server.Models
{
    public class ChiTietDonHang
    {
        // Mã chi tiết đơn hàng (Primary Key, tự tăng)
        public int MaCTDH { get; set; }

        // Mã đơn hàng (FK tới DonHang)
        public int MaDH { get; set; }

        // Mã sản phẩm (FK tới SanPham)
        public int MaSP { get; set; }

        // Mã size (FK tới bảng Size nếu có)
        public int MaSize { get; set; }

        // Số lượng (mặc định = 1)
        public int SoLuong { get; set; } = 1;

        // Giá vốn (giá nhập hoặc giá gốc)
        public decimal GiaVon { get; set; }

        // Tổng giá (SoLuong * Giá bán)
        public decimal TongGia { get; set; }
    }
}
