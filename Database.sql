USE master

IF DB_ID('QL_DH_GH') IS NOT NULL
	DROP DATABASE QL_DH_GH
GO

CREATE DATABASE QL_DH_GH
GO

USE QL_DH_GH

GO 

CREATE TABLE CHINHANH
(
	IDChiNhanh Char(5),
	IDDoiTac Char(5) NOT NULL,
	DiaChi Nvarchar(100) NOT NULL,
	TenChiNhanh Nvarchar(50) NOT NULL,
	TGHD Time NOT NULL,
	TTCH Nvarchar(30) NOT NULL
	PRIMARY KEY(IDChiNhanh)
)

CREATE TABLE DOITAC
(
	IDDoiTac Char(5),
	IDKhuVuc Char(5) NOT NULL,
	IDMON char(5),
	EmailDT Nvarchar(50) NOT NULL UNIQUE,
	TenQuan Nvarchar(30) NOT NULL,
	NguoiDaiDien Nvarchar(30) NOT NULL UNIQUE,
	SLChiNhanh int NOT NULL check(SLChiNhanh>0),
	SLDonHang int NOT NULL check(SLDonHang>0),
	DCKinhDoanh Nvarchar(100) NOT NULL,
	SDTDT Char(12) NOT NULL UNIQUE,
	MaSoThue Char(5) NOT NULL UNIQUE,
	SoTaiKhoanDT Char(20) NOT NULL UNIQUE,
	NganHang nvarchar(100) NOT NULL
	PRIMARY KEY(IDDoiTac)
)

CREATE TABLE KHUVUC
(
	IDKhuVuc Char(5),
	ThanhPho Nvarchar(10) NOT NULL UNIQUE,
	Quan Nvarchar(50) NOT NULL,
	PRIMARY KEY(IDKhuVuc)
)

CREATE TABLE MON
(
	IDMon Char(5) ,
	TenMon Nvarchar(80) NOT NULL unique,
	Rating int check(Rating>=0 and Rating<=5),
	GiaMon money NOT NULL
	PRIMARY KEY(IDMon)
)

CREATE TABLE QLTHUCDON
(
	IDDoiTac Char(5),
	IDMon Char(5),
	IDChiNhanh Char(5),
	MieuTaMon Nvarchar(50) NOT NULL,
	TinhTrangMon Nvarchar(30) NOT NULL,
	TuyChonChoMon Nvarchar(30) NOT NULL
	PRIMARY KEY(IDDoiTac,IDMon,IDChiNhanh)
)

CREATE TABLE TAIXE
(
	IDTaiXe Char(5),
	IDTaiKhoan Char(5) NOT NULL,
	IDKhuVuc Char(5) NOT NULL,
	TenTX Nvarchar(50) NOT NULL,
	CMND Char(20) NOT NULL UNIQUE,
	BienSo Char(20) NOT NULL UNIQUE,
	STKTX Char(20) NOT NULL UNIQUE,
	NganHang Char(20) NOT NULL,
	SDTTX Char(12) NOT NULL UNIQUE,
	EmailTX Char(20) NOT NULL UNIQUE,
	DiaChiTX Nvarchar(50) NOT NULL 
	PRIMARY KEY(IDTaiXe)
)

CREATE TABLE TAIKHOAN
(
	IDTaiKhoan Char(5),
	Username Char(10) NOT NULL,
	Password Char(20) NOT NULL UNIQUE,
	isNV bit NOT NULL,
	isQuanTri bit NOT NULL,
	PRIMARY KEY(IDTaiKhoan)
)
CREATE TABLE KHACHHANG
(
	IDKH Char(5),
	IDTaiKhoan Char(5) NOT NULL UNIQUE,
	TenKH Nvarchar(50) NOT NULL,
	SDTKH Char(12) NOT NULL UNIQUE,
	DiaChiKH Nvarchar(100) NOT NULL UNIQUE,
	EmailKH Char(20) NOT NULL UNIQUE,
	PRIMARY KEY(IDKH)
)
CREATE TABLE HOPDONG
(
	MaSoHopDong Char(5),
	IDDoiTac Char(5) NOT NULL UNIQUE,
	IDTaiKhoan Char(5) NOT NULL UNIQUE,
	NgayLap date NOT NULL,
	TenQuan nvarchar(30) NOT NULL,
	NguoiDaiDien nvarchar(30) NOT NULL UNIQUE,
	PhiKichHoat money NOT NULL check(PhiKichHoat=1000000),
	PhiHoaHong money NOT NULL,
	NgayBatDau date NOT NULL,
	NgayKetThuc date NOT NULL,
	TrangThaiDuyet Char(3) check(TrangThaiDuyet='Y' or TrangThaiDuyet='N')
	PRIMARY KEY(MaSoHopDong)
)

