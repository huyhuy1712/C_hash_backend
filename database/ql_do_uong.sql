-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: ql_do_uong
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `buzzer`
--

DROP TABLE IF EXISTS `buzzer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `buzzer` (
  `MaBuzzer` int NOT NULL AUTO_INCREMENT,
  `SoHieu` varchar(20) NOT NULL,
  `TrangThai` int DEFAULT '1',
  PRIMARY KEY (`MaBuzzer`),
  UNIQUE KEY `SoHieu` (`SoHieu`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `buzzer`
--

LOCK TABLES `buzzer` WRITE;
/*!40000 ALTER TABLE `buzzer` DISABLE KEYS */;
INSERT INTO `buzzer` VALUES (1,'BZ01',0),(2,'BZ02',0),(3,'BZ03',1),(4,'BZ04',1),(5,'BZ05',1),(6,'BZ06',1),(7,'BZ07',1),(8,'BZ08',1),(9,'BZ09',1),(10,'BZ10',1);
/*!40000 ALTER TABLE `buzzer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chitietcongthuc`
--

DROP TABLE IF EXISTS `chitietcongthuc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chitietcongthuc` (
  `MaCT` int DEFAULT NULL,
  `MaNL` int DEFAULT NULL,
  `SL` int DEFAULT NULL,
  KEY `MaCT` (`MaCT`),
  KEY `MaNL` (`MaNL`),
  CONSTRAINT `chitietcongthuc_ibfk_1` FOREIGN KEY (`MaCT`) REFERENCES `congthuc` (`MaCT`),
  CONSTRAINT `chitietcongthuc_ibfk_2` FOREIGN KEY (`MaNL`) REFERENCES `nguyenlieu` (`MaNL`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chitietcongthuc`
--

