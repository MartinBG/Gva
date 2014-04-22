SET IDENTITY_INSERT DocFileTypes ON

INSERT INTO DocFileTypes(DocFileTypeId, HasEmbeddedUri, Name, Alias, DocTypeUri, MimeType, Extention, IsEditable, IsActive) 
          select 1, 0, N'Документ Microsotf Word (.doc)', N'DOC', N'', 'application/msword', N'.doc', 0, 1
union all select 2, 0, N'Документ Microsotf Word (.docx)', N'DOCX', N'', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', N'.docx', 0, 1
union all select 3, 0, N'Документ Microsotf Excel (.xls)', N'XLS', N'', 'application/vnd.ms-excel', N'.xls', 0, 1
union all select 4, 0, N'Документ Microsotf Excel (.xlsx)', N'XLSX', N'', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', N'.xlsx', 0, 1
union all select 5, 0, N'Документ в преносим формат (.pdf)', N'PDF', N'', 'application/pdf', N'.pdf', 0, 1
union all select 6, 0, N'Текстов документ(.txt)', N'TXT', N'', 'text/plain', N'.txt', 0, 1
union all select 7, 0, N'Extensible Markup Language (.xml)', N'XML', N'', 'application/xml', N'.xml', 0, 1
union all select 8, 0, N'JSON', N'JSON', N'', 'application/json', N'.json', 0, 1
union all select 9, 0, N'Неопределен', N'UnknownBinary', N'', 'application/octet-stream', N'.*', 0, 1

--Услуги
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (201, N'Документ УРИ 0010-004686', N'R-4686', N'0010-004686', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (202, N'Документ УРИ 0010-005090', N'R-5090', N'0010-005090', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (203, N'Документ УРИ 0010-005000', N'R-5000', N'0010-005000', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (204, N'Документ УРИ 0010-004834', N'R-4834', N'0010-004834', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (205, N'Документ УРИ 0010-004186', N'R-4186', N'0010-004186', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (206, N'Документ УРИ 0010-004244', N'R-4244', N'0010-004244', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (207, N'Документ УРИ 0010-004864', N'R-4864', N'0010-004864', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (208, N'Документ УРИ 0010-004242', N'R-4242', N'0010-004242', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (209, N'Документ УРИ 0010-004240', N'R-4240', N'0010-004240', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (210, N'Документ УРИ 0010-004296', N'R-4296', N'0010-004296', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (211, N'Документ УРИ 0010-004900', N'R-4900', N'0010-004900', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (212, N'Документ УРИ 0010-004860', N'R-4860', N'0010-004860', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (213, N'Документ УРИ 0010-004862', N'R-4862', N'0010-004862', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (214, N'Документ УРИ 0010-004356', N'R-4356', N'0010-004356', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (215, N'Документ УРИ 0010-004284', N'R-4284', N'0010-004284', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (216, N'Документ УРИ 0010-004958', N'R-4958', N'0010-004958', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (217, N'Документ УРИ 0010-004824', N'R-4824', N'0010-004824', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (218, N'Документ УРИ 0010-004738', N'R-4738', N'0010-004738', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (219, N'Документ УРИ 0010-004764', N'R-4764', N'0010-004764', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (220, N'Документ УРИ 0010-004766', N'R-4766', N'0010-004766', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (221, N'Документ УРИ 0010-004588', N'R-4588', N'0010-004588', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (222, N'Документ УРИ 0010-004590', N'R-4590', N'0010-004590', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (223, N'Документ УРИ 0010-004614', N'R-4614', N'0010-004614', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (224, N'Документ УРИ 0010-004598', N'R-4598', N'0010-004598', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (225, N'Документ УРИ 0010-004606', N'R-4606', N'0010-004606', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (226, N'Документ УРИ 0010-004470', N'R-4470', N'0010-004470', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (227, N'Документ УРИ 0010-004544', N'R-4544', N'0010-004544', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (228, N'Документ УРИ 0010-004514', N'R-4514', N'0010-004514', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (229, N'Документ УРИ 0010-004810', N'R-4810', N'0010-004810', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (230, N'Документ УРИ 0010-004490', N'R-4490', N'0010-004490', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (231, N'Документ УРИ 0010-004396', N'R-4396', N'0010-004396', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (232, N'Документ УРИ 0010-004566', N'R-4566', N'0010-004566', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (233, N'Документ УРИ 0010-004578', N'R-4578', N'0010-004578', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (234, N'Документ УРИ 0010-004926', N'R-4926', N'0010-004926', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (235, N'Документ УРИ 0010-004378', N'R-4378', N'0010-004378', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (236, N'Документ УРИ 0010-000090', N'R-0090', N'0010-000090', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (237, N'Документ УРИ 0010-000101', N'R-0101', N'0010-000101', 0, N'application/xml', N'.xml', 1, 1)
INSERT [dbo].[DocFileTypes] ([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES (238, N'Документ УРИ 0010-003010', N'R-3010', N'0010-003010', 0, N'application/xml', N'.xml', 1, 1)

--Отговори на услуги
INSERT INTO DocFileTypes(DocFileTypeId, HasEmbeddedUri, Name, Alias, DocTypeUri, MimeType, Extention, IsEditable, IsActive) 
		  select 401, 1, N'Съобщение, че получаването не се потвърждава', N'ReceiptNotAcknowledgedMessage', N'0010-000001', 'application/xml', N'.xml', 1, 1
union all select 402, 1, N'Потвърждаване за получаване', N'ReceiptAcknowledgedMessage', N'0010-000002', 'application/xml', N'.xml', 1, 1
union all select 403, 1, N'Указания за отстраняване на нередовности', N'RemovingIrregularitiesInstructions', N'0010-003010', 'application/xml', N'.xml', 1, 1


SET IDENTITY_INSERT DocFileTypes OFF
GO
