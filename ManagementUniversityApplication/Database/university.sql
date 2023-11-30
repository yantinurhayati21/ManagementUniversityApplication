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
    StNim VARCHAR(10),
    StName VARCHAR(50),
    StDOB DATE,
    StGen VARCHAR(10),
    StDepId INT,
    StDepName VARCHAR(50),
    StSem INT,
    StPhoto LONGBLOB,
    FOREIGN KEY (StDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Lecturer
CREATE TABLE Lecturer (
    LrId INT PRIMARY KEY AUTO_INCREMENT,
    LrName VARCHAR(50),
    LrDOB DATE,
    LrGen VARCHAR(10),
    LrQual VARCHAR(15),
    LrDepId INT,
    LrDepName VARCHAR(50),
    LrSalary DECIMAL(10, 2),
    LrPhoto LONGBLOB,
    FOREIGN KEY (LrDepId) REFERENCES Department(DepId)
);

-- Membuat tabel Courses
CREATE TABLE Courses (
    CId INT PRIMARY KEY AUTO_INCREMENT,
    CName VARCHAR(50),
    CPrice DECIMAL(10, 2),
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
    FAmount DECIMAL(10, 2),
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
INSERT INTO Students (StNim,StName, StDOB, StGen, StDepId, StDepName, StSem)
VALUES ('02042211025','Yanti Nurhayati', '2002-11-21', 'Female', 1, 'Computer Science', 3),
       ('02032211004','Dinar Aghnaya', '2002-04-23', 'Male', 2, 'Biology', 3);

-- Insert ke tabel Lecturer
INSERT INTO Lecturer (LrName, LrDOB, LrGen, LrQual, LrDepId, LrDepName, LrSalary)
VALUES ('Dr. Erna', '1975-10-25', 'Male', 'Ph.D. Computer Science', 1, 'Computer Science', 60000.00),
       ('Prof. Wilson', '1980-06-18', 'Female', 'M.Sc. Biology', 2, 'Biology', 55000.00);

-- Insert ke tabel Courses
INSERT INTO Courses (CName, CPrice, CLrId, CLrName)
VALUES ('Database Management', 150.00, 1, 'Dr. Erna'),
       ('Genetics 101', 200.00, 2, 'Prof. Wilson');
       
-- Insert ke tabel Learning
INSERT INTO Learning (LrnStId, LrnStName, LrnCId, LrCName, LrnLrId, LrnLrName, LrnDuration)
VALUES (1, 'Yanti Nurhayati', 1, 'Database Management', 1, 'Dr. Erna', 12),
       (2, 'Dinar Aghnaya', 2, 'Genetics 101', 2, 'Prof. Wilson', 15);
       
-- Insert ke tabel Fees
INSERT INTO Fees (FStId, FStName, FDepId, FPeriod, FAmount, PayDate)
VALUES (1, 'Yanti Nurhayati', 1, 3, 50000.00, '2023-11-15'),
       (2, 'Dinar Aghnaya', 2, 3, 45000.00, '2023-11-20');

-- Insert ke tabel Salary
INSERT INTO Salary (SLrId, SLrName, SLrSalary, SPeriod, SPDate)
VALUES (1, 'Dr. Erna', 60000.00, 3, '2023-11-25'),
       (2, 'Prof. Wilson', 55000.00, 3, '2023-11-28');