CREATE TABLE QLDONHANG
(
	IDMon Char(5),
	IDDonHang Char(5),
	SLSanPham int NOT NULL,
	ThanhTien money NOT NULL
	PRIMARY KEY (IDMon,IDDonHang )
)
CREATE TABLE  DONHANG
(
	 IDDonHang Char(5),
	 MaSoHopDong Char(5) NOT NULL,
	 IDKH Char(5) NOT NULL,
	 IDTaiXe Char(5) ,
	 HinhThucThanhToan Nvarchar(15) NOT NULL,
	 NgayTao date NOT NULL,
	 DiaChiGiaoHang Nvarchar(100) NOT NULL,
	 PhiVanChuyen money NOT NULL,
	 PhiSanPham money NOT NULL,
	 TrangThaiVanChuyen Nvarchar(30) NOT NULL check(TrangThaiVanChuyen=N'Đang giao' or TrangThaiVanChuyen=N'Đã giao' or TrangThaiVanChuyen=N'Giao thành công'),
	 TrangThaiDonHang Nvarchar(30) NOT NULL check(TrangThaiDonHang=N'Đã tiếp nhận' or TrangThaiDonHang=N'đang chuẩn bị'or TrangThaiDonHang=N'Đã xử lý')
	 PRIMARY KEY (IDDonHang)
)

ALTER TABLE QLTHUCDON
ADD 
	CONSTRAINT FK_QLTD_CN
	FOREIGN KEY (IDCHINHANH)
	REFERENCES CHINHANH(IDCHINHANH),

	CONSTRAINT FK_QLTD_DT
	FOREIGN KEY (IDDOITAC)
	REFERENCES DOITAC(IDDOITAC),

	CONSTRAINT FK_QLTD_MON
	FOREIGN KEY (IDMON)
	REFERENCES MON(IDMON)

ALTER TABLE CHINHANH
ADD 
	CONSTRAINT FK_CN_DT
	FOREIGN KEY (IDDOITAC)
	REFERENCES DOITAC(IDDOITAC)

ALTER TABLE DOITAC
ADD 
	CONSTRAINT FK_DT_KV
	FOREIGN KEY (IDKHUVUC)
	REFERENCES KHUVUC(IDKHUVUC)


ALTER TABLE TAIXE
ADD 
	CONSTRAINT FK_TX_TK
	FOREIGN KEY (IDTAIKHOAN)
	REFERENCES TAIKHOAN(IDTAIKHOAN),

	CONSTRAINT FK_TX_KV
	FOREIGN KEY (IDKHUVUC)
	REFERENCES KHUVUC(IDKHUVUC)

ALTER TABLE KHACHHANG
ADD 
	CONSTRAINT FK_KH_TK
	FOREIGN KEY (IDTAIKHOAN)
	REFERENCES TAIKHOAN(IDTAIKHOAN)

ALTER TABLE QLDONHANG
ADD 
	CONSTRAINT FK_QLDH_MON
	FOREIGN KEY (IDMON)
	REFERENCES MON(IDMON),

	CONSTRAINT FK_QLDH_DH
	FOREIGN KEY (IDDONHANG)
	REFERENCES DONHANG(IDDONHANG)

