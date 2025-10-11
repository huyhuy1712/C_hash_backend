CREATE TABLE `SanPham` (
  `MaSP` int PRIMARY KEY AUTO_INCREMENT,
  `TenSP` varchar(100) NOT NULL,
  `Gia` decimal(12,2) NOT NULL,
  `Anh` varchar(50),
  `SLDuKien` int,
  `TrangThai` int,
  `MaLoai` int
);

CREATE TABLE `Size` (
  `MaSize` int PRIMARY KEY AUTO_INCREMENT,
  `TenSize` varchar(50) NOT NULL,
  `PhuThu` int DEFAULT 0
);

CREATE TABLE `Loai` (
  `MaLoai` int PRIMARY KEY AUTO_INCREMENT,
  `TenLoai` varchar(50) NOT NULL,
  `MoTa` varchar(50)
);

CREATE TABLE `TaiKhoan` (
  `MaTK` int PRIMARY KEY AUTO_INCREMENT,
  `TenTaiKhoan` varchar(50) UNIQUE NOT NULL,
  `anh` varchar(50),
  `MatKhau` varchar(50),
  `TrangThai` int,
  `MaQuyen` int
);

CREATE TABLE `Quyen` (
  `MaQuyen` int PRIMARY KEY AUTO_INCREMENT,
  `TenQuyen` varchar(100) UNIQUE NOT NULL,
  `Mota` text
);

CREATE TABLE `ChucNang` (
  `MaChucNang` int PRIMARY KEY AUTO_INCREMENT,
  `TenChucNang` varchar(100) UNIQUE NOT NULL,
  `MoTa` text
);

CREATE TABLE `Quyen_ChucNang` (
  `MaQuyen` int,
  `MaChucNang` int
);

CREATE TABLE `NhanVien` (
  `MaNV` int PRIMARY KEY AUTO_INCREMENT,
  `TenNV` varchar(50) NOT NULL,
  `SDT` varchar(50) NOT NULL,
  `NgayLam` date DEFAULT (current_date),
  `MaTK` int
);

CREATE TABLE `Buzzer` (
  `MaBuzzer` int PRIMARY KEY AUTO_INCREMENT,
  `SoHieu` varchar(20) UNIQUE NOT NULL,
  `TrangThai` int DEFAULT 1
);

CREATE TABLE `DonHang` (
  `MaDH` int PRIMARY KEY AUTO_INCREMENT,
  `MaNV` int,
  `NgayLap` datetime,
  `GioLap` time,
  `TrangThai` int DEFAULT '1',
  `MaBuzzer` int,
  `PhuongThucThanhToan` int,
  `TongGia` decimal(12,2)
);

CREATE TABLE `ChiTietDonHang` (
  `MaCTDH` int PRIMARY KEY AUTO_INCREMENT,
  `MaDH` int,
  `MaSP` int,
  `MaSize` int,
  `SoLuong` int DEFAULT 1,
  `GiaVon` decimal(12,2),
  `TongGia` decimal(12,2)
);

CREATE TABLE `CTKhuyenMai` (
  `MaCTKhuyenMai` int PRIMARY KEY AUTO_INCREMENT,
  `TenCTKhuyenMai` varchar(50),
  `MoTa` text,
  `NgayBatDau` datetime,
  `NgayKetThuc` datetime,
  `PhanTramKhuyenMai` int,
  `TrangThai` int
);

CREATE TABLE `PhieuNhap` (
  `MaPN` int PRIMARY KEY AUTO_INCREMENT,
  `NgayNhap` date,
  `SoLuong` int,
  `MaNV` int,
  `TongTien` decimal(12,2)
);

CREATE TABLE `ChiTietPhieuNhap` (
  `MaChiTietPhieuNhap` int PRIMARY KEY AUTO_INCREMENT,
  `MaPN` int,
  `SoLuong` int,
  `MaNguyenLieu` int,
  `DonGiaNhap` decimal(12,2),
  `TongGia` decimal(12,2)
);

CREATE TABLE `NguyenLieu` (
  `MaNL` int PRIMARY KEY AUTO_INCREMENT,
  `SoLuong` int,
  `Ten` varchar(50),
  `GiaBan` decimal(12,2)
);

CREATE TABLE `CongThuc` (
  `MaCT` int PRIMARY KEY AUTO_INCREMENT,
  `Ten` text,
  `MaSP` int,
  `Mota` text
);

CREATE TABLE `ChiTietCongThuc` (
  `MaCT` int,
  `MaNL` int,
  `SL` int
);

CREATE TABLE `DoanhThu` (
  `MaDT` int PRIMARY KEY AUTO_INCREMENT,
  `Ngay` int NOT NULL,
  `Thang` int NOT NULL,
  `Nam` int NOT NULL,
  `Gio` time NOT NULL,
  `SLBan` int,
  `MaSP` int,
  `MaLoai` int,
  `MaKM` int,
  `MaSize` int,
  `TongChiPhi` int,
  `TongDoanhThu` decimal(12,2)
);

