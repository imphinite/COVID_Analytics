SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[RegionETL]
   ON  [COVID_Analytics].[dbo].[Regions] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @REGION_ID nvarchar(128);
	DECLARE @NAME nvarchar(128);
	DECLARE @PROVINCE nvarchar(128);

	IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT * FROM deleted)
	-- ON UPDATE
	BEGIN
		SELECT @REGION_ID = [RegionID]
            ,@NAME = [Name]
            ,@PROVINCE =[Province]
        FROM inserted;

        UPDATE [dbo].DimRegions
		SET Name = @NAME,
			Province = @PROVINCE
        WHERE RegionID=@REGION_ID;
	END
	ELSE IF EXISTS (SELECT 1 FROM inserted)
	-- ON INSERT
	BEGIN
		INSERT INTO dbo.DimRegions(DimRegionKey, RegionID, Name, Province)
		SELECT NEWID(), RegionID, Name, Province FROM inserted AS i;
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	-- ON DELETE
	BEGIN
		SELECT @REGION_ID = [RegionID]
        FROM DELETED

        DELETE [dbo].DimRegions
        WHERE RegionID = @REGION_ID
	END
END
