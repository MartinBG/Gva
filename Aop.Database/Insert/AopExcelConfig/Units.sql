print 'Excel Insert Units'
GO

SET IDENTITY_INSERT [Units] ON
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(1,N'Системни',1,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(2,N'Система',2,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(3,N'system',3,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(4,N'Администратор',2,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(5,N'admin',3,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(6,N'Служител АОП',2,0,1);
INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES(7,N'systemuseraop',3,0,1);
SET IDENTITY_INSERT [Units] OFF
GO

