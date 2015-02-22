USE [SamplePerformanceDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('usp_FillSampleData') IS NOT NULL
DROP PROC usp_FillSampleData
GO
CREATE PROCEDURE dbo.usp_FillSampleData 
    @iterationsCount int = 10000000
AS
DECLARE @index INT = 0
  WHILE @index < @iterationsCount
	BEGIN
		INSERT INTO SamplePerformanceTable
		VALUES(GETDATE(), 'lorem ipsum')
		SET @iterationsCount -= 1
	END
RETURN 0 
GO

EXEC usp_FillSampleData
GO

EXEC sp_spaceused
GO

SELECT COUNT(*) FROM SamplePerformanceTable


DBCC DROPCLEANBUFFERS
DBCC FREEPROCCACHE
GO
SELECT sampleDate, sampleText 
FROM SamplePerformanceTable
WHERE sampleDate > DATEFROMPARTS(2015, 02,21)
GO
--Resulted time: 1:46

IF OBJECT_ID('IX_SamplePerformanceTable_sampleDate') IS NOT NULL
DROP INDEX IX_SamplePerformanceTable_sampleDate
ON dbo.SamplePerformanceTable
GO
CREATE INDEX IX_SamplePerformanceTable_sampleDate
    ON SamplePerformanceTable
    (sampleDate)
GO

DBCC DROPCLEANBUFFERS
DBCC FREEPROCCACHE
GO
DECLARE @Date DATETIME
SET @Date = DATEFROMPARTS(2015, 02, 21)
SELECT sampleDate 
FROM SamplePerformanceTable
WHERE sampleDate > @Date
GO
--Resulted time: 1:27