print 'Excel Insert ClassificationRelations'
GO

SET IDENTITY_INSERT [ClassificationRelations] ON
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(1,1,NULL,1);
SET IDENTITY_INSERT [ClassificationRelations] OFF
GO

