USE TrainingShippingDB;
GO

/* =========================================================
   DROP EXISTING PROCEDURES / VIEWS IF THEY EXIST
   ========================================================= */

DROP VIEW IF EXISTS vw_BillShippingSummary;
GO

DROP VIEW IF EXISTS vw_ContainerDetails;
GO

DROP PROCEDURE IF EXISTS GetClients;
GO

DROP PROCEDURE IF EXISTS GetClientByID;
GO

DROP PROCEDURE IF EXISTS InsertClient;
GO

DROP PROCEDURE IF EXISTS UpdateClient;
GO

DROP PROCEDURE IF EXISTS DeleteClient;
GO

DROP PROCEDURE IF EXISTS SearchClients;
GO

DROP PROCEDURE IF EXISTS GetVoyages;
GO

DROP PROCEDURE IF EXISTS GetVoyageByID;
GO

DROP PROCEDURE IF EXISTS InsertVoyage;
GO

DROP PROCEDURE IF EXISTS UpdateVoyage;
GO

DROP PROCEDURE IF EXISTS DeleteVoyage;
GO

DROP PROCEDURE IF EXISTS SearchVoyages;
GO

DROP PROCEDURE IF EXISTS GetBills;
GO

DROP PROCEDURE IF EXISTS GetBillByID;
GO

DROP PROCEDURE IF EXISTS InsertBill;
GO

DROP PROCEDURE IF EXISTS UpdateBill;
GO

DROP PROCEDURE IF EXISTS DeleteBill;
GO

DROP PROCEDURE IF EXISTS SearchBills;
GO

DROP PROCEDURE IF EXISTS GetContainers;
GO

DROP PROCEDURE IF EXISTS GetContainerByID;
GO

DROP PROCEDURE IF EXISTS InsertContainer;
GO

DROP PROCEDURE IF EXISTS UpdateContainer;
GO

DROP PROCEDURE IF EXISTS DeleteContainer;
GO

DROP PROCEDURE IF EXISTS SearchContainers;
GO

DROP PROCEDURE IF EXISTS GetDashboardStatistics;
GO


/* =========================================================
   CLIENT PROCEDURES
   ========================================================= */

CREATE PROCEDURE GetClients
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        Name,
        Email,
        Phone,
        Address
    FROM Clients
    ORDER BY ID DESC;
END
GO


CREATE PROCEDURE GetClientByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        Name,
        Email,
        Phone,
        Address
    FROM Clients
    WHERE ID = @ID;
END
GO


