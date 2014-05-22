SET IDENTITY_INSERT DocFileOriginTypes ON

INSERT INTO DocFileOriginTypes(DocFileOriginTypeId, Name, Alias, IsActive) 
		  select 4, N'Редактируем файл', N'EditableFile', 0

SET IDENTITY_INSERT DocFileOriginTypes OFF
GO
