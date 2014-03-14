SET IDENTITY_INSERT DocUnitRoles ON

--адресация
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (1, N'От', N'From', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (2, N'До', N'To', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (3, N'Въвел', N'ImportedBy', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (4, N'Изготвил', N'MadeBy', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (5, N'Копие до', N'CCopy', 1)
--възлагане
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (6, N'Отговорник', N'InCharge', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (7, N'Контролиращ', N'Controlling', 1)
--доп. права
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (8, N'Читатели', N'Readers', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (9, N'Редактори', N'Editors', 1)
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (10, N'Регистратори', N'Registrators', 1)

--отговорник
INSERT INTO DocUnitRoles(DocUnitRoleId, Name, Alias, IsActive) VALUES (11, N'Отговорник за изпълнение на услуга', N'PersonInCharge', 1)

SET IDENTITY_INSERT DocUnitRoles OFF
GO 
