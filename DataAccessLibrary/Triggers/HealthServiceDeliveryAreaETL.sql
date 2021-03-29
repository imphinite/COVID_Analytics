SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[HealthServiceDeliveryAreaETL]
   ON  [COVID_Analytics].[dbo].[HealthServiceDeliveryAreas] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @AREA nvarchar(128);
	DECLARE @HSDA_ID nvarchar(128);
	DECLARE @HA_ID nvarchar(128);
	DECLARE @DIM_HA_KEY nvarchar(128);

	IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT * FROM deleted)
	-- ON UPDATE
	BEGIN
		SELECT @AREA = [AREA]
            ,@HSDA_ID = [HealthServiceDeliveryAreaID]
			,@HA_ID = i.HealthAuthorityID
			,@DIM_HA_KEY = [DimHealthAuthorityKey]
        FROM inserted i
		LEFT JOIN DimHealthAuthorities dha ON i.HealthAuthorityID = dha.HealthAuthorityID;

        UPDATE [dbo].DimHealthServiceDeliveryAreas
		SET Area = @AREA,
			DimHealthAuthorityKey = @DIM_HA_KEY
        WHERE HealthServiceDeliveryAreaID = @HSDA_ID;
	END
	ELSE IF EXISTS (SELECT 1 FROM inserted)
	-- ON INSERT
	BEGIN
		INSERT INTO dbo.DimHealthServiceDeliveryAreas(DimHealthServiceDeliveryAreaKey, HealthServiceDeliveryAreaID, Area, DimHealthAuthorityKey)
		SELECT NEWID(), i.HealthServiceDeliveryAreaID, i.Area, dha.DimHealthAuthorityKey
        FROM inserted i
		LEFT JOIN DimHealthAuthorities dha ON i.HealthAuthorityID = dha.HealthAuthorityID;
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	-- ON DELETE
	BEGIN
		SELECT @HSDA_ID = [HealthServiceDeliveryAreaID]
        FROM DELETED;

        DELETE [dbo].DimHealthServiceDeliveryAreas
        WHERE HealthServiceDeliveryAreaID=@HSDA_ID
	END
END
