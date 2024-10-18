
CREATE DATABASE QLSanpham;
GO


USE QLSanpham;
GO


CREATE TABLE LoaiSP (
    MaLoai CHAR(2) PRIMARY KEY,
    TenLoai NVARCHAR(30) NOT NULL
);
GO
drop table LoaiSP	

CREATE TABLE Sanpham (
    MaSP CHAR(6) PRIMARY KEY,
    TenSP NVARCHAR(30) NOT NULL,
    Ngaynhap DATETIME,
    MaLoai CHAR(2),
    FOREIGN KEY (MaLoai) REFERENCES LoaiSP(MaLoai)
);
GO
drop table Sanpham

INSERT INTO LoaiSP (MaLoai, TenLoai)
VALUES ('L1', N'Loại 1'),
       ('L2', N'Loại 2');
GO

INSERT INTO Sanpham (MaSP, TenSP, Ngaynhap, MaLoai)
VALUES ('SP001', N'Sản phẩm 1', '2024-10-01', 'L1'),
       ('SP002', N'Sản phẩm 2', '2024-10-15', 'L2'),
       ('SP003', N'Sản phẩm 3', '2024-09-20', 'L1');
GO
select * from LoaiSP
select * from Sanpham 
SELECT MaSP, TenSP, 
       CONVERT(VARCHAR(10), Ngaynhap, 103) AS Ngaynhap, 
       MaLoai
FROM Sanpham;
GO
