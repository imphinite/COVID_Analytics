SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].FactCaseETL
   ON  [COVID_Analytics].[dbo].DimCases
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @AGE_GROUP_ID nvarchar(128);
	DECLARE @RANGE nvarchar(128);

	IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT * FROM deleted)
	-- ON UPDATE
	BEGIN
		SELECT @AGE_GROUP_ID = AgeGroupID
            ,@RANGE = [Range]
        FROM inserted AS i;

        UPDATE [dbo].DimAgeGroups
		SET Range = @RANGE
        WHERE AgeGroupID = @AGE_GROUP_ID;
	END
	ELSE IF EXISTS (SELECT 1 FROM inserted)
	-- ON INSERT
	BEGIN
		INSERT INTO dbo.DimAgeGroups(DimAgeGroupKey, AgeGroupID, Range)
		SELECT NEWID(), AgeGroupID, Range FROM inserted AS i;
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	-- ON DELETE
	BEGIN
		SELECT @AGE_GROUP_ID = AgeGroupID
        FROM DELETED;

        DELETE [dbo].DimAgeGroups
        WHERE AgeGroupID = @AGE_GROUP_ID
	END
END
