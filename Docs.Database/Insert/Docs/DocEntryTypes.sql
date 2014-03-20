SET IDENTITY_INSERT DocEntryTypes ON

INSERT INTO DocEntryTypes(DocEntryTypeId, Name, Alias, IsActive) VALUES (1, N'Документ', N'Document', 1)
INSERT INTO DocEntryTypes(DocEntryTypeId, Name, Alias, IsActive) VALUES (2, N'Резолюция', N'Resolution', 1)
INSERT INTO DocEntryTypes(DocEntryTypeId, Name, Alias, IsActive) VALUES (3, N'Задача', N'Task', 1)
INSERT INTO DocEntryTypes(DocEntryTypeId, Name, Alias, IsActive) VALUES (4, N'Забележка', N'Remark', 1)

SET IDENTITY_INSERT DocEntryTypes OFF
GO 
