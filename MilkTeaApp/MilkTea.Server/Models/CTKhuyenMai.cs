namespace MilkTea.Server.Models
{
    public class CTKhuyenMai
    {
        // Mã chương trình khuyến mãi (Primary Key, tự tăng)
        public int MaCTKhuyenMai { get; set; }

        // Tên chương trình khuyến mãi
        public string TenCTKhuyenMai { get; set; } = string.Empty;

        // Mô tả chi tiết khuyến mãi
        public string? MoTa { get; set; }

        // Ngày bắt đầu
        public DateTime? NgayBatDau { get; set; }

        // Ngày kết thúc
        public DateTime? NgayKetThuc { get; set; }

        // Phần trăm khuyến mãi (ví dụ: 10, 20, 50…)
        public int PhanTramKhuyenMai { get; set; }

        // Trạng thái (1 = đang áp dụng, 0 = ngừng, …)
        public int TrangThai { get; set; }
    }
}
