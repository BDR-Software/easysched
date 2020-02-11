DROP DATABASE easySched;
CREATE DATABASE easySched;
USE easySched;

CREATE TABLE Licence
(
		id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
        agreement VARCHAR(1000)
);

CREATE TABLE CompanyLicence 
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    cost DOUBLE,
    start DATE,
    end DATE,
    licenceId INT UNSIGNED,
	FOREIGN KEY (licenceId) 
    REFERENCES Licence(id)
);
CREATE TABLE phoneType
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    type VARCHAR(4) DEFAULT "User"
);
CREATE TABLE phoneNumber
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    phonetypeId INT UNSIGNED,
    number VARCHAR(10),
    FOREIGN KEY (phonetypeId) REFERENCES phonetype(id)
);

CREATE TABLE Company 
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    companyLicenseId INT UNSIGNED,
    name VARCHAR(50),
    address VARCHAR(100),
    phoneNumberId INT UNSIGNED,
    FOREIGN KEY (companylicenseId) REFERENCES CompanyLicence(id),
    FOREIGN KEY (phoneNumberId) REFERENCES phoneNumber(id)
);

CREATE TABLE Access
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    rights VARCHAR(4) NOT NULL
);
CREATE TABLE Employee 
(
	id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY NOT NULL,
    accessId INT UNSIGNED NOT NULL, 
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30) NOT NULL,
    employeeid VARCHAR(10) ,
    email VARCHAR(50),
    phoneNumberId INT UNSIGNED,
	FOREIGN KEY (phoneNumberId) REFERENCES phoneNumber(id),
    FOREIGN KEY (accessId) REFERENCES access(id)
);




