use QL_DH_GH
go
CREATE PROC Sp_KH_MUASP
	@IDMON CHAR(5),
	@IDDOITAC CHAR(5),
	@IDCHINHANH CHAR(5), 
	@SoLuong int,
	@IDDonHang Char(5),
	@MaSoHopDong Char(5),
	@IDKH Char(5),
	@IDTaiXe Char(5) ,
	@HinhThucThanhToan Nvarchar(15),
	@NgayTao date,
	@DiaChiGiaoHang Nvarchar(100),
	@PhiVanChuyen money,
	@PhiSanPham money,
	@TrangThaiVanChuyen Nvarchar(30),
	@TrangThaiDonHang Nvarchar(30)
	 
AS
	DECLARE @TINHTRANGMON NVARCHAR(30) = (SELECT TINHTRANGMON
							FROM QLTHUCDON 
							WHERE @IDMON = IDMon AND @IDDOITAC = IDDoiTac AND @IDCHINHANH = IDChiNhanh)
	IF (@TINHTRANGMON = N'Hết hàng')
	BEGIN
		PRINT N'Món ăn này đã hết hàng'
		RETURN 0;
	END
	insert donhang values (@IDDonHang,@MaSoHopDong,
	@IDKH,@IDTaiXe,@HinhThucThanhToan,@NgayTao,@DiaChiGiaoHang,
	@PhiVanChuyen,@PhiSanPham,@TrangThaiVanChuyen,@TrangThaiDonHang)
	insert qldonhang values (@IDMon,@IDDonHang,@Soluong,@PhiSanPham )
	commit tran
	RETURN 1;
GO
--đối tác thêm món
CREATE PROC Sp_DT_THEMSP
	@IDMON CHAR(5),
	@TENMON nvarCHAR(50),
	@GiaMon money, 
	@IDDoiTac CHAR(5),
	@IDChiNhanh CHAR(5),
	@MieuTaMon Nvarchar(50),
	@TinhTrangMon Nvarchar(50),
	@TuyChonChoMon Nvarchar(30)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS(SELECT*
				FROM DOITAC DT, CHINHANH CN, MON M
				WHERE DT.IDDoiTac=@IDDoiTac AND CN.IDChiNhanh=@IDChiNhanh AND m.TenMon=@TENMON)
			BEGIN
				PRINT N'MON DA TON TAI'
				ROLLBACK TRAN
				RETURN 1
			END 

		IF NOT EXISTS(SELECT*
				FROM DOITAC DT
				WHERE DT.IDDoiTac=@IDDoiTac)
			BEGIN
				PRINT N'DOI TAC KHONG TON TAI'
				ROLLBACK TRAN
				RETURN 1
			END
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
	INSERT MON(IDMon,TenMon,Rating,GiaMon) VALUES (@IDMON,@TENMON,0,@GiaMon)
	INSERT QLTHUCDON(IDDoiTac,IDMon,IDChiNhanh,MieuTaMon,TinhTrangMon,TuyChonChoMon) VALUES (@IDDoiTac,@IDMON,@IDChiNhanh,@MieuTaMon,@TinhTrangMon,@TuyChonChoMon)
	COMMIT TRAN
GO

-- Đối tác cập nhật giá sản phẩm
CREATE PROC DT_UPDATE_GiASP
	@IDMON CHAR(5),
	@GIAMOI MONEY
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS(SELECT *
					FROM MON
					WHERE IDMON = @IDMON)
		BEGIN
			PRINT N'MÓN ĂN KHÔNG TỒN TẠI'
			ROLLBACK TRAN
			RETURN 1
		END 

		IF @GIAMOI = 0
		BEGIN
			PRINT N'GIÁ MÓN KHÔNG THỂ BẰNG 0'
			ROLLBACK TRAN 
			RETURN 1
		END
		-----
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
COMMIT TRAN
RETURN 0
GO

-- lấy thông tin sản phẩm
CREATE PROC Sp_KH_XEMSP
	@IDDOITAC CHAR(5)
AS
BEGIN TRAN
SET TRANSACTION ISOLATION LEVEL Serializable
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM CHINHANH WHERE IDDoiTac = @IDDOITAC)
		BEGIN
			PRINT N'ĐỐI TÁC KHÔNG TỒN TẠI'
		END
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
	END CATCH
	SELECT  M.TENMON, QLTD.TinhTrangMon, M.GiaMon, CN.DIACHI, M.IDMon 
                FROM MON M, QLTHUCDON QLTD, CHINHANH CN
                WHERE QLTD.IDChiNhanh = CN.IDCHINHANH
                AND QLTD.IDDoiTac = CN.IDDoiTac
                AND QLTD.IDDoiTac = @IDDOITAC AND QLTD.IDMon = M.IDMon
COMMIT TRAN
GO
--
CREATE PROC SP_DANGNHAPKH
        @IDKH CHAR(5),
        @USERNAME CHAR(10),
        @PASS CHAR(20),
        @isNV BIT,
        @isQT BIT
