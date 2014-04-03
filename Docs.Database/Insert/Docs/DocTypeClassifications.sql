SET IDENTITY_INSERT [DocTypeClassifications] ON

INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(1,101,1,1,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(2,102,1,1,1);

SET IDENTITY_INSERT [DocTypeClassifications] OFF
GO

DECLARE @i INT

WHILE EXISTS(SELECT null FROM DocTypes WHERE DocTypeId NOT IN (SELECT DocTypeId FROM DocTypeClassifications))
BEGIN
	SELECT TOP 1 @i = DocTypeId FROM DocTypes WHERE DocTypeId NOT IN (SELECT DocTypeId FROM DocTypeClassifications)
	INSERT INTO [DocTypeClassifications]([DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(@i,1,1,1);
END 

GO
