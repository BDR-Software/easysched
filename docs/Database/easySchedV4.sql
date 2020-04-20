create database easySched;
CREATE TABLE `Billing Address` (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, Street varchar(255), PostalCode int(10), City varchar(255), Province varchar(255), PRIMARY KEY (ID));
CREATE TABLE Company (ID int(10) NOT NULL AUTO_INCREMENT, Name varchar(255), CompanyNumber varchar(255), PRIMARY KEY (ID));
CREATE TABLE Employee (ID int(10) NOT NULL AUTO_INCREMENT, PrivelegesID int(10) NOT NULL, CompanyID int(10) NOT NULL, FirstName VARCHAR(255), LastName VARCHAR(255), EmployeeNumber int(10), Email VARCHAR(255), Wages double, PRIMARY KEY (ID));
CREATE TABLE `Employee Availability` (ID int(10) NOT NULL AUTO_INCREMENT, WeekdayID int(10) NOT NULL, EmployeeID int(10) NOT NULL, Start DATETIME, End DATETIME, PRIMARY KEY (ID));
CREATE TABLE Licencing (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, Cost double, Start DATETIME, End DATETIME, PRIMARY KEY (ID));
CREATE TABLE Phone (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, EmployeeID int(10), PhoneTypeID int(10) NOT NULL, Number int(10) NOT NULL, PRIMARY KEY (ID));
CREATE TABLE PhoneType (ID int(10) NOT NULL AUTO_INCREMENT, Type varchar(255), PRIMARY KEY (ID));
CREATE TABLE Priveleges (ID int(10) NOT NULL AUTO_INCREMENT, Type int(10), Name VARCHAR(255), PRIMARY KEY (ID));
CREATE TABLE Schedule (ID int(10) NOT NULL AUTO_INCREMENT, CompanyID int(10) NOT NULL, DepartmentID int(10) NOT NULL, Start DATE, End DATE, PRIMARY KEY (ID));
CREATE TABLE Department(ID int(10) PRIMARY KEY NOT NULL AUTO_INCREMENT, CompanyID int(10), Name VARCHAR(255));
CREATE TABLE Weekday (ID int(10) NOT NULL AUTO_INCREMENT, Day int(10), PRIMARY KEY (ID));
CREATE TABLE Shift (ID int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL, EmployeeID int(10) NOT NULL, ScheduleID int(10), Day DATE, Start DATETIME, End DATETIME);
CREATE TABLE Login (ID int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL, Email VARCHAR(255) NOT NULL, Pass VARCHAR(255) NOT NULL, ConfirmPass VARCHAR(255) NOT NULL, EmployeeID int(10));
CREATE TABLE TimeOffRequest (ID int(10) AUTO_INCREMENT PRIMARY KEY NOT NULL, EmployeeID int(10) NOT NULL, Created DATETIME NOT NULL, Day DATE NOT NULL, Message VARCHAR(255), Approved BOOL);
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
ALTER TABLE Schedule ADD CONSTRAINT FKSchedule872344 FOREIGN KEY (CompanyID) REFERENCES Company (ID);
ALTER TABLE Shift ADD CONSTRAINT FKShift123456 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);
ALTER TABLE Shift ADD CONSTRAINT FKSchedule125456 FOREIGN KEY (ScheduleID) REFERENCES Schedule (ID);
ALTER TABLE Login ADD CONSTRAINT FKEmployee1312456 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);
ALTER TABLE TimeOffRequest ADD CONSTRAINT FKTimeOffRequest1234 FOREIGN KEY (EmployeeID) REFERENCES Employee (ID);


INSERT INTO Priveleges (Type, Name) VALUES (
	1,
    "Employee"
);
INSERT INTO Priveleges (Type, Name) VALUES (
	2,
    "Schedule Editor"
);

INSERT INTO Company (Name, CompanyNumber) VALUES (
	"Joes Pizza",
    "85739"
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	2,
    1,
    "Joe",
    "Doe",
    10001,
    "joe@joespizza.ca",
    22.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"joe@joespizza.ca",
    "joespizza",
    "joespizza",
    1
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    1,
    "Cameron",
    "Mudd",
    10002,
    "cameronmudd@gmail.com",
    14.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"cameronmudd@gmail.com",
    "cameronmudd",
    "cameronmudd",
    2
);

INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    1,
    "Albert",
    "Einstein",
    10003,
    "AEinstein@conestogac.on.ca",
    14.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"AEinstein@conestogac.on.ca",
    "einstein",
	"einstein",
    3
);

