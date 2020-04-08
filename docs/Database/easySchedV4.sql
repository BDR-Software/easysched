create database easySched;
CREATE TABLE `Billing Address` (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, Street varchar(255), PostalCode int(10), City varchar(255), Province varchar(255), PRIMARY KEY (ID));
CREATE TABLE Company (ID int(10) NOT NULL AUTO_INCREMENT, Name varchar(255), CompanyNumber varchar(255), PRIMARY KEY (ID));
CREATE TABLE Employee (ID int(10) NOT NULL AUTO_INCREMENT, PrivelegesID int(10) NOT NULL, CompanyID int(10) NOT NULL, FirstName VARCHAR(255), LastName VARCHAR(255), EmployeeNumber int(10), Email VARCHAR(255), Wages double, PRIMARY KEY (ID));
CREATE TABLE `Employee Availability` (ID int(10) NOT NULL AUTO_INCREMENT, WeekdayID int(10) NOT NULL, EmployeeID int(10) NOT NULL, Start DATETIME, End DATETIME, PRIMARY KEY (ID));
CREATE TABLE Licencing (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, Cost double, Start DATETIME, End DATETIME, PRIMARY KEY (ID));
CREATE TABLE Phone (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, EmployeeID int(10), PhoneTypeID int(10) NOT NULL, Number int(10) NOT NULL, PRIMARY KEY (ID));
CREATE TABLE PhoneType (ID int(10) NOT NULL AUTO_INCREMENT, Type varchar(255), PRIMARY KEY (ID));
CREATE TABLE Priveleges (ID int(10) NOT NULL AUTO_INCREMENT, Type int(10), PRIMARY KEY (ID));
CREATE TABLE Schedule (ID int(10) NOT NULL AUTO_INCREMENT, DepartmentID int(10) NOT NULL, Week int(10), PRIMARY KEY (ID));
CREATE TABLE Department(ID int(10) PRIMARY KEY NOT NULL AUTO_INCREMENT, CompanyID int(10), Name VARCHAR(255));
CREATE TABLE Weekday (ID int(10) NOT NULL AUTO_INCREMENT, Day int(10), PRIMARY KEY (ID));
CREATE TABLE Shift (ID int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL, EmployeeID int(10) NOT NULL, ScheduleID int(10), Start DATETIME, End DATETIME);
CREATE TABLE Login (ID int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL, Email VARCHAR(255) NOT NULL, Pass VARCHAR(255) NOT NULL, EmployeeID int(10) NOT NULL);
ALTER TABLE Phone ADD CONSTRAINT FKPhone530938 FOREIGN KEY (PhoneTypeID) REFERENCES PhoneType (ID);
ALTER TABLE Employee ADD CONSTRAINT FKEmployee833112 FOREIGN KEY (PrivelegesID) REFERENCES Priveleges (ID);
ALTER TABLE Licencing ADD CONSTRAINT FKLicencing37786 FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE Phone ADD CONSTRAINT FKPhone588853 FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE Phone ADD CONSTRAINT FKPhone263678 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);
ALTER TABLE Employee ADD CONSTRAINT FKEmployee435943 FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE `Employee Availability` ADD CONSTRAINT `FKEmployee A593831` FOREIGN KEY (WeekdayID) REFERENCES Weekday (ID);
ALTER TABLE `Employee Availability` ADD CONSTRAINT `FKEmployee A983524` FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);
ALTER TABLE `Billing Address` ADD CONSTRAINT `FKBilling Ad812261` FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE Department ADD CONSTRAINT FKCompany872714 FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE Schedule ADD CONSTRAINT FKSchedule876714 FOREIGN KEY (DepartmentID) REFERENCES Department (ID);
ALTER TABLE Shift ADD CONSTRAINT FKShift123456 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);
ALTER TABLE Shift ADD CONSTRAINT FKSchedule125456 FOREIGN KEY (ScheduleID) REFERENCES Schedule (ID);
ALTER TABLE Login ADD CONSTRAINT FKEmployee1312456 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);

INSERT INTO Priveleges (Type) VALUES (
	1
);
INSERT INTO Company (Name, CompanyNumber) VALUES (
	"Conetoga College",
    "12345"
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    1,
    "Jackson",
    "Ruby",
    582490,
    "jruby6699@conestogac.on.ca",
    14.50
);
INSERT INTO Login (Email, Pass, EmployeeID) VALUES (
	"jruby6699@conestogac.on.ca",
    "password",
    1
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    1,
    "John",
    "Doe",
    523450,
    "johndoe@conestogac.on.ca",
    14.50
);
INSERT INTO Login (Email, Pass, EmployeeID) VALUES (
	"johndoe@conestogac.on.ca",
    "password",
    2
);
INSERT INTO Department (CompanyID, Name) VALUES (
	1,
    'Front End'
);
INSERT INTO Schedule (DepartmentID, Week) VALUES 
(
	1,
    '5'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	1, 1, '2020-04-06 9:00:00', '2020-04-06 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	1, 1, '2020-04-07 9:00:00', '2020-04-07 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	1, 1, '2020-04-08 9:00:00', '2020-04-08 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	1, 1, '2020-04-09 9:00:00', '2020-04-09 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	1, 1, '2020-04-10 9:00:00', '2020-04-10 17:00:00'
);

INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	2, 1, '2020-04-13 9:00:00', '2020-04-13 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	2, 1, '2020-04-14 9:00:00', '2020-04-14 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	2, 1, '2020-04-15 9:00:00', '2020-04-15 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	2, 1, '2020-04-16 9:00:00', '2020-04-16 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, start, end) VALUES (
	2, 1, '2020-04-17 9:00:00', '2020-04-17 17:00:00'
);