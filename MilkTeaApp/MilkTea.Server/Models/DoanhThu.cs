namespace MilkTea.Server.Models
{
    public class DoanhThu
    {
        // Mã doanh thu (Primary Key, tự tăng)
        public int MaDT { get; set; }

        // Ngày bán (1–31)
        public int Ngay { get; set; }

        // Tháng bán (1–12)
        public int Thang { get; set; }

        // Năm bán (ví dụ: 2025)
        public int Nam { get; set; }

        // Giờ bán (map từ SQL TIME sang TimeSpan trong C#)
        public TimeSpan Gio { get; set; }

        // Số lượng sản phẩm bán ra
        public int? SLBan { get; set; }

        // Mã sản phẩm (FK tới SanPham)
        public int? MaSP { get; set; }

        // Mã loại (FK tới Loai)
        public int? MaLoai { get; set; }

        // Mã khuyến mãi (FK tới CTKhuyenMai)
        public int? MaKM { get; set; }

        // Mã size (FK tới Size nếu có)
        public int? MaSize { get; set; }

        // Tổng doanh thu
        public decimal TongDoanhThu { get; set; }
    }
}
