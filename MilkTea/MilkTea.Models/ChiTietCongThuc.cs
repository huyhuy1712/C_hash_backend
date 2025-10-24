namespace MilkTea.Server.Models
{
    public class ChiTietCongThuc
    {
        // Mã công thức (FK tới bảng CongThuc)
        public int MaCT { get; set; }

        // Mã nguyên liệu (FK tới bảng NguyenLieu)
        public int MaNL { get; set; }

        // Số lượng nguyên liệu cần dùng trong công thức
        public int SL { get; set; }
    }
}
