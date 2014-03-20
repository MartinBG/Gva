SET IDENTITY_INSERT [RegisterIndexes] ON

-- 0 - номер на док
-- 1 - дата на док
-- 2 - мастър номер на преписка
-- 3 - външен номер
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(1,N'9999',N'9999',1,N'9999-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс невалидни услуги');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(2,N'9000',N'Others',1,N'9000-{0:000000}-{1:dd.MM.yyyy}',N'Регистър индекс други');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(3,N'0000',N'External',1,N'{3}-{2:dd.MM.yyyy}',N'Външен номер');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(4,N'000099',N'000099',1,N'000099-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(5,N'0001',N'DocInventory',1,N'{0}/{1:dd.MM.yyyy}',N'Регистър Описи');

SET IDENTITY_INSERT [RegisterIndexes] OFF
GO
