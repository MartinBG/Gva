print 'Excel Insert DocFileTypes'
GO

SET IDENTITY_INSERT [DocFileTypes] ON
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(201,N'Документ УРИ 0010-004686',N'R-0001',N'0010-aop',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(401,N'Съобщение, че получаването не се потвърждава',N'ReceiptNotAcknowledgedMessage',N'0010-000001',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(402,N'Потвърждаване за получаване',N'ReceiptAcknowledgedMessage',N'0010-000002',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(403,N'Указания за отстраняване на нередовности',N'RemovingIrregularitiesInstructions',N'0010-003010',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(1001,N'Чеклист',N'Checklist',N'Checklist',0,'application/json','.json',1,1);
SET IDENTITY_INSERT [DocFileTypes] OFF
GO

