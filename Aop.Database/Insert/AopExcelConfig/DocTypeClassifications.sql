print 'Excel Insert DocTypeClassifications'
GO

SET IDENTITY_INSERT [DocTypeClassifications] ON
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(1,13,1,1,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(2,13,1,2,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(3,15,1,1,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(4,15,1,2,1);
SET IDENTITY_INSERT [DocTypeClassifications] OFF
GO

