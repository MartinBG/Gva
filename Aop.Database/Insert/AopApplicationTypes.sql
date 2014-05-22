SET IDENTITY_INSERT AopApplicationTypes ON

INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (1, N'Открита', N'Open', 1)
INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (2, N'Ограничена', N'Limited', 1)
INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (3, N'Ускорена ограничена', N'AcceleratedLimited', 1)
INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (4, N'Договаряне с обявление', N'Dealed', 1)
INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (5, N'Ускорена на договаряне с обявление', N'AcceleratedDealed', 1)
INSERT INTO AopApplicationTypes(AopApplicationTypeId, Name, Alias, IsActive) VALUES (6, N'Състезателен диалог', N'CompetitiveDialog', 1)

SET IDENTITY_INSERT AopApplicationTypes OFF
GO 
