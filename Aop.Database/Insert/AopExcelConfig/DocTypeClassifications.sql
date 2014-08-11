print 'Excel Insert DocTypeClassifications'
GO

SET IDENTITY_INSERT [DocTypeClassifications] ON
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(1,15,1,1,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(2,15,1,2,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(3,17,1,1,1);
INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES(4,17,1,2,1);
SET IDENTITY_INSERT [DocTypeClassifications] OFF
GO

