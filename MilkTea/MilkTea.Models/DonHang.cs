namespace MilkTea.Server.Models
{
    public class DonHang
    {
        // Mã đơn hàng (Primary Key, tự tăng)
        public int MaDH { get; set; }

        // Mã nhân viên (FK tới NhanVien) - có thể null
        public int? MaNV { get; set; }

        // Ngày lập đơn
        public DateTime? NgayLap { get; set; }

        // Giờ lập (DB lưu kiểu TIME, trong C# có thể map sang TimeSpan)
        public TimeSpan? GioLap { get; set; }

        // Trạng thái (1 = mới, 0 = hủy, 2 = hoàn thành, …)
        public int TrangThai { get; set; } = 1;

        // Mã buzzer (FK tới Buzzer)
        public int? MaBuzzer { get; set; }

        // Phương thức thanh toán (ví dụ: 1 = tiền mặt, 2 = thẻ, 3 = ví điện tử)
        public int? PhuongThucThanhToan { get; set; }

        // Tổng giá trị đơn hàng
        public decimal TongGia { get; set; }
    }
}
