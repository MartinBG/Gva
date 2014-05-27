print 'Excel Insert ClassificationRelations'
GO

SET IDENTITY_INSERT [ClassificationRelations] ON
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(1,1,NULL,1);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(2,2,NULL,2);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(3,3,2,2);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(4,4,2,2);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(5,5,2,2);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(6,6,NULL,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(7,7,6,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(8,8,7,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(9,9,7,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(10,10,7,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(11,11,6,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(12,12,11,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(13,13,11,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(14,14,11,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(15,15,6,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(16,16,15,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(17,17,15,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(18,18,15,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(19,19,6,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(20,20,19,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(21,21,19,6);
INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES(22,22,19,6);
SET IDENTITY_INSERT [ClassificationRelations] OFF
GO

