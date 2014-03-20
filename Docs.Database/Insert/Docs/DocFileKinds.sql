SET IDENTITY_INSERT DocFileKinds ON

INSERT INTO DocFileKinds(DocFileKindId, Name, Alias, IsActive) 
          select 1, N'Вътрешен файл', N'PrivateAttachedFile', 1
union all select 2, N'Публичен файл', N'PublicAttachedFile', 1

SET IDENTITY_INSERT DocFileKinds OFF
GO
