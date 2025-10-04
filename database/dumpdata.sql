USE ql_do_uong;

-- 1. Loai sản phẩm
INSERT IGNORE INTO Loai (TenLoai, MoTa) VALUES
('Trà sữa truyền thống', 'Trà sữa cơ bản'),
('Trà sữa matcha', 'Trà sữa vị matcha'),
('Trà sữa socola', 'Trà sữa vị socola'),
('Trà sữa trái cây', 'Trà sữa trái cây'),
('Trà sữa khoai', 'Trà sữa khoai'),
('Trà sữa đặc biệt', 'Trà sữa đặc biệt');

-- 2. SanPham
INSERT IGNORE INTO SanPham (TenSP, Gia, Anh, SLDuKien, TrangThai, MaLoai) VALUES
('Trà sữa truyền thống', 25000, 'tra_sua_truyen_thong.png', 50, 1, 1),
('Trà sữa matcha truyền thống', 30000, 'tra_sua_matcha_truyen_thong.png', 40, 1, 2),
('Trà sữa socola', 28000, 'tra_sua_socola.png', 30, 1, 3),
('Trà sữa caramel', 32000, 'tra_sua_caramel.png', 20, 1, 3),
('Trà sữa dâu', 27000, 'tra_sua_dau.png', 25, 1, 4),
('Trà sữa khoai môn', 30000, 'tra_sua_khoai_mon.png', 15, 1, 5),
('Trà sữa hạt dẻ', 35000, 'tra_sua_hat_de.png', 10, 1, 5),
('Trà sữa bạc hà', 33000, 'tra_sua_bac_ha.png', 12, 1, 1),
('Trà sữa matcha socola', 36000, 'tra_sua_matcha_socola.png', 8, 1, 2),
('Trà sữa hoàng kim', 40000, 'tra_sua_hoang_kim.png', 5, 1, 6);

-- 3. Size
INSERT IGNORE INTO Size (TenSize, PhuThu) VALUES
('S', 10000),
('M', 15000),
('L', 20000),
('XL', 25000);

-- 4. Quyen
INSERT IGNORE INTO Quyen (TenQuyen, Mota) VALUES
('Admin', 'Quyền toàn quyền'),
('Nhân viên bán hàng', 'Quyền bán hàng'),
('Nhân viên kho', 'Quyền quản lý kho'),
('Kế toán', 'Quyền xem báo cáo'),
('Quản lý', 'Quyền quản lý tổng');

-- 5. ChucNang
INSERT IGNORE INTO ChucNang (TenChucNang, MoTa) VALUES
('Thêm đơn hàng', 'Thêm đơn hàng'),
('Thêm sản phẩm', 'Thêm sản phẩm'),
('Xóa sản phẩm', 'Xóa sản phẩm'),
('Sửa sản phẩm', 'Sửa sản phẩm'),
('Vào thống kê', 'Vào thống kê'),
('Vào tài khoản', 'Vào tài khoản'),
('Thêm tài khoản', 'Thêm tài khoản'),
('Sửa tài khoản', 'Sửa tài khoản'),
('Xóa tài khoản', 'Xóa tài khoản'),
('Xem tài khoản', 'Xem tài khoản'),
('Thêm quyền', 'Thêm quyền'),
('Sửa quyền', 'Sửa quyền'),
('Xóa quyền', 'Xóa quyền'),
('Vào khuyến mãi', 'Vào khuyến mãi'),
('Thêm khuyến mãi', 'Thêm khuyến mãi'),
('Xóa khuyến mãi', 'Xóa khuyến mãi'),
('Sửa khuyến mãi', 'Sửa khuyến mãi'),
('Vào hóa đơn', 'Vào hóa đơn'),
('Xóa hóa đơn', 'Xóa hóa đơn'),
('Vào nhập hàng', 'Vào nhập hàng'),
('Thêm phiếu nhập', 'Thêm phiếu nhập'),
('Xóa phiếu nhập', 'Xóa phiếu nhập'),
('Sửa phiếu nhập', 'Sửa phiếu nhập'),
('Nhập excel phiếu nhập', 'Nhập excel phiếu nhập');




-- 6. TaiKhoan
INSERT IGNORE INTO TaiKhoan (TenTaiKhoan, Anh, TrangThai, MaQuyen) VALUES
('nv_banhang1', 'nv_banhang1.jpg', 1, 2),
('nv_banhang2', 'nv_banhang2.jpg', 1, 2),
('nv_banhang3', 'nv_banhang3.jpg', 1, 2),
('nv_kho1', 'nv_kho1.jpg', 1, 3),
('nv_kho2', 'nv_kho2.jpg', 1, 3),
('admin1', 'admin1.jpg', 1, 1),
('kt1', 'kt1.jpg', 1, 4),
('ql1', 'ql1.jpg', 1, 5),
('nv_banhang4', 'nv_banhang4.jpg', 1, 2),
('nv_banhang5', 'nv_banhang5.jpg', 1, 2);

