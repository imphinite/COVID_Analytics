SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[HealthAuthorityETL]
   ON  [COVID_Analytics].[dbo].[HealthAuthorities] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @HA_ID nvarchar(128);
	DECLARE @REGION_ID nvarchar(128);
	DECLARE @DIM_REGION_KEY nvarchar(128);

	IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT * FROM deleted)
	-- ON UPDATE
	BEGIN
		SELECT @REGION_ID = i.RegionID,
			@DIM_REGION_KEY = dr.DimRegionKey
        FROM inserted i
		LEFT JOIN DimRegions dr ON i.RegionID = dr.RegionID;

        UPDATE [dbo].DimHealthAuthorities
		SET DimRegionKey = @DIM_REGION_KEY
        WHERE HealthAuthorityID = @HA_ID;
	END
	ELSE IF EXISTS (SELECT 1 FROM inserted)
	-- ON INSERT
	BEGIN
		INSERT INTO dbo.DimHealthAuthorities(DimHealthAuthorityKey, HealthAuthorityID, DimRegionKey)
		SELECT NEWID(), i.HealthAuthorityID, dr.DimRegionKey
		FROM inserted AS i
		LEFT JOIN DimRegions dr ON i.RegionID = dr.RegionID;
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	-- ON DELETE
	BEGIN
		SELECT @HA_ID = [HealthAuthorityID]
        FROM DELETED;

        DELETE [dbo].DimHealthAuthorities
        WHERE HealthAuthorityID = @HA_ID
	END
END