AS
BEGIN 
        SET @IDKH = 'NULL'
        IF NOT EXISTS (SELECT IDTaiKhoan FROM TAIKHOAN TK WHERE TK.Username = @USERNAME and TK.Password = @PASS )
        BEGIN
            PRINT N'Tài khoản hoặc mật khẩu không đúng'
            RETURN 1
        END

        SET @IDKH = (SELECT IDTaiKhoan FROM TAIKHOAN TK WHERE TK.Username = @USERNAME and TK.Password = @PASS)

        SET @isNV = (SELECT isNV FROM TAIKHOAN TK WHERE TK.Username = @USERNAME and TK.Password = @PASS)

        SET @isQT = (SELECT isQuanTri FROM TAIKHOAN TK WHERE TK.Username = @USERNAME and TK.Password = @PASS)

        IF(@IDKH != 'NULL')
        BEGIN
            PRINT 'Đăng nhập thành công'
            RETURN 0
        END
        ELSE RETURN 1
END


--Duyệt hợp đồng
GO
CREATE PROC SP_DUYETHOPDONG1
        @MSHD CHAR(5)
AS
BEGIN TRAN
	BEGIN TRY
		
		IF NOT EXISTS(SELECT MaSoHopDong FROM HOPDONG HD with (nolock) WHERE HD.MaSoHopDong = @MSHD)
		BEGIN
			PRINT N'Hợp đồng không tồn tại'
			ROLLBACK TRAN
			RETURN 1
		END

	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
UPDATE HOPDONG with(xlock)
SET TrangThaiDuyet = 'Y'
WHERE MaSoHopDong = @MSHD
PRINT 'Hợp đồng được duyệt'
COMMIT TRAN
RETURN 0


--Khôgn duyệt hợp đồng
GO
CREATE PROC SP_DUYETHOPDONG2
        @MSHD CHAR(5)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS(SELECT MaSoHopDong FROM HOPDONG HD with(nolock) WHERE HD.MaSoHopDong = @MSHD)
		BEGIN
			PRINT N'Hợp đồng không tồn tại'
			ROLLBACK TRAN
			RETURN 1
		END
		
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 1
	END CATCH
UPDATE HOPDONG with(xlock)
SET TrangThaiDuyet = 'N'
WHERE MaSoHopDong = @MSHD
PRINT 'Hợp đồng không được duyệt'
COMMIT TRAN
RETURN 0
go
--Đổi mật khẩu tài khoản nhân viên --
CREATE PROC CHANGE_PASSWORD
	@IDTaiKhoan Char(5) ,
	@Password Char(20)
	
AS
BEGIN TRANSACTION
		BEGIN TRY
			IF NOT EXISTS ( SELECT * FROM TAIKHOAN WHERE TAIKHOAN.IDTaiKhoan = @IDTaiKhoan)
				BEGIN
					PRINT N'TÀI KHOẢN KHÔNG TỒN TẠI'
					ROLLBACK TRANSACTION
					RETURN 1
				END
		END TRY
		BEGIN CATCH
			PRINT N'LỖI HỆ THỐNG'
			ROLLBACK
			RETURN 1
		END CATCH
	UPDATE TAIKHOAN
	SET Password = @Password
	WHERE IDTaiKhoan = @IDTaiKhoan
	COMMIT TRANSACTION
	RETURN 0
go

--Lấy thông tin tài khoản nhân viên,--
CREATE PROC Sp_NV_LayTongTinTK
	@USERNAME VARCHAR(10),
	@PASSWORD VARCHAR(20)
AS
BEGIN TRAN
SET TRANSACTION ISOLATION LEVEL Repeatable read
	BEGIN TRY	
	DECLARE @IDTaiKhoan CHAR(5)
	SET @IDTaiKhoan  = 'NULL'
	SET @IDTaiKhoan  = (SELECT   TK.IDTaiKhoan        
                FROM TAIKHOAN tk
                WHERE TK.Username=@USERNAME AND TK.Password=@PASSWORD)
	--kiểm tra tài khoản có tồn tại hay không
	IF @IDTaiKhoan  = 'NULL'
	BEGIN
		PRINT N'Tài Khoản Không Tồn Tại'
		ROLLBACK TRAN
		RETURN 1
	END	
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRANSACTION
		RETURN 1

	END CATCH
	SELECT * FROM TAIKHOAN WHERE TAIKHOAN.Username=@USERNAME AND TAIKHOAN.Password=@PASSWORD AND TAIKHOAN.IDTaiKhoan=@IDTaiKhoan
	COMMIT TRANSACTION
GO
--Đổi thông tin tài khoản nhân viên
CREATE PROC Sp_KH_DoiThongTinTK
	@IDKH Char(5),
	@TenKH Nvarchar(50),
	@SDTKH Char(12),
	@DiaChiKH Nvarchar(100),
	@EmailKH Char(20)
AS
BEGIN
	--kiểm tra mã nhân viên có tồn tại hay không
	IF NOT EXISTS (SELECT * 
				FROM KHACHHANG KH
				WHERE KH.IDKH=@IDKH)
	BEGIN
		PRINT  N' Mã khách hàng không Tồn Tại'
		ROLLBACK TRAN
		RETURN 0
	END	

	-- xử lí Update
	UPDATE KHACHHANG
	SET TenKH = @TenKH, SDTKH = @SDTKH, DiaChiKH = @DiaChiKH, EMAILKH = @EMAILKH
	WHERE IDKH=@IDKH
	COMMIT TRAN
	RETURN 1
