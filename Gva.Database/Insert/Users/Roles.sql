SET IDENTITY_INSERT [Roles] ON

INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (1, N'Администратор', N'sys#admin', 1)
INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (2, N'Регистратор', N'', 1)
INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (3, N'Експерт', N'', 1)
INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (4, N'Изготвяне на справки', N'', 1)
INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (5, N'Съгласуващ', N'', 1)
INSERT INTO [Roles](RoleId, Name, [Permissions], IsActive) VALUES (6, N'Мениджър', N'', 1)

SET IDENTITY_INSERT [Roles] OFF
GO 
