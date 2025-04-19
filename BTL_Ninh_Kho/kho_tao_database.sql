create database BTL_KHO_TKPM

CREATE TABLE tblQuyen (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenquyen NVARCHAR(225) NOT NULL
);


CREATE TABLE tblNhanvien (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenNV NVARCHAR(225),
    dNgaySinh DATE,
    sGioitinh NVARCHAR(10),
    sQuequan NVARCHAR(255),
    dNgayvaolam DATETIME DEFAULT GETDATE(),
    sSDT VARCHAR(20),
    sCCCD VARCHAR(20),
    bTrangthai BIT DEFAULT 0,
    quyen INT FOREIGN KEY REFERENCES tblQuyen(ID)
);


CREATE TABLE tblKho (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenkho NVARCHAR(225) NOT NULL,
    sDiachi NVARCHAR(255),
    iSucchua INT,
    fDientichkho FLOAT,
    iTinhtrangkho BIT DEFAULT 0,
    iQuanly INT FOREIGN KEY REFERENCES tblNhanvien(ID)
);


CREATE TABLE tblLoaihang (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenloaihang NVARCHAR(225) NOT NULL
);


CREATE TABLE tblDonvitinh (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTendonvitinh NVARCHAR(50) NOT NULL
);


CREATE TABLE tblHanghoa (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenhanghoa NVARCHAR(225) NOT NULL,
    iLoaihang INT FOREIGN KEY REFERENCES tblLoaihang(ID),
    iDonvitinh INT FOREIGN KEY REFERENCES tblDonvitinh(ID)
);


CREATE TABLE tblNhacungcap (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTenNCC NVARCHAR(225) NOT NULL,
    sDiachi NVARCHAR(225),
    sSDT NVARCHAR(15),
    sEmail NVARCHAR(225)
);


CREATE TABLE tblTrangthai (
    ID INT PRIMARY KEY IDENTITY(1,1),
    sTentrangthai NVARCHAR(50) NOT NULL
);


CREATE TABLE tblDonnhap (
    ID INT PRIMARY KEY IDENTITY(1,1),
    dNgaynhap DATETIME DEFAULT GETDATE() NOT NULL,
    iNCC INT FOREIGN KEY REFERENCES tblNhacungcap(ID),
    iKho INT FOREIGN KEY REFERENCES tblKho(ID),
    iTrangthai INT NOT NULL FOREIGN KEY REFERENCES tblTrangthai(ID)
);


CREATE TABLE tblChitiet_donnhap (
    ID INT PRIMARY KEY IDENTITY(1,1),
    iDonnhap INT NOT NULL FOREIGN KEY REFERENCES tblDonnhap(ID),
    iHanghoa INT FOREIGN KEY REFERENCES tblHanghoa(ID),
    iSoluong INT,
    fGianhap FLOAT
);


CREATE TABLE tblDonxuat (
    ID INT PRIMARY KEY IDENTITY(1,1),
    dNgayxuat DATETIME DEFAULT GETDATE() NOT NULL,
    iKho INT FOREIGN KEY REFERENCES tblKho(ID),
    iTrangthai INT NOT NULL FOREIGN KEY REFERENCES tblTrangthai(ID)
);


CREATE TABLE tblChitiet_donxuat (
    ID INT PRIMARY KEY IDENTITY(1,1),
    iDonxuat INT NOT NULL FOREIGN KEY REFERENCES tblDonxuat(ID),
    iHanghoa INT FOREIGN KEY REFERENCES tblHanghoa(ID),
    iSoluong INT
);


CREATE TABLE tblKiemke (
    ID INT PRIMARY KEY IDENTITY(1,1),
    dNgaykiemke DATETIME DEFAULT GETDATE() NOT NULL,
    iKho INT FOREIGN KEY REFERENCES tblKho(ID),
    iTrangthai INT NOT NULL FOREIGN KEY REFERENCES tblTrangthai(ID),
    iNguoikiemke INT FOREIGN KEY REFERENCES tblNhanvien(ID)
);


CREATE TABLE tblChitiet_kiemke (
    ID INT PRIMARY KEY IDENTITY(1,1),
    iKiemke INT NOT NULL FOREIGN KEY REFERENCES tblKiemke(ID),
    iHanghoa INT FOREIGN KEY REFERENCES tblHanghoa(ID),
    iSoluongKiemke INT,
    iSoluongThucte INT
);


CREATE TABLE tblDieuchuyen (
    ID INT PRIMARY KEY IDENTITY(1,1),
    iKhoGoc INT NOT NULL FOREIGN KEY REFERENCES tblKho(ID),
    iKhoDich INT NOT NULL FOREIGN KEY REFERENCES tblKho(ID),
    dNgayden DATE,
    dNgaydi DATE,
    iTrangthai INT NOT NULL FOREIGN KEY REFERENCES tblTrangthai(ID)
);


