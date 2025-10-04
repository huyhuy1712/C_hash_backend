namespace MilkTea.Server.Models
{
    public class PhieuNhap
    {
        // Mã phiếu nhập (Primary Key, tự tăng)
        public int MaPN { get; set; }

        // Ngày nhập hàng
        public DateTime? NgayNhap { get; set; }

        // Tổng số lượng hàng nhập
        public int SoLuong { get; set; }

        // Mã nhân viên (FK tới bảng NhanVien)
        public int? MaNV { get; set; }

        // Tổng tiền nhập
        public decimal TongTien { get; set; }
    }
}
