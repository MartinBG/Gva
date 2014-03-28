SET IDENTITY_INSERT DocTypes ON

--Системни
INSERT INTO DocTypes(DocTypeId, DocTypeGroupId, IsElectronicService, Name, Alias, IsActive) VALUES (1, 1, 0, N'Документ', N'Document', 1)
INSERT INTO DocTypes(DocTypeId, DocTypeGroupId, IsElectronicService, Name, Alias, IsActive) VALUES (2, 1, 0, N'Резолюция', N'Resolution', 0)
INSERT INTO DocTypes(DocTypeId, DocTypeGroupId, IsElectronicService, Name, Alias, IsActive) VALUES (3, 1, 0, N'Задача', N'Task', 0)
INSERT INTO DocTypes(DocTypeId, DocTypeGroupId, IsElectronicService, Name, Alias, IsActive) VALUES (4, 1, 0, N'Забележка', N'Remark', 0)

--Общи
INSERT INTO DocTypes(DocTypeId, DocTypeGroupId, PrimaryRegisterIndexId, IsElectronicService, Name, Alias, IsActive) VALUES (101, 100, 4, 0, N'Писмо', N'', 1)

SET IDENTITY_INSERT DocTypes OFF


