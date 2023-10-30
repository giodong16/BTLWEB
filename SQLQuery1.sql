CREATE DATABASE QuanLiSinhVienWeb
USE QuanLiSinhVienWeb


create table WebUser
(
	UserName nvarchar(50) NOT NULL PRIMARY KEY,
    Password varchar(20) null,
	Loai nvarchar(20) null,
	Avatar nvarchar(50) null,
)
CREATE TABLE Class(
	ClassID int primary key identity(1,1) not null,
	ClassName nvarchar(50) null
)
CREATE TABLE Subject (
	SubjectID int primary key identity(1,1) not null,
	ClassID   int foreign key references Class (ClassID) null,
	SubjectName nvarchar(50) null
)

Create table Student (
	StudentID int primary key identity(1,1) not null,
	Name Nvarchar(50) null,
	DOB date null,
	Gender nvarchar(50) null,
	Mobile bigint null,
	RollNo nvarchar(50) null,
	Address nvarchar(max) null,
	ClassID   int foreign key references Class (ClassID) null,
)
Create table Teacher(
	TeacherID int primary key identity(1,1) not null,
	UserName nvarchar(50)  foreign key references  webUser(UserName)  null,
	Name Nvarchar(50) null,
	DOB date null,
	Gender nvarchar(50) null,
	Mobile bigint null,
	Email varchar(50) null,
	Address nvarchar(max) null,

)

Create table TeacherSubject(
	Id int primary key identity(1,1) not null,
	ClassID   int foreign key references Class (ClassID) null,
	SubjectID int foreign key references Subject(SubjectID) null,
	TeacherID int foreign key references Teacher(TeacherID) null,
)

Create table TeacherAttendance(
	Id int primary key identity(1,1) not null,
	TeacherID int foreign key references Teacher(TeacherID) null,
	Status bit null,
	Date date null,
)
Create table StudentAttendance(
	Id int primary key identity(1,1) not null,
	ClassID int foreign key references Class (ClassID) null,
	SubjectID int foreign key references Subject(SubjectID) null,
	RollNo nvarchar(20) null,
	Status bit null,
	Date date null,
)
Create  table Fees(
	FeesId int primary key identity(1,1) not null,
	ClassID int foreign key references Class (ClassID) null,
	FeesAmount int null,
)
Create table Exam
(
	ExamId int primary key identity(1,1) not null,
	ClassID int foreign key references Class (ClassID) null,
	SubjectID int foreign key references Subject(SubjectID) null,
	RollNo nvarchar(20) null,
	TotalMarks int null,
	OutOfMarks int null
)
Create table Expense(
	ExpenseId int primary key identity(1,1) not null,
	ClassID int foreign key references Class (ClassID) null,
	SubjectID int foreign key references Subject(SubjectID) null,
	ChargeAmount int null
)
create table Admin(
	AdminID int primary key identity(1,1) not null,
	UserName nvarchar(50)  foreign key references  webUser(UserName)  null,
	Name Nvarchar(50) null,
	DOB date null,
	Gender nvarchar(50) null,
	Mobile bigint null,
	Email varchar(50) null,
	Address nvarchar(max) null,
 
)

INSERT INTO WebUser(UserName,Password,Loai) values('laimanhdung','123456','teacher'),('admin1','123456',N'admin')

INSERT INTO Class (ClassName)
VALUES ('Class A'), ('Class B'), ('Class C'),('Class D'), ('Class E'), ('Class F'),('Class G'), ('Class H'), ('Class I'),('Class K'), ('Class L'), ('Class M'),('Class N');

INSERT INTO Subject (ClassID, SubjectName)
VALUES (1, N'Cấu trúc dữ liệu và giải thuật'), (1, N'Hệ điều hành'),
       (1, N'Cơ sở dữ liệu'), (1, N'Mạng máy tính'), (1, N'Phân tích và thiết kế hệ thống'),
       (1, N'Trí tuệ nhân tạo'), (1, N'An toàn thông tin'),(1, N'Thuật toán và ứng dụng'),
	   (1, N'H Lâp trình Web'),
       (1, N'Tin học đại cương'), (1, N'Công nghệ Java'), (1, N'Toán rời rạc'),
       (1, N'Lịch sử Đảng'), (1, N'Chi tiết máy'),(1, N'Nhập môn ngành CNTT'), (1, N'Đại số tuyến tính');

INSERT INTO Student (Name, DOB, Gender,Mobile, RollNo, Address, ClassID)
VALUES (N'Nguyễn Văn Thích', '2003-01-01', N'Nam',1234567890, '211200692', N'Hà Nội', 1),
       (N'Trần Thị Bình', '2002-05-15', N'Nữ',1234567890,  '211200693', N'Hồ Chí Minh', 1),
       (N'Lê Văn Cao', '2003-03-20', N'Nam',1234567890,  '211200694', N'Đà Nẵng', 2);
