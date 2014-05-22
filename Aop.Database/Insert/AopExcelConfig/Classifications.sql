print 'Excel Insert Classifications'
GO

SET IDENTITY_INSERT [Classifications] ON
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(1,N'Всички документи',N'',1);
SET IDENTITY_INSERT [Classifications] OFF
GO

