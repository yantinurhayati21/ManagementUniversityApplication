-- Menghapus database University
DROP DATABASE University;

-- Membuat database University
CREATE DATABASE University;

-- Menggunakan database University
USE University;

-- Membuat tabel Admin
CREATE TABLE Admin (
    username VARCHAR(10) PRIMARY KEY,
    contact VARCHAR(13),
    pass VARCHAR(10),
    confirm VARCHAR(10)
);

-- Membuat tabel Department
CREATE TABLE Department (
    DepId VARCHAR(5) PRIMARY KEY,
    DepName VARCHAR(50),
    DepNmDekan VARCHAR(50),
    DepDescription VARCHAR(100)
);

-- Membuat tabel Students
CREATE TABLE Students (
    StId VARCHAR(5) PRIMARY KEY,
    StNim VARCHAR(11),
    StName VARCHAR(50),
    StDOB DATE,
    StGen VARCHAR(10),
    StSem INT,
    StDepId VARCHAR(5),
    StDepName VARCHAR(50),
    StPhoto LONGBLOB,
    FOREIGN KEY (StDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Lecturer
CREATE TABLE Lecturer (
    LrId VARCHAR(5) PRIMARY KEY,
    LrName VARCHAR(50),
    LrQual VARCHAR(15),
    LrDOB DATE,
    LrGen VARCHAR(10),
    LrSalary int,
    LrDepId VARCHAR(5),
    LrDepName VARCHAR(50),
    LrPhoto LONGBLOB,
    FOREIGN KEY (LrDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Courses
CREATE TABLE Courses (
    CId VARCHAR(5) PRIMARY KEY,
    CName VARCHAR(50),
    CPrice DECIMAL(10, 2),
    CRoom VARCHAR(10),
    CLrId VARCHAR(5),
    CLrName VARCHAR(50),
    FOREIGN KEY (CLrId) REFERENCES Lecturer(LrId)
);

-- Membuat tabel Learning
CREATE TABLE Learning (
    LrnId VARCHAR(5) PRIMARY KEY,
    LrnStId VARCHAR(5),
    LrnStName VARCHAR(50),
    LrnCId VARCHAR(5),
    LrCName VARCHAR(50),
    LrCRoom VARCHAR(10),
    LrnTimes DATE,
    LrnLrId VARCHAR(5),
    LrnLrName VARCHAR(50),
    LrnDuration INT,
    FOREIGN KEY (LrnStId) REFERENCES Students(StId),
    FOREIGN KEY (LrnCId) REFERENCES Courses(CId),
    FOREIGN KEY (LrnLrId) REFERENCES Lecturer(LrId)
);

-- Membuat tabel Fees
CREATE TABLE Fees (
    FId VARCHAR(5) PRIMARY KEY,
    FStId VARCHAR(5),
    FStName VARCHAR(50),
    FDepId VARCHAR(5),
    FDepName VARCHAR(50),
    FPeriod INT,
    FAmount INT,
    PayDate DATE,
    FOREIGN KEY (FStId) REFERENCES Students(StId),
    FOREIGN KEY (FDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Salary
CREATE TABLE Salary (
    SId VARCHAR(5) PRIMARY KEY,
    SLrId VARCHAR(5),
    SLrName VARCHAR(100),
    SLrSalary int,
    SPeriod INT,
    SPDate DATE,
    FOREIGN KEY (SLrId) REFERENCES Lecturer(LrId)
);

-- Insert ke tabel Admin
INSERT INTO Admin VALUES 
	('yanti', '085321840790', 'pass24434', 'pass24434');

-- Insert ke tabel Department
INSERT INTO Department VALUES 
	('D0001','Computer Science', 'Dr. Smith', 'Focusing on computer studies'),
	('D0002','Biology', 'Prof. Johnson', 'Specializing in biological sciences');

-- Insert ke tabel Students
INSERT INTO Students VALUES
	('S0001','02042211025','Yanti Nurhayati', '2002-11-21', 'Female', 3, 'D0001', 'Computer Science', NULL),
	('S0002','02032211004','Dinar Aghnaya', '2002-04-23', 'Male', 3, 'D0002', 'Biology', NULL);

-- Insert ke tabel Lecturer
INSERT INTO Lecturer VALUES 
	('L0001', 'Dr. Erni', 'Doktor', '1975-10-25', 'Male', 60000.00, 'D0001', 'Computer Science', NULL),
	('L0002', 'Prof. Wilson', 'Master', '1980-06-18', 'Female', 50000.00, 'D0002', 'Biology', NULL);

-- Insert ke tabel Courses
INSERT INTO Courses VALUES 
    ('C0001','Database Management', 1500.00, 'B31', 'L0001', 'Dr. Erna'),
    ('C0002','Genetics 101', 2000.00, 'B32', 'L0002', 'Prof. Wilson');
       
-- Insert ke tabel Learning
INSERT INTO Learning VALUES 
    ('LR001','S0001', 'Yanti Nurhayati', 'C0001', 'Database Management', 'B31', '2023-11-26', 'L0001', 'Dr. Erna', 4),
    ('LR002','S0002', 'Dinar Aghnaya', 'C0002', 'Genetics 101', 'B32', '2023-11-21', 'L0002', 'Prof. Wilson', 4);
    
-- Insert ke tabel Fees
INSERT INTO Fees VALUES 
    ('F0001','S0001', 'Yanti Nurhayati', 'D0001', 'Computer Science', 3, 50000.00, '2023-11-15'),
    ('F0002','S0002', 'Dinar Aghnaya', 'D0002', 'Biology', 3, 45000.00, '2023-11-20');

-- Insert ke tabel Salary
INSERT INTO Salary VALUES 
	('SA001','L0001', 'Dr. Erna', 60000, 3, '2023-11-25'),
	('SA002','L0002', 'Prof. Wilson', 55000, 3, '2023-11-28');
