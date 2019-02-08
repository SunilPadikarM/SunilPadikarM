Drop TABLE if EXISTS WRITES;
Drop TABLE if EXISTS WORK_HOURS;
Drop TABLE if EXISTS TRANSACION_DETAILS;
Drop TABLE if EXISTS TRANSACTION;
Drop TABLE if EXISTS EXPENSES;
Drop TABLE if EXISTS AUTHOR;
Drop TABLE if EXISTS ARTICLE;
Drop TABLE if EXISTS MAGAZINE;
Drop TABLE if EXISTS ITEM;

CREATE TABLE `ITEM` (
  `ID` int(11) NOT NULL auto_increment,
  `Price` double NOT NULL,
  PRIMARY KEY (`ID`)
);

CREATE TABLE `MAGAZINE` (
  `Maganize_ID` int(11) NOT NULL,
  `Title` varchar(200) NOT NULL,
  `Volume` double NOT NULL,
  `Year_Publish` varchar(45) NOT NULL,
  PRIMARY KEY (`Title`,`Volume`),
  UNIQUE KEY `Maganize_ID_UNIQUE` (`Maganize_ID`),
  CONSTRAINT `fk_item_ID` FOREIGN KEY (`Maganize_ID`) REFERENCES `ITEM` (`id`)
);

CREATE TABLE `ARTICLE` (
  `Article_ID` int(11) NOT NULL auto_increment,
  `Title` varchar(200) DEFAULT NULL,
  `Page_Start` int(11) DEFAULT NULL,
  `Page_End` int(11) DEFAULT NULL,
  `Magazine_ID` int(11) NOT NULL,
  PRIMARY KEY (`Article_ID`),
  KEY `fk_Article_Id_idx` (`Magazine_ID`),
  CONSTRAINT `fk_Article_Id` FOREIGN KEY (`Magazine_ID`) REFERENCES `MAGAZINE` (`Maganize_ID`)
);

CREATE TABLE `AUTHOR` (
  `Author_ID` int(11) NOT NULL  auto_increment,
  `A_Name` varchar(45) DEFAULT NULL,
  `A_Last` varchar(45) DEFAULT NULL,
  `Email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Author_ID`)
);

Drop TABLE if EXISTS CUSTOMER;
CREATE TABLE `CUSTOMER` (
  `CID` int(11) NOT NULL auto_increment,
  `C_Name` varchar(45) DEFAULT NULL,
  `C_Last` varchar(45) DEFAULT NULL,
  `Phone` bigint(15) DEFAULT NULL,
  `Mail_Addr` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CID`)
);

Drop TABLE if EXISTS EMPLOYEE;
CREATE TABLE `EMPLOYEE` (
  `SIN` int(11) NOT NULL,
  `E_Name` varchar(100) DEFAULT NULL,
  `E_Last` varchar(100) DEFAULT NULL,
  `Rate_Hour` double DEFAULT NULL,
  PRIMARY KEY (`SIN`)
);

CREATE TABLE `EXPENSES` (
  `Year` int(11) NOT NULL,
  `Month` int(11) NOT NULL,
  `Electricity` double DEFAULT NULL,
  `Heat` double DEFAULT NULL,
  `Water` double DEFAULT NULL,
  `Rent` double DEFAULT NULL,
  `Total_Emp_Exp` double DEFAULT NULL,
  PRIMARY KEY (`Month`,`Year`)
);



CREATE TABLE `TRANSACTION` (
  `Trxn_Number` int(11) NOT NULL auto_increment,
  `Date` date DEFAULT NULL,
  `CID` int(11) DEFAULT NULL,
  `Discount_Code` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Trxn_Number`),
  KEY `fk_transaction_CID_idx` (`CID`),
  CONSTRAINT `fk_transaction_CID` FOREIGN KEY (`CID`) REFERENCES `CUSTOMER` (`cid`)
);

CREATE TABLE `TRANSACION_DETAILS` (
  `Trxn_Number` int(11) NOT NULL,
  `ID` int(11) NOT NULL,
  `Quantity` int(11) DEFAULT NULL,
  PRIMARY KEY (`Trxn_Number`,`ID`),
  KEY `fk_transaction_details_CID_idx` (`ID`),
  CONSTRAINT `fk_transaction_details_CID` FOREIGN KEY (`ID`) REFERENCES `ITEM` (`id`),
  CONSTRAINT `fk_transaction_details_Trnx_Number` FOREIGN KEY (`Trxn_Number`) REFERENCES `TRANSACTION` (`Trxn_Number`)
);

CREATE TABLE `WORK_HOURS` (
  `Date` datetime NOT NULL,
  `SIN` int(11) NOT NULL,
  `Hours_Worked` double DEFAULT NULL,
  PRIMARY KEY (`Date`,`SIN`),
  KEY `fk_work_hours_SIN_idx` (`SIN`),
  CONSTRAINT `fk_work_hours_SIN` FOREIGN KEY (`SIN`) REFERENCES `EMPLOYEE` (`SIN`)
);

CREATE TABLE `WRITES` (
  `Author_ID` int(11) NOT NULL,
  `Article_ID` int(11) NOT NULL,
  PRIMARY KEY (`Author_ID`,`Article_ID`),
  KEY `fk_writes_article_id_idx` (`Article_ID`),
  CONSTRAINT `fk_writes_article_id` FOREIGN KEY (`Article_ID`) REFERENCES `ARTICLE` (`Article_ID`),
  CONSTRAINT `fk_writes_author_id` FOREIGN KEY (`Author_ID`) REFERENCES `AUTHOR` (`Author_ID`)
);