CREATE TABLE `DoanhThuTopping` (
  `MaDTTP` int PRIMARY KEY AUTO_INCREMENT,
  `Ngay` int NOT NULL,
  `Thang` int NOT NULL,
  `Nam` int NOT NULL,
  `Gio` time NOT NULL,
  `SLBan` int,
  `MaNL` int,
  `TongChiPhi` int,
  `TongDoanhThu` decimal(12,2)
);

CREATE TABLE `sanpham_khuyenmai` (
  `MaSP` int,
  `MaCTKhuyenMai` int
);

CREATE TABLE `ctdonhang_topping` (
  `MaNL` int,
  `MaCTDH` int,
  `SL` int
);

ALTER TABLE `NguyenLieu` ADD FOREIGN KEY (`MaNL`) REFERENCES `DoanhThuTopping` (`MaNL`);

ALTER TABLE `ctdonhang_topping` ADD FOREIGN KEY (`MaNL`) REFERENCES `NguyenLieu` (`MaNL`);

ALTER TABLE `ctdonhang_topping` ADD FOREIGN KEY (`MaCTDH`) REFERENCES `ChiTietDonHang` (`MaCTDH`);

ALTER TABLE `SanPham` ADD FOREIGN KEY (`MaLoai`) REFERENCES `Loai` (`MaLoai`);

ALTER TABLE `TaiKhoan` ADD FOREIGN KEY (`MaQuyen`) REFERENCES `Quyen` (`MaQuyen`);

ALTER TABLE `Quyen_ChucNang` ADD FOREIGN KEY (`MaQuyen`) REFERENCES `Quyen` (`MaQuyen`);

ALTER TABLE `Quyen_ChucNang` ADD FOREIGN KEY (`MaChucNang`) REFERENCES `ChucNang` (`MaChucNang`);

ALTER TABLE `NhanVien` ADD FOREIGN KEY (`MaTK`) REFERENCES `TaiKhoan` (`MaTK`);

ALTER TABLE `DonHang` ADD FOREIGN KEY (`MaNV`) REFERENCES `NhanVien` (`MaNV`);

ALTER TABLE `DonHang` ADD FOREIGN KEY (`MaBuzzer`) REFERENCES `Buzzer` (`MaBuzzer`);

ALTER TABLE `ChiTietDonHang` ADD FOREIGN KEY (`MaDH`) REFERENCES `DonHang` (`MaDH`);

ALTER TABLE `ChiTietDonHang` ADD FOREIGN KEY (`MaSP`) REFERENCES `SanPham` (`MaSP`);

ALTER TABLE `ChiTietDonHang` ADD FOREIGN KEY (`MaSize`) REFERENCES `Size` (`MaSize`);

ALTER TABLE `PhieuNhap` ADD FOREIGN KEY (`MaNV`) REFERENCES `NhanVien` (`MaNV`);

ALTER TABLE `ChiTietPhieuNhap` ADD FOREIGN KEY (`MaPN`) REFERENCES `PhieuNhap` (`MaPN`);

ALTER TABLE `ChiTietPhieuNhap` ADD FOREIGN KEY (`MaNguyenLieu`) REFERENCES `NguyenLieu` (`MaNL`);

ALTER TABLE `CongThuc` ADD FOREIGN KEY (`MaSP`) REFERENCES `SanPham` (`MaSP`);

ALTER TABLE `ChiTietCongThuc` ADD FOREIGN KEY (`MaCT`) REFERENCES `CongThuc` (`MaCT`);

ALTER TABLE `ChiTietCongThuc` ADD FOREIGN KEY (`MaNL`) REFERENCES `NguyenLieu` (`MaNL`);

ALTER TABLE `DoanhThu` ADD FOREIGN KEY (`MaSP`) REFERENCES `SanPham` (`MaSP`);

ALTER TABLE `DoanhThu` ADD FOREIGN KEY (`MaLoai`) REFERENCES `Loai` (`MaLoai`);

ALTER TABLE `DoanhThu` ADD FOREIGN KEY (`MaKM`) REFERENCES `CTKhuyenMai` (`MaCTKhuyenMai`);

ALTER TABLE `DoanhThu` ADD FOREIGN KEY (`MaSize`) REFERENCES `Size` (`MaSize`);

ALTER TABLE `sanpham_khuyenmai` ADD FOREIGN KEY (`MaSP`) REFERENCES `SanPham` (`MaSP`);

ALTER TABLE `sanpham_khuyenmai` ADD FOREIGN KEY (`MaCTKhuyenMai`) REFERENCES `CTKhuyenMai` (`MaCTKhuyenMai`);

ALTER TABLE `DoanhThu` ADD FOREIGN KEY (`TongChiPhi`) REFERENCES `DoanhThu` (`MaDT`);
