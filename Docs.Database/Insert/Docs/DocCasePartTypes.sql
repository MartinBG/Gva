SET IDENTITY_INSERT DocCasePartTypes ON

INSERT INTO DocCasePartTypes(DocCasePartTypeId, Name, Alias, Description, IsActive) VALUES (1, N'Официален', N'Public', NULL, 1)
INSERT INTO DocCasePartTypes(DocCasePartTypeId, Name, Alias, Description, IsActive) VALUES (2, N'Вътрешен', N'Internal', NULL, 1)
INSERT INTO DocCasePartTypes(DocCasePartTypeId, Name, Alias, Description, IsActive) VALUES (3, N'Контролен', N'Control', NULL, 1)

SET IDENTITY_INSERT DocCasePartTypes OFF
GO 
