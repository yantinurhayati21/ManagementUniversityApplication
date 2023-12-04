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
    DepId INT PRIMARY KEY AUTO_INCREMENT,
    DepName VARCHAR(50),
    DepNmDekan VARCHAR(50),
    DepDescription VARCHAR(100)
);

-- Membuat tabel Students
CREATE TABLE Students (
    StId INT PRIMARY KEY AUTO_INCREMENT,
    StNim VARCHAR(11),
    StName VARCHAR(50),
    StDOB DATE,
    StGen VARCHAR(10),
    StSem INT,
    StDepId INT,
    StDepName VARCHAR(50),
    StPhoto LONGBLOB,
    FOREIGN KEY (StDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Lecturer
CREATE TABLE Lecturer (
    LrId INT PRIMARY KEY AUTO_INCREMENT,
    LrName VARCHAR(50),
    LrQual VARCHAR(15),
    LrDOB DATE,
    LrGen VARCHAR(10),
    LrSalary DECIMAL(10, 2),
    LrDepId INT,
    LrDepName VARCHAR(50),
    LrPhoto LONGBLOB,
    FOREIGN KEY (LrDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Courses
CREATE TABLE Courses (
    CId INT PRIMARY KEY AUTO_INCREMENT,
    CName VARCHAR(50),
    CPrice DECIMAL(10, 2),
    CRoom VARCHAR(10),
    CLrId INT,
    CLrName VARCHAR(50),
    FOREIGN KEY (CLrId) REFERENCES Lecturer(LrId)
);

-- Membuat tabel Learning
CREATE TABLE Learning (
    LrnId INT PRIMARY KEY AUTO_INCREMENT,
    LrnStId INT,
    LrnStName VARCHAR(50),
    LrnCId INT,
    LrCName VARCHAR(50),
    LrCRoom VARCHAR(10),
    LrnTimes DATE,
    LrnLrId INT,
    LrnLrName VARCHAR(50),
    LrnDuration INT,
    FOREIGN KEY (LrnStId) REFERENCES Students(StId),
    FOREIGN KEY (LrnCId) REFERENCES Courses(CId),
    FOREIGN KEY (LrnLrId) REFERENCES Lecturer(LrId)
);

-- Membuat tabel Fees
CREATE TABLE Fees (
    FId INT PRIMARY KEY AUTO_INCREMENT,
    FStId INT,
    FStName VARCHAR(50),
    FDepId INT,
    FPeriod INT,
    FAmount INT,
    PayDate DATE,
    FOREIGN KEY (FStId) REFERENCES Students(StId),
    FOREIGN KEY (FDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Salary
CREATE TABLE Salary (
    SId INT PRIMARY KEY AUTO_INCREMENT,
    SLrId INT,
    SLrName VARCHAR(100),
    SLrSalary DECIMAL(10, 2),
    SPeriod INT,
    SPDate DATE,
    FOREIGN KEY (SLrId) REFERENCES Lecturer(LrId)
);

-- Insert ke tabel Admin
INSERT INTO Admin (username, contact, pass, confirm)
VALUES ('yanti', '085321840790', 'pass24434', 'pass24434');

-- Insert ke tabel Department
INSERT INTO Department (DepName, DepNmDekan, DepDescription)
VALUES ('Computer Science', 'Dr. Smith', 'Department focusing on computer studies'),
       ('Biology', 'Prof. Johnson', 'Department specializing in biological sciences');

-- Insert ke tabel Students
INSERT INTO Students (StNim,StName, StDOB, StGen, StSem, StDepId, StDepName)
VALUES ('02042211025','Yanti Nurhayati', '2002-11-21', 'Female', 3, 1, 'Computer Science'),
       ('02032211004','Dinar Aghnaya', '2002-04-23', 'Male', 3, 2, 'Biology');

-- Insert ke tabel Lecturer
INSERT INTO Lecturer (LrName, LrQual, LrDOB, LrGen, LrSalary, LrDepId, LrDepName)
VALUES ('Dr. Erna', 'Ph.D. Computer Science', '1975-10-25', 'Male', 60000.00, 1, 'Computer Science'),
       ('Prof. Wilson', 'M.Sc. Biology', '1980-06-18', 'Female', 55000.00, 2, 'Biology');

-- Insert ke tabel Courses
INSERT INTO Courses (CName, CPrice, CRoom, CLrId, CLrName)
VALUES ('Database Management', 1500.00, 'B31', 1, 'Dr. Erna'),
       ('Genetics 101', 2000.00, 'B32', 2, 'Prof. Wilson');
       
-- Insert ke tabel Learning
INSERT INTO Learning (LrnStId, LrnStName, LrnCId, LrCName, LrCRoom, LrnTimes, LrnLrId, LrnLrName, LrnDuration)
VALUES (1, 'Yanti Nurhayati', 1, 'Database Management', 'B31', '2023-11-26', 1, 'Dr. Erna', 4),
       (2, 'Dinar Aghnaya', 2, 'Genetics 101', 'B32', '2023-11-21', 2, 'Prof. Wilson', 4);
       `admin`
-- Insert ke tabel Fees
INSERT INTO Fees (FStId, FStName, FDepId, FPeriod, FAmount, PayDate)
VALUES (1, 'Yanti Nurhayati', 1, 3, 50000.00, '2023-11-15'),
       (2, 'Dinar Aghnaya', 2, 3, 45000.00, '2023-11-20');

-- Insert ke tabel Salary
INSERT INTO Salary (SLrId, SLrName, SLrSalary, SPeriod, SPDate)
VALUES (1, 'Dr. Erna', 60000.00, 3, '2023-11-25'),
       (2, 'Prof. Wilson', 55000.00, 3, '2023-11-28');