-- 7. Quyen_ChucNang
INSERT IGNORE INTO Quyen_ChucNang (MaQuyen, MaChucNang) VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),
(2,2),(2,5),
(3,4),
(4,5),
(5,1),(5,2),(5,3),(5,4),(5,5);

-- 8. NhanVien
INSERT IGNORE INTO NhanVien (TenNV, SDT, MaTK) VALUES
('Nguyen Van A','0901234561',1),
('Nguyen Van B','0901234562',2),
('Nguyen Van C','0901234563',3),
('Tran Thi D','0901234564',4),
('Le Van E','0901234565',5),
('Pham Thi F','0901234566',6),
('Nguyen Van G','0901234567',7),
('Tran Thi H','0901234568',8),
('Le Van I','0901234569',9),
('Pham Thi K','0901234570',10);

-- 9. Buzzer
INSERT IGNORE INTO Buzzer (SoHieu, TrangThai) VALUES
('BZ01',1),('BZ02',1),('BZ03',1),('BZ04',1),('BZ05',1),
('BZ06',1),('BZ07',1),('BZ08',1),('BZ09',1),('BZ10',1);

-- 10. DonHang
INSERT IGNORE INTO DonHang (MaNV, NgayLap, GioLap, TrangThai, MaBuzzer, PhuongThucThanhToan, TongGia) VALUES
(1,'2025-09-27','09:00:00',1,1,1,50000),
(2,'2025-09-27','10:15:00',1,2,1,30000),
(3,'2025-09-27','11:30:00',1,3,1,84000),
(4,'2025-09-27','12:45:00',1,4,1,25000),
(5,'2025-09-27','14:00:00',1,5,1,64000),
(6,'2025-09-27','15:30:00',1,6,1,30000),
(7,'2025-09-27','16:45:00',1,7,1,35000);

-- 11. ChiTietDonHang
INSERT IGNORE INTO ChiTietDonHang (MaDH, MaSP, MaSize, SoLuong, GiaVon, TongGia) VALUES
(1,1,2,2,25000,50000),
(2,2,2,1,30000,30000),
(3,3,3,3,28000,84000),
(4,1,1,1,25000,25000),
(5,4,2,2,32000,64000),
(6,2,3,1,30000,30000),
(7,5,2,1,35000,35000);

-- 12. NguyenLieu
INSERT IGNORE INTO NguyenLieu (SoLuong, Ten, GiaBan) VALUES
(1000, 'Trà đen', 5000.00),
(800, 'Trà xanh', 6000.00),
(500, 'Sữa đặc', 10000.00),
(300, 'Đường', 2000.00),
(200, 'Trân châu đen', 15000.00),
(150, 'Trân châu trắng', 16000.00),
(100, 'Pudding', 12000.00),
(50, 'Thạch dừa', 10000.00),
(400, 'Đá viên', 1000.00),
(250, 'Matcha bột', 20000.00);

-- 13. PhieuNhap
INSERT IGNORE INTO PhieuNhap (NgayNhap, SoLuong, MaNV, TongTien) VALUES
('2025-09-01',50,1,500000),
('2025-09-02',30,2,300000),
('2025-09-03',40,3,400000),
('2025-09-04',25,4,250000),
('2025-09-05',60,5,600000),
('2025-09-06',35,6,350000),
('2025-09-07',45,7,450000),
('2025-09-08',20,8,200000),
('2025-09-09',55,9,550000),
('2025-09-10',50,10,500000);

-- 14. ChiTietPhieuNhap
INSERT IGNORE INTO ChiTietPhieuNhap (MaPN, SoLuong, MaNguyenLieu, DonGiaNhap, TongGia) VALUES
(1,20,1,5000,100000),
(1,30,2,4000,120000),
(2,15,3,6000,90000),
(2,15,4,5000,75000),
(3,25,1,5000,125000),
(3,15,5,7000,105000),
(4,10,2,4000,40000),
(5,30,3,6000,180000),
(6,20,4,5000,100000),
(7,25,5,7000,175000);