INSERT INTO Student (Name, DOB, Gender,Mobile, RollNo, Address, ClassID)
VALUES (N'Nguyễn Đình Tiếp', '2003-03-16', N'Nam',1234567890, '211200695', N'Hà Nội', 1),
       (N'Khổng Thu Trang', '2002-05-15', N'Nữ',1234567890,  '211200696', N'Hồ Chí Minh', 1),
       (N'Nguyễn Tiến Đạt', '2003-03-2', N'Nam',1234567890,  '211200116', N'Đà Nẵng', 2),
	   (N'Hà Minh Tiến', '2003-03-1', N'Nam',1234567890, '211200697', N'Hà Nội', 1),
	   (N'Lê Thành Nam', '2003-03-6', N'Nam',1234567890, '211200698', N'Hà Nội', 1),
	   (N'Đỗ Danh Chung', '2003-05-16', N'Nam',1234567890, '211200699', N'Hà Nội', 1),
	   (N'Trần Thị Thu Trang', '2002-05-15', N'Nữ',1234567890,  '211200700', N'Hồ Chí Minh', 1),
	   (N'Nguyễn Thị Hà', '2002-05-15', N'Nữ',1234567890,  '211200701', N'Hồ Chí Minh', 1),
	   (N'Hoa Mộc Lan', '2002-05-15', N'Nữ',0123456789,  '211200702', N'Hồ Chí Minh', 1),
	   (N'Nguyễn Huệ', '2002-05-15', N'Nữ',0123456789,  '211200702', N'Hồ Chí Minh', 1),
	   (N'Trần Thị Tuyết Băng', '2002-05-15', N'Nữ',0123456789,  '211200703', N'Hồ Chí Minh', 1),
	   (N'Lê Quang Dũng', '2002-05-15', N'Nam',0123456789,  '211200704', N'Hồ Chí Minh', 1);
	   
INSERT INTO Teacher (Name, DOB, Gender,Mobile, Email, Address) values (N'Cao Thị Luyên','1980-1-1',N'Nữ',1234567890,'caoluyen@gmail.com',N'Cầu Giấy-Hà Nội'),
(N'Lại Mạnh Dũng','1980-1-10',N'Nam',1234567890,'dunglaimanh@gmail.com',N'Cầu Giấy-Hà Nội'),
(N'Nguyễn Đức Dư','1980-10-10',N'Nam',1234567890,'ducduc@gmail.com',N'Cầu Giấy-Hà Nội'),
(N'Đào Thị Lệ Thủy','1980-9-1',N'Nữ',1234567890,'caoluyen@gmail.com',N'Cầu Giấy-Hà Nội'),
(N'Nguyễn Kim Sao','1980-2-5',N'Nữ',1234567890,'kimsao@gmail.com',N'Cầu Giấy-Hà Nội');

UPDATE Teacher SET UserName='laimanhdung' where  Name=N'Lại Mạnh Dũng'


INSERT INTO Fees (ClassID,FeesAmount) 
values(1,500000),(1,550000),(1,600000),(1,400000),(1,450000),
	(2,1000000),(2,550000),(2,600000),(2,400000),(2,450000)

insert into TeacherSubject(ClassID, SubjectID, TeacherID) 
		values(1, 1, 4), (1, 2, 3), (1, 3, 5), (1, 4, 1), (1, 5, 2)


insert into TeacherAttendance(TeacherID, Status, Date)
	values(1, 1, '2018-09-05'), (3, 0, '2019-09-05'), (4, 1, '2018-10-10'),(5, 0, '2020-10-18'), (2,1,'2018-09-05')


insert into StudentAttendance(ClassID, SubjectID, RollNo, Status, Date)
		values(1, 1, 211200692, 1, '2018-09-05'), (1, 1, 211200693, 1, '2018-09-05'), (1, 1, 211200694, 1, '2018-09-05'),
		(1, 2, 211200695, 0, '2019-09-05'), (1, 2, 211200696, 0, '2019-09-05'), (1, 2, 211200116, 0, '2019-09-05'),
		(2, 3, 211200697, 1, '2018-10-10'),(2, 3, 211200699, 1, '2018-10-10'), (2, 3, 211200700, 1, '2018-10-10'),
		(3, 4, 211200701, 0, '2020-10-18'), (3, 4, 211200702, 0, '2020-10-18'), (3, 4, 211200703, 0, '2020-10-18'),
		(4, 5, 211200704, 0, '2020-10-20')


insert into Exam (ClassID, SubjectID, RollNo, TotalMarks, OutOfMarks)
	values(1, 1, 211200692,10, 10), (1, 1, 211200693, 8, 10), (1, 1, 211200694, 9, 10),
		(1, 2, 211200695, 7, 10), (1, 2, 211200696, 7, 10), (1, 2, 211200116,8, 10),
		(2, 3, 211200697, 9, 10),(2, 3, 211200699, 10, 10), (2, 3, 211200700, 9, 10),
		(3, 4, 211200701, 6, 10), (3, 4, 211200702, 7, 10), (3, 4, 211200703, 8, 10),
		(4, 5, 211200704, 9, 10)


insert into Expense( SubjectID, ClassID, ChargeAmount)
	Values(1, 1, 300000), (2, 1, 100000), (3, 1, 500000), (4, 1, 500000), (5, 1, 100000),
		(6, 1, 500000),(7, 1, 300000), (8, 1, 300000), (9, 1, 400000), (10, 1, 500000),
		(11, 1, 500000), (12, 1, 500000), (13, 1, 500000), (14, 1, 500000),(15, 1, 500000), (16, 1, 500000)