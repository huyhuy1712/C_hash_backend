namespace MilkTea.Server.Models
{
    public class ChiPhi
    {
        // Mã chi phí (Primary Key, tự tăng)
        public int MaCP { get; set; }

        // Ngày chi phí (1–31)
        public int Ngay { get; set; }

        // Tháng chi phí (1–12)
        public int Thang { get; set; }

        // Năm chi phí (ví dụ: 2025)
        public int Nam { get; set; }

        // Mã sản phẩm (FK tới SanPham) - có thể null
        public int? MaSP { get; set; }

        // Mã loại sản phẩm (FK tới Loai) - có thể null
        public int? MaLoai { get; set; }

        // Mã khuyến mãi (FK tới CTKhuyenMai) - có thể null
        public int? MaKM { get; set; }

        // Tổng chi phí cho sản phẩm (chi phí trực tiếp sản xuất/sp)
        public decimal TongChiPhiSP { get; set; }

        // Tổng chi phí nguyên liệu (chi phí nhập nguyên liệu)
        public decimal TongChiPhiNL { get; set; }
    }
}