INSERT INTO Department (CompanyID, Name) VALUES (
	1,
    'Customer Service'
);
INSERT INTO Department (CompanyID, Name) VALUES (
	1,
    'Cooking'
);
INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	1,
	1,
    '2020-04-19',
    '2020-04-25'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 1, '2020-04-20', '2020-04-20 9:00:00', '2020-04-20 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 1, '2020-04-21', '2020-04-21 9:00:00', '2020-04-21 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 1, '2020-04-22', '2020-04-22 9:00:00', '2020-04-22 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 1, '2020-04-23', '2020-04-23 9:00:00', '2020-04-23 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 1, '2020-04-24', '2020-04-24 9:00:00', '2020-04-24 17:00:00'
);

INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	1,
	2,
    '2020-04-19',
    '2020-04-25'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 2, '2020-04-20', '2020-04-20 9:00:00', '2020-04-20 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 2, '2020-04-21', '2020-04-21 9:00:00', '2020-04-21 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 2, '2020-04-22', '2020-04-22 9:00:00', '2020-04-22 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 2, '2020-04-23', '2020-04-23 9:00:00', '2020-04-23 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	2, 2, '2020-04-24', '2020-04-24 9:00:00', '2020-04-24 17:00:00'
);

INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	1,
	1,
    '2020-04-26',
    '2020-05-02'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	1, 2, '2020-04-27', '2020-04-27 9:00:00', '2020-04-27 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	1, 2, '2020-04-28', '2020-04-28 9:00:00', '2020-04-28 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	1, 2, '2020-04-29', '2020-04-29 9:00:00', '2020-04-29 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	1, 2, '2020-04-30', '2020-04-30 9:00:00', '2020-04-30 17:00:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	1, 2, '2020-05-01', '2020-05-01 9:00:00', '2020-05-01 17:00:00'
);




INSERT INTO Company (Name, CompanyNumber) VALUES (
	"Stus Loos",
    "85740"
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	2,
    2,
    "Stuart",
    "Little",
    11001,
    "stuart@stusloos.ca",
    22.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"stuart@stusloos.ca",
    "stuartlittle",
    "stuartlittle",
    4
);
INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    2,
    "Judy",
    "Little",
    11002,
    "judy@stusloos.ca",
    22.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"judy@stusloos.ca",
    "judylittle",
    "judylittle",
    5
);

INSERT INTO Employee (PrivelegesID, CompanyID, FirstName, LastName, EmployeeNumber, Email, Wages) VALUES (
	1,
    2,
    "Thomas",
    "Edison",
    11003,
    "tedison@bell.ca",
    14.50
);
INSERT INTO Login (Email, Pass, ConfirmPass, EmployeeID) VALUES (
	"tedison@bell.ca",
    "thomasedison",
	"thomasedison",
    6
);

INSERT INTO Department (CompanyID, Name) VALUES (
	2,
    'Customer Service'
);
INSERT INTO Department (CompanyID, Name) VALUES (
	2,
    'Manufacturing'
);
INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	2,
	3,
    '2020-05-03',
    '2020-05-09'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 4, '2020-05-04', '2020-05-04 9:30:00', '2020-05-04 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 4, '2020-05-05', '2020-05-05 9:30:00', '2020-05-05 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 4, '2020-05-06', '2020-05-06 9:30:00', '2020-05-06 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 4, '2020-05-07', '2020-05-07 9:30:00', '2020-05-07 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 4, '2020-05-08', '2020-05-08 9:30:00', '2020-05-08 17:30:00'
);

INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	2,
	4,
    '2020-05-03',
    '2020-05-09'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	6, 5, '2020-05-04', '2020-05-04 9:30:00', '2020-05-04 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	6, 5, '2020-05-05', '2020-05-05 9:30:00', '2020-05-05 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	6, 5, '2020-05-06', '2020-05-06 9:30:00', '2020-05-06 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	6, 5, '2020-05-07', '2020-05-07 9:30:00', '2020-05-07 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	6, 5, '2020-05-08', '2020-05-08 9:30:00', '2020-05-08 17:30:00'
);

INSERT INTO Schedule (CompanyID, DepartmentID, Start, End) VALUES 
(
	2,
	3,
    '2020-05-10',
    '2020-05-16'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 6, '2020-05-11', '2020-05-11 9:30:00', '2020-05-11 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 6, '2020-05-12', '2020-05-12 9:30:00', '2020-05-12 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 6, '2020-05-13', '2020-05-13 9:30:00', '2020-05-13 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 6, '2020-05-14', '2020-05-14 9:30:00', '2020-05-14 17:30:00'
);
INSERT INTO Shift (EmployeeID, ScheduleID, Day, start, end) VALUES (
	5, 6, '2020-05-15', '2020-05-15 9:30:00', '2020-05-15 17:30:00'
);