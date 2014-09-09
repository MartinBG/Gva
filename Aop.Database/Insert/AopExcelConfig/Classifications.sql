print 'Excel Insert Classifications'
GO

SET IDENTITY_INSERT [Classifications] ON
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(1,N'Всички документи',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(2,N'Заявления',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(3,N'Регистър заявления',N'AopApplication',1);
SET IDENTITY_INSERT [Classifications] OFF
GO

