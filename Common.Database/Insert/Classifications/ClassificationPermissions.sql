﻿SET IDENTITY_INSERT ClassificationPermissions ON

INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (1, N'Четене', N'Read', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (2, N'Редакция', N'Edit', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (3, N'Регистриране', N'Register', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (4, N'Одобряване, съгласуване, подписване', N'Management', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (5, N'Ел.подписване', N'ESign', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (6, N'Приключване', N'Finish', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (7, N'Сторниране', N'Reverse', 1)

INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (8, N'Одобряване, съгласуване, подписване от др. лице', N'SubstituteManagement', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (9, N'Изтриване на одобряване, съгласуване, подписване', N'DeleteManagement', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (10, N'Техн. редакция', N'EditTech', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (11, N'Техн. редакция на етап', N'EditTechElectronicServiceStage', 1)
INSERT INTO ClassificationPermissions(ClassificationPermissionId, Name, Alias, IsActive) VALUES (12, N'Управление раздел на преписка', N'DocCasePartManagement', 1)

SET IDENTITY_INSERT ClassificationPermissions OFF
GO 




