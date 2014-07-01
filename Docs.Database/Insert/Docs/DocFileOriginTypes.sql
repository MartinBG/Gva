SET IDENTITY_INSERT DocFileOriginTypes ON

INSERT INTO DocFileOriginTypes(DocFileOriginTypeId, Name, Alias, IsActive) 
		  select 1, N'Прикачен файл', N'AttachedFile', 1
union all select 2, N'Електронно заявление', N'EApplication', 1
union all select 3, N'Прикачен файл в електронно заявление', N'EApplicationAttachedFile', 1
union all select 4, N'Редактируем файл', N'EditableFile', 0

SET IDENTITY_INSERT DocFileOriginTypes OFF
GO
