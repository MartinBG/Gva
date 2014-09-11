print 'Excel Insert RegisterIndexes'
GO

SET IDENTITY_INSERT [RegisterIndexes] ON
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(1,N'9999',N'9999',1,N'9999-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс невалидни услуги');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(2,N'9000',N'Others',1,N'9000-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс други');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(3,N'0000',N'External',1,N'{3}-{2:dd.MM.yyyy}',N'Външен номер');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(4,N'000099',N'000099',1,N'000099-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(5,N'0001',N'DocInventory',1,N'{0}/{1:dd.MM.yyyy}',N'Регистър Описи');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(6,N'0002',N'Manual',1,N'{0}',N'Потребителски регистър');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(7,N'0003',N'Incoming',1,N'{0}',N'Регистър на външно входирани документи');
SET IDENTITY_INSERT [RegisterIndexes] OFF
GO

