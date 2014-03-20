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

SET IDENTITY_INSERT DocFileTypes OFF
GO
