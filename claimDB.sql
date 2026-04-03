
CREATE DATABASE ClaimDB;
GO

USE ClaimDB;
GO

CREATE TABLE Claims (
    ClaimId INT IDENTITY(1,1) PRIMARY KEY,
    ClaimNumber NVARCHAR(50),
    PatientName NVARCHAR(100),
    SubmissionDate DATETIME,
    ClaimAmount DECIMAL(18,2),
    ProcessingStatus NVARCHAR(50),
    Remarks NVARCHAR(255)
);
GO

CREATE TABLE ClaimHistory (
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,
    ClaimNumber NVARCHAR(50),
    Status NVARCHAR(50),
    UpdatedBy NVARCHAR(50),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    Remarks NVARCHAR(255)
);
GO

CREATE PROCEDURE sp_GetAllClaims
    @ClaimNumber NVARCHAR(50) = '',
    @Status NVARCHAR(50) = ''
AS
BEGIN
    SELECT *
    FROM Claims
    WHERE (@ClaimNumber = '' OR ClaimNumber = @ClaimNumber)
      AND (@Status = '' OR ProcessingStatus = @Status)
    ORDER BY SubmissionDate DESC;
END
GO

CREATE PROCEDURE sp_GetClaimById
    @ClaimId INT
AS
BEGIN
    SELECT *
    FROM Claims
    WHERE ClaimId = @ClaimId;
END
GO

CREATE PROCEDURE sp_UpdateClaim
    @ClaimId INT,
    @ProcessingStatus NVARCHAR(50),
    @Remarks NVARCHAR(255),
    @UpdatedBy NVARCHAR(50)
AS
BEGIN
    DECLARE @ClaimNumber NVARCHAR(50);

    SELECT @ClaimNumber = ClaimNumber
    FROM Claims
    WHERE ClaimId = @ClaimId;

    UPDATE Claims
    SET ProcessingStatus = @ProcessingStatus,
        Remarks = @Remarks
    WHERE ClaimId = @ClaimId;

    INSERT INTO ClaimHistory (ClaimNumber, Status, UpdatedBy, Remarks)
    VALUES (@ClaimNumber, @ProcessingStatus, @UpdatedBy, @Remarks);
END
GO

CREATE PROCEDURE sp_GetClaimHistory
    @ClaimNumber NVARCHAR(50)
AS
BEGIN
    SELECT ClaimNumber, Status, UpdatedBy, UpdatedDate, Remarks
    FROM ClaimHistory
    WHERE ClaimNumber = @ClaimNumber
    ORDER BY UpdatedDate DESC;
END
GO