-- 15. CTKhuyenMai
INSERT IGNORE INTO CTKhuyenMai (TenCTKhuyenMai, MoTa, NgayBatDau, NgayKetThuc, PhanTramKhuyenMai, TrangThai) VALUES
('KM Mua 1 tặng 1','Khuyến mãi 1', '2025-09-01','2025-09-30',50,1),
('KM Giảm 10%','Khuyến mãi 2', '2025-09-01','2025-09-30',10,1),
('KM Giảm 20%','Khuyến mãi 3', '2025-09-05','2025-09-30',20,1),
('KM Cuối tuần','Khuyến mãi 4', '2025-09-06','2025-09-30',15,1),
('KM Combo','Khuyến mãi 5', '2025-09-07','2025-09-30',25,1),
('KM Sáng sớm','Khuyến mãi 6', '2025-09-01','2025-09-30',5,1),
('KM Chiều','Khuyến mãi 7', '2025-09-01','2025-09-30',12,1),
('KM Giảm 5k','Khuyến mãi 8', '2025-09-10','2025-09-30',5,1),
('KM Mùa hè','Khuyến mãi 9', '2025-06-01','2025-09-30',10,1),
('KM Sinh nhật','Khuyến mãi 10', '2025-09-15','2025-09-30',30,1);

-- 16. sanpham_khuyenmai
INSERT IGNORE INTO sanpham_khuyenmai (MaSP, MaCTKhuyenMai) VALUES
(1,1),(2,2),(3,3),(4,4),(5,5),
(6,6),(7,7),(8,8),(9,9),(10,10);

-- 17. CongThuc
INSERT IGNORE INTO CongThuc (Ten, MaSP, Mota) VALUES
('Trà sữa truyền thống',1,'Trà sữa truyền thống'),
('Trà sữa matcha',2,'Trà sữa matcha'),
('Trà sữa socola',3,'Trà sữa socola'),
('Trà sữa caramel',4,'Trà sữa caramel'),
('Trà sữa dâu',5,'Trà sữa dâu'),
('Trà sữa khoai môn',6,'Trà sữa khoai môn'),
('Trà sữa hạt dẻ',7,'Trà sữa hạt dẻ'),
('Trà sữa bạc hà',8,'Trà sữa bạc hà'),
('Trà sữa matcha socola',9,'Trà sữa matcha socola'),
('Trà sữa hoàng kim',10,'Trà sữa hoàng kim');

-- 18. ChiTietCongThuc
INSERT IGNORE INTO ChiTietCongThuc (MaCT, MaNL, SL) VALUES
(1,1,20),(1,3,10),
(2,2,25),(2,10,5),
(3,1,15),(3,5,5),
(4,1,10),(4,4,5),
(5,2,5),(5,6,3),
(6,2,10),(6,7,5),
(7,1,10),(7,8,5),
(8,2,5),(8,4,5),
(9,2,5),(9,5,5),
(10,1,10),(10,3,10);

-- 19. DoanhThu
INSERT IGNORE INTO DoanhThu (Ngay, Thang, Nam, Gio, SLBan, MaSP, MaLoai, MaKM, MaSize, TongDoanhThu) VALUES
(27,9,2025,'09:00:00',2,1,1,1,2,50000),
(27,9,2025,'10:15:00',1,2,2,2,2,30000),
(27,9,2025,'11:30:00',3,3,3,3,3,84000),
(27,9,2025,'12:45:00',1,1,1,4,1,25000),
(27,9,2025,'14:00:00',2,4,4,5,2,64000),
(27,9,2025,'15:30:00',1,2,2,6,3,30000),
(27,9,2025,'16:45:00',1,5,5,7,2,35000),
(26,9,2025,'17:00:00',2,6,6,8,2,60000),
(26,9,2025,'18:00:00',1,7,2,9,3,85000),
(26,9,2025,'19:00:00',1,8,2,10,2,40000);

-- 20. ChiPhi
INSERT IGNORE INTO ChiPhi (Ngay, Thang, Nam, Gio, MaSP, MaLoai, MaKM, TongChiPhiSP, TongChiPhiNL) VALUES
(27,9,2025,'09:00:00',1,1,1,80000,50000),
(27,9,2025,'10:15:00',2,2,2,75000,30000),
(27,9,2025,'11:30:00',3,3,3,120000,84000),
(27,9,2025,'12:45:00',4,4,4,60000,25000),
(27,9,2025,'14:00:00',5,5,5,90000,64000),
(27,9,2025,'15:30:00',6,6,6,70000,30000),
(27,9,2025,'16:45:00',7,7,7,85000,35000),
(26,9,2025,'17:00:00',8,8,8,95000,60000),
(26,9,2025,'18:00:00',9,9,9,140000,90000),
(26,9,2025,'19:00:00',10,10,10,40000,25000);