CREATE TABLE tblChitiet_dieuchuyen (
    ID INT PRIMARY KEY IDENTITY(1,1),
    iDieuchuyen INT NOT NULL FOREIGN KEY REFERENCES tblDieuchuyen(ID),
    iHanghoa INT FOREIGN KEY REFERENCES tblHanghoa(ID),
    iSoluong INT,
    iDonxuat INT FOREIGN KEY REFERENCES tblDonxuat(ID)
);


INSERT INTO tblQuyen (sTenquyen) VALUES (N'Quản trị');
INSERT INTO tblQuyen (sTenquyen) VALUES (N'Nhân viên kho');
INSERT INTO tblQuyen (sTenquyen) VALUES (N'Kế toán');

INSERT INTO tblNhanvien (sTenNV, dNgaySinh, sGioitinh, sQuequan, sSDT, sCCCD, quyen)
VALUES (N'Nguyễn Văn A', '1985-05-10', N'Nam', N'Hải Phòng', '0912345678', '012345678901', 1);

INSERT INTO tblNhanvien (sTenNV, dNgaySinh, sGioitinh, sQuequan, sSDT, sCCCD, quyen)
VALUES (N'Trần Thị B', '1990-08-20', N'Nữ', N'Hà Nội', '0987654321', '987654321098', 2);


INSERT INTO tblKho (sTenkho, sDiachi, iSucchua, fDientichkho, iQuanly)
VALUES (N'Kho A', N'Số 1 Đà Nẵng, Hải Phòng', 1000, 150.5, 1);

INSERT INTO tblKho (sTenkho, sDiachi, iSucchua, fDientichkho, iQuanly)
VALUES (N'Kho B', N'Số 2 Lê Lợi, Hà Nội', 800, 120.75, 2);

INSERT INTO tblLoaihang (sTenloaihang) VALUES (N'Thiết bị điện');
INSERT INTO tblLoaihang (sTenloaihang) VALUES (N'Văn phòng phẩm');

INSERT INTO tblDonvitinh (sTendonvitinh) VALUES (N'Chiếc');
INSERT INTO tblDonvitinh (sTendonvitinh) VALUES (N'Bộ');
INSERT INTO tblDonvitinh (sTendonvitinh) VALUES (N'Thùng');


INSERT INTO tblHanghoa (sTenhanghoa, iLoaihang, iDonvitinh)
VALUES (N'Bóng đèn', 1, 1);

INSERT INTO tblHanghoa (sTenhanghoa, iLoaihang, iDonvitinh)
VALUES (N'Bút bi', 2, 2);

INSERT INTO tblHanghoa (sTenhanghoa, iLoaihang, iDonvitinh)
VALUES (N'Giấy A4', 2, 3);


INSERT INTO tblNhacungcap (sTenNCC, sDiachi, sSDT, sEmail)
VALUES (N'Công ty Điện máy A', N'123 Lý Thường Kiệt', '091234567', 'dienmaya@example.com');

INSERT INTO tblNhacungcap (sTenNCC, sDiachi, sSDT, sEmail)
VALUES (N'Công ty Văn phòng B', N'456 Trần Hưng Đạo', '098765432', 'vpb@example.com');


INSERT INTO tblTrangthai (sTentrangthai) VALUES (N'Chờ duyệt');
INSERT INTO tblTrangthai (sTentrangthai) VALUES (N'Đã duyệt');
INSERT INTO tblTrangthai (sTentrangthai) VALUES (N'Hoàn thành');


INSERT INTO tblDonnhap (iNCC, iKho, iTrangthai) VALUES (1, 1, 1);
INSERT INTO tblDonnhap (iNCC, iKho, iTrangthai) VALUES (2, 2, 2);

INSERT INTO tblChitiet_donnhap (iDonnhap, iHanghoa, iSoluong, fGianhap)
VALUES (1, 1, 95, 313.76);

INSERT INTO tblChitiet_donnhap (iDonnhap, iHanghoa, iSoluong, fGianhap)
VALUES (2, 2, 92, 406.14);



INSERT INTO tblDonxuat (iKho, iTrangthai) VALUES (1, 1);
INSERT INTO tblDonxuat (iKho, iTrangthai) VALUES (2, 2);

INSERT INTO tblChitiet_donxuat (iDonxuat, iHanghoa, iSoluong)
VALUES (1, 1, 35);

INSERT INTO tblChitiet_donxuat (iDonxuat, iHanghoa, iSoluong)
VALUES (2, 2, 42);


INSERT INTO tblKiemke (iKho, iTrangthai, iNguoikiemke) VALUES (1, 1, 1);
INSERT INTO tblKiemke (iKho, iTrangthai, iNguoikiemke) VALUES (2, 2, 2);

INSERT INTO tblChitiet_kiemke (iKiemke, iHanghoa, iSoluongKiemke, iSoluongThucte)
VALUES (1, 1, 41, 45);

INSERT INTO tblChitiet_kiemke (iKiemke, iHanghoa, iSoluongKiemke, iSoluongThucte)
VALUES (2, 2, 15, 36);