ALTER TABLE DONHANG
ADD 
	CONSTRAINT FK_DH_HD
	FOREIGN KEY (MASOHOPDONG)
	REFERENCES HOPDONG(MASOHOPDONG),

	CONSTRAINT FK_QLDH_KH
	FOREIGN KEY (IDKH)
	REFERENCES KHACHHANG(IDKH),

	CONSTRAINT FK_QLDH_TX
	FOREIGN KEY (IDTAIXE)
	REFERENCES TAIXE(IDTAIXE)

ALTER TABLE HopDong
ADD 
	CONSTRAINT FK_x
	FOREIGN KEY (iddoitac)
	REFERENCES Doitac(iddoitac),
	CONSTRAINT FK_y
	FOREIGN KEY (IDTaiKhoan)
	REFERENCES taikhoan(idtaikhoan)


	
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES ('1',N'Quận 1', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('2',N'Quận 2', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('3',N'Quận 3', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('4',N'Quận 4', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('5',N'Quận 5', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('6',N'Quận 6', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('7',N'Quận 7', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('8',N'Quận 8', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('9',N'Quận 9', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('10',N'Quận 10', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('11',N'Quận 11', N'Thành Phố Hồ Chí Minh')
INSERT INTO KHUVUC(IDKhuVuc,ThanhPho,Quan) VALUES('12',N'Quận 12', N'Thành Phố Hồ Chí Minh')
GO

INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('QT1','QT1','0123456789', 0,1)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('QT2','QT2','1234567890', 0,1)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('KH1','KH1','2345678901', 0,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('KH2','KH2','3456789012', 0,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('TX1','TX1','5678901234', 0,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('TX2','TX2','6789012345', 0,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('NV1','NV1','7890123456', 1,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('NV2','NV2','8901234567', 1,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('DT1','DT1','9012345678', 0,0)
INSERT INTO TAIKHOAN(IDTaiKhoan,Username,Password,isNV,isQuanTri) VALUES('DT2','DT2','0234567891', 0,0)
go

INSERT INTO DOITAC(IDDoiTac ,IDKhuVuc,EmailDT,
	TenQuan,NguoiDaiDien,SLChiNhanh,SLDonHang,
	DCKinhDoanh,SDTDT,MaSoThue ,SoTaiKhoanDT,NganHang)values
	('DT001','1','mixifood@gmail.com',N'MixiFood',N'Độ Mixi',1,100,N'123 Yên Lãng, Tp. Hồ Chí Minh','0123456789','MST01','901704190','viet capital bank'),
	('DT002','3','comnieu@gmail.com',N'Cơm Niêu',N'Trần Văn Cường',2,50,N'18 Chế Lan Viên, Tp. Hồ Chí Minh','0123456798','MST02','901704191','viet capital bank')
go

INSERT INTO HOPDONG(MaSoHopDong,IDDoiTac,
	IDTaiKhoan,NgayLap,TenQuan,
	NguoiDaiDien,PhiKichHoat,
	PhiHoaHong,NgayBatDau,NgayKetThuc,
	TrangThaiDuyet)VALUES
	('HD001','DT001','DT1','2022-11-28',
	'Quan Com','Martinon',1000000,10,
	'2022-11-29','2024-11-29','Y')
INSERT INTO HOPDONG(MaSoHopDong,IDDoiTac,
	IDTaiKhoan,NgayLap,TenQuan,
	NguoiDaiDien,PhiKichHoat,
	PhiHoaHong,NgayBatDau,NgayKetThuc,
	TrangThaiDuyet)VALUES('HD002','DT002','DT2','2022-10-28',
	'Quan Com','Braams',1000000,10,
	'2022-11-29','2024-11-29','N')

INSERT INTO TaiXe(IDTaiXe,IDTaiKhoan,IDKhuVuc,
	TenTX,CMND,BienSo,STKTX,NganHang,SDTTX,EmailTX,
	DiaChiTX)VALUES
	('TX1','TX1','1',N'16 Typh','251211111','16F1 - 11638','901704100','viet capital bank','0125478369','typh@gmail.com', N'Hải Phòng City'),
	('TX2','TX2','2',N'Lil Wuyn','251222222','49B1 - 02868','901704101','viet capital bank','0125478368','wuyn@gmail.com', N'Đà Lạt City')

INSERT INTO KHACHHANG (IDKH,IDTaiKhoan,TenKH,SDTKH,
	DiaChiKH ,EmailKH)VALUES
	('KH001','KH1',N'Nguyễn Văn A', '0157482369','Q7, SG','mtp@mtp.com'),
	('KH002','KH2',N'Nguyễn Văn B','0235698741','Hàng Mã, Hà Nội','gd@gd.com')

INSERT INTO DONHANG(IDDonHang,MaSoHopDong,IDKH,
	IDTaiXe,HinhThucThanhToan,NgayTao,
	DiaChiGiaoHang,PhiVanChuyen,PhiSanPham,
	TrangThaiVanChuyen,TrangThaiDonHang)VALUES
	('DH001','HD002','KH001','TX2',N'trực tiếp','2022-11-01',N'44 Cach Mang Thang Tam, phuong 2, quan Tan Binh, Thanh pho Ho Chi Minh',40000,300000,N'Đang giao',N'Đã xử lý'),
	('DH002','HD001','KH002','TX2',N'trực tiếp','2022-05-01',N'98 Ly Chinh Thang, phuong 10, quan Binh Thanh, Thanh pho Ho Chi Minh',30000,200000,N'Giao thành công',N'Đã tiếp nhận'),
	('DH003','HD001','KH002','TX2',N'trực tiếp','2022-05-01',N'98 Ly Chinh Thang, phuong 10, quan Binh Thanh, Thanh pho Ho Chi Minh',30000,200000,N'Giao thành công',N'Đã tiếp nhận')

INSERT INTO MON(IDMon,
	TenMon,
	Rating,
	GiaMon)values
	('1',N'Cơm',4,50000),
	('2',N'Nước ngọt',3,20000),
	('3',N'Mì',4,30000),
	('4',N'Trà sữa',4,25000),
	('5',N'Phở',4,40000),
	('6',N'Bánh mì',4,15000),
	('7',N'Xôi',4,15000),
	('8',N'Bánh bao',4,15000),
	('9',N'Bánh canh',4,25000),
	('10',N'Mì xào',4,25000)

INSERT INTO QLDONHANG(IDMon,IDDonHang,SLSanPham ,
ThanhTien)values
	('1','DH001',6,300000),
	('8','DH001',6,300000),
	('9','DH001',6,300000),
	('10','DH001',6,300000),
	('2','DH002',2,100000),
	('3','DH002',4,100000),
	('4','DH003',1,20000),
	('5','DH003',1,30000),
	('6','DH003',2,50000),
	('7','DH003',2,100000)

INSERT INTO CHINHANH(IDDoiTac,IDChiNhanh,DiaChi,
	TenChiNhanh,TGHD,TTCH)values
	('DT001','1',N'227 Nguyễn Văn Cừ Quận 5 TP.Hồ Chí Minh',N'Mixifood','07:00:00',N'Mở cửa'),
	('DT002','2',N'228 Nguyễn Văn Cừ Quận 5 TP.Hồ Chí Minh',N'Cơm niêu','07:00:00',N'Mở cửa'),
	('DT002','3',N'229 Nguyễn Văn Cừ Quận 5 TP.Hồ Chí Minh',N'Cơm niêu 1','07:00:00',N'Mở cửa')

INSERT INTO QLTHUCDON(IDDoiTac,IDMon,IDChiNhanh,
	MieuTaMon,TinhTrangMon,TuyChonChoMon)values
	('DT001','1','1',N'Cơm kèm thức ăn theo tùy chọn của khách',N'Còn hàng','Thịt,Cá,Canh'),
	('DT002','2','2',N'Các loại nước giải khát',N'Còn hàng',''),
	('DT002','7','3',N'Xôi và các loại đồ ăn kèm',N'Còn hàng','Xôi mặn hoặc xôi ngọt')

