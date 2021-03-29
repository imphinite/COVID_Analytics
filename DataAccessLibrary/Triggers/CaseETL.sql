SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[CaseETL]
   ON  [COVID_Analytics].[dbo].[Cases] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @CASE_ID nvarchar(128);
	DECLARE @REPORTED_DATE datetime;
	DECLARE @DIM_HA_KEY nvarchar(128);
	DECLARE @SEX nvarchar(128);
	DECLARE @DIM_AGE_GROUP_KEY nvarchar(128);
	DECLARE @CLASSIFICATION_REPORTED nvarchar(128);

	IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT * FROM deleted)
	-- ON UPDATE
	BEGIN
		SELECT @CASE_ID = i.CaseID
			,@REPORTED_DATE = i.ReportedDate
            ,@DIM_HA_KEY = dha.DimHealthAuthorityKey
			,@SEX = i.SEX
			,@DIM_AGE_GROUP_KEY = dag.DimAgeGroupKey
			,@CLASSIFICATION_REPORTED = i.ClassificationReported
        FROM inserted i
		LEFT JOIN DimHealthAuthorities dha ON i.HealthAuthorityID = dha.HealthAuthorityID
		LEFT JOIN DimRegions dr ON dr.DimRegionKey = dha.DimRegionKey
		LEFT JOIN DimAgeGroups dag ON dag.AgeGroupID = i.AgeGroupID;

        UPDATE [dbo].DimCases
		SET ReportedDate = @REPORTED_DATE,
            DimHealthAuthorityKey = @DIM_HA_KEY,
			SEX = @SEX,
			DimAgeGroupKey = @DIM_AGE_GROUP_KEY,
			ClassificationReported = @CLASSIFICATION_REPORTED
        WHERE CaseID = @CASE_ID;
	END
	ELSE IF EXISTS (SELECT 1 FROM inserted)
	-- ON INSERT
	BEGIN
		INSERT INTO dbo.DimCases(DimCaseKey, CaseID, ReportedDate, DimHealthAuthorityKey, Sex, DimAgeGroupKey, ClassificationReported)
		SELECT NEWID(), i.CaseID, i.ReportedDate, dha.DimHealthAuthorityKey, i.Sex, dag.DimAgeGroupKey, i.ClassificationReported
        FROM inserted i
		LEFT JOIN DimHealthAuthorities dha ON i.HealthAuthorityID = dha.HealthAuthorityID
		LEFT JOIN DimRegions dr ON dr.DimRegionKey = dha.DimRegionKey
		LEFT JOIN DimAgeGroups dag ON dag.AgeGroupID = i.AgeGroupID;
	END
	ELSE IF EXISTS (SELECT 1 FROM deleted)
	-- ON DELETE
	BEGIN
		SELECT @CASE_ID = CaseID
        FROM DELETED;

        DELETE [dbo].DimCases
        WHERE CaseID = @CASE_ID
	END
END
