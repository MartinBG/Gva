print 'Excel Insert UnitRelations'
GO

SET IDENTITY_INSERT [UnitRelations] ON
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(1,1,NULL,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(2,2,1,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(3,3,2,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(4,4,1,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(5,5,4,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(6,6,1,1);
INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES(7,7,6,1);
SET IDENTITY_INSERT [UnitRelations] OFF
GO

