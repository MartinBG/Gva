SET IDENTITY_INSERT [UnitRelations] ON

INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(1000,1000,NULL,1000);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(1001,1001,1000,1000);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(1002,1002,1000, 1000);

SET IDENTITY_INSERT [UnitRelations] OFF
GO