LOCK TABLES `chitietcongthuc` WRITE;
/*!40000 ALTER TABLE `chitietcongthuc` DISABLE KEYS */;
INSERT INTO `chitietcongthuc` VALUES (1,1,20),(1,3,10),(2,2,25),(2,10,5),(3,1,15),(3,5,5),(4,1,10),(4,4,5),(5,2,5),(5,6,3),(6,2,10),(6,7,5),(7,1,10),(7,8,5),(8,2,5),(8,4,5),(9,2,5),(9,5,5),(10,1,10),(10,3,10);
/*!40000 ALTER TABLE `chitietcongthuc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chitietdonhang`
--

DROP TABLE IF EXISTS `chitietdonhang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chitietdonhang` (
  `MaCTDH` int NOT NULL AUTO_INCREMENT,
  `MaDH` int DEFAULT NULL,
  `MaSP` int DEFAULT NULL,
  `MaSize` int DEFAULT NULL,
  `SoLuong` int DEFAULT '1',
  `GiaVon` decimal(12,2) DEFAULT NULL,
  `TongGia` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`MaCTDH`),
  KEY `MaDH` (`MaDH`),
  KEY `MaSP` (`MaSP`),
  KEY `MaSize` (`MaSize`),
  CONSTRAINT `chitietdonhang_ibfk_1` FOREIGN KEY (`MaDH`) REFERENCES `donhang` (`MaDH`),
  CONSTRAINT `chitietdonhang_ibfk_2` FOREIGN KEY (`MaSP`) REFERENCES `sanpham` (`MaSP`),
  CONSTRAINT `chitietdonhang_ibfk_3` FOREIGN KEY (`MaSize`) REFERENCES `size` (`MaSize`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chitietdonhang`
--

LOCK TABLES `chitietdonhang` WRITE;
/*!40000 ALTER TABLE `chitietdonhang` DISABLE KEYS */;
INSERT INTO `chitietdonhang` VALUES (1,1,1,2,2,25000.00,50000.00),(2,2,2,2,1,30000.00,30000.00),(3,3,3,3,3,28000.00,84000.00),(4,4,1,1,1,25000.00,25000.00),(5,5,4,2,2,32000.00,64000.00),(6,6,2,3,1,30000.00,30000.00),(7,7,5,2,1,35000.00,35000.00),(8,1,9,3,4,36000.00,209600.00),(9,1,3,1,1,28000.00,32400.00);
/*!40000 ALTER TABLE `chitietdonhang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chitietphieunhap`
--

DROP TABLE IF EXISTS `chitietphieunhap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chitietphieunhap` (
  `MaChiTietPhieuNhap` int NOT NULL AUTO_INCREMENT,
  `MaPN` int DEFAULT NULL,
  `SoLuong` int DEFAULT NULL,
  `MaNguyenLieu` int DEFAULT NULL,
  `DonGiaNhap` decimal(12,2) DEFAULT NULL,
  `TongGia` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`MaChiTietPhieuNhap`),
  KEY `MaPN` (`MaPN`),
  KEY `MaNguyenLieu` (`MaNguyenLieu`),
  CONSTRAINT `chitietphieunhap_ibfk_1` FOREIGN KEY (`MaPN`) REFERENCES `phieunhap` (`MaPN`),
  CONSTRAINT `chitietphieunhap_ibfk_2` FOREIGN KEY (`MaNguyenLieu`) REFERENCES `nguyenlieu` (`MaNL`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chitietphieunhap`
--

LOCK TABLES `chitietphieunhap` WRITE;
/*!40000 ALTER TABLE `chitietphieunhap` DISABLE KEYS */;
INSERT INTO `chitietphieunhap` VALUES (1,1,20,1,5000.00,100000.00),(2,1,30,2,4000.00,120000.00),(3,2,15,3,6000.00,90000.00),(4,2,15,4,5000.00,75000.00),(5,3,25,1,5000.00,125000.00),(6,3,15,5,7000.00,105000.00),(7,4,10,2,4000.00,40000.00),(8,5,30,3,6000.00,180000.00),(9,6,20,4,5000.00,100000.00),(10,7,25,5,7000.00,175000.00),(11,11,4,1,5000.00,20000.00),(12,11,1,1,5000.00,5000.00),(13,12,5,1,5000.00,25000.00),(14,12,3,1,5000.00,15000.00),(15,13,4,1,5000.00,20000.00);
/*!40000 ALTER TABLE `chitietphieunhap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chucnang`
--

DROP TABLE IF EXISTS `chucnang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chucnang` (
  `MaChucNang` int NOT NULL AUTO_INCREMENT,
  `TenChucNang` varchar(100) NOT NULL,
  `MoTa` text,
  PRIMARY KEY (`MaChucNang`),
  UNIQUE KEY `TenChucNang` (`TenChucNang`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chucnang`
--

LOCK TABLES `chucnang` WRITE;
/*!40000 ALTER TABLE `chucnang` DISABLE KEYS */;
INSERT INTO `chucnang` VALUES (1,'Thêm đơn hàng','Thêm đơn hàng'),(2,'Thêm sản phẩm','Thêm sản phẩm'),(3,'Xóa sản phẩm','Xóa sản phẩm'),(4,'Sửa sản phẩm','Sửa sản phẩm'),(5,'Vào thống kê','Vào thống kê'),(6,'Vào tài khoản','Vào tài khoản'),(7,'Thêm tài khoản','Thêm tài khoản'),(8,'Sửa tài khoản','Sửa tài khoản'),(9,'Xóa tài khoản','Xóa tài khoản'),(10,'Xem tài khoản','Xem tài khoản'),(11,'Thêm quyền','Thêm quyền'),(12,'Sửa quyền','Sửa quyền'),(13,'Xóa quyền','Xóa quyền'),(14,'Vào khuyến mãi','Vào khuyến mãi'),(15,'Thêm khuyến mãi','Thêm khuyến mãi'),(16,'Xóa khuyến mãi','Xóa khuyến mãi'),(17,'Sửa khuyến mãi','Sửa khuyến mãi'),(18,'Vào hóa đơn','Vào hóa đơn'),(19,'Xóa hóa đơn','Xóa hóa đơn'),(20,'Vào nhập hàng','Vào nhập hàng'),(21,'Thêm phiếu nhập','Thêm phiếu nhập'),(22,'Xóa phiếu nhập','Xóa phiếu nhập'),(23,'Sửa phiếu nhập','Sửa phiếu nhập'),(24,'Nhập excel phiếu nhập','Nhập excel phiếu nhập'),(25,'Vào nguyên liệu','Vào nguyên liệu'),(26,'Thêm nguyên liệu','Thêm nguyên liệu'),(27,'Xóa nguyên liệu','Xóa nguyên liệu'),(28,'Sửa nguyên liệu','Sửa nguyên liệu'),(29,'Vào nhà cung cấp','Vào nhà cung cấp'),(30,'Thêm nhà cung cấp','Thêm nhà cung cấp'),(31,'Xóa nhà cung cấp','Xóa nhà cung cấp'),(32,'Sửa nhà cung cấp','Sửa nhà cung cấp');
/*!40000 ALTER TABLE `chucnang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `congthuc`
--

DROP TABLE IF EXISTS `congthuc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `congthuc` (
  `MaCT` int NOT NULL AUTO_INCREMENT,
  `Ten` text,
  `MaSP` int DEFAULT NULL,
  `Mota` text,
  PRIMARY KEY (`MaCT`),
  KEY `MaSP` (`MaSP`),
  CONSTRAINT `congthuc_ibfk_1` FOREIGN KEY (`MaSP`) REFERENCES `sanpham` (`MaSP`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `congthuc`
--

LOCK TABLES `congthuc` WRITE;
/*!40000 ALTER TABLE `congthuc` DISABLE KEYS */;
INSERT INTO `congthuc` VALUES (1,'Trà sữa truyền thống',1,'Trà sữa truyền thống'),(2,'Trà sữa matcha',2,'Trà sữa matcha'),(3,'Trà sữa socola',3,'Trà sữa socola'),(4,'Trà sữa caramel',4,'Trà sữa caramel'),(5,'Trà sữa dâu',5,'Trà sữa dâu'),(6,'Trà sữa khoai môn',6,'Trà sữa khoai môn'),(7,'Trà sữa hạt dẻ',7,'Trà sữa hạt dẻ'),(8,'Trà sữa bạc hà',8,'Trà sữa bạc hà'),(9,'Trà sữa matcha socola',9,'Trà sữa matcha socola'),(10,'Trà sữa hoàng kim',10,'Trà sữa hoàng kim');
/*!40000 ALTER TABLE `congthuc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ctdonhang_topping`
--

DROP TABLE IF EXISTS `ctdonhang_topping`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ctdonhang_topping` (
  `MaNL` int DEFAULT NULL,
  `MaCTDH` int DEFAULT NULL,
  `SL` int DEFAULT NULL,
  KEY `MaNL` (`MaNL`),
  KEY `MaCTDH` (`MaCTDH`),
  CONSTRAINT `ctdonhang_topping_ibfk_1` FOREIGN KEY (`MaNL`) REFERENCES `nguyenlieu` (`MaNL`),
  CONSTRAINT `ctdonhang_topping_ibfk_2` FOREIGN KEY (`MaCTDH`) REFERENCES `chitietdonhang` (`MaCTDH`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ctdonhang_topping`
--

LOCK TABLES `ctdonhang_topping` WRITE;
/*!40000 ALTER TABLE `ctdonhang_topping` DISABLE KEYS */;
INSERT INTO `ctdonhang_topping` VALUES (8,8,25),(9,8,25);
/*!40000 ALTER TABLE `ctdonhang_topping` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ctkhuyenmai`
--

DROP TABLE IF EXISTS `ctkhuyenmai`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ctkhuyenmai` (
  `MaCTKhuyenMai` int NOT NULL AUTO_INCREMENT,
  `TenCTKhuyenMai` varchar(50) DEFAULT NULL,
  `MoTa` text,
  `NgayBatDau` datetime DEFAULT NULL,
  `NgayKetThuc` datetime DEFAULT NULL,
  `PhanTramKhuyenMai` int DEFAULT NULL,
  `TrangThai` int DEFAULT NULL,
  PRIMARY KEY (`MaCTKhuyenMai`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ctkhuyenmai`
--

LOCK TABLES `ctkhuyenmai` WRITE;
/*!40000 ALTER TABLE `ctkhuyenmai` DISABLE KEYS */;
INSERT INTO `ctkhuyenmai` VALUES (1,'KM Mua 1 tặng 1','Khuyến mãi 1','2025-09-01 00:00:00','2025-09-30 00:00:00',50,1),(2,'KM Giảm 10%','Khuyến mãi 2','2025-09-01 00:00:00','2025-09-30 00:00:00',10,1),(3,'KM Giảm 20%','Khuyến mãi 3','2025-09-05 00:00:00','2025-09-30 00:00:00',20,1),(4,'KM Cuối tuần','Khuyến mãi 4','2025-09-06 00:00:00','2025-09-30 00:00:00',15,1),(5,'KM Combo','Khuyến mãi 5','2025-09-07 00:00:00','2025-09-30 00:00:00',25,1),(6,'KM Sáng sớm','Khuyến mãi 6','2025-09-01 00:00:00','2025-09-30 00:00:00',5,1),(7,'KM Chiều','Khuyến mãi 7','2025-09-01 00:00:00','2025-09-30 00:00:00',12,1),(8,'KM Giảm 5k','Khuyến mãi 8','2025-09-10 00:00:00','2025-09-30 00:00:00',5,1),(9,'KM Mùa hè','Khuyến mãi 9','2025-06-01 00:00:00','2025-09-30 00:00:00',10,1),(10,'KM Sinh nhật','Khuyến mãi 10','2025-09-15 00:00:00','2025-09-30 00:00:00',30,1);
/*!40000 ALTER TABLE `ctkhuyenmai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doanhthu`
--

DROP TABLE IF EXISTS `doanhthu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doanhthu` (
  `MaDT` int NOT NULL AUTO_INCREMENT,
  `Ngay` int NOT NULL,
  `Thang` int NOT NULL,
  `Nam` int NOT NULL,
  `Gio` time NOT NULL,
  `SLBan` int DEFAULT NULL,
  `MaSP` int DEFAULT NULL,
  `MaLoai` int DEFAULT NULL,
  `MaKM` int DEFAULT NULL,
  `MaSize` int DEFAULT NULL,
  `TongChiPhi` int DEFAULT NULL,
  `TongDoanhThu` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`MaDT`),
  KEY `MaSP` (`MaSP`),
  KEY `MaLoai` (`MaLoai`),
  KEY `MaKM` (`MaKM`),
  KEY `MaSize` (`MaSize`),
  KEY `TongChiPhi` (`TongChiPhi`),
  CONSTRAINT `doanhthu_ibfk_1` FOREIGN KEY (`MaSP`) REFERENCES `sanpham` (`MaSP`),
  CONSTRAINT `doanhthu_ibfk_2` FOREIGN KEY (`MaLoai`) REFERENCES `loai` (`MaLoai`),
  CONSTRAINT `doanhthu_ibfk_3` FOREIGN KEY (`MaKM`) REFERENCES `ctkhuyenmai` (`MaCTKhuyenMai`),
  CONSTRAINT `doanhthu_ibfk_4` FOREIGN KEY (`MaSize`) REFERENCES `size` (`MaSize`),
  CONSTRAINT `doanhthu_ibfk_5` FOREIGN KEY (`TongChiPhi`) REFERENCES `doanhthu` (`MaDT`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doanhthu`
--

LOCK TABLES `doanhthu` WRITE;
/*!40000 ALTER TABLE `doanhthu` DISABLE KEYS */;
INSERT INTO `doanhthu` VALUES (11,27,9,2025,'09:00:00',2,1,1,1,2,10000,50000.00),(12,27,9,2025,'10:15:00',1,2,2,2,2,10000,30000.00),(13,27,9,2025,'11:30:00',3,3,3,3,3,10000,84000.00),(14,27,9,2025,'12:45:00',1,1,1,4,1,10000,25000.00),(15,27,9,2025,'14:00:00',2,4,4,5,2,10000,64000.00),(16,27,9,2025,'15:30:00',1,2,2,6,3,10000,30000.00),(17,27,9,2025,'16:45:00',1,5,5,7,2,10000,35000.00),(18,26,9,2025,'17:00:00',2,6,6,8,2,10000,60000.00),(19,26,9,2025,'18:00:00',1,7,2,9,3,10000,85000.00),(20,26,9,2025,'19:00:00',1,8,2,10,2,10000,40000.00);
/*!40000 ALTER TABLE `doanhthu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `donhang`
--

DROP TABLE IF EXISTS `donhang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `donhang` (
  `MaDH` int NOT NULL AUTO_INCREMENT,
  `MaNV` int DEFAULT NULL,
  `NgayLap` datetime DEFAULT NULL,
  `GioLap` time DEFAULT NULL,
  `TrangThai` int DEFAULT '1',
  `MaBuzzer` int DEFAULT NULL,
  `PhuongThucThanhToan` int DEFAULT NULL,
  `TongGia` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`MaDH`),
  KEY `MaNV` (`MaNV`),
  KEY `MaBuzzer` (`MaBuzzer`),
  CONSTRAINT `donhang_ibfk_1` FOREIGN KEY (`MaNV`) REFERENCES `nhanvien` (`MaNV`),
  CONSTRAINT `donhang_ibfk_2` FOREIGN KEY (`MaBuzzer`) REFERENCES `buzzer` (`MaBuzzer`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `donhang`
--

LOCK TABLES `donhang` WRITE;
/*!40000 ALTER TABLE `donhang` DISABLE KEYS */;
INSERT INTO `donhang` VALUES (1,1,'2025-09-27 00:00:00','09:00:00',1,1,1,50000.00),(2,2,'2025-09-27 00:00:00','10:15:00',1,2,1,30000.00),(3,3,'2025-09-27 00:00:00','11:30:00',1,3,1,84000.00),(4,4,'2025-09-27 00:00:00','12:45:00',1,4,1,25000.00),(5,5,'2025-09-27 00:00:00','14:00:00',1,5,1,64000.00),(6,6,'2025-09-27 00:00:00','15:30:00',1,6,1,30000.00),(7,7,'2025-09-27 00:00:00','16:45:00',1,7,1,35000.00),(8,6,'2025-10-23 00:52:57','00:52:57',0,1,0,209600.00),(9,6,'2025-10-25 19:06:44','19:06:44',0,2,0,32400.00);
/*!40000 ALTER TABLE `donhang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loai`
--

DROP TABLE IF EXISTS `loai`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loai` (
  `MaLoai` int NOT NULL AUTO_INCREMENT,
  `TenLoai` varchar(50) NOT NULL,
  `MoTa` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`MaLoai`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loai`
--

LOCK TABLES `loai` WRITE;
/*!40000 ALTER TABLE `loai` DISABLE KEYS */;
INSERT INTO `loai` VALUES (1,'Trà sữa truyền thống','Trà sữa cơ bản'),(2,'Trà sữa matcha','Trà sữa vị matcha'),(3,'Trà sữa socola','Trà sữa vị socola'),(4,'Trà sữa trái cây','Trà sữa trái cây'),(5,'Trà sữa khoai','Trà sữa khoai'),(6,'Trà sữa đặc biệt','Trà sữa đặc biệt');
/*!40000 ALTER TABLE `loai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nguyenlieu`
--

DROP TABLE IF EXISTS `nguyenlieu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nguyenlieu` (
  `MaNL` int NOT NULL AUTO_INCREMENT,
  `SoLuong` int DEFAULT NULL,
  `Ten` varchar(50) DEFAULT NULL,
  `GiaBan` decimal(12,2) DEFAULT NULL,
  `TrangThai` int DEFAULT NULL,
  PRIMARY KEY (`MaNL`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nguyenlieu`
--

LOCK TABLES `nguyenlieu` WRITE;
/*!40000 ALTER TABLE `nguyenlieu` DISABLE KEYS */;
INSERT INTO `nguyenlieu` VALUES (1,85,'Trà đen',5000.00,1),(2,75,'Trà xanh',6000.00,1),(3,100,'Sữa đặc',10000.00,1),(4,100,'Đường',2000.00,1),(5,70,'Trân châu đen',15000.00,1),(6,100,'Trân châu trắng',16000.00,1),(7,100,'Pudding',12000.00,1),(8,75,'Thạch dừa',10000.00,1),(9,75,'Đá viên',1000.00,1),(10,100,'Matcha bột',20000.00,1);
/*!40000 ALTER TABLE `nguyenlieu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nhacungcap`
--

DROP TABLE IF EXISTS `nhacungcap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nhacungcap` (
  `MaNCC` int NOT NULL AUTO_INCREMENT,
  `TenNCC` varchar(50) NOT NULL,
  `SDT` varchar(50) NOT NULL,
  `DiaChi` varchar(100) NOT NULL,
  `TrangThai` int DEFAULT NULL,
  PRIMARY KEY (`MaNCC`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nhacungcap`
--

LOCK TABLES `nhacungcap` WRITE;
/*!40000 ALTER TABLE `nhacungcap` DISABLE KEYS */;
INSERT INTO `nhacungcap` VALUES (1,'Công ty TNHH Trà Sữa A','0901111001','123 Lê Lợi, Quận 1, TP.HCM',1),(2,'Công ty TNHH Nguyên Liệu B','0901111002','45 Trần Hưng Đạo, Quận 5, TP.HCM',1),(3,'Cơ Sở Cung Ứng C','0901111003','78 Nguyễn Huệ, Quận 1, TP.HCM',1),(4,'Công ty TNHH Đóng Gói D','0901111004','9 Lý Thường Kiệt, Quận 11, TP.HCM',1),(5,'Nhà Cung Cấp E','0901111005','210 Phan Đình Phùng, TP.Đà Lạt',1),(6,'Công ty F','0901111006','56 Hai Bà Trưng, Quận 3, TP.HCM',1),(7,'Cơ Sở G','0901111007','12 Trần Phú, Hà Đông, Hà Nội',1),(8,'Công ty H','0901111008','88 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM',1),(9,'Nhà Cung Cấp I','0901111009','33 Bà Triệu, Hoàn Kiếm, Hà Nội',1),(10,'Công ty J','0901111010','150 Võ Văn Kiệt, Quận 1, TP.HCM',1);
/*!40000 ALTER TABLE `nhacungcap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nhanvien`
--

DROP TABLE IF EXISTS `nhanvien`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nhanvien` (
  `MaNV` int NOT NULL AUTO_INCREMENT,
  `TenNV` varchar(50) NOT NULL,
  `SDT` varchar(50) NOT NULL,
  `NgayLam` date DEFAULT (curdate()),
  `MaTK` int DEFAULT NULL,
  PRIMARY KEY (`MaNV`),
  KEY `MaTK` (`MaTK`),
  CONSTRAINT `nhanvien_ibfk_1` FOREIGN KEY (`MaTK`) REFERENCES `taikhoan` (`MaTK`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nhanvien`
--

LOCK TABLES `nhanvien` WRITE;
/*!40000 ALTER TABLE `nhanvien` DISABLE KEYS */;
INSERT INTO `nhanvien` VALUES (1,'Nguyen Van A','0901234561','2025-10-23',1),(2,'Nguyen Van B','0901234562','2025-10-23',2),(3,'Nguyen Van C','0901234563','2025-10-23',3),(4,'Tran Thi D','0901234564','2025-10-23',4),(5,'Le Van E','0901234565','2025-10-23',5),(6,'Pham Thi F','0901234566','2025-10-23',6),(7,'Nguyen Van G','0901234567','2025-10-23',7),(8,'Tran Thi H','0901234568','2025-10-23',8),(9,'Le Van I','0901234569','2025-10-23',9),(10,'Pham Thi K','0901234570','2025-10-23',10);
/*!40000 ALTER TABLE `nhanvien` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phieunhap`
--

DROP TABLE IF EXISTS `phieunhap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `phieunhap` (
  `MaPN` int NOT NULL AUTO_INCREMENT,
  `NgayNhap` date DEFAULT NULL,
  `SoLuong` int DEFAULT NULL,
  `TrangThai` int DEFAULT NULL,
  `MaNCC` int DEFAULT NULL,
  `MaNV` int DEFAULT NULL,
  `TongTien` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`MaPN`),
  KEY `MaNV` (`MaNV`),
  KEY `MaNCC` (`MaNCC`),
  CONSTRAINT `phieunhap_ibfk_1` FOREIGN KEY (`MaNV`) REFERENCES `nhanvien` (`MaNV`),
  CONSTRAINT `phieunhap_ibfk_2` FOREIGN KEY (`MaNCC`) REFERENCES `nhacungcap` (`MaNCC`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phieunhap`
--

LOCK TABLES `phieunhap` WRITE;
/*!40000 ALTER TABLE `phieunhap` DISABLE KEYS */;
INSERT INTO `phieunhap` VALUES (1,'2025-09-01',50,1,1,1,500000.00),(2,'2025-09-02',30,1,2,2,300000.00),(3,'2025-09-03',40,1,3,3,400000.00),(4,'2025-09-04',25,1,4,4,250000.00),(5,'2025-09-05',60,1,5,5,600000.00),(6,'2025-09-06',35,1,6,6,350000.00),(7,'2025-09-07',45,0,7,7,450000.00),(8,'2025-09-08',20,1,8,8,200000.00),(9,'2025-09-09',55,1,9,9,550000.00),(10,'2025-09-10',50,1,9,10,500000.00),(11,'2025-10-23',5,1,1,1,25000.00),(12,'2025-10-23',8,1,1,1,40000.00),(13,'2025-10-24',4,0,1,1,20000.00),(14,'2025-10-25',3,1,1,1,15000.00);
/*!40000 ALTER TABLE `phieunhap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `quyen`
--

DROP TABLE IF EXISTS `quyen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quyen` (
  `MaQuyen` int NOT NULL AUTO_INCREMENT,
  `TenQuyen` varchar(100) NOT NULL,
  `TrangThai` int DEFAULT NULL,
  `Mota` text,
  PRIMARY KEY (`MaQuyen`),
  UNIQUE KEY `TenQuyen` (`TenQuyen`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quyen`
--

LOCK TABLES `quyen` WRITE;
/*!40000 ALTER TABLE `quyen` DISABLE KEYS */;
INSERT INTO `quyen` VALUES (1,'Admin',1,'Quyền toàn quyền'),(2,'Nhân viên bán hàng',1,'Quyền bán hàng'),(3,'Nhân viên kho',1,'Quyền quản lý kho'),(4,'Kế toán',1,'Quyền xem báo cáo'),(5,'Quản lý',1,'Quyền quản lý tổng');
/*!40000 ALTER TABLE `quyen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `quyen_chucnang`
--

DROP TABLE IF EXISTS `quyen_chucnang`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quyen_chucnang` (
  `MaQuyen` int DEFAULT NULL,
  `MaChucNang` int DEFAULT NULL,
  KEY `MaQuyen` (`MaQuyen`),
  KEY `MaChucNang` (`MaChucNang`),
  CONSTRAINT `quyen_chucnang_ibfk_1` FOREIGN KEY (`MaQuyen`) REFERENCES `quyen` (`MaQuyen`),
  CONSTRAINT `quyen_chucnang_ibfk_2` FOREIGN KEY (`MaChucNang`) REFERENCES `chucnang` (`MaChucNang`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quyen_chucnang`
--

LOCK TABLES `quyen_chucnang` WRITE;
/*!40000 ALTER TABLE `quyen_chucnang` DISABLE KEYS */;
INSERT INTO `quyen_chucnang` VALUES (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),(1,15),(1,16),(1,17),(1,18),(1,19),(1,20),(1,21),(1,22),(1,23),(1,24),(1,25),(1,26),(1,27),(1,28),(1,29),(1,30),(1,31),(1,32),(2,2),(2,5),(3,4),(4,5),(5,1),(5,2),(5,3),(5,4),(5,5);
/*!40000 ALTER TABLE `quyen_chucnang` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sanpham`
--

DROP TABLE IF EXISTS `sanpham`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sanpham` (
  `MaSP` int NOT NULL AUTO_INCREMENT,
  `TenSP` varchar(100) NOT NULL,
  `Gia` decimal(12,2) NOT NULL,
  `Anh` varchar(50) DEFAULT NULL,
  `SLDuKien` int DEFAULT NULL,
  `TrangThai` int DEFAULT NULL,
  `MaLoai` int DEFAULT NULL,
  PRIMARY KEY (`MaSP`),
  KEY `MaLoai` (`MaLoai`),
  CONSTRAINT `sanpham_ibfk_1` FOREIGN KEY (`MaLoai`) REFERENCES `loai` (`MaLoai`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sanpham`
--

LOCK TABLES `sanpham` WRITE;
/*!40000 ALTER TABLE `sanpham` DISABLE KEYS */;
INSERT INTO `sanpham` VALUES (1,'Trà sữa truyền thống',25000.00,'tra_sua_truyen_thong.png',50,1,1),(2,'Trà sữa matcha truyền thống',30000.00,'tra_sua_matcha_truyen_thong.png',40,1,2),(3,'Trà sữa socola',28000.00,'tra_sua_socola.png',30,1,3),(4,'Trà sữa caramel',32000.00,'tra_sua_caramel.png',20,1,3),(5,'Trà sữa dâu',27000.00,'tra_sua_dau.png',25,1,4),(6,'Trà sữa khoai môn',30000.00,'tra_sua_khoai_mon.png',15,1,5),(7,'Trà sữa hạt dẻ',35000.00,'tra_sua_hat_de.png',10,1,5),(8,'Trà sữa bạc hà',33000.00,'tra_sua_bac_ha.png',12,1,1),(9,'Trà sữa matcha socola',36000.00,'tra_sua_matcha_socola.png',8,1,2),(10,'Trà sữa hoàng kim',40000.00,'tra_sua_hoang_kim.png',5,1,6);
/*!40000 ALTER TABLE `sanpham` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sanpham_khuyenmai`
--

DROP TABLE IF EXISTS `sanpham_khuyenmai`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sanpham_khuyenmai` (
  `MaSP` int DEFAULT NULL,
  `MaCTKhuyenMai` int DEFAULT NULL,
  KEY `MaSP` (`MaSP`),
  KEY `MaCTKhuyenMai` (`MaCTKhuyenMai`),
  CONSTRAINT `sanpham_khuyenmai_ibfk_1` FOREIGN KEY (`MaSP`) REFERENCES `sanpham` (`MaSP`),
  CONSTRAINT `sanpham_khuyenmai_ibfk_2` FOREIGN KEY (`MaCTKhuyenMai`) REFERENCES `ctkhuyenmai` (`MaCTKhuyenMai`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sanpham_khuyenmai`
--

LOCK TABLES `sanpham_khuyenmai` WRITE;
/*!40000 ALTER TABLE `sanpham_khuyenmai` DISABLE KEYS */;
INSERT INTO `sanpham_khuyenmai` VALUES (1,1),(2,2),(3,3),(4,4),(5,5),(6,6),(7,7),(8,8),(9,9),(10,10);
/*!40000 ALTER TABLE `sanpham_khuyenmai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `size`
--

DROP TABLE IF EXISTS `size`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `size` (
  `MaSize` int NOT NULL AUTO_INCREMENT,
  `TenSize` varchar(50) NOT NULL,
  `PhuThu` int DEFAULT '0',
  PRIMARY KEY (`MaSize`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `size`
--

LOCK TABLES `size` WRITE;
/*!40000 ALTER TABLE `size` DISABLE KEYS */;
INSERT INTO `size` VALUES (1,'S',10000),(2,'M',15000),(3,'L',20000),(4,'XL',25000);
/*!40000 ALTER TABLE `size` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `taikhoan`
--

DROP TABLE IF EXISTS `taikhoan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `taikhoan` (
  `MaTK` int NOT NULL AUTO_INCREMENT,
  `TenTaiKhoan` varchar(50) NOT NULL,
  `anh` varchar(50) DEFAULT NULL,
  `MatKhau` varchar(50) DEFAULT NULL,
  `TrangThai` int DEFAULT NULL,
  `MaQuyen` int DEFAULT NULL,
  PRIMARY KEY (`MaTK`),
  UNIQUE KEY `TenTaiKhoan` (`TenTaiKhoan`),
  KEY `MaQuyen` (`MaQuyen`),
  CONSTRAINT `taikhoan_ibfk_1` FOREIGN KEY (`MaQuyen`) REFERENCES `quyen` (`MaQuyen`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `taikhoan`
--

LOCK TABLES `taikhoan` WRITE;
/*!40000 ALTER TABLE `taikhoan` DISABLE KEYS */;
INSERT INTO `taikhoan` VALUES (1,'Nguyen Van A','nv_banhang1.jpg','0901234561',1,2),(2,'Nguyen Van B','nv_banhang2.jpg','0901234562',1,2),(3,'Nguyen Van C','nv_banhang3.jpg','0901234563',1,2),(4,'Tran Thi D','nv_kho1.jpg','0901234564',1,3),(5,'Le Van E','nv_kho2.jpg','0901234565',1,3),(6,'Pham Thi F','admin1.jpg','0901234566',1,1),(7,'Nguyen Van G','kt1.jpg','0901234567',1,4),(8,'Tran Thi H','ql1.jpg','0901234568',1,5),(9,'Le Van I','nv_banhang4.jpg','0901234569',1,2),(10,'Pham Thi K','nv_banhang5.jpg','0901234570',1,2);
/*!40000 ALTER TABLE `taikhoan` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-10-25 22:42:40