END
GO
--PROCEDURE ĐỐI TÁC THÊM CHI NHÁNH
CREATE PROC sp_DT_ThemChiNhanh 
	@IDChiNhanh Char(5),
	@IDDoiTac Char(5),
	@DiaChi Nvarchar(100),
	@TenChiNhanh Nvarchar(50),
	@TGHD time,
	@TTCH Nvarchar(30)
AS

BEGIN TRAN
	BEGIN TRY
	--Kiểm tra địa chỉ có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE IDDoiTac = @IDDoiTac AND DIACHI = @diachi))
			begin
			rollback tran
			RETURN  0
			end

	-- Kiểm tra mã chi nhánh có trùng hay không
	IF(EXISTS(SELECT * FROM CHINHANH WHERE IDCHINHANH = @IDchinhanh and IDDoiTac = @IDDoiTac))
			begin
			rollback tran
			RETURN  0
			end
	END TRY
	BEGIN CATCH
		PRINT N'LỖI HỆ THỐNG'
		ROLLBACK TRAN
		RETURN 0
	END CATCH
	
	INSERT INTO CHINHANH
	VALUES
		(@IDChiNhanh,@IDDoiTac,@DiaChi,@TenChiNhanh,@TGHD,@TTCH)

	UPDATE DOITAC
	SET SLChiNhanh = SLChiNhanh + 1
	WHERE IDDoiTac = @IDDoiTac
COMMIT TRAN
	return 1
GO

--PROCEDURE khách hàng xem danh sách đối tác


CREATE PROC sp_KH_XemDSDoiTac
AS
BEGIN TRAN
	BEGIN TRY
		SELECT IDDoiTac,IDKhuVuc,EmailDT,
				TenQuan,NguoiDaiDien,SLChiNhanh,
				SLDonHang,DCKinhDoanh,SDTDT,MaSoThue,
				SoTaiKhoanDT,NganHang
	FROM DOITAC
	END TRY
	BEGIN CATCH
			PRINT N'LỖI HỆ THỐNG'
			ROLLBACK TRAN
			RETURN 0
	END CATCH
COMMIT TRAN
return 1
GO

CREATE PROC sp_KH_XemDSChiNhanh @IDDoiTac char(5)
AS
BEGIN TRAN
SET TRANSACTION ISOLATION LEVEL Serializable
	BEGIN TRY
	if not exists(select*
	from CHINHANH cn
	where cn.IDDoiTac=@IDDoiTac)
	BEGIN
		begin
			rollback tran
			RETURN  -1
			end
	END
	END TRY
	BEGIN CATCH
			PRINT N'LỖI HỆ THỐNG'
			ROLLBACK TRAN
			RETURN 0
	END CATCH
	SELECT CN.IDChiNhanh,CN.IDDoiTac,CN.TenChiNhanh,CN.DiaChi,CN.TGHD,CN.TTCH
	FROM CHINHANH CN
	where cn.IDDoiTac=@IDDoiTac
COMMIT TRAN
return 1
GO

CREATE PROC sp_XoaChiNhanh @IDChiNhanh char(5),@IDDoiTac char(5)
AS
BEGIN TRAN
	BEGIN TRY
	if not exists(select*
	from CHINHANH CN
	where cn.IDDoiTac=@IDDoiTac and cn.IDChiNhanh=@IDChiNhanh)
	BEGIN
		begin
			rollback tran
			RETURN  -1
			end
	END
	END TRY
	BEGIN CATCH
			PRINT N'LỖI HỆ THỐNG'
			ROLLBACK TRAN
			RETURN 0
	END CATCH
	delete qlthucdon where IDDoiTac=@IDDoiTac and IDChiNhanh=@IDChiNhanh
	delete CHINHANH where IDDoiTac=@IDDoiTac and IDChiNhanh=@IDChiNhanh
COMMIT TRAN
return 1
GO

CREATE PROCEDURE DT_CAPNHATTRANGTHAIDONHANG_DH
@IDDonHang varchar(20), 
@TrangThaiDonHang nvarchar(50)
AS
BEGIN TRANSACTION 
	
		BEGIN TRY
		IF NOT EXISTS ( SELECT IDDonHang FROM DONHANG WHERE IDDonHang=@IDDonHang)
			BEGIN
				PRINT N'Đơn hàng không tồn tại'
				ROLLBACK TRANSACTION
				RETURN 1 
			END
			
		END TRY
		BEGIN CATCH
			PRINT N'Lỗi hệ thống'
			ROLLBACK TRANSACTION
			RETURN 1
		END CATCH
			SELECT TrangThaiDonHang FROM DONHANG WHERE IDDonHang = @IDDonHang
			UPDATE DONHANG SET TrangThaiDonHang = @TrangThaiDonHang WHERE IDDonHang = @IDDonHang
			-- Waiting for system's update--
			
			COMMIT TRANSACTION
			RETURN 0	
GO