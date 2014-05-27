print 'Excel Insert DocTypeGroups'
GO

SET IDENTITY_INSERT [DocTypeGroups] ON
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(1,N'Системни',0,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(100,N'Общи',0,1);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(201,N'Заявление',0,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(202,N'Електронни услуги',1,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(300,N'Отговори на услуги',1,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(400,N'Невалидни услуги',1,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(900,N'Други',0,0);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(1001,N'Електронни услуги София',1,1);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(1002,N'Електронни услуги Пловдив',1,1);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(1003,N'Електронни услуги Варна',1,1);
INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES(1004,N'Електронни услуги Бургас',1,1);
SET IDENTITY_INSERT [DocTypeGroups] OFF
GO

