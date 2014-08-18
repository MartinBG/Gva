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
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(8,N'000001',N'000001',1,N'000001-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър МОСВ');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(9,N'000002',N'000002',1,N'000002-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър ИАОС');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(10,N'000003',N'000003',1,N'000003-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър БД Благоевград');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(11,N'000004',N'000004',1,N'000004-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър БД Варна');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(12,N'000005',N'000005',1,N'000005-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър БД Пловдив');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(13,N'000006',N'000006',1,N'000006-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър БД Плевен');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(14,N'000007',N'000007',1,N'000007-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър ДНП Пирин');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(15,N'000008',N'000008',1,N'000008-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър ДНП Рила');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(16,N'000009',N'000009',1,N'000009-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър ДНП Централен Балкан');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(17,N'000010',N'000010',1,N'000010-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Благоевград');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(18,N'000011',N'000011',1,N'000011-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Бургас');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(19,N'000012',N'000012',1,N'000012-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Варна');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(20,N'000013',N'000013',1,N'000013-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ ВеликоТърново');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(21,N'000014',N'000014',1,N'000014-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Враца');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(22,N'000015',N'000015',1,N'000015-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Монтана');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(23,N'000016',N'000016',1,N'000016-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Пазарджик');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(24,N'000017',N'000017',1,N'000017-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Перник');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(25,N'000018',N'000018',1,N'000018-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Плевен');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(26,N'000019',N'000019',1,N'000019-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Пловдив');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(27,N'000020',N'000020',1,N'000020-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Русе');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(28,N'000021',N'000021',1,N'000021-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Смолян');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(29,N'000022',N'000022',1,N'000022-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ София');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(30,N'000023',N'000023',1,N'000023-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Стара Загора');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(31,N'000024',N'000024',1,N'000024-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Хасково');
INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES(32,N'000025',N'000025',1,N'000025-{0}-{1:dd.MM.yyyy}',N'Официален документен регистър РИОСВ Шумен');
SET IDENTITY_INSERT [RegisterIndexes] OFF
GO

