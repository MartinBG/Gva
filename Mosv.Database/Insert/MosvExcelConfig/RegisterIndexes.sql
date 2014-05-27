print 'Excel Insert RegisterIndexes'
GO

SET IDENTITY_INSERT [RegisterIndexes] ON
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(1,N'9999',N'9999',1,N'9999-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс невалидни услуги');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(2,N'9000',N'Others',1,N'9000-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс други');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(3,N'0000',N'External',1,N'{3}-{2:dd.MM.yyyy}',N'Външен номер');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(4,N'000099',N'000099',1,N'000099-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(5,N'0001',N'DocInventory',1,N'{0}/{1:dd.MM.yyyy}',N'Регистър Описи');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(6,N'0002',N'Manual',1,N'{0}',N'Потребителски регистър');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(7,N'000022',N'000022',1,N'000022-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър София');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(8,N'000019',N'000019',1,N'000019-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър Пловдив');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(9,N'000012',N'000012',1,N'000012-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър Варна');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(10,N'000011',N'000011',1,N'000011-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър Бургас');
SET IDENTITY_INSERT [RegisterIndexes] OFF
GO

