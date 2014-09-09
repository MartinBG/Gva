print 'Excel Insert ClassificationRelations'
GO

SET IDENTITY_INSERT [ClassificationRelations] ON
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(1,1,NULL,1);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(2,2,NULL,2);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(3,3,NULL,3);
SET IDENTITY_INSERT [ClassificationRelations] OFF
GO

