/* =========================================================
   Training Shipping Management System
   Database: TrainingShippingDB

   IMPORTANT:
   This is a completely fictional training database.
   No production/company data is used.
   ========================================================= */

USE master;
GO

IF DB_ID('TrainingShippingDB') IS NOT NULL
BEGIN
    ALTER DATABASE TrainingShippingDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE TrainingShippingDB;
END
GO

CREATE DATABASE TrainingShippingDB;
GO

USE TrainingShippingDB;
GO


/* =========================================================
   CLIENTS
   ========================================================= */

CREATE TABLE Clients
(
    ID INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    TaxNumber NVARCHAR(50) NULL,
    Phone NVARCHAR(50) NULL,
    Address NVARCHAR(300) NULL,

    CONSTRAINT PK_Clients PRIMARY KEY (ID),
    CONSTRAINT UQ_Clients_Email UNIQUE (Email)
);
GO


/* =========================================================
   VOYAGES
   ========================================================= */

CREATE TABLE Voyages
(
    ID INT IDENTITY(1,1) NOT NULL,
    VoyageNumber NVARCHAR(50) NOT NULL,
    VesselName NVARCHAR(200) NOT NULL,
    ETA DATETIME NOT NULL,
    ETD DATETIME NOT NULL,

    CONSTRAINT PK_Voyages PRIMARY KEY (ID),
    CONSTRAINT UQ_Voyages_VoyageNumber UNIQUE (VoyageNumber),
    CONSTRAINT CK_Voyages_DateSequence CHECK (ETD >= ETA)
);
GO


/* =========================================================
   BILLS
   ========================================================= */

CREATE TABLE Bills
(
    ID INT IDENTITY(1,1) NOT NULL,
    BillNumber NVARCHAR(100) NOT NULL,
    ClientID INT NOT NULL,
    VoyageID INT NOT NULL,
    GrossWeight DECIMAL(18,2) NOT NULL,
    NetWeight DECIMAL(18,2) NOT NULL,

    CONSTRAINT PK_Bills PRIMARY KEY (ID),

    CONSTRAINT UQ_Bills_BillNumber
        UNIQUE (BillNumber),

    CONSTRAINT FK_Bills_Clients
        FOREIGN KEY (ClientID)
        REFERENCES Clients(ID),

    CONSTRAINT FK_Bills_Voyages
        FOREIGN KEY (VoyageID)
        REFERENCES Voyages(ID),

    CONSTRAINT CK_Bills_GrossWeight
        CHECK (GrossWeight >= 0),

    CONSTRAINT CK_Bills_NetWeight
        CHECK (NetWeight >= 0),

    CONSTRAINT CK_Bills_NetLessThanGross
        CHECK (NetWeight <= GrossWeight)
);
GO


/* =========================================================
   CONTAINERS
   ========================================================= */

CREATE TABLE Containers
(
    ID INT IDENTITY(1,1) NOT NULL,
    ContainerNumber NVARCHAR(20) NOT NULL,
    ContainerType NVARCHAR(20) NOT NULL,
    BillID INT NOT NULL,

    CONSTRAINT PK_Containers PRIMARY KEY (ID),

    CONSTRAINT UQ_Containers_ContainerNumber
        UNIQUE (ContainerNumber),

    CONSTRAINT FK_Containers_Bills
        FOREIGN KEY (BillID)
        REFERENCES Bills(ID)
);
GO


/* =========================================================
   INDEXES
   ========================================================= */

CREATE INDEX IX_Bills_ClientID
ON Bills(ClientID);
GO

CREATE INDEX IX_Bills_VoyageID
ON Bills(VoyageID);
GO

CREATE INDEX IX_Containers_BillID
ON Containers(BillID);
GO

CREATE INDEX IX_Clients_Name
ON Clients(Name);
GO

CREATE INDEX IX_Voyages_VoyageNumber
ON Voyages(VoyageNumber);
GO

PRINT 'Database and tables created successfully.';
GO
