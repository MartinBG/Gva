SET IDENTITY_INSERT [Units] ON

INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(1000,N'Системни',1,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(1001,N'Система',2,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(1002,N'admin',3,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(1003,N'system',3,0,1);

SET IDENTITY_INSERT [Units] OFF
GO

