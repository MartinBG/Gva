print 'Excel Insert UnitClassifications'
GO

SET IDENTITY_INSERT [UnitClassifications] ON
INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationRoleId])VALUES(1,3,1,2);
INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationRoleId])VALUES(2,5,1,2);
INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationRoleId])VALUES(3,7,1,2);
SET IDENTITY_INSERT [UnitClassifications] OFF
GO

