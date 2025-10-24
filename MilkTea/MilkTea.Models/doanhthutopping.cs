namespace MilkTea.Server.Models
{
    public class DoanhThuTopping
    {
        // Mã doanh thu topping (khóa chính, tự tăng)
        public int MaDTTP { get; set; }

        // Ngày - Tháng - Năm bán
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }

        // Giờ bán (map từ SQL TIME -> TimeSpan trong C#)
        public TimeSpan Gio { get; set; }

        // Số lượng bàn hoặc số lượng bán
        public int SLBan { get; set; }

        // Mã nguyên liệu (FK tới bảng nguyenlieu)
        public int MaNL { get; set; }

        // Tổng chi phí (có thể int hoặc decimal, tùy database)
        public decimal TongChiPhi { get; set; }

        // Tổng doanh thu
        public decimal TongDoanhThu { get; set; }
    }
}