CREATE PROCEDURE InsertClient
    @Name NVARCHAR(200),
    @Email NVARCHAR(200),
    @Phone NVARCHAR(50),
    @Address NVARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clients
    (
        Name,
        Email,
        Phone,
        Address
    )
    VALUES
    (
        @Name,
        @Email,
        @Phone,
        @Address
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ID;
END
GO


CREATE PROCEDURE UpdateClient
    @ID INT,
    @Name NVARCHAR(200),
    @Email NVARCHAR(200),
    @Phone NVARCHAR(50),
    @Address NVARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clients
    SET
        Name = @Name,
        Email = @Email,
        Phone = @Phone,
        Address = @Address
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE DeleteClient
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Clients
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE SearchClients
    @Search NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        Name,
        Email,
        Phone,
        Address
    FROM Clients
    WHERE
        Name LIKE '%' + @Search + '%'
        OR Email LIKE '%' + @Search + '%'
        OR Phone LIKE '%' + @Search + '%'
        OR Address LIKE '%' + @Search + '%'
    ORDER BY Name;
END
GO


/* =========================================================
   VOYAGE PROCEDURES
   ========================================================= */

CREATE PROCEDURE GetVoyages
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        VoyageNumber,
        VesselName,
        ETA,
        ETD
    FROM Voyages
    ORDER BY ETA;
END
GO


CREATE PROCEDURE GetVoyageByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        VoyageNumber,
        VesselName,
        ETA,
        ETD
    FROM Voyages
    WHERE ID = @ID;
END
GO


CREATE PROCEDURE InsertVoyage
    @VoyageNumber NVARCHAR(50),
    @VesselName NVARCHAR(200),
    @ETA DATETIME,
    @ETD DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Voyages
    (
        VoyageNumber,
        VesselName,
        ETA,
        ETD
    )
    VALUES
    (
        @VoyageNumber,
        @VesselName,
        @ETA,
        @ETD
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ID;
END
GO


CREATE PROCEDURE UpdateVoyage
    @ID INT,
    @VoyageNumber NVARCHAR(50),
    @VesselName NVARCHAR(200),
    @ETA DATETIME,
    @ETD DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Voyages
    SET
        VoyageNumber = @VoyageNumber,
        VesselName = @VesselName,
        ETA = @ETA,
        ETD = @ETD
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE DeleteVoyage
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Voyages
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE SearchVoyages
    @Search NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        VoyageNumber,
        VesselName,
        ETA,
        ETD
    FROM Voyages
    WHERE
        VoyageNumber LIKE '%' + @Search + '%'
        OR VesselName LIKE '%' + @Search + '%'
    ORDER BY ETA;
END
GO


/* =========================================================
   BILL PROCEDURES
   ========================================================= */

CREATE PROCEDURE GetBills
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        BillNumber,
        ClientID,
        VoyageID,
        GrossWeight,
        NetWeight
    FROM Bills
    ORDER BY ID DESC;
END
GO


CREATE PROCEDURE GetBillByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        BillNumber,
        ClientID,
        VoyageID,
        GrossWeight,
        NetWeight
    FROM Bills
    WHERE ID = @ID;
END
GO


CREATE PROCEDURE InsertBill
    @BillNumber NVARCHAR(100),
    @ClientID INT,
    @VoyageID INT,
    @GrossWeight DECIMAL(18,2),
    @NetWeight DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Bills
    (
        BillNumber,
        ClientID,
        VoyageID,
        GrossWeight,
        NetWeight
    )
    VALUES
    (
        @BillNumber,
        @ClientID,
        @VoyageID,
        @GrossWeight,
        @NetWeight
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ID;
END
GO


CREATE PROCEDURE UpdateBill
    @ID INT,
    @BillNumber NVARCHAR(100),
    @ClientID INT,
    @VoyageID INT,
    @GrossWeight DECIMAL(18,2),
    @NetWeight DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Bills
    SET
        BillNumber = @BillNumber,
        ClientID = @ClientID,
        VoyageID = @VoyageID,
        GrossWeight = @GrossWeight,
        NetWeight = @NetWeight
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE DeleteBill
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Bills
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE SearchBills
    @Search NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        B.ID,
        B.BillNumber,
        B.ClientID,
        B.VoyageID,
        B.GrossWeight,
        B.NetWeight
    FROM Bills B
    WHERE
        B.BillNumber LIKE '%' + @Search + '%'
    ORDER BY B.ID DESC;
END
GO


/* =========================================================
   CONTAINER PROCEDURES
   ========================================================= */

CREATE PROCEDURE GetContainers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        ContainerNumber,
        ContainerType,
        BillID
    FROM Containers
    ORDER BY ID DESC;
END
GO


CREATE PROCEDURE GetContainerByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        ContainerNumber,
        ContainerType,
        BillID
    FROM Containers
    WHERE ID = @ID;
END
GO


CREATE PROCEDURE InsertContainer
    @ContainerNumber NVARCHAR(20),
    @ContainerType NVARCHAR(20),
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Containers
    (
        ContainerNumber,
        ContainerType,
        BillID
    )
    VALUES
    (
        @ContainerNumber,
        @ContainerType,
        @BillID
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ID;
END
GO


CREATE PROCEDURE UpdateContainer
    @ID INT,
    @ContainerNumber NVARCHAR(20),
    @ContainerType NVARCHAR(20),
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Containers
    SET
        ContainerNumber = @ContainerNumber,
        ContainerType = @ContainerType,
        BillID = @BillID
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE DeleteContainer
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Containers
    WHERE ID = @ID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


CREATE PROCEDURE SearchContainers
    @Search NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ID,
        ContainerNumber,
        ContainerType,
        BillID
    FROM Containers
    WHERE
        ContainerNumber LIKE '%' + @Search + '%'
        OR ContainerType LIKE '%' + @Search + '%'
    ORDER BY ID DESC;
END
GO


/* =========================================================
   VIEW 1 - BILL SHIPPING SUMMARY
   ========================================================= */

CREATE VIEW vw_BillShippingSummary
AS
SELECT
    B.ID AS BillID,
    B.BillNumber,

    C.ID AS ClientID,
    C.Name AS ClientName,

    V.ID AS VoyageID,
    V.VoyageNumber,
    V.VesselName,
    V.ETA,
    V.ETD,

    B.GrossWeight,
    B.NetWeight,

    COUNT(CT.ID) AS ContainerCount

FROM Bills B

INNER JOIN Clients C
    ON C.ID = B.ClientID

INNER JOIN Voyages V
    ON V.ID = B.VoyageID

LEFT JOIN Containers CT
    ON CT.BillID = B.ID

GROUP BY
    B.ID,
    B.BillNumber,
    C.ID,
    C.Name,
    V.ID,
    V.VoyageNumber,
    V.VesselName,
    V.ETA,
    V.ETD,
    B.GrossWeight,
    B.NetWeight;
GO


/* =========================================================
   VIEW 2 - CONTAINER DETAILS
   ========================================================= */

CREATE VIEW vw_ContainerDetails
AS
SELECT
    CT.ID AS ContainerID,
    CT.ContainerNumber,
    CT.ContainerType,

    B.ID AS BillID,
    B.BillNumber,

    C.ID AS ClientID,
    C.Name AS ClientName,

    V.ID AS VoyageID,
    V.VoyageNumber,
    V.VesselName

FROM Containers CT

INNER JOIN Bills B
    ON B.ID = CT.BillID

INNER JOIN Clients C
    ON C.ID = B.ClientID

INNER JOIN Voyages V
    ON V.ID = B.VoyageID;
GO


/* =========================================================
   DASHBOARD STATISTICS
   ========================================================= */

CREATE PROCEDURE GetDashboardStatistics
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        (SELECT COUNT(*) FROM Clients) AS TotalClients,
        (SELECT COUNT(*) FROM Voyages) AS TotalVoyages,
        (SELECT COUNT(*) FROM Bills) AS TotalBills,
        (SELECT COUNT(*) FROM Containers) AS TotalContainers;
END
GO


PRINT 'Stored Procedures and Views created successfully.';
GO