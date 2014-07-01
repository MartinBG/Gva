print 'Excel Insert DocFileTypes'
GO

SET IDENTITY_INSERT [DocFileTypes] ON
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(201,N'Документ УРИ 0010-006056',N'R-6056',N'0010-006056',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(202,N'Документ УРИ 0010-006016',N'R-6016',N'0010-006016',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(203,N'Документ УРИ 0010-006054',N'R-6054',N'0010-006054',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(204,N'Документ УРИ 0010-006090',N'R-6090',N'0010-006090',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(401,N'Съобщение, че получаването не се потвърждава',N'ReceiptNotAcknowledgedMessage',N'0000-000001',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(402,N'Потвърждаване за получаване',N'ReceiptAcknowledgedMessage',N'0000-000002',1,'application/xml','.xml',1,1);
INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES(403,N'Указания за отстраняване на нередовности',N'RemovingIrregularitiesInstructions',N'0000-003010',1,'application/xml','.xml',1,1);
SET IDENTITY_INSERT [DocFileTypes] OFF
GO

