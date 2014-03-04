SET IDENTITY_INSERT DocUnitPermissions ON

INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (1, N'Четене', N'Read', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (2, N'Редакция', N'Edit', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (3, N'Регистриране', N'Register', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (4, N'Одобряване, съгласуване, подписване', N'Management', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (5, N'Ел.подписване', N'ESign', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (6, N'Приключване', N'Finish', 1)
INSERT INTO DocUnitPermissions(DocUnitPermissionId, Name, Alias, IsActive) VALUES (7, N'Сторниране', N'Reverse', 1)

SET IDENTITY_INSERT DocUnitPermissions OFF
GO 





