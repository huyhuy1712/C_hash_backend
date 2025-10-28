namespace MilkTea.Server.Models
{
    public class ChiTietPhieuNhap
    {
        // Mã chi tiết phiếu nhập (Primary Key, tự tăng)
        public int MaChiTietPhieuNhap { get; set; }

        // Mã phiếu nhập (FK tới bảng PhieuNhap)
        public int MaPN { get; set; }

        // Số lượng nguyên liệu nhập
        public int SoLuong { get; set; }

        // Mã nguyên liệu (FK tới bảng NguyenLieu)
        public int MaNguyenLieu { get; set; }

        // Đơn giá nhập (giá 1 đơn vị nguyên liệu)
        public decimal DonGiaNhap { get; set; }

        // Tổng giá trị (SoLuong * DonGiaNhap)
        public decimal TongGia { get; set; }
    }
}
